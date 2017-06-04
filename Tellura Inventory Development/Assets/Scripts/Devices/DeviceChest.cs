using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;

/// <summary>
/// Basic storage vessel with an inventory of 8.
/// </summary>
public class DeviceChest : Device {
    public Inventory chestContents;

    protected override void Awake() {
        _portUp = new DevicePort(this, Keywords.Names.PORT_TYPE_PROVIDER);
        _portDn = new DevicePort(this, Keywords.Names.PORT_TYPE_PROVIDER);
        _portLt = new DevicePort(this, Keywords.Names.PORT_TYPE_PROVIDER);
        _portRt = new DevicePort(this, Keywords.Names.PORT_TYPE_PROVIDER);
        gameObject.name     = "Chest " + gameObject.GetInstanceID();
        chestContents       = new Inventory(8);
        print(gameObject.name + " is Awake");
    }

    protected override void Start() {
        base.Start();
    }

    public override bool ToggleUp() {
        return true;
    }

    public override bool ToggleDn() {
        return true;
    }

    public override bool ToggleLt() {
        return true;
    }

    public override bool ToggleRt() {
        return true;
    }
}
