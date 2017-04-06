using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceKiln : Device {
    private Inventory input;
    private Inventory output;
    private RibbonNode inputNode;
    private RibbonNode outputNode;


	private void Awake () {
        this.shortName  = "kiln";
        input           = new Inventory(1);
        output          = new Inventory(1);
	}

    private void Start() {
        this.helloNeighbor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
