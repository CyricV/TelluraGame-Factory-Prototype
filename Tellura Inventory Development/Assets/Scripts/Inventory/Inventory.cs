using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
    /// <summary>
    /// Number of slots in this inventory.
    /// </summary>
    private int _size;
    public int size { get { return _size; } }

    private InventoryContents contents;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sizeParam">Number of slots the inventory will have.</param>
    public Inventory(int size = 1) {
        _size       = size;
        contents    = new InventoryContents(size);
    }
    
    /// <summary>
    /// Determines how many of a specified item is in the inventory.
    /// </summary>
    /// <param name="name">Item name to look for.</param>
    /// <returns>The number of specified item present in the inventory.</returns>
    public int ContainsItem(string name) {
        int totalItems = 0;
        for (int i = 0; i < size; i++) {
            InventoryItem currentItem = contents.contentsArray[i];
            if (currentItem.name == name) {
                totalItems += currentItem.stackCurrent;
            }
        }
        return totalItems;
    }

    /// <summary>
    /// Attempts to add an amount of an item specified by name.
    /// </summary>
    /// <param name="name">Name of the item to be added.</param>
    /// <param name="amount">Number of items to attempt to add.</param>
    /// <returns>The amount of items that were not able to be added.</returns> 
    public int addItem(string name, int amount) {
        InventoryItem itemTemplate;
        if (!BackstageActor.masterList.items.TryGetValue(name, out itemTemplate)) return -1;
        int[] vacantSlotArray           = new int[_size];
        int vacantSlotArrayIndex        = 0;

        // Loop through and fill existing item stacks first.
        // Record vacant slots.
        for (int i = 0; i < this._size; i++) {
            // Item in the slot presently being examined
            InventoryItem currentItem   = this.contents.contentsArray[i];

            // Current slot is empty, add it to vacantSlotArray.
            if(currentItem == null) {
                vacantSlotArray[vacantSlotArrayIndex] = i;
                vacantSlotArrayIndex++;
            // IDs match and there is some room in the stack
            } else if(currentItem.id == itemTemplate.id &&
                      currentItem.stackMax - currentItem.stackCurrent > 0) {
                // Sufficient space in this stack, Done!
                if (currentItem.stackMax-currentItem.stackCurrent >= amount) {
                    currentItem.addStack(amount);
                    return 0;
                // Room in the stack, but not enough. Fill it up!
                } else {
                    int availableSpace = currentItem.stackMax-currentItem.stackCurrent;
                    currentItem.addStack(availableSpace);
                    amount -= availableSpace;
                }
            }
        } // End first loop.

        // Loop through vacant spots and deposite remaining amount of items.
        for (int i = 0; i < vacantSlotArrayIndex; i++) {
            // All remaining items can fit in this vacant slot.
            if (amount <= itemTemplate.stackMax) {
                this.contents.contentsArray[vacantSlotArray[i]] = new InventoryItem(itemTemplate, amount);
                return 0;
            // We have more than can fit in this slot, add a max stack and move on.
            } else {
                this.contents.contentsArray[vacantSlotArray[i]] = new InventoryItem(itemTemplate, itemTemplate.stackMax);
                amount -= itemTemplate.stackMax;
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
        return addItem(item.name, item.stackCurrent);
    }

    /// <summary>
    /// Attempts to add an Item to a specified inventory slot.
    /// </summary>
    /// <param name="item">Item to be added.</param>
    /// <param name="index">The index at which to add the item.</param>
    /// <returns>An item with any amount not able to be added in its stack.</returns>
    public InventoryItem AddItemAtIndex(InventoryItem item, int index) {
        InventoryItem targetItemSlot = this.contents.contentsArray[index];

        // If the target slot is empty
        if(targetItemSlot == null) {
            this.contents.contentsArray[index] = item;
            return null;
        // Check if target slot contains a different item.
        } else if(targetItemSlot.id != item.id) {
            return item;
        // Check if target slot is full.
        } else if(targetItemSlot.stackCurrent == targetItemSlot.stackMax) {
            return item;
        // The target slot has enough room in its stack to accomodate the item.
        } else if (item.stackCurrent <= targetItemSlot.stackMax - targetItemSlot.stackCurrent) {
            targetItemSlot.addStack(item.stackCurrent);
            return null;
        // The target slot has some space in the stack but not enough.
        } else {
            int availableSpace = targetItemSlot.stackMax - targetItemSlot.stackCurrent;
            item.addStack(-availableSpace);
            targetItemSlot.addStack(availableSpace);
            return item;
        }
    }

    public InventoryItem GetItemAtIndex(int index) {
        if (index > size-1 || index < 0) return null;
        return contents.contentsArray[index];
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
    public InventoryItem TakeItemAtIndex(int index, int amount = int.MaxValue) {
        InventoryItem returnItem = null;

        // Nothing in the target slot.
        if (this.contents.contentsArray[index] == null) {
            return null;
        // More than enough in the target slot to fullfill the removed amount.
        } else if (this.contents.contentsArray[index].stackCurrent > amount) {
            returnItem = new InventoryItem(this.contents.contentsArray[index], amount);
            this.contents.contentsArray[index].addStack(-amount);
        // Exactly enough or not enough in the target slot to fullfill the removed amount.
        } else {
            returnItem = this.contents.contentsArray[index].copy();
            if (this.contents.contentsArray[index].isLock) {
                this.contents.contentsArray[index].addStack(this.contents.contentsArray[index].stackCurrent);
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
        InventoryContents newContents   = new InventoryContents(this._size - truncateBy);

        // Create the return inventory
        for (int i = this._size - truncateBy; i < this._size; i++) {
            returnInventory.contents.contentsArray[i - (this._size - truncateBy)] = this.contents.contentsArray[i];
        }
        // Create the new contents array for this inventory
        for (int i = 0; i < this._size - truncateBy; i++) {
            newContents.contentsArray[i] = this.contents.contentsArray[i];
        }
        this.contents = newContents;
        this._size -= truncateBy;
        return returnInventory;
    }

    /// <summary>
    /// Expands this inventory by the size of, and adds all the items of another inventory.
    /// </summary>
    /// <param name="consumedInventory">The inventory that will be added onto this one.</param>
    public void mergeInventory(Inventory consumedInventory) {
        int newSize = this._size + consumedInventory._size;
        InventoryContents newContents = new InventoryContents(newSize);
        for (int i = 0; i < this._size; i++) {
            newContents.contentsArray[i] = this.contents.contentsArray[i];
        }
        for (int i = 0; i < consumedInventory._size; i++) {
            newContents.contentsArray[i+this._size] = consumedInventory.contents.contentsArray[i];
        }
        this.contents = newContents;
    }

    /// <summary>
    /// Adds a number of empty slots to the end of this inventory.
    /// </summary>
    /// <param name="addSize">Number of slots to add.</param>
    public void expandInventory(int addSize) {
        InventoryContents newContents = new InventoryContents(this._size + addSize);
        for (int i = 0; i < this._size; i++) {
            newContents.contentsArray[i] = this.contents.contentsArray[i];
        }
        this._size += addSize;
    }

    /// <summary>
    /// BROKEN.
    /// </summary>
    /// <returns></returns>
    public int destroyInventory() {
        return 0;
    }

    public string DEBUGReportInventory() {
        string outString = "Inventory Report: \n";
        for (int i = 0; i<this._size; i++) {
            outString += ("\t" + i.ToString() + "\t");
            if(this.contents.contentsArray[i] == null) outString += ("Empty\n");
            else outString += (this.contents.contentsArray[i].stackCurrent + " " + this.contents.contentsArray[i].displayName + "\n");
        }
        return outString;
    }
}
