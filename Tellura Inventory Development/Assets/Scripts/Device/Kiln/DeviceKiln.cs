using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceKiln : Device {
    private Inventory inventory;
    public override bool enableUp { get { return _enableUp; } set { _enableUp = value; } }
    public override bool enableDn { get { return false; } }
    public override bool enableLt { get { return false; } }
    public override bool enableRt { get { return false; } }


	private void Awake () {
        gameObject.name = "Kiln " + gameObject.GetInstanceID();
        inventory       = new Inventory(1);
        enableUp        = true;
        enableDn        = false;
        enableLt        = false;
        enableRt        = false;
	}

    private void Start() {
        this.HelloNeighbor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override InventoryItem RequestItem(InventoryItem item, int amount = 0) {
        return base.RequestItem(item, amount);
    }

    //public override InventoryItem GiveItem(InventoryItem item) {
    //    if (inventory.GetItemAtIndex(0) == null) {
    //        item.addStack(-1)
    //    }
    //    return item;
    //}
}
