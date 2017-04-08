using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour {
    protected bool      _enableUp;
    protected bool      _enableDown;
    protected bool      _enableLeft;
    protected bool      _enableRight;
    
    public virtual bool enableUp {
        get {
            return _enableUp;
        }
        set {
            _enableUp = value;
        }
    }
    public virtual bool enableDown {
        get {
            return _enableDown;
        }
        set {
            _enableDown = value;
        }
    }
    public virtual bool enableLeft {
        get {
            return _enableLeft;
        }
        set {
            _enableLeft = value;
        }
    }
    public virtual bool enableRight {
        get {
            return _enableRight;
        }
        set {
            _enableRight = value;
        }
    }

    protected Device    upNeighbor;
    protected Device    downNeighbor;
    protected Device    leftNeighbor;
    protected Device    rightNeighbor;

    public RibbonNode   upNode;
    public RibbonNode   downNode;
    public RibbonNode   leftNode;
    public RibbonNode   rightNode;

    protected Device this[int i] {
        get {
            switch (i) {
                case (0): return this.upNeighbor;
                case (1): return this.downNeighbor;
                case (2): return this.leftNeighbor;
                case (3): return this.rightNeighbor;
                //case (4): return this.upNode;
                //case (5): return this.downNode;
                //case (6): return this.leftNode;
                //case (7): return this.rightNode;
            }
            return null;
        }
    }

    /// <summary>
    /// Sets the devices neighbor variables and updates neighbors' neighbor variables.
    /// </summary>
    /// <param name="up">Check and set up neighbor.</param>
    /// <param name="down">Check and set down neighbor.</param>
    /// <param name="left">Check and set left neighbor.</param>
    /// <param name="right">Check and set right neighbor.</param>
    public void helloNeighbor(bool up = true, bool down = true, bool left = true, bool right = true, bool isResponse = false) {
        RaycastHit currentHit;
        Vector3 origin          = transform.position;
        Vector3 upOrigin        = origin + new Vector3(0,1,-2);
        Vector3 downOrigin      = origin + new Vector3(0,-1,-2);
        Vector3 leftOrigin      = origin + new Vector3(-1,0,-2);
        Vector3 rightOrigin     = origin + new Vector3(1,0,-2);

        if (up) {
            if (Physics.Raycast(upOrigin, Vector3.forward, out currentHit)) {
                //currentHit.transform.gameObject.layer; // Might need this for only targeting devices i.e. when the player is in front of a device
                this.upNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (!isResponse) this.upNeighbor.helloNeighbor(false, true, false, false, true);
            }
        }
        if (down) {
            if (Physics.Raycast(downOrigin, Vector3.forward, out currentHit)) {
                this.downNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (!isResponse) this.downNeighbor.helloNeighbor(true, false, false, false, true);
            }
        }
        if (left) {
            if (Physics.Raycast(leftOrigin, Vector3.forward, out currentHit)) {
                this.leftNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (!isResponse) this.leftNeighbor.helloNeighbor(false, false, false, true, true);
            }
        }
        if (right) {
            if (Physics.Raycast(rightOrigin, Vector3.forward, out currentHit)) {
                this.rightNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (!isResponse) this.rightNeighbor.helloNeighbor(false, false, true, false, true);
            }
        }
    }

    // Attempts to place all item's in this device's inventories as well as the device into a target inventory.
    public void pickUpDevice(Inventory targetInventory) {

    }

    public string DEBUGReportNeighbors() {
        string upName                           = "null";
        string downName                         = "null";
        string leftName                         = "null";
        string rightName                        = "null";
        if (upNeighbor != null) upName          = upNeighbor.name;
        if (downNeighbor != null) downName      = downNeighbor.name;
        if (leftNeighbor != null) leftName      = leftNeighbor.name;
        if (rightNeighbor != null) rightName    = rightNeighbor.name;

        return ("UP: " + upName + ", DOWN: " + downName + ", LEFT: " + leftName + ", RIGHT: " + rightName);
    }
}
