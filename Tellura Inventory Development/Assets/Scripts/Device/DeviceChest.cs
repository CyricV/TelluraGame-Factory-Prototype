using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic storage vessel with an inventory of 8.
/// </summary>
public class DeviceChest : Device {
    public Inventory chestContents;

	private void Awake () {
        gameObject.name     = "Chest " + gameObject.GetInstanceID();
        shortName           = "chest";
        chestContents       = new Inventory(8);
	}

    private void Start() {
        helloNeighbor();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
