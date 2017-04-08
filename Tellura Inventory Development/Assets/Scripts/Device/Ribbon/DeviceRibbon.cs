using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceRibbon : Device {
    private bool enableUp;
    private bool enableDown;
    private bool enableLeft;
    private bool enableRight;
    GameObject partUp;
    GameObject partDown;
    GameObject partLeft;
    GameObject partRight;

    private void Awake() {
        gameObject.name = "Ribbon " + gameObject.GetInstanceID();
        enableUp        = true;
        enableDown      = true;
        enableLeft      = true;
        enableRight     = true;
    }
    
	void Start () {
        helloNeighbor();

        string loadName;
        if(upNeighbor != null && enableUp) {
            loadName = "Prefabs/RibbonPartConnector";
            if (upNeighbor is DeviceRibbon) loadName = "Prefabs/RibbonPart";
                partUp = makePart("ribbon_up " + gameObject.GetInstanceID(), loadName, Quaternion.identity);
        }
        if(downNeighbor != null && enableDown) {
            loadName = "Prefabs/RibbonPartConnector";
            print(downNeighbor.GetType() + downNeighbor.gameObject.name);
            if (downNeighbor is DeviceRibbon) loadName = "Prefabs/RibbonPart";
            partDown = makePart("ribbon_down " + gameObject.GetInstanceID(), loadName, Quaternion.Euler(0, 0, 180));
        }
        if(leftNeighbor != null && enableLeft) {
            loadName = "Prefabs/RibbonPartConnector";
            if (leftNeighbor is DeviceRibbon) loadName = "Prefabs/RibbonPart";
            partLeft = makePart("ribbon_left " + gameObject.GetInstanceID(), loadName, Quaternion.Euler(0, 0, 90));
        }
        if(rightNeighbor != null && enableRight) {
            loadName = "Prefabs/RibbonPartConnector";
            if (rightNeighbor is DeviceRibbon) loadName = "Prefabs/RibbonPart";
            partRight = makePart("ribbon_right " + gameObject.GetInstanceID(), loadName, Quaternion.Euler(0, 0, 270));
        }
	}

    protected GameObject makePart(string name, string loadName, Quaternion rotation) {
        GameObject part = Instantiate(
                            Resources.Load(loadName) as GameObject,
                            gameObject.transform.position,
                            rotation);
        part.name       = name;
        return part;
    }
}
