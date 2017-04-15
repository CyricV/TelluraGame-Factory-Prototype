using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;
using SimpleJSON;
using System.IO;

public class InventoryItemMasterList {
    /// <summary>
    /// A template for every item that exists.
    /// </summary>
    public InventoryItem[] items;

    public InventoryItemMasterList() {
        string data = File.ReadAllText(Application.streamingAssetsPath+"/JSON/items.JSON");
        JSONNode json = JSON.Parse(data);
        items = new InventoryItem[json[Generic.ITEMS].Count];
        for (int i = 0; i < json[Generic.ITEMS].Count; i++) {
            items[i] = new InventoryItem(
                i,
                json[Generic.ITEMS][i][Generic.STACK_MAX],
                json[Generic.ITEMS][i][Generic.DISPLAY_NAME],
                json[Generic.ITEMS][i][Generic.NAME]);
        }
    }
}
