using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    /// <summary>
    /// Number of slots in this inventory.
    /// </summary>
    private int size {
        get; set;
    }

    // Somehow specifies the types of items this inventory can accept
    // Filter?
    
    /// <summary>
    /// InventoryContents object of this Inventory.
    /// </summary>
    private InventoryContents contents;

    /// <summary>
    /// Number of current empty slots.
    /// </summary>
    private int emptySlots {
        get {
            return this.size-this.occupiedSlots;
        }
    }

    /// <summary>
    /// Number of non maxed stackable inventory slots.
    /// </summary>
    private int occupiedSlots;
    
    /// <summary>
    /// Number of maxed stack slots, or non stackable slots.
    /// </summary>
    private int fullSlots;
    
    /// <summary>
    /// Constructor with size.
    /// </summary>
    /// <param name="sizeParam">Number of slots the inventory will have.</param>
    public Inventory(int sizeParam = 1) {
        this.size       = sizeParam;
        //Filter
        contents        = new InventoryContents(sizeParam);
        occupiedSlots   = 0;
        fullSlots       = 0;
    }

    /// <summary>
    /// Checks if all slots have max stacks.
    /// </summary>
    /// <returns>True if the inventory is full. False if not.</returns>
    public bool isFull() {
        if(this.fullSlots == this.size) {
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// Determines how many of a specified item is in the inventory.
    /// </summary>
    /// <param name="id">Item id to look for.</param>
    /// <returns>The number of specified item present in the inventory.</returns>
    public int containsItem(int id) {
        int totalItems = 0;
        if (occupiedSlots > 0) {
            for (int i = 0; i < occupiedSlots; i++) {
                InventoryItem currentItem = contents.contentsArray[i];
                if (currentItem.getID() == id) {
                    totalItems += currentItem.getStackCurrent();
                }
            }
        }
        return totalItems;
    }

    /// <summary>
    /// Attempts to add an amount of an item specified by id.
    /// </summary>
    /// <param name="id">Item id to be added.</param>
    /// <param name="amount">Number of items to attempt to add.</param>
    /// <returns>The amount of items that were not able to be added.</returns> 
    public int addItem(int id, int amount) {
        // Invalid id check
        if (BackstageActor.masterList.items[id] == null) return -1;
        // Template of item to add.
        InventoryItem itemTemplate      = BackstageActor.masterList.items[id];
        // Array of vacant indexes to be built during first loop.
        int[] vacantSlotArray           = new int[this.size];
        // Track vacantSlotArray's current index
        int vacantSlotArrayIndex        = 0;

        // Loop through and fill existing item stacks first.
        // Record vacant slots.
        for (int i = 0; i < this.size; i++) {
            // Item in the slot presently being examined
            InventoryItem currentItem   = this.contents.contentsArray[i];

            // Current slot is empty, add it to vacantSlotArray.
            if(currentItem == null) {
                vacantSlotArray[vacantSlotArrayIndex] = i;
                vacantSlotArrayIndex++;
            // IDs match and there is some room in the stack
            } else if(currentItem.getID() == itemTemplate.getID() &&
                      currentItem.getStackMax() - currentItem.getStackCurrent() > 0) {
                // Sufficient space in this stack, Done!
                if (currentItem.getStackMax()-currentItem.getStackCurrent() >= amount) {
                    currentItem.addStack(amount);
                    return 0;
                // Room in the stack, but not enough. Fill it up!
                } else {
                    int availableSpace = currentItem.getStackMax()-currentItem.getStackCurrent();
                    currentItem.addStack(availableSpace);
                    amount -= availableSpace;
                }
            }
        } // End first loop.

        // Loop through vacant spots and deposite remaining amount of items.
        for (int i = 0; i < vacantSlotArrayIndex; i++) {
            // All remaining items can fit in this vacant slot.
            if (amount <= itemTemplate.getStackMax()) {
                this.contents.contentsArray[vacantSlotArray[i]] = new InventoryItem(itemTemplate, amount);
                return 0;
            // We have more than can fit in this slot, add a max stack and move on.
            } else {
                this.contents.contentsArray[vacantSlotArray[i]] = new InventoryItem(itemTemplate, itemTemplate.getStackMax());
                amount -= itemTemplate.getStackMax();
            }
        }// End second loop.
        // If we reach this point we have items that could not fit in the inventory.
        return amount;
    }

    /// <summary>
    /// Attempts to add a specific item.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    /// <returns>The amount of items that were not able to be added.</returns>
    public int addItem(InventoryItem item) {
        // BEHAVE DIFFERENT IF ITEM HAS "DATA" OF SOME KIND!!!!
        return addItem(item.getID(), item.getStackCurrent());
    }

    /// <summary>
    /// Attempts to add an Item to a specified inventory slot.
    /// </summary>
    /// <param name="item">Item to be added.</param>
    /// <param name="index">The index at which to add the item.</param>
    /// <returns>An item with any amount not able to be added in its stack.</returns>
    public InventoryItem addItemAtIndex(InventoryItem item, int index) {
        InventoryItem targetItemSlot = this.contents.contentsArray[index];

        // If the target slot is empty
        if(targetItemSlot == null) {
            this.contents.contentsArray[index] = item;
            return null;
        // Check if target slot contains a different item.
        } else if(targetItemSlot.getID() != item.getID()) {
            return item;
        // Check if target slot is full.
        } else if(targetItemSlot.getStackCurrent() == targetItemSlot.getStackMax()) {
            return item;
        // The target slot has enough room in its stack to accomodate the item.
        } else if (item.getStackCurrent() <= targetItemSlot.getStackMax() - targetItemSlot.getStackCurrent()) {
            targetItemSlot.addStack(item.getStackCurrent());
            return null;
        // The target slot has some space in the stack but not enough.
        } else {
            int availableSpace = targetItemSlot.getStackMax() - targetItemSlot.getStackCurrent();
            item.addStack(-availableSpace);
            targetItemSlot.addStack(availableSpace);
            return item;
        }
    }

    /// <summary>
    /// BROKEN Attempts to remove an amount of an item specified by id.
    /// </summary>
    /// <param name="id">INT Item id to be removed.</param>
    /// <param name="amount">INT Number of items to attempt to remove.</param>
    /// <returns>INT The amount of items that were actually removed.</returns> 
    public int removeItem(int id, int amount) {

        return 0;
    }

    /// <summary>
    /// Remove the item stack or an amount from the item stack of a specified inventory slot.
    /// </summary>
    /// <param name="index">The index at which to remove the item.</param>
    /// <param name="amount">OPTIONAL Ammount to attempt to remove.</param>
    /// <returns>The removed Item. Durrent stack size may not match requested amount if not enough was available in the targeted slot</returns>
    public InventoryItem removeItemAtIndex(int index, int amount = Int32.MaxValue) {
        InventoryItem returnItem = null;

        // Nothing in the target slot.
        if (this.contents.contentsArray[index] == null) {
            return null;
        // More than enough in the target slot to fullfill the removed amount.
        } else if (this.contents.contentsArray[index].getStackCurrent() > amount) {
            returnItem = new InventoryItem(this.contents.contentsArray[index], amount);
            this.contents.contentsArray[index].addStack(-amount);
        // Exactly enough or not enough in the target slot to fullfill the removed amount.
        } else {
            returnItem = this.contents.contentsArray[index].copy();
            if (this.contents.contentsArray[index].getLock()) {
                this.contents.contentsArray[index].addStack(this.contents.contentsArray[index].getStackCurrent());
            } else {
                this.contents.contentsArray[index] = null;
            }
        }
        return returnItem;
    }

    /// <summary>
    /// Splits the specified number of slots from the end of this inventory into a separate inventory.
    /// </summary>
    /// <param name="truncateBy">The number of slots to snip off this end of the inventory.</param>
    /// <returns>A new inventory that contains the slots and items removed from this inventory.</returns>
    public Inventory truncateInventory(int truncateBy) {
        Inventory returnInventory       = new Inventory(truncateBy);
        InventoryContents newContents   = new InventoryContents(this.size - truncateBy);

        // Create the return inventory
        for (int i = this.size - truncateBy; i < this.size; i++) {
            returnInventory.contents.contentsArray[i - (this.size - truncateBy)] = this.contents.contentsArray[i];
        }
        // Create the new contents array for this inventory
        for (int i = 0; i < this.size - truncateBy; i++) {
            newContents.contentsArray[i] = this.contents.contentsArray[i];
        }
        this.contents = newContents;
        this.size -= truncateBy;
        return returnInventory;
    }

    /// <summary>
    /// Expands this inventory by the size of, and adds all the items of another inventory.
    /// </summary>
    /// <param name="consumedInventory">The inventory that will be added onto this one.</param>
    public void mergeInventory(Inventory consumedInventory) {
        int newSize = this.size + consumedInventory.size;
        InventoryContents newContents = new InventoryContents(newSize);
        for (int i = 0; i < this.size; i++) {
            newContents.contentsArray[i] = this.contents.contentsArray[i];
        }
        for (int i = 0; i < consumedInventory.size; i++) {
            newContents.contentsArray[i+this.size] = consumedInventory.contents.contentsArray[i];
        }
        this.contents = newContents;
    }

    /// <summary>
    /// Adds a number of empty slots to the end of this inventory.
    /// </summary>
    /// <param name="addSize">Number of slots to add.</param>
    public void expandInventory(int addSize) {
        InventoryContents newContents = new InventoryContents(this.size + addSize);
        for (int i = 0; i < this.size; i++) {
            newContents.contentsArray[i] = this.contents.contentsArray[i];
        }
        this.size += addSize;
    }

    public int destroyInventory() {
        return 0;
    }

    public string DEBUGReportInventory() {
        string outString = "Inventory Report: \n";
        for (int i = 0; i<this.size; i++) {
            outString += ("\t" + i.ToString() + "\t");
            if(this.contents.contentsArray[i] == null) outString += ("Empty\n");
            else outString += (this.contents.contentsArray[i].getStackCurrent() + " " + this.contents.contentsArray[i].getName() + "\n");
        }
        return outString;
    }
}
