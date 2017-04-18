using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;

public class Device : MonoBehaviour {
    protected bool      _enableUp;
    protected bool      _enableDn;
    protected bool      _enableLt;
    protected bool      _enableRt;
    public virtual bool enableUp { get { return _enableUp; } set { _enableUp = value; } }
    public virtual bool enableDn { get { return _enableDn; } set { _enableDn = value; } }
    public virtual bool enableLt { get { return _enableLt; } set { _enableLt = value; } }
    public virtual bool enableRt { get { return _enableRt; } set { _enableRt = value; } }

    protected Device    neighborUp;
    protected Device    neighborDn;
    protected Device    neighborLt;
    protected Device    neighborRt;
    protected Device    this[int i] { get {
        switch (i) {
            case (0): return neighborUp;
            case (1): return neighborDn;
            case (2): return neighborLt;
            case (3): return neighborRt;
        }
        return null;
    } }

    public RibbonNode   nodeUp;
    public RibbonNode   nodeDn;
    public RibbonNode   nodeLt;
    public RibbonNode   nodeRt;

    private Device SuperDevice;

    protected virtual void HelloNeighbor(bool up = true, bool dn = true, bool lt = true, bool rt = true, bool respond = true) {
        RaycastHit currentHit;
        Vector3 origin      = transform.position;
        Vector3 originUp    = origin + new Vector3(0,1,-2);
        Vector3 originDn    = origin + new Vector3(0,-1,-2);
        Vector3 originLt    = origin + new Vector3(-1,0,-2);
        Vector3 originRt    = origin + new Vector3(1,0,-2);

        if (up) {
            if (Physics.Raycast(originUp, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                neighborUp = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) neighborUp.Notify(false, true, false, false);
            }
        }
        if (dn) {
            if (Physics.Raycast(originDn, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                neighborDn = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) neighborDn.Notify(true, false, false, false);
            }
        }
        if (lt) {
            if (Physics.Raycast(originLt, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                this.neighborLt = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) neighborLt.Notify(false, false, false, true);
            }
        }
        if (rt) {
            if (Physics.Raycast(originRt, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                neighborRt = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) neighborRt.Notify(false, false, true, false);
            }
        }
    }

    /// <summary>
    /// Tells this Device to re-evaluate its neighbor in the specified directions.
    /// </summary>
    /// <param name="up">Look at upwards neighbor.</param>
    /// <param name="dn">Look at downwards neighbor.</param>
    /// <param name="lt">Look at leftwards neighbor.</param>
    /// <param name="rt">Look at righwards neighbor.</param>
    public virtual void Notify(bool up = false, bool dn = false, bool lt = false, bool rt = false) {
        HelloNeighbor(up, dn, lt, rt, false);
    }

    public bool ToggleUp() {
        enableUp = enableUp ? false : true;
        if (neighborUp != null) neighborUp.Notify(false, true, false, false);
        return enableUp;
    }

    public bool ToggleDown() {
        enableDn = enableDn ? false : true;
        if (neighborDn != null) neighborDn.Notify(true, false, false, false);
        return enableDn;
    }

    public bool ToggleLeft() {
        enableLt = enableLt ? false : true;
        if (neighborLt != null) neighborLt.Notify(false, false, false, true);
        return enableLt;
    }

    public bool ToggleRight() {
        enableRt = enableRt ? false : true;
        if (neighborRt != null) neighborRt.Notify(false, false, true, false);
        return enableRt;
    }

    /// <summary>
    /// Request an item from this device.
    /// </summary>
    /// <param name="item">The item you want, used as a template.</param>
    /// <param name="amount">How many of the item you want. Zero or less will use item's stackCurrent.</param>
    /// <returns>An item with as much of the requested amount as possible. Or null if none are available.</returns>
    public virtual InventoryItem RequestItem(InventoryItem item, int amount = 0) {
        return null;
    }

    /// <summary>
    /// Give an item to this device.
    /// </summary>
    /// <param name="item">The item being given.</param>
    /// <returns></returns>
    public virtual InventoryItem GiveItem(InventoryItem item) {
        return null;
    }

    public string DEBUGReportNeighbors() {
        string nameUp                   = "null";
        string nameDn                   = "null";
        string nameLt                   = "null";
        string nameRt                   = "null";
        if (neighborUp != null) nameUp  = neighborUp.name;
        if (neighborDn != null) nameDn  = neighborDn.name;
        if (neighborLt != null) nameLt  = neighborLt.name;
        if (neighborRt != null) nameRt  = neighborRt.name;
        if (!_enableUp) nameUp          = "(X)"+nameUp;
        if (!_enableDn) nameDn          = "(X)"+nameDn;
        if (!_enableLt) nameLt          = "(X)"+nameLt;
        if (!_enableRt) nameRt          = "(X)"+nameRt;

        return (
            gameObject.name + " NEIGHBORS\n" +
                "\tUP: \t" + nameUp +
                "\n\tDOWN: \t" + nameDn +
                "\n\tLEFT: \t" + nameLt + 
                "\n\tRIGHT: \t" + nameRt
        );
    }

    public virtual void DestroySelf() {
        enableUp = false;
        enableDn = false;
        enableLt = false;
        enableRt = false;
        if (neighborUp != null) neighborUp.Notify(false, true, false, false);
        if (neighborDn != null) neighborDn.Notify(true, false, false, false);
        if (neighborLt != null) neighborLt.Notify(false, false, false, true);
        if (neighborRt != null) neighborRt.Notify(false, false, true, false);
        Destroy(gameObject);
    }
}
