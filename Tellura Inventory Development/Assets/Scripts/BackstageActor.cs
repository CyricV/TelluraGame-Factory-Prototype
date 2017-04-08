using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject ribbonPart_PF;


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

        GameObject chest1 = Instantiate(chest_PF, new Vector3(0,0,0), Quaternion.identity);
        GameObject chest2 = Instantiate(chest_PF, new Vector3(1,0,0), Quaternion.identity);
        chest1.GetComponent<DeviceChest>().chestContents.addItem(0, 100);
        print(chest1.GetComponent<DeviceChest>().chestContents.DEBUGReportInventory());
        makeKiln(4, 0);

        makeRibbon(1, 1);
        makeRibbon(1, 2);
        makeRibbon(2, 2);
        makeRibbon(3, 2);
        makeRibbon(4, 2);
        makeRibbon(4, 1);

        makeRibbon(-1, -1);
        makeRibbon(0, -1);
        makeRibbon(1, -1);
        makeRibbon(2, -1);
        makeRibbon(3, -1);
        GameObject ribbo = makeRibbon(4, -1);
        for (int x = 0; x<10; x++) {
            for (int y = -1; y>-4; y--) {
                makeRibbon(x, y);
            }
        }
        ribbo.GetComponent<DeviceRibbon>().enableDown = false;
        ribbo.GetComponent<DeviceRibbon>().enableRight = false;
    }
    
    void Update() {
    }

    public GameObject makeRibbon(int x, int y) {
        if (Physics.Raycast(new Vector3(x, y, 0), Vector3.forward)) return null;
        return Instantiate(ribbon_PF, new Vector3(x, y, 0), Quaternion.identity);;
    }

    public GameObject makeKiln(int x, int y) {
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
