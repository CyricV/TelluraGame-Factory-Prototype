using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceRibbon : Device {
    GameObject partUp;
    GameObject partDown;
    GameObject partLeft;
    GameObject partRight;

    public override bool enableUp {
        get {
            return _enableUp;
        }
        set {
            if (value && partUp == null && upNeighbor is Device) {
                _enableUp = value;
                MakePart(true, false, false, false);
            } else if (!value) {
                _enableUp = value;
                RemovePart(true, false, false, false);
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
                MakePart(false, true, false, false);
            } else if (!value) {
                _enableDown = value;
                RemovePart(false, true, false, false);
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
                MakePart(false, false, true, false);
            } else if (!value) {
                _enableLeft = value;
                RemovePart(false, false, true, false);
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
                MakePart(false, false, false, true);
            } else if (!value) {
                _enableRight = value;
                RemovePart(false, false, false, true);
            } else _enableRight = value;
        }
    }

    RibbonNode node;

    private void Awake() {
        gameObject.name = "Ribbon " + gameObject.GetInstanceID();
        HelloNeighbor();
        enableUp        = true;
        enableDown      = true;
        enableLeft      = true;
        enableRight     = true;
    }
    
	void Start () {
	}

    public override void HelloNeighbor(bool up = true, bool down = true, bool left = true, bool right = true, bool respond = true) {
        base.HelloNeighbor(up, down, left, right, respond);
        if (!respond) {
            if (up && upNeighbor.enableDown)        MakePart(true, false, false, false, false);
            if (down && downNeighbor.enableUp)      MakePart(false, true, false, false, false);
            if (left && leftNeighbor.enableRight)   MakePart(false, false, true, false, false);
            if (right && rightNeighbor.enableLeft)  MakePart(false, false, false, true, false);
        }
    }

    /// <summary>
    /// Creates and sets directional ribbon parts.
    /// </summary>
    /// <param name="up">Create the up part.</param>
    /// <param name="down">Create the down part.</param>
    /// <param name="left">Create the left part.</param>
    /// <param name="right">Create the right part.</param>
    /// <param name="respond">Whether or not to prompt a response from neighbors.</param>
    private void MakePart(bool up = true, bool down = true, bool left = true, bool right = true, bool respond = true) {
        if (!(upNeighbor is Device)) up         = false;
        if (!(downNeighbor is Device)) down     = false;
        if (!(leftNeighbor is Device)) left     = false;
        if (!(rightNeighbor is Device)) right   = false;

        if (up && enableUp && upNeighbor.enableDown) {
            string loadName = "Prefabs/RibbonPartConnector";
            string name = "ribbon_up " + gameObject.GetInstanceID();
            if (upNeighbor is DeviceRibbon) {
                loadName = "Prefabs/RibbonPart";
                if (respond) (upNeighbor as DeviceRibbon).MakePart(false, true, false, false, false);
            }
            partUp = InstantiatePart(0, loadName, name);
        } else if (up && enableUp) RemovePart(true, false, false, false);

        if (down && enableDown && downNeighbor.enableUp) {
            string loadName = "Prefabs/RibbonPartConnector";
            string name = "ribbon_down " + gameObject.GetInstanceID();
            if (downNeighbor is DeviceRibbon) {
                loadName = "Prefabs/RibbonPart";
                if (respond) (downNeighbor as DeviceRibbon).MakePart(true, false, false, false, false);
            }
            partDown = InstantiatePart(180, loadName, name);
        } else if (down && enableDown) RemovePart(false, true, false, false);

        if (left && enableLeft && leftNeighbor.enableRight) {
            string loadName = "Prefabs/RibbonPartConnector";
            string name = "ribbon_left " + gameObject.GetInstanceID();
            if (leftNeighbor is DeviceRibbon) {
                loadName = "Prefabs/RibbonPart";
                if (respond) (leftNeighbor as DeviceRibbon).MakePart(false, false, false, true, false);
            }
            partLeft = InstantiatePart(90, loadName, name);
        } else if (left && enableLeft) RemovePart(false, false, true, false);

        if (right && enableRight && rightNeighbor.enableLeft) {
            string loadName = "Prefabs/RibbonPartConnector";
            string name = "ribbon_right " + gameObject.GetInstanceID();
            if (rightNeighbor is DeviceRibbon) {
                loadName = "Prefabs/RibbonPart";
                if (respond) (rightNeighbor as DeviceRibbon).MakePart(false, false, true, false, false);
            }
            partRight = InstantiatePart(270, loadName, name);
        } else if (right && enableRight) RemovePart(false, false, false, true);
    }

    private GameObject InstantiatePart(int rotation, string loadName, string name) {
        GameObject part = Instantiate(
                Resources.Load(loadName) as GameObject,
                gameObject.transform.position,
                Quaternion.Euler(0, 0, rotation));
        part.name       = name;
        return part;
    }

    private void RemovePart(bool up = true, bool down = true, bool left = true, bool right = true, bool respond = true) {
        if (up) {
            Destroy(partUp);
            partUp = null;
            if (respond && upNeighbor is DeviceRibbon) (upNeighbor as DeviceRibbon).RemovePart(false, true, false, false, false);
        }
        if (down) {
            Destroy(partDown);
            partDown = null;
            if (respond && downNeighbor is DeviceRibbon) (downNeighbor as DeviceRibbon).RemovePart(true, false, false, false, false);
        }
        if (left) {
            Destroy(partLeft);
            partLeft = null;
            if (respond && leftNeighbor is DeviceRibbon) (leftNeighbor as DeviceRibbon).RemovePart(false, false, false, true, false);
        }
        if (right) {
            Destroy(partRight);
            partRight= null;
            if (respond && rightNeighbor is DeviceRibbon) (rightNeighbor as DeviceRibbon).RemovePart(false, false, true, false, false);
        }
    }
}
