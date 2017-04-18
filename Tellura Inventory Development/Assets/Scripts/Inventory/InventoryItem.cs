using System.Collections;
using System.Collections.Generic;

public class InventoryItem {
    /// <summary>
    /// True means the item is a template for creating other items.
    /// </summary>
    public bool     isTemplate { get { return _isTemplate; } }
    private bool    _isTemplate;
    /// <summary>
    /// True means the item acts as a lock on an inventory slot.
    /// </summary>
    public bool     isLock { get { return _isLock; } }
    private bool    _isLock;
    /// <summary>
    /// Item ID.
    /// </summary>
    public int      id { get { return _id; } }
    private int     _id;
    /// <summary>
    /// Max amount of this item in one inventory slot.
    /// </summary>
    public int      stackMax { get { return _stackMax; } }
    private int     _stackMax;
    /// <summary>
    /// Current amount of this item present in the slot.
    /// </summary>
    public int      stackCurrent { get { return _stackCurrent; } }
    private int     _stackCurrent;
    /// <summary>
    /// Display name of the item.
    /// </summary>
    public string   displayName { get { return _displayName; } }
    private string  _displayName;
    /// <summary>
    /// Internal name of the item.
    /// </summary>
    public string   name { get { return _name; } }
    private string  _name;
    /// <summary>
    /// List of tags used in item requests.
    /// </summary>
    public List<string> tags { get { return _tags; } }
    private List<string> _tags;

    //public          InventoryItemData data;

    /// <summary>
    /// Template Constructor. Allows creation of all attributes via parameter.
    /// </summary>
    public InventoryItem(int id, string name, string displayName, int stackMax) {
        _isTemplate         = true;
        _isLock             = false;
        _id                 = id;
        _name               = name;
        _displayName        = displayName;
        _stackMax           = stackMax;
        _stackCurrent       = 1;
        _tags               = new List<string>();
    }
    
    /// <summary>
    /// Instance Constructor. Makes an item based on a provided template.
    /// </summary>
    /// <param name="template">Item to be used as a template.</param>
    /// <param name="amount">Desired number of items in the stack. Will not exceed stackMax.</param>
    /// <param name="isLockParam">True means the item acts as a lock on its inventory slot.</param>
    public InventoryItem(InventoryItem template, int amount, bool isLock = false) {
        _isTemplate     = false;
        _isLock         = isLock;
        _id             = template.id;
        _name           = template.name;
        _displayName    = template.displayName;
        _stackMax       = template.stackMax;
        _tags           = template.tags;
        if (amount > stackMax) {
            _stackCurrent   = stackMax;
        } else if (amount < 1 && !this.isLock) {
            _stackCurrent   = 1;
        } else if (amount < 1 && this.isLock) {
            _stackCurrent   = 0;
        } else {
            _stackCurrent   = amount;
        }
    }

    /// <summary>
    /// Adds to the current stack of this item. Will not go above max stack size or below 1.
    /// </summary>
    /// <param name="amount">Amount to add to the current stack. Negative values subtract.</param>
    /// <returns>Current size of the item stack.</returns>
    public int addStack(int amount) {
        _stackCurrent += amount;
        if (stackCurrent > stackMax) _stackCurrent = stackMax;
        else if (this.stackCurrent < 1) _stackCurrent = 1;
        return this.stackCurrent;
    }

    public InventoryItem copy() {
        InventoryItem item;
        if (!BackstageActor.masterList.items.TryGetValue(name, out item)) return null;
        return new InventoryItem(item, this.stackCurrent);
    }
}
