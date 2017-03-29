using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic storage vessel.
public class DeviceChest : Device {

    Inventory localBinContents;
    Inventory sharedContents;

    public DeviceChest(int x, int y) {
        this.xGridPos = x;
        this.yGridPos = y;
        this.shortName = "chest";
        // This will fail if there is alread an object on the grid at this location.
        this.placeDevice(x, y);
        localBinContents = new Inventory(8);
        this.chestLinkCheck();
    }


    private void chestLinkCheck() {
        Device up       = BackstageActor.factoryGrid.getIndex(this.xGridPos, this.yGridPos + 1);
        Device down     = BackstageActor.factoryGrid.getIndex(this.xGridPos, this.yGridPos - 1);
        Device left     = BackstageActor.factoryGrid.getIndex(this.xGridPos - 1, this.yGridPos);
        Device right    = BackstageActor.factoryGrid.getIndex(this.xGridPos + 1, this.yGridPos);


    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
