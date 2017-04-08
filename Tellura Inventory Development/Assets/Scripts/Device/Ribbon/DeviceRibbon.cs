using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceRibbon : Device {
    GameObject      partUp;
    GameObject      partDown;
    GameObject      partLeft;
    GameObject      partRight;

    public override bool enableUp {
        get {
            return _enableUp;
        }
        set {
            if (value && !_enableUp && upNeighbor is Device) {
                makePart("up");
            } else if (!value) {
                removePart("up");
            }
            _enableUp = value;
        }
    }
    public override bool enableDown {
        get {
            return _enableDown;
        }
        set {
            if (value && !_enableDown && downNeighbor is Device) {
                makePart("down");
            } else if (!value) {
                print("butts");
                removePart("down");
            }
            _enableDown = value;
        }
    }
    public override bool enableLeft {
        get {
            return _enableLeft;
        }
        set {
            if (value && !_enableLeft && leftNeighbor is Device) {
                makePart("left");
            } else if (!value) {
                removePart("left");
            }
            _enableLeft = value;
        }
    }
    public override bool enableRight {
        get {
            return _enableRight;
        }
        set {
            if (value && !_enableRight && rightNeighbor is Device) {
                makePart("right");
            } else if (!value) {
                removePart("right");
            }
            _enableRight = value;
        }
    }

    RibbonNode node;
    
	void Start () {
        gameObject.name = "Ribbon " + gameObject.GetInstanceID();
        enableUp        = true;
        enableDown      = true;
        enableLeft      = true;
        enableRight     = true;

        helloNeighbor();

        if(upNeighbor != null && enableUp) {
            partUp = makePart("up");
        }
        if(downNeighbor != null && enableDown) {
            partDown = makePart("down");
        }
        if(leftNeighbor != null && enableLeft) {
            partLeft = makePart("left");
        }
        if(rightNeighbor != null && enableRight) {
            partRight = makePart("right");
        }
	}

    public GameObject makePart(string rotation, bool respond = true) {
        int z;
        string name;
        string loadName;

        if (rotation == "up" && upNeighbor.enableDown) {
            z                   = 0;
            name                = "ribbon_up " + gameObject.GetInstanceID();
            loadName            = "Prefabs/RibbonPartConnector";
            if (upNeighbor is DeviceRibbon) {
                loadName        = "Prefabs/RibbonPart";
                if (respond) (upNeighbor as DeviceRibbon).makePart("down", false);
            }
        } else if (rotation == "down" && downNeighbor.enableUp) {
            z                   = 180;
            name                = "ribbon_down " + gameObject.GetInstanceID();
            loadName            = "Prefabs/RibbonPartConnector";
            if (downNeighbor is DeviceRibbon) {
                loadName        = "Prefabs/RibbonPart";
                if (respond) (downNeighbor as DeviceRibbon).makePart("up", false);
            }
        } else if (rotation == "left" && leftNeighbor.enableRight) {
            z                   = 90;
            name                = "ribbon_left " + gameObject.GetInstanceID();
            loadName            = "Prefabs/RibbonPartConnector";
            if (leftNeighbor is DeviceRibbon){
                loadName        = "Prefabs/RibbonPart";
                if (respond) (leftNeighbor as DeviceRibbon).makePart("right", false);
            }
        } else if (rotation == "right" && rightNeighbor.enableLeft) {
            z                   = 270;
            name                = "ribbon_right " + gameObject.GetInstanceID();
            loadName            = "Prefabs/RibbonPartConnector";
            if (rightNeighbor is DeviceRibbon){
                loadName        = "Prefabs/RibbonPart";
                if (respond) (rightNeighbor as DeviceRibbon).makePart("left", false);
            }
        } else return null;

        GameObject part = Instantiate(
                        Resources.Load(loadName) as GameObject,
                        gameObject.transform.position,
                        Quaternion.Euler(0, 0, z));
        part.name       = name;
        return part;
    }

    private void removePart(string target, bool respond = true) {
        if (target == "up") {
            Destroy(partUp);
            if (respond && upNeighbor is DeviceRibbon) (upNeighbor as DeviceRibbon).removePart("down", false);
        } else if (target == "down") {
            print("destroy "+partDown.name);
            partDown.name = "DELETE ME";
            Destroy(partDown);
            if (respond && downNeighbor is DeviceRibbon) (downNeighbor as DeviceRibbon).removePart("up", false);
        } else if (target == "left") {
            Destroy(partLeft);
            if (respond && leftNeighbor is DeviceRibbon) (leftNeighbor as DeviceRibbon).removePart("right", false);
        } else if (target == "right") {
            Destroy(partRight);
            if (respond && rightNeighbor is DeviceRibbon) (rightNeighbor as DeviceRibbon).removePart("left", false);
        }
    }
}
