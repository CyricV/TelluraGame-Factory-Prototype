using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackstageActor : MonoBehaviour {
    /// <summary>
    /// A template for every device that exists.
    /// </summary>
    //public Device[] deviceMasterList = new Device[1];
    public static InventoryItemMasterList masterList;


	void Start () {
        masterList = new InventoryItemMasterList();

        string stringItemMasterList = "Item Master List:\n";
	    for(int i = 0; i<masterList.items.Length; i++) {
            stringItemMasterList += ("\t"+masterList.items[i].getID()+"\t"+masterList.items[i].getName()+"\n");
        }
        print(stringItemMasterList);

        Inventory testPlayerInventory = new Inventory(100);

        // addItem testing
        //Inventory testInventory0 = new Inventory(10);
        //testInventory0.addItem(0, 100);
        //Debug.Log(testInventory0.DEBUGReportInventory());
        //testInventory0.addItem(0, 2);
        //Debug.Log(testInventory0.DEBUGReportInventory());
        //testInventory0.addItem(1, 2);
        //Debug.Log(testInventory0.DEBUGReportInventory());
        //testInventory0.addItem(0, 100);
        //Debug.Log(testInventory0.DEBUGReportInventory());

        //// addItemAtIndex testing
        //testInventory0.addItemAtIndex(new InventoryItem(masterList.items[0], 20), 3);
        //Debug.Log(testInventory0.DEBUGReportInventory());
        //testInventory0.addItemAtIndex(new InventoryItem(masterList.items[0], 20), 3);
        //Debug.Log(testInventory0.DEBUGReportInventory());
        //testInventory0.addItemAtIndex(new InventoryItem(masterList.items[0], 20), 6);
        //Debug.Log(testInventory0.DEBUGReportInventory());
        //testInventory0.addItem(0, 100);
        //Debug.Log(testInventory0.DEBUGReportInventory());

        //// removeItemTesting
        //testInventory0.removeItemAtIndex(0, 24);
        //Debug.Log(testInventory0.DEBUGReportInventory());
        //testInventory0.removeItemAtIndex(0, 24);
        //Debug.Log(testInventory0.DEBUGReportInventory());


        // device testing
        GameObject chest1 = testMakeChest(0, 0);
        GameObject chest2 = testMakeChest(1, 0);
        chest1.GetComponent<DeviceChest>().chestContents.addItem(0, 100);
        print(chest1.GetComponent<DeviceChest>().chestContents.DEBUGReportInventory());
        testMakeKiln(4, 0);

        testMakeRibbon(1, 1);
        testMakeRibbon(1, 2);
        testMakeRibbon(2, 2);
        testMakeRibbon(3, 2);
        testMakeRibbon(4, 2);
        testMakeRibbon(4, 1);

    }
    
    void Update() {
    }

    public GameObject testMakeChest(int x, int y) {
        GameObject chest                                    = GameObject.CreatePrimitive(PrimitiveType.Cube);
        chest.transform.position                            = new Vector3(x, y, 0);
        chest.transform.localScale                          = new Vector3(0.6f, 0.6f, 0.6f);
        chest.GetComponent<Renderer>().material.color       = new Color(0.82f, 0.41f, 0.11f);
        chest.name                                          = "Chest";
        chest.AddComponent<DeviceChest>();
        return chest;
    }

    public void testMakeKiln(int x, int y) {
        GameObject kiln                                     = GameObject.CreatePrimitive(PrimitiveType.Cube);
        kiln.transform.position                             = new Vector3(x, y, 0);
        kiln.GetComponent<Renderer>().material.color        = new Color(1.0f, 0.1f, 0.1f);
        kiln.name                                           = "Kiln";
        kiln.transform.localScale                           = new Vector3(0.6f, 0.9f, 0.6f);
        kiln.AddComponent<DeviceKiln>();
    }
    
    public void testMakeRibbon(int x, int y) {
        GameObject ribbon                                   = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ribbon.transform.position                           = new Vector3(x, y, 0);
        ribbon.GetComponent<Renderer>().material.color      = new Color(0.02f, 0.02f, 0.02f);
        ribbon.name                                         = "Kiln";
        ribbon.transform.localScale                         = new Vector3(0.1f, 0.1f, 0.1f);
        ribbon.AddComponent<DeviceRibbon>();
    }
}
