using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic storage vessel.
public class DeviceChest : Device {

    private Inventory   localChestContents;
    private Inventory   sharedContents;
    private bool        isMerged;
    
    private void chestLink() {
        helloNeighbor();
        for (int i = 0; i < 4; i++) {
            DeviceChest currentNeighnor = this[i] as DeviceChest;

        }
    }

    public bool getMerged() {
        return this.isMerged;
    }

	// Use this for initialization
	void Start () {
        this.shortName = "chest";
        localChestContents = new Inventory(8);
        this.chestLink();
        Debug.Log("Chest exists");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
