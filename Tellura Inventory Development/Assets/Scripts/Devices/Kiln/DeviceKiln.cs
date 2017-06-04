using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;

public class DeviceKiln : Device {
    private Inventory inventory;
    private RecipeKiln currentRecipe;

    protected override void Awake() {
        gameObject.name = "Kiln " + gameObject.GetInstanceID();
        inventory       = new Inventory(1);
        _portUp         = new DevicePort(this, Keywords.Names.PORT_TYPE_REQUESTER);
        print(gameObject.name + " is Awake");
    }

    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		if(inventory.GetItemAtIndex(0) != null
        && BackstageActor.masterList.kilnRecipes.TryGetValue(inventory.GetItemAtIndex(0).name, out currentRecipe)) {
            ProcessInventory();
        }
	}

    public override bool ToggleUp() {
        return true;
    }

    public override bool ToggleDn() {
        return false;
    }

    public override bool ToggleLt() {
        return false;
    }

    public override bool ToggleRt() {
        return false;
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
