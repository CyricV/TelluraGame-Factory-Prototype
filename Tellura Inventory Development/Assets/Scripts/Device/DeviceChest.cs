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
        chestContents       = new Inventory(8);
        enableUp        = true;
        enableDown      = true;
        enableLeft      = true;
        enableRight     = true;
	}

    private void Start() {
        helloNeighbor();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
