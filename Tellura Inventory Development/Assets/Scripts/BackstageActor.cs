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

        Inventory testPlayerInventory = new Inventory(10);

        Inventory testInventory0 = new Inventory(10);

        // addItem testing
        testInventory0.addItem(0, 100);
        testInventory0.DEBUGReportInventory();
        testInventory0.addItem(0, 2);
        testInventory0.DEBUGReportInventory();
        testInventory0.addItem(1, 2);
        testInventory0.DEBUGReportInventory();
        testInventory0.addItem(0, 100);
        testInventory0.DEBUGReportInventory();
        print(testInventory0.addItem(2, 10000)+"\n");
        testInventory0.DEBUGReportInventory();

        //// addItemAtIndex testing
        //testInventory0.addItemAtIndex(new InventoryItem(masterList.items[0], 20), 3);
        //testInventory0.DEBUGReportInventory();
        //testInventory0.addItemAtIndex(new InventoryItem(masterList.items[0], 20), 3);
        //testInventory0.DEBUGReportInventory();
        //testInventory0.addItemAtIndex(new InventoryItem(masterList.items[0], 20), 6);
        //testInventory0.DEBUGReportInventory();
        //testInventory0.addItem(0, 100);
        //testInventory0.DEBUGReportInventory();

        //// removeItemTesting
        //testInventory0.removeItemAtIndex(0, 24);
        //testInventory0.DEBUGReportInventory();
        //testInventory0.removeItemAtIndex(0, 24);
        //testInventory0.DEBUGReportInventory();


        //// chest testing
        //GameObject chest1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //chest1.transform.position = new Vector3(0, 0, 0);
        //chest1.transform.localScale *= 0.9f;
        //chest1.name = "Chest";
        //chest1.AddComponent<DeviceChest>();

        //GameObject chest2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //chest2.transform.position = new Vector3(1, 0, 0);
        //chest2.transform.localScale *= 0.9f;
        //chest2.name = "Chest";
        //chest2.AddComponent<DeviceChest>();


    }

	void Update () {
		
	}
}
