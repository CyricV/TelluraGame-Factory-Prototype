using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;

public class InputController : MonoBehaviour {
    bool BuildMode;

    private void Start() {
        BuildMode = true;
    }

    void Update () {
        if (BuildMode) BuildModeMoveCamera();
        else MoveCamera();
	}

    private void BuildModeMoveCamera() {
        float xpos          = Input.mousePosition.x;
        float ypos          = Input.mousePosition.y;
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
        if(destination.z < -GameValues.MaxCameraHeight) {
            destination.z = -GameValues.MaxCameraHeight;
        } else if(destination.z > -GameValues.MinCameraHeight) {
            destination.z = -GameValues.MinCameraHeight;
        }

        //if a change in position is detected perform the necessary update
        if(destination != origin) {
            Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * GameValues.MaxScrollSpeed);
        }
    }

    private void MoveCamera() {
    }
}
