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
            if (value && partUp == null && upNeighbor is Device) {
                _enableUp = value;
                partUp = makePart("up");
            } else if (!value) {
                _enableUp = value;
                removePart("up");
            } else _enableUp = value;
        }
    }
    public override bool enableDown {
        get {
            return _enableDown;
        }
        set {
            if (value && partDown == null && downNeighbor is Device) {
                _enableDown = value;
                partDown = makePart("down");
            } else if (!value) {
                _enableDown = value;
                removePart("down");
            } else _enableDown = value;
        }
    }
    public override bool enableLeft {
        get {
            return _enableLeft;
        }
        set {
            if (value && partLeft == null && leftNeighbor is Device) {
                _enableLeft = value;
                partLeft = makePart("left");
            } else if (!value) {
                _enableLeft = value;
                removePart("left");
            } else _enableLeft = value;
        }
    }
    public override bool enableRight {
        get {
            return _enableRight;
        }
        set {
            if (value && partRight == null && rightNeighbor is Device) {
                _enableRight = value;
                partRight = makePart("right");
            } else if (!value) {
                _enableRight = value;
                removePart("right");
            } else _enableRight = value;
        }
    }

    RibbonNode node;

    private void Awake() {
        gameObject.name = "Ribbon " + gameObject.GetInstanceID();
        helloNeighbor();
        enableUp        = true;
        enableDown      = true;
        enableLeft      = true;
        enableRight     = true;
    }
    
	void Start () {
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

        if (rotation == "up") partUp            = part;
        else if (rotation == "down") partDown   = part;
        else if (rotation == "left") partLeft   = part;
        else if (rotation == "right") partRight = part;
        return part;
    }

    private void removePart(string target, bool respond = true) {
        if (target == "up") {
            Destroy(partUp);
            partUp = null;
            if (respond && upNeighbor is DeviceRibbon) (upNeighbor as DeviceRibbon).removePart("down", false);
        } else if (target == "down") {
            Destroy(partDown);
            partDown = null;
            if (respond && downNeighbor is DeviceRibbon) (downNeighbor as DeviceRibbon).removePart("up", false);
        } else if (target == "left") {
            Destroy(partLeft);
            partLeft = null;
            if (respond && leftNeighbor is DeviceRibbon) (leftNeighbor as DeviceRibbon).removePart("right", false);
        } else if (target == "right") {
            Destroy(partRight);
            partRight= null;
            if (respond && rightNeighbor is DeviceRibbon) (rightNeighbor as DeviceRibbon).removePart("left", false);
        }
    }
}
