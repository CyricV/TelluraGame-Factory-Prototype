using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;

public class BackstageActor : MonoBehaviour {
    /// <summary>
    /// A template for every device that exists.
    /// </summary>
    //public Device[] deviceMasterList = new Device[1];
    public static InventoryItemMasterList masterList;

    //Prefabs
    public GameObject chest_PF;
    public GameObject kiln_PF;
    public GameObject ribbon_PF;
    public GameObject selectionIndicator_PF;


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

        GameObject chest1 = makeChest(0, 0);
        GameObject chest2 = makeChest(1, 0);
        chest1.GetComponent<DeviceChest>().chestContents.addItem(0, 100);
        print(chest1.GetComponent<DeviceChest>().chestContents.DEBUGReportInventory());

        makeKiln(4, 0);
        
        //for (int x = 1; x<=4; x++) {
        //    for (int y = 3; y>=1; y--) {
        //        print(makeRibbon(x, y).GetComponent<Device>().DEBUGReportNeighbors());
        //    }
        //}
        //GameObject ribbon1 = makeRibbon(0, 2);
        //GameObject ribbon2 = makeRibbon(0, 1);
        //print(ribbon1.GetComponent<Device>().DEBUGReportNeighbors());
        //print(ribbon2.GetComponent<Device>().DEBUGReportNeighbors());
        //ribbon1.GetComponent<Device>().enableRight = false;
        ////print(ribbon1.GetComponent<Device>().DEBUGReportNeighbors());
        //ribbon1.GetComponent<Device>().enableDown = false;
        ////print(ribbon1.GetComponent<Device>().DEBUGReportNeighbors());
        //ribbon2.GetComponent<Device>().enableRight = false;
        //ribbon1.GetComponent<Device>().enableDown = true;

    }
    
    void Update() {
    }

    public GameObject makeRibbon(int x, int y) {
        if (Physics.Raycast(new Vector3(x, y, 0), Vector3.forward, Mathf.Infinity, GameValues.LayerMaskDevice)) return null;
        return Instantiate(ribbon_PF, new Vector3(x, y, 0), Quaternion.identity);
    }

    public GameObject makeChest(int x, int y) {
        if (Physics.Raycast(new Vector3(x, y, 0), Vector3.forward, Mathf.Infinity, GameValues.LayerMaskDevice)) return null;
        return Instantiate(chest_PF, new Vector3(x, y, 0), Quaternion.identity);
    }

    public GameObject makeKiln(int x, int y) {
        if (Physics.Raycast(new Vector3(x, y, 0), Vector3.forward, Mathf.Infinity, GameValues.LayerMaskDevice)) return null;
        return Instantiate(kiln_PF, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void testDrawGrid(Vector3 start, int xSize, int ySize, float duration = 99f) {
        for (int x = 0; x < xSize; x++) {
            Debug.DrawRay(start + new Vector3(x, 0, 0), Vector3.down, Color.green, duration, false);
        }
        for (int y = 0; y < xSize; y++) {
            Debug.DrawRay(start - new Vector3(y, 0, 0), Vector3.right, Color.green, duration, false);
        }
    }
}
