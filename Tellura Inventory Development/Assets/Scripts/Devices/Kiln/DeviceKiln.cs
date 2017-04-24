using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceKiln : Device {
    public override bool enableUp { get { return _enableUp; } set { _enableUp = value; } }
    public override bool enableDn { get { return false; } }
    public override bool enableLt { get { return false; } }
    public override bool enableRt { get { return false; } }
    private Inventory inventory;
    private RecipeKiln currentRecipe;

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
		if(inventory.GetItemAtIndex(0) != null
        && BackstageActor.masterList.kilnRecipes.TryGetValue(inventory.GetItemAtIndex(0).name, out currentRecipe)) {
            ProcessInventory();
        }
	}

    public override InventoryItem RequestItem(InventoryItem item, int amount = 0) {
        return base.RequestItem(item, amount);
    }

    public override InventoryItem GiveItem(InventoryItem item) {
        if (item.stackCurrent == 0 || inventory.GetItemAtIndex(0) != null) return item;
        else if (item.stackCurrent == 1) return inventory.AddItemAtIndex(item, 0);
        inventory.AddItemAtIndex(new InventoryItem(item, 1), 0);
        item.addStack(-1);
        return item;
    }

    private void ProcessInventory() {
        if (currentRecipe.name == inventory.GetItemAtIndex(0).name) {
            inventory.TakeItemAtIndex(0);
            inventory.AddItemAtIndex(currentRecipe.output.copy(), 0);
        }
    }
}
