﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;

public class DeviceRibbon : Device {
    private GameObject partUp;
    private GameObject partDn;
    private GameObject partLt;
    private GameObject partRt;

    protected override void Awake() {
        gameObject.name = "Ribbon " + gameObject.GetInstanceID();
        _portUp = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON);
        _portDn = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON);
        _portLt = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON);
        _portRt = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON);
        _portUp.siblings.AddRange(new DevicePort[] { _portDn, _portLt, _portRt });
        _portDn.siblings.AddRange(new DevicePort[] { _portUp, _portLt, _portRt });
        _portLt.siblings.AddRange(new DevicePort[] { _portUp, _portDn, _portRt });
        _portRt.siblings.AddRange(new DevicePort[] { _portUp, _portDn, _portLt });
    }

    protected override void Start() {
        base.Start();
    }

    protected override void HelloNeighbor(bool up = true, bool dn = true, bool lt = true, bool rt = true, bool respond = true) {
        base.HelloNeighbor(up, dn, lt, rt, respond);
        ManagePart(up, dn, lt, rt);
    }

    public override void Notify(bool up = false, bool dn = false, bool lt = false, bool rt = false) {
        base.Notify(up, dn, lt, rt);
        ManagePart(up, dn, lt, rt);
    }

    public override bool ToggleUp() {
        bool returnValue;
        if (portUp != null) {
            _portDn.siblings.Remove(_portUp);
            _portLt.siblings.Remove(_portUp);
            _portRt.siblings.Remove(_portUp);
            _portUp = null;
            returnValue = false;
        } else {
            _portUp = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON, null, new List<DevicePort>(new DevicePort[] { _portDn, _portLt, _portRt }));
            returnValue = true;
        }
        ManagePart(true, false, false, false);
        if (_neighborUp != null) _neighborUp.Notify(false, true, false, false);
        return returnValue;
    }

    public override bool ToggleDn() {
        bool returnValue;
        if (portDn != null) {
            _portUp.siblings.Remove(_portDn);
            _portLt.siblings.Remove(_portDn);
            _portRt.siblings.Remove(_portDn);
            _portDn = null;
            returnValue = false;
        } else {
            _portDn = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON, null, new List<DevicePort>(new DevicePort[] { _portUp, _portLt, _portRt }));
            returnValue = true;
        }
        ManagePart(false, true, false, false);
        if (_neighborDn != null) _neighborDn.Notify(true, false, false, false);
        return returnValue;
    }

    public override bool ToggleLt() {
        bool returnValue;
        if (portLt != null) {
            _portUp.siblings.Remove(_portLt);
            _portDn.siblings.Remove(_portLt);
            _portRt.siblings.Remove(_portLt);
            _portLt = null;
            returnValue = false;
        } else {
            _portLt = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON, null, new List<DevicePort>(new DevicePort[] { _portUp, _portDn, _portRt }));
            returnValue = true;
        }
        ManagePart(false, false, true, false);
        if (_neighborLt != null) _neighborLt.Notify(false, false, false, true);
        return returnValue;
    }

    public override bool ToggleRt() {
        bool returnValue;
        if (portRt != null) {
            _portUp.siblings.Remove(_portRt);
            _portDn.siblings.Remove(_portRt);
            _portLt.siblings.Remove(_portRt);
            _portRt = null;
            returnValue = false;
        } else {
            _portRt = new DevicePort(this, Keywords.Names.PORT_TYPE_RIBBON, null, new List<DevicePort>(new DevicePort[] { _portUp, _portDn, _portLt}));
            returnValue = true;
        }
        ManagePart(false, false, false, true);
        if (_neighborRt != null) _neighborRt.Notify(false, false, true, false);
        return returnValue;
    }

    private void ManagePart(bool up = true, bool dn = true, bool lt = true, bool rt = true) {
        RemovePart(up, dn, lt, rt);
        MakePart(up, dn, lt, rt);
    }

    private void MakePart(bool up = true, bool dn = true, bool lt = true, bool rt = true) {
        if (up) {
            string name                     = "Ribbon_Up " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (portUp == null) loadName    = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedUp) loadName       = (_neighborUp is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partUp    = InstantiatePart(Keywords.Rotations.UP, loadName, name);
        }
        if (dn) {
            string name                     = "Ribbon_Dn " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (portDn == null) loadName   = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedDn) loadName       = (_neighborDn is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partDn    = InstantiatePart(Keywords.Rotations.DN, loadName, name);
        }
        if (lt) {
            string name                     = "Ribbon_Lt " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (portLt == null) loadName    = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedLt) loadName       = (_neighborLt is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partLt    = InstantiatePart(Keywords.Rotations.LT, loadName, name);
        }
        if (rt) {
            string name                     = "Ribbon_Rt " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (portRt == null) loadName    = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedRt) loadName       = (_neighborRt is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partRt    = InstantiatePart(Keywords.Rotations.RT, loadName, name);
        }
    }

    private GameObject InstantiatePart(int rotation, string loadName, string name) {
        GameObject part = Instantiate(
                Resources.Load(loadName) as GameObject,
                gameObject.transform.position,
                Quaternion.Euler(0, 0, rotation));
        part.name = name;
        return part;
    }

    private void RemovePart(bool up = true, bool dn = true, bool lt = true, bool rt = true) {
        if (up) {
            Destroy(partUp);
            partUp = null;
        }
        if (dn) {
            Destroy(partDn);
            partDn = null;
        }
        if (lt) {
            Destroy(partLt);
            partLt = null;
        }
        if (rt) {
            Destroy(partRt);
            partRt = null;
        }
    }

    public override void DestroySelf() {
        Destroy(partUp);
        Destroy(partDn);
        Destroy(partLt);
        Destroy(partRt);
        base.DestroySelf();
    }
}
