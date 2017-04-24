using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;

public class Device : MonoBehaviour {
    public      DevicePort portUp { get { return _portUp; } }
    protected   DevicePort _portUp;
    public      DevicePort portDn { get { return _portDn; } }
    protected   DevicePort _portDn;
    public      DevicePort portLt { get { return _portLt; } }
    protected   DevicePort _portLt;
    public      DevicePort portRt { get { return _portRt; } }
    protected   DevicePort _portRt;

    protected bool      _enableUp;
    protected bool      _enableDn;
    protected bool      _enableLt;
    protected bool      _enableRt;
    /// <summary>
    /// If this device is allowed to connect to its upwards neighbor.
    /// </summary>
    public virtual bool enableUp { get { return _enableUp; } set { _enableUp = value; } }
    /// <summary>
    /// If this device is allowed to connect to its downwards neighbor.
    /// </summary>
    public virtual bool enableDn { get { return _enableDn; } set { _enableDn = value; } }
    /// <summary>
    /// If this device is allowed to connect to its leftwards neighbor.
    /// </summary>
    public virtual bool enableLt { get { return _enableLt; } set { _enableLt = value; } }
    /// <summary>
    /// If this device is allowed to connect to its rightwards neighbor.
    /// </summary>
    public virtual bool enableRt { get { return _enableRt; } set { _enableRt = value; } }
    /// <summary>
    /// If this device is connected to its upwards neighbor.
    /// </summary>
    public bool         connectedUp { get { return (enableUp && _neighborUp != null && _neighborUp.enableDn); } }
    /// <summary>
    /// If this device is connected to its downwards neighbor.
    /// </summary>
    public bool         connectedDn { get { return (enableDn && _neighborDn != null && _neighborDn.enableUp); } }
    /// <summary>
    /// If this device is connected to its leftwards neighbor.
    /// </summary>
    public bool         connectedLt { get { return (enableLt && _neighborLt != null && _neighborLt.enableRt); } }
    /// <summary>
    /// If this device is connected to its rightwards neighbor.
    /// </summary>
    public bool         connectedRt { get { return (enableRt && _neighborRt != null && _neighborRt.enableLt); } }

    /// <summary>
    /// Upwards neighboring device.
    /// </summary>
    public      Device  neighborUp { get { return _neighborUp; } }
    protected   Device  _neighborUp;
    /// <summary>
    /// Downwards neighboring device.
    /// </summary>
    public      Device  neighborDn { get { return _neighborDn; } }
    protected   Device  _neighborDn;
    /// <summary>
    /// Leftwards neighboring device.
    /// </summary>
    public      Device  neighborLt { get { return _neighborLt; } }
    protected   Device  _neighborLt;
    /// <summary>
    /// Rightwards neighboring device.
    /// </summary>
    public      Device  neighborRt { get { return _neighborRt; } }
    protected   Device  _neighborRt;
    protected   Device  this[int i] { get {
        switch (i) {
            case (0): return _neighborUp;
            case (1): return _neighborDn;
            case (2): return _neighborLt;
            case (3): return _neighborRt;
        }
        return null;
    } }

    protected RibbonGraph   _graphUp;
    public RibbonGraph      graphUp { get { return _graphUp; } set { _graphUp = value; } }
    protected RibbonGraph   _graphDn;
    public RibbonGraph      graphDn { get { return _graphDn; } set { _graphDn = value; } }
    protected RibbonGraph   _graphLt;
    public RibbonGraph      graphLt { get { return _graphLt; } set { _graphLt = value; } }
    protected RibbonGraph   _graphRt;
    public RibbonGraph      graphRt { get { return _graphRt; } set { _graphRt = value; } }
    public int              dijkstraDistance;
    public Device           dijkstraPrevious;

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
                _neighborUp = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) _neighborUp.Notify(false, true, false, false);
            }
        }
        if (dn) {
            if (Physics.Raycast(originDn, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                _neighborDn = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) _neighborDn.Notify(true, false, false, false);
            }
        }
        if (lt) {
            if (Physics.Raycast(originLt, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                this._neighborLt = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) _neighborLt.Notify(false, false, false, true);
            }
        }
        if (rt) {
            if (Physics.Raycast(originRt, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                _neighborRt = currentHit.transform.gameObject.GetComponent<Device>();
                if (respond) _neighborRt.Notify(false, false, true, false);
            }
        }
    }

    /// <summary>
    /// Tells this device to re-evaluate its neighbor in the specified directions.
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
        if (_neighborUp != null) _neighborUp.Notify(false, true, false, false);
        return enableUp;
    }

    public bool ToggleDown() {
        enableDn = enableDn ? false : true;
        if (_neighborDn != null) _neighborDn.Notify(true, false, false, false);
        return enableDn;
    }

    public bool ToggleLeft() {
        enableLt = enableLt ? false : true;
        if (_neighborLt != null) _neighborLt.Notify(false, false, false, true);
        return enableLt;
    }

    public bool ToggleRight() {
        enableRt = enableRt ? false : true;
        if (_neighborRt != null) _neighborRt.Notify(false, false, true, false);
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
    /// <returns>An item with any amount that could not be added in its stack, or null if everything was added.</returns>
    public virtual InventoryItem GiveItem(InventoryItem item) {
        return item;
    }

    protected virtual void ManageGraph() {
    }

    public virtual void DestroySelf() {
        enableUp = false;
        enableDn = false;
        enableLt = false;
        enableRt = false;
        if (_neighborUp != null) _neighborUp.Notify(false, true, false, false);
        if (_neighborDn != null) _neighborDn.Notify(true, false, false, false);
        if (_neighborLt != null) _neighborLt.Notify(false, false, false, true);
        if (_neighborRt != null) _neighborRt.Notify(false, false, true, false);
        Destroy(gameObject);
    }

    public string DEBUGReportNeighbors() {
        string nameUp                   = "null";
        string nameDn                   = "null";
        string nameLt                   = "null";
        string nameRt                   = "null";
        if (_neighborUp != null) nameUp = _neighborUp.name;
        if (_neighborDn != null) nameDn = _neighborDn.name;
        if (_neighborLt != null) nameLt = _neighborLt.name;
        if (_neighborRt != null) nameRt = _neighborRt.name;
        if (!connectedUp) nameUp        = "(X)"+nameUp;
        if (!connectedDn) nameDn        = "(X)"+nameDn;
        if (!connectedLt) nameLt        = "(X)"+nameLt;
        if (!connectedRt) nameRt        = "(X)"+nameRt;

        return (
            gameObject.name + " NEIGHBORS\n" +
                "\tUP: \t" + nameUp +
                "\n\tDOWN: \t" + nameDn +
                "\n\tLEFT: \t" + nameLt + 
                "\n\tRIGHT: \t" + nameRt
        );
    }
}
