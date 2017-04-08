using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceKiln : Device {
    private Inventory input;


	private void Awake () {
        gameObject.name = "Kiln " + gameObject.GetInstanceID();
        input           = new Inventory(1);
	}

    private void Start() {
        this.helloNeighbor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
