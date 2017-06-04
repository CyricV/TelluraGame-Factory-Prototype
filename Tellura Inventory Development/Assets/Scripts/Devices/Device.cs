using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;
using Keywords;

public class Device : MonoBehaviour {
    public virtual  DevicePort  portUp { get { return _portUp; } }
    protected       DevicePort  _portUp;
    public virtual  DevicePort  portDn { get { return _portDn; } }
    protected       DevicePort  _portDn;
    public virtual  DevicePort  portLt { get { return _portLt; } }
    protected       DevicePort  _portLt;
    public virtual  DevicePort  portRt { get { return _portRt; } }
    protected       DevicePort  _portRt;

    /// <summary>
    /// If this device is connected to its upwards neighbor.
    /// </summary>
    public bool connectedUp { get { return (portUp != null && _neighborUp is Device && _neighborUp.portDn !=null); } }
    /// <summary>
    /// If this device is connected to its downwards neighbor.
    /// </summary>
    public bool connectedDn { get { return (portDn != null && _neighborDn is Device && _neighborDn.portUp !=null); } }
    /// <summary>
    /// If this device is connected to its leftwards neighbor.
    /// </summary>
    public bool connectedLt { get { return (portLt != null && _neighborLt is Device && _neighborLt.portRt !=null); } }
    /// <summary>
    /// If this device is connected to its rightwards neighbor.
    /// </summary>
    public bool connectedRt { get { return (portRt != null && _neighborRt is Device && _neighborRt.portLt !=null); } }

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

    //private Device SuperDevice;

    protected virtual void Awake() {
        _portUp = new DevicePort(this);
        _portDn = new DevicePort(this);
        _portLt = new DevicePort(this);
        _portRt = new DevicePort(this);
    }

    protected virtual void Start() {
        HelloNeighbor();
    }

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
                if(_portUp != null) _portUp.companion = _neighborUp.portDn;
                if (respond) _neighborUp.Notify(false, true, false, false);
            }
        }
        if (dn) {
            if (Physics.Raycast(originDn, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                _neighborDn = currentHit.transform.gameObject.GetComponent<Device>();
                if(_portDn != null) _portDn.companion = _neighborDn.portUp;
                if (respond) _neighborDn.Notify(true, false, false, false);
            }
        }
        if (lt) {
            if (Physics.Raycast(originLt, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                this._neighborLt = currentHit.transform.gameObject.GetComponent<Device>();
                if(_portLt != null) _portLt.companion = _neighborLt.portRt;
                if (respond) _neighborLt.Notify(false, false, false, true);
            }
        }
        if (rt) {
            if (Physics.Raycast(originRt, Vector3.forward, out currentHit, Mathf.Infinity, GameValues.LM_DEVICE)) {
                _neighborRt = currentHit.transform.gameObject.GetComponent<Device>();
                if(_portRt != null) _portRt.companion = _neighborRt.portLt;
                if (respond) _neighborRt.Notify(false, false, true, false);
            }
        }
        //print(DEBUGReportPorts());
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

    public virtual bool ToggleUp() {
        bool returnValue;
        if (portUp != null) {
            _portUp = null;
            returnValue = false;
        } else {
            _portUp = new DevicePort(this);
            returnValue = true;
        }
        if (_neighborUp != null) _neighborUp.Notify(false, true, false, false);
        return returnValue;
    }

    public virtual bool ToggleDn() {
        bool returnValue;
        if (portDn != null) {
            _portDn = null;
            returnValue = false;
        } else {
            _portDn = new DevicePort(this);
            returnValue = true;
        }
        if (_neighborDn != null) _neighborDn.Notify(true, false, false, false);
        return returnValue;
    }

    public virtual bool ToggleLt() {
        bool returnValue;
        if (portLt != null) {
            _portLt = null;
            returnValue = false;
        } else {
            _portLt = new DevicePort(this);
            returnValue = true;
        }
        if (_neighborLt != null) _neighborLt.Notify(false, false, false, true);
        return returnValue;
    }

    public virtual bool ToggleRt() {
        bool returnValue;
        if (portRt != null) {
            _portRt = null;
            returnValue = false;
        } else {
            _portRt = new DevicePort(this);
            returnValue = true;
        }
        if (_neighborRt != null) _neighborRt.Notify(false, false, true, false);
        return returnValue;
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

    public virtual void DestroySelf() {
        _portUp = null;
        _portDn = null;
        _portLt = null;
        _portRt = null;
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

    public string DEBUGReportPorts() {
        string dataUp;
        string dataDn;
        string dataLt;
        string dataRt;
        if (_portUp == null) {
            dataUp      = "Off\n";
        } else {
            dataUp      = "Type: " + _portUp.portType + "\n";
            if (_portUp.companion != null) {
                dataUp  += "Companion Type: " + _portUp.companion.portType +"\n";
            }
            dataUp      += "Siblings: " + _portUp.siblings.Count + "\n";
        }
        if (_portDn == null) {
            dataDn      = "Off\n";
        } else {
            dataDn      = "Type: " + _portDn.portType + "\n";
            if (_portDn.companion != null) {
                dataDn  += "Companion Type: " + _portDn.companion.portType +"\n";
            }
            dataDn      += "Siblings: " + _portDn.siblings.Count + "\n";
        }
        if (_portLt == null) {
            dataLt      = "Off\n";
        } else {
            dataLt      = "Type: " + _portLt.portType + "\n";
            if (_portLt.companion != null) {
                dataLt  += "Companion Type: " + _portLt.companion.portType +"\n";
            }
            dataLt      += "Siblings: " + _portLt.siblings.Count + "\n";
        }
        if (_portRt == null) {
            dataRt      = "Off\n";
        } else {
            dataRt      = "Type: " + _portRt.portType + "\n";
            if (_portRt.companion != null) {
                dataRt  += "Companion Type: " + _portRt.companion.portType +"\n";
            }
            dataRt      += "Siblings: " + _portRt.siblings.Count + "\n";
        }
        return (
            "PORT REPORT FROM " + gameObject.name + "\n" +
            "UP\n" + dataUp + "\n" +
            "DOWN\n" + dataDn + "\n" +
            "LEFT\n" + dataLt + "\n" +
            "RIGHT\n" + dataRt + "\n"
        );
    }
}
