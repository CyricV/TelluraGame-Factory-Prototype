using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceRibbon : Device {
    private bool enableUp;
    private bool enableDown;
    private bool enableLeft;
    private bool enableRight;
    GameObject partUp;
    GameObject partDown;
    GameObject partLeft;
    GameObject partRight;

	// Use this for initialization
	void Start () {
        enableUp        = true;
        enableDown      = true;
        enableLeft      = true;
        enableRight     = true;

        this.helloNeighbor();
        if(this.upNeighbor != null && enableUp) {
            makeUpPart();
        }
        if(this.downNeighbor != null && enableDown) {
            makeDownPart();
        }
        if(this.leftNeighbor != null && enableLeft) {
            makeLeftPart();
        }
        if(this.rightNeighbor != null && enableRight) {
            makeRightPart();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void makeUpPart() {
        GameObject part                                 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.transform.position                         = gameObject.transform.position + new Vector3(0.0f, 0.25f, 0.0f);
        part.transform.localScale                       = new Vector3(0.09f, 0.5f, 0.09f);
        part.GetComponent<Renderer>().material.color    = new Color(0.02f, 0.02f, 0.02f);
        part.name                                       = "ribbon_up";
        this.partUp                                     = part;
    }
    private void makeDownPart() {
        GameObject part                                 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.transform.position                         = gameObject.transform.position + new Vector3(0.0f, -0.25f, 0.0f);
        part.transform.localScale                       = new Vector3(0.09f, 0.5f, 0.09f);
        part.GetComponent<Renderer>().material.color    = new Color(0.02f, 0.02f, 0.02f);
        part.name                                       = "ribbon_down";
        this.partDown                                   = part;
    }
    private void makeLeftPart() {
        GameObject part                                 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.transform.position                         = gameObject.transform.position + new Vector3(-0.25f, 0.0f, 0.0f);
        part.transform.localScale                       = new Vector3(0.5f, 0.09f, 0.09f);
        part.GetComponent<Renderer>().material.color    = new Color(0.02f, 0.02f, 0.02f);
        part.name                                       = "ribbon_left";
        this.partLeft                                   = part;
    }
    private void makeRightPart() {
        GameObject part                                 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        part.transform.position                         = gameObject.transform.position + new Vector3(0.25f, 0.0f, 0.0f);
        part.transform.localScale                       = new Vector3(0.5f, 0.09f, 0.09f);
        part.GetComponent<Renderer>().material.color    = new Color(0.02f, 0.02f, 0.02f);
        part.name                                       = "ribbon_right";
        this.partRight                                  = part;
    }
}
