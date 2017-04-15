using System.Collections;
using System.Collections.Generic;

public class InventoryItem {
    private bool    isTemplate;     // Master List items.
    private bool    isLock;         // For locking inventory slots and filters.
    private int     id;            // ID.
    private int     stackMax;      // Max amount of this item in one inventory slot.
    private int     stackCurrent;  // Current amount of this item present in the slot.
    private string  name;          // Display name of the item.
    private string  shortName;     // Code name for item.
    //public          InventoryItemData data;

    /// <summary>
    /// Template Constructor. Allows creation of all attributes via parameter.
    /// </summary>
    /// <param name="idParam">Desired ID.</param>
    /// <param name="stackMaxParam">Desired max stack size.</param>
    /// <param name="nameParam">Desired Item name.</param>
    public InventoryItem(
        int idParam             = -1,
        int stackMaxParam       = 1,
        string nameParam        = "Dummy Item",
        string shortNameParam   = "_dummy_item") {

        this.isTemplate     = true;
        this.isLock         = false;
        this.id             = idParam;
        this.stackMax       = stackMaxParam;
        this.stackCurrent   = 1;
        this.name           = nameParam;
        this.shortName      = shortNameParam;
    }
    
    /// <summary>
    /// Instance Constructor. Makes an item based on a provided template.
    /// </summary>
    /// <param name="template">Item to be used as a template.</param>
    /// <param name="amount">Desired number of items in the stack.</param>
    /// <param name="isLockParam">True means the item acts as a lock on its inventory slot.</param>
    public InventoryItem(InventoryItem template, int amount, bool isLockParam = false) {
        this.isTemplate         = false;
        this.isLock             = isLockParam;
        this.id                 = template.id;
        this.stackMax           = template.stackMax;
        this.name               = template.name;
        this.shortName          = template.shortName;
        if (amount > this.stackMax) {
            this.stackCurrent   = this.stackMax;
        } else if (amount < 1 && !this.isLock) {
            this.stackCurrent   = 1;
        } else if (amount < 1 && this.isLock) {
            this.stackCurrent   = 0;
        } else {
            this.stackCurrent   = amount;
        }
    }

    public bool getIsTemplate() {
        return this.isTemplate;
    }

    public bool getLock() {
        return this.isLock;
    }

    public void setLock(bool isLockParam) {
        this.isLock = isLockParam;
    }
    
    public int getID() {
        return this.id;
    }
    
    public int getStackMax() {
        return this.stackMax;
    }
    
    public int getStackCurrent() {
        return this.stackCurrent;
    }
    
    public string getName() {
        return this.name;
    }
    
    public string getShortName() {
        return this.shortName;
    }

    /// <summary>
    /// Adds to the current stack of this item. Will not go above max stack size or below 1.
    /// </summary>
    /// <param name="amount">Amount to add to the current stack.</param>
    /// <returns>Current size of the item stack.</returns>
    public int addStack(int amount) {
        this.stackCurrent += amount;
        if (this.stackCurrent > this.stackMax) this.stackCurrent = this.stackMax;
        else if (this.stackCurrent < 1) this.stackCurrent = 1;
        return this.stackCurrent;
    }


    public InventoryItem copy() {
        return new InventoryItem(BackstageActor.masterList.items[this.id], this.stackCurrent);
    }
}
