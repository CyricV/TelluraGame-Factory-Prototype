using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceKiln : Device {
    private Inventory input;


	private void Awake () {
        gameObject.name = "Kiln " + gameObject.GetInstanceID();
        input           = new Inventory(1);
        enableUp        = true;
        enableDown      = false;
        enableLeft      = false;
        enableRight     = false;
	}

    private void Start() {
        this.helloNeighbor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
