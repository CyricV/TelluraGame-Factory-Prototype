using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;

public class DeviceRibbon : Device {
    private GameObject partUp;
    private GameObject partDn;
    private GameObject partLt;
    private GameObject partRt;

    public override bool enableUp {
        get {
            return _enableUp;
        }
        set {
            _enableUp = value;
            ManagePart(true, false, false, false);
            if (_neighborUp != null) _neighborUp.Notify(false, true, false, false);
        }
    }
    public override bool enableDn {
        get {
            return _enableDn;
        }
        set {
            _enableDn = value;
            ManagePart(false, true, false, false);
            if (_neighborDn != null) _neighborDn.Notify(true, false, false, false);
        }
    }
    public override bool enableLt {
        get {
            return _enableLt;
        }
        set {
            _enableLt = value;
            ManagePart(false, false, true, false);
            if (_neighborLt != null) _neighborLt.Notify(false, false, false, true);
        }
    }
    public override bool enableRt {
        get {
            return _enableRt;
        }
        set {
            _enableRt = value;
            ManagePart(false, false, false, true);
            if (_neighborRt != null) _neighborRt.Notify(false, false, true, false);
        }
    }

    protected RibbonGraph mainGraph  {
        get { return _graphUp; }
        set { _graphUp = value; }
    }

    private void Awake() {
        gameObject.name     = "Ribbon " + gameObject.GetInstanceID();
        _enableUp           = true;
        _enableDn           = true;
        _enableLt           = true;
        _enableRt           = true;
    }
    
	void Start () {
        HelloNeighbor();
        ManageGraph();
    }

    protected override void HelloNeighbor(bool up = true, bool dn = true, bool lt = true, bool rt = true, bool respond = true) {
        base.HelloNeighbor(up, dn, lt, rt, respond);
        if (up) ManagePart(true,  false, false, false);
        if (dn) ManagePart(false, true,  false, false);
        if (lt) ManagePart(false, false, true,  false);
        if (rt) ManagePart(false, false, false, true);
    }

    public override void Notify(bool up = false, bool dn = false, bool lt = false, bool rt = false) {
        base.Notify(up, dn, lt, rt);
        ManagePart(up, dn, lt, rt);
    }

    protected override void ManageGraph() {
        mainGraph = new RibbonGraph();
        mainGraph.addRibbon(this);
        if (connectedUp && neighborUp is DeviceRibbon) {
            mainGraph = (neighborUp as DeviceRibbon).mainGraph;
            mainGraph.addRibbon(this);
        } else if (connectedUp && neighborUp is Device) {
            neighborUp.graphDn = mainGraph;
        }
        if (connectedRt && neighborRt is DeviceRibbon) {

        }
    }

    private void ManagePart(bool up = true, bool dn = true, bool lt = true, bool rt = true) {
        RemovePart(up, dn, lt, rt);
        MakePart(up, dn, lt, rt);
    }

    private void MakePart(bool up = true, bool dn = true, bool lt = true, bool rt = true) {
        if (up) {
            string name                     = "Ribbon_Up " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (!enableUp) loadName         = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedUp) loadName       = (_neighborUp is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partUp    = InstantiatePart(0, loadName, name);
        }
        if (dn) {
            string name                     = "Ribbon_Dn " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (!enableDn) loadName         = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedDn) loadName       = (_neighborDn is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partDn    = InstantiatePart(180, loadName, name);
        }
        if (lt) {
            string name                     = "Ribbon_Lt " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (!enableLt) loadName         = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedLt) loadName       = (_neighborLt is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partLt    = InstantiatePart(90, loadName, name);
        }
        if (rt) {
            string name                     = "Ribbon_Rt " + gameObject.GetInstanceID();
            string loadName                 = null;
            if (!enableRt) loadName         = Keywords.Path.PF_RIBBON_PLUG;
            if (connectedRt) loadName       = (_neighborRt is DeviceRibbon) ? Keywords.Path.PF_RIBBON_PART : Keywords.Path.PF_RIBBON_CONNECTOR;
            if (loadName != null) partRt    = InstantiatePart(270, loadName, name);
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
