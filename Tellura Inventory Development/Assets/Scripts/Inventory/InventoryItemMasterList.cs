using System.Collections;
using System.Collections.Generic;

public class InventoryItemMasterList {
    /// <summary>
    /// A template for every item that exists.
    /// </summary>
    public InventoryItem[] items;

    public InventoryItemMasterList(int size = 10) {
        items  = new InventoryItem[size];

        items[0] = new InventoryItem(
                0,
                32,
                "Ancient Dust"
        );
        items[1] = new InventoryItem(
                1,
                16,
                "Ley Crystal"
        );
        items[2] = new InventoryItem(
                2,
                16,
                "Simple Scrap"
        );
        items[3] = new InventoryItem(
                3,
                16,
                "Simple Core"
        );
        items[4] = new InventoryItem(
                4,
                16,
                "Clay"
        );
        items[5] = new InventoryItem(
                5,
                32,
                "Ceramic Plate"
        );
        items[6] = new InventoryItem(
                6,
                32,
                "Iron Dust"
        );
        items[7] = new InventoryItem(
                7,
                32,
                "Iron Plate"
        );
        items[8] = new InventoryItem(
                8,
                32,
                "Copper Dust"
        );
        items[9] = new InventoryItem(
                9,
                32,
                "Copper Plate"
        );
    }
}
