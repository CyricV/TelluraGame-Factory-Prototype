using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeKiln {
    /// <summary>
    /// Internal name of the recipe. For kilns this will be the name of the input item.
    /// </summary>
    public string           name { get { return _name; } }
    private string          _name;
    /// <summary>
    /// Item required for the recipe.
    /// </summary>
    public InventoryItem    input { get { return _input; } }
    private InventoryItem   _input;
    /// <summary>
    /// Item produced by the recipe.
    /// </summary>
    public InventoryItem    output { get { return _output; } }
    private InventoryItem   _output;
    /// <summary>
    /// Item produced by the recipe.
    /// </summary>
    public int              solCost { get { return _solCost; } }
    private int             _solCost;

    public RecipeKiln(InventoryItem input, InventoryItem output, int solCost) {
        _name =     input.name;
        _input =    input;
        _output =   output;
        _solCost =  solCost;
    }
}
