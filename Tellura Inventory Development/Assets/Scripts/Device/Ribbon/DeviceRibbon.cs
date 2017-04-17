using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceRibbon : Device {
    GameObject partUp;
    GameObject partDn;
    GameObject partLt;
    GameObject partRt;

    public override bool enableUp {
        get {
            return _enableUp;
        }
        set {
            _enableUp = value;
            ManagePart(true, false, false, false);
            if (neighborUp != null) neighborUp.Notify(false, true, false, false);
        }
    }
    public override bool enableDn {
        get {
            return _enableDn;
        }
        set {
            _enableDn = value;
            ManagePart(false, true, false, false);
            if (neighborDn != null) neighborDn.Notify(true, false, false, false);
        }
    }
    public override bool enableLt {
        get {
            return _enableLt;
        }
        set {
            _enableLt = value;
            ManagePart(false, false, true, false);
            if (neighborLt != null) neighborLt.Notify(false, false, false, true);
        }
    }
    public override bool enableRt {
        get {
            return _enableRt;
        }
        set {
            _enableRt = value;
            ManagePart(false, false, false, true);
            if (neighborRt != null) neighborRt.Notify(false, false, true, false);
        }
    }

    RibbonNode node;

    private void Awake() {
        gameObject.name     = "Ribbon " + gameObject.GetInstanceID();
        enableUp            = true;
        enableDn            = true;
        enableLt            = true;
        enableRt            = true;
    }
    
	void Start () {
        HelloNeighbor();
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

    private void ManagePart(bool up = true, bool dn = true, bool lt = true, bool rt = true) {
        if (up) {
            if (neighborUp == null || !neighborUp.enableDn || !enableUp) {
                RemovePart(true, false, false, false);
            } else if (partUp == null && enableUp) {
                MakePart(true, false, false, false);
            }
        }
        if (dn) {
            if (neighborDn == null || !neighborDn.enableUp || !enableDn) {
                RemovePart(false, true, false, false);
            } else if (partDn == null && enableDn) {
                MakePart(false, true, false, false);
            }
        }
        if (lt) {
            if (neighborLt == null || !neighborLt.enableRt || !enableLt) {
                RemovePart(false, false, true, false);
            } else if (partLt == null && enableLt) {
                MakePart(false, false, true, false);
            }
        }
        if (rt) {
            if (neighborRt == null || !neighborRt.enableLt || !enableRt) {
                RemovePart(false, false, false, true);
            } else if (partRt == null && enableRt) {
                MakePart(false, false, false, true);
            }
        }
    }

    private void MakePart(bool up = true, bool dn = true, bool lt = true, bool rt = true) {
        if (!(neighborUp is Device)) up = false;
        if (!(neighborDn is Device)) dn = false;
        if (!(neighborLt is Device)) lt = false;
        if (!(neighborRt is Device)) rt = false;

        if (up && enableUp && neighborUp.enableDn) {
            string name         = "ribbon_up " + gameObject.GetInstanceID();
            string loadName     = (neighborUp is DeviceRibbon) ? "Prefabs/RibbonPart" : "Prefabs/RibbonPartConnector";
            partUp              = InstantiatePart(0, loadName, name);
        }
        if (dn && enableDn && neighborDn.enableUp) {
            string name         = "ribbon_down " + gameObject.GetInstanceID();
            string loadName     = (neighborDn is DeviceRibbon) ? "Prefabs/RibbonPart" : "Prefabs/RibbonPartConnector";
            partDn              = InstantiatePart(180, loadName, name);
        }
        if (lt && enableLt && neighborLt.enableRt) {
            string name         = "ribbon_left " + gameObject.GetInstanceID();
            string loadName     = (neighborLt is DeviceRibbon) ? "Prefabs/RibbonPart" : "Prefabs/RibbonPartConnector";
            partLt              = InstantiatePart(90, loadName, name);
        }
        if (rt && enableRt && neighborRt.enableLt) {
            string name         = "ribbon_right " + gameObject.GetInstanceID();
            string loadName     = (neighborRt is DeviceRibbon) ? "Prefabs/RibbonPart" : "Prefabs/RibbonPartConnector";
            partRt              = InstantiatePart(270, loadName, name);
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
