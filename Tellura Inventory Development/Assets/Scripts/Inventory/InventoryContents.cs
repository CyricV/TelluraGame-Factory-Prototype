using System.Collections;
using System.Collections.Generic;

public class InventoryContents {
    public InventoryItem[] contentsArray;

    /// <summary>
    /// Default Constructor
    /// </summary>
    /// <param name="size"></param>
    public InventoryContents(int size) {
        contentsArray = new InventoryItem[size];
    }


    public int sort() {
        return 0;
    }
}
