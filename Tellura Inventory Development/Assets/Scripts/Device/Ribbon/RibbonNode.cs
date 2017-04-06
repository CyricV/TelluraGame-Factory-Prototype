using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonNode {
    private List<Inventory>     providers;
    private List<Inventory>     requesters;
    private List<DeviceRibbon>  ribbons;

    public void addProvider(Inventory inventory) {
        providers.Add(inventory);
    }
    public void addRequester(Inventory inventory) {
        requesters.Add(inventory);
    }
    public void addRibbon(DeviceRibbon ribbon) {
        ribbons.Add(ribbon);
    }

    public void mergeNode(RibbonNode node) {
        foreach (Inventory inventory in node.providers) {
            providers.Add(inventory);
            inventory.node = this;
        }
        foreach (Inventory inventory in node.requesters) {
            requesters.Add(inventory);
            inventory.node = this;
        }
        foreach (DeviceRibbon ribbon in node.ribbons) {
            ribbons.Add(ribbon);
            ribbon.node = this;
        }
    }
}