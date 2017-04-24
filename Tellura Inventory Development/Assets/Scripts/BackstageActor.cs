using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tellura;

public class BackstageActor : MonoBehaviour {
    /// <summary>
    /// A template for every device that exists.
    /// </summary>
    //public Device[] deviceMasterList = new Device[1];
    //public static InventoryItemMasterList masterList;
    public static DerivedMasterList masterList;

    //Prefabs
    public GameObject chest_PF;
    public GameObject kiln_PF;
    public GameObject ribbon_PF;
    public GameObject selectionIndicator_PF;


	void Start () {

        masterList = new DerivedMasterList();
        print(masterList.DEBUGPrintItemMasterList());

        Inventory testPlayerInventory = new Inventory(100);

        //GameObject chest1 = makeChest(0, 0);
        //GameObject chest2 = makeChest(1, 0);
        //chest1.GetComponent<DeviceChest>().chestContents.addItem("dust_ancient", 100);
        //print(chest1.GetComponent<DeviceChest>().chestContents.DEBUGReportInventory());

        //makeKiln(4, 0);

        makeChest(-1, 0);
        makeKiln(1, 0);

        makeRibbon(-1, 1);
        makeRibbon(0, 1);
        makeRibbon(1, 1);

        makeRibbon(-1, 2);
        makeRibbon(-1, 3);
        makeRibbon(0, 3);
        makeRibbon(1, 3);
        makeRibbon(1, 2);
    }
    
    void Update() {
    }

    public GameObject makeRibbon(int x, int y) {
        if (Physics.Raycast(new Vector3(x, y, 0), Vector3.forward, Mathf.Infinity, GameValues.LM_DEVICE)) return null;
        return Instantiate(ribbon_PF, new Vector3(x, y, 0), Quaternion.identity);
    }

    public GameObject makeChest(int x, int y) {
        if (Physics.Raycast(new Vector3(x, y, 0), Vector3.forward, Mathf.Infinity, GameValues.LM_DEVICE)) return null;
        return Instantiate(chest_PF, new Vector3(x, y, 0), Quaternion.identity);
    }

    public GameObject makeKiln(int x, int y) {
        if (Physics.Raycast(new Vector3(x, y, 0), Vector3.forward, Mathf.Infinity, GameValues.LM_DEVICE)) return null;
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
