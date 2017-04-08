using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Storage vessel that can combine with other Merge Chests to form a larger inventory.
/// </summary>
public class DeviceMergeChest : Device {

    private Inventory       localChestContents;
    private Inventory           sharedContents;
    private DeviceChestGroup    group;
    private bool                isMerged {
        get {
            if (this.group == null) return false;
            return true;
        }
    }
    
    private void chestLink() {
        helloNeighbor();
        for (int i = 0; i < 4; i++) {
            DeviceMergeChest currentNeighbor = this[i] as DeviceMergeChest;
            if (currentNeighbor != null) {
                // Merge into already merged chests
                if (currentNeighbor.isMerged) {

                }
            }

        }
    }

	// Use this for initialization
	void Start () {
        localChestContents = new Inventory(8);
        this.chestLink();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
