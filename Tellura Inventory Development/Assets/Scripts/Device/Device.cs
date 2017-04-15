using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;

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
                case (0): return upNeighbor;
                case (1): return downNeighbor;
                case (2): return leftNeighbor;
                case (3): return rightNeighbor;
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
    /// <param name="respond">Whether or not to tell neighbors to update themselves.</param>
    public virtual void HelloNeighbor(bool up = true, bool down = true, bool left = true, bool right = true, bool respond = true) {
        RaycastHit currentHit;
        Vector3 origin          = transform.position;
        Vector3 upOrigin        = origin + new Vector3(0,1,-2);
        Vector3 downOrigin      = origin + new Vector3(0,-1,-2);
        Vector3 leftOrigin      = origin + new Vector3(-1,0,-2);
        Vector3 rightOrigin     = origin + new Vector3(1,0,-2);

        if (up) {
            if (Physics.Raycast(upOrigin, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                this.upNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) this.upNeighbor.HelloNeighbor(false, true, false, false, false);
            }
        }
        if (down) {
            if (Physics.Raycast(downOrigin, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                this.downNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) this.downNeighbor.HelloNeighbor(true, false, false, false, false);
            }
        }
        if (left) {
            if (Physics.Raycast(leftOrigin, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                this.leftNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) this.leftNeighbor.HelloNeighbor(false, false, false, true, false);
            }
        }
        if (right) {
            if (Physics.Raycast(rightOrigin, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                this.rightNeighbor = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) this.rightNeighbor.HelloNeighbor(false, false, true, false, false);
            }
        }
    }

    public bool ToggleUp() {
        enableUp = enableUp ? false : true;
        upNeighbor.HelloNeighbor(false, true, false, false, false);
        return enableUp;
    }

    public bool ToggleDown() {
        enableDown = enableDown ? false : true;
        downNeighbor.HelloNeighbor(true, false, false, false, false);
        return enableDown;
    }

    public bool ToggleLeft() {
        enableLeft = enableLeft ? false : true;
        leftNeighbor.HelloNeighbor(false, false, false, true, false);
        return enableLeft;
    }

    public bool ToggleRight() {
        enableRight = enableRight ? false : true;
        rightNeighbor.HelloNeighbor(false, false, true, false, false);
        return enableRight;
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
        if (!_enableUp) upName                  = "(X)"+upName;
        if (!_enableDown) downName              = "(X)"+downName;
        if (!_enableLeft) leftName              = "(X)"+leftName;
        if (!_enableRight) rightName            = "(X)"+rightName;

        return (
            gameObject.name + " NEIGHBORS\n" +
                "\tUP: \t" + upName +
                "\n\tDOWN: \t" + downName +
                "\n\tLEFT: \t" + leftName + 
                "\n\tRIGHT: \t" + rightName);
    }

    public void destroySelf() {
        if (upNeighbor != null) upNeighbor.downNeighbor = null;
        if (downNeighbor != null) downNeighbor.upNeighbor = null;
        if (leftNeighbor != null) leftNeighbor.rightNeighbor = null;
        if (rightNeighbor != null) rightNeighbor.leftNeighbor = null;
        Destroy(gameObject);
    }
}
