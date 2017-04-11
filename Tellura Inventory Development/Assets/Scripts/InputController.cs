using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;

public class InputController : MonoBehaviour {
    GameObject  selectionIndicatorLOAD;
    GameObject  cursorIndicatorLOAD;
    GameObject  ribbonLOAD;
    Sprite      ribbonSpriteLOAD;
    GameObject  chestLOAD;
    Sprite      chestSpriteLOAD;
    GameObject  kilnLOAD;
    Sprite      kilnSpriteLOAD;

    bool        BuildMode;

    GameObject  deviceToPlace;
    GameObject  selectedDevice;
    GameObject  selectionIndicator;
    GameObject  cursorIndicator;

    private void Start() {
        selectionIndicatorLOAD  = Resources.Load("Prefabs/SelectionIndicator") as GameObject;
        cursorIndicatorLOAD     = Resources.Load("Prefabs/CursorIndicator") as GameObject;
        ribbonLOAD              = Resources.Load("Prefabs/Ribbon") as GameObject;
        ribbonSpriteLOAD        = Resources.Load("Sprites/Devices/Ribbon") as Sprite;
        chestLOAD               = Resources.Load("Prefabs/Chest") as GameObject;
        chestSpriteLOAD         = Resources.Load("Sprites/Devices/Chest") as Sprite;
        kilnLOAD                = Resources.Load("Prefabs/Kiln") as GameObject;
        kilnSpriteLOAD          = Resources.Load("Sprites/Devices/Kiln") as Sprite;
        BuildMode               = true;
        deviceToPlace           = null;
    }

    void Update () {
        if (BuildMode) {
            Vector3 mousePos = Input.mousePosition;
            BuildModeMoveCamera(mousePos);
            ClickCheck(mousePos);
            KeyCheck();
            MoveCursorIndicator(mousePos);
        } else MoveCamera();

	}

    private void BuildModeMoveCamera(Vector3 mousePos) {
        float xpos          = mousePos.x;
        float ypos          = mousePos.y;
        Vector3 movement    = new Vector3(0,0,0);
        Vector3 origin      = Camera.main.transform.position;
        int scrollSpeed     = Mathf.RoundToInt(Mathf.Lerp(GameValues.MinScrollSpeed, GameValues.MaxScrollSpeed, origin.z/GameValues.MaxCameraHeight));

        //horizontal camera movement
        if(xpos < GameValues.ScrollWidth) {
            movement.x -= scrollSpeed;
        } else if(xpos > Screen.width - GameValues.ScrollWidth) {
            movement.x += scrollSpeed;
        }
 
        //vertical camera movement
        if(ypos < GameValues.ScrollWidth) {
            movement.y -= scrollSpeed;
        } else if(ypos > Screen.height - GameValues.ScrollWidth) {
            movement.y += scrollSpeed;
        }

        //away from ground movement
        movement.z = GameValues.ZoomScrollSpeed * Input.GetAxis("Mouse ScrollWheel");

        //calculate desired camera position based on received input
        Vector3 destination = origin;
        destination.x += movement.x;
        destination.y += movement.y;
        destination.z += movement.z;

        //limit away from ground movement to be between a minimum and maximum distance
        if (destination.z < -GameValues.MaxCameraHeight) {
            destination.z = -GameValues.MaxCameraHeight;
        } else if(destination.z > -GameValues.MinCameraHeight) {
            destination.z = -GameValues.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if (destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * GameValues.MaxScrollSpeed);
        }
    }

    private void MoveCamera() {
    }

    private void ClickCheck(Vector3 mousePos) {
        if (Input.GetMouseButtonDown(0) && deviceToPlace == null) {
            RaycastHit currentHit;
            //Debug.DrawRay(Camera.main.ScreenPointToRay(mousePos).origin, Camera.main.ScreenPointToRay(mousePos).direction * 20, Color.blue, 99);
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out currentHit, Mathf.Infinity, GameValues.LayerMaskDevice)) {
                selectedDevice = currentHit.transform.gameObject;
                print(selectedDevice.name);
                Destroy(selectionIndicator);
                selectionIndicator = Instantiate(
                        selectionIndicatorLOAD,
                        selectedDevice.transform.position,
                        Quaternion.identity);
            } else {
                Destroy(selectionIndicator);
                selectedDevice = null;
            }
        } else if (Input.GetMouseButton(0) && deviceToPlace != null) {
            RaycastHit currentHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), Mathf.Infinity, GameValues.LayerMaskDevice)) return;
            Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out currentHit, Mathf.Infinity, GameValues.LayerMaskBuildPlane);
            print("Creating at "+Mathf.RoundToInt(currentHit.point.x)+", "+Mathf.RoundToInt(currentHit.point.y));
            Instantiate(deviceToPlace, new Vector3(Mathf.RoundToInt(currentHit.point.x),Mathf.RoundToInt(currentHit.point.y),0), Quaternion.identity);
        }
    }

    private void MoveCursorIndicator(Vector3 mousePos) {
        if(cursorIndicator == null && 0 < mousePos.x && mousePos.x < Screen.width && 0 < mousePos.y && mousePos.y < Screen.height) {
            cursorIndicator = Instantiate(
                cursorIndicatorLOAD,
                new Vector3(0,0,0),
                Quaternion.identity);
        }
        if (cursorIndicator != null) {
            int curX = Mathf.RoundToInt(cursorIndicator.transform.position.x);
            int curY = Mathf.RoundToInt(cursorIndicator.transform.position.y);
            RaycastHit currentHit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out currentHit, Mathf.Infinity, GameValues.LayerMaskBuildPlane)) {
                cursorIndicator.transform.position = new Vector3(Mathf.RoundToInt(currentHit.point.x), Mathf.RoundToInt(currentHit.point.y), 0);
            }
        }
    }

    private void KeyCheck() {
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            deviceToPlace = null;
            setBuildGhostSprite(null);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            deviceToPlace = ribbonLOAD;
            setBuildGhostSprite(deviceToPlace.GetComponent<SpriteRenderer>().sprite);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            deviceToPlace = chestLOAD;
            setBuildGhostSprite(deviceToPlace.GetComponent<SpriteRenderer>().sprite);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            deviceToPlace = kilnLOAD;
            setBuildGhostSprite(deviceToPlace.GetComponent<SpriteRenderer>().sprite);
        }
    }

    private void setBuildGhostSprite(Sprite sprite) {
        SpriteRenderer[] renderers = cursorIndicator.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in renderers) {
            if(renderer.gameObject.name == "BuildGhost") {
                renderer.sprite = sprite;
            }
        }
    }
}
