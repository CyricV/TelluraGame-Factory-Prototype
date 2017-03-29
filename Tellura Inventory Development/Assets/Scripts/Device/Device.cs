using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour {
    protected int       xGridPos;
    protected int       yGridPos;
    protected string    shortName;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Heartbeat() {

    }

    public int getXPos() {
        return this.xGridPos;
    }
    
    public int getYPos() {
        return this.yGridPos;
    }
    
    public string getShortName() {
        return this.shortName;
    }

    public int placeDevice(int x, int y) {
        if (BackstageActor.factoryGrid.getIndex(x, y) == null) {
            BackstageActor.factoryGrid.setIndex(x, y, this);
        }
        return 0;
    }

    // Attempts to place all item's in this device's inventories as well as the device into a target inventory.
    public void pickUpDevice(Inventory targetInventory) {

    }
}
