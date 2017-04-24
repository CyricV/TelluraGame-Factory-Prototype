using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;
using SimpleJSON;
using System.IO;

public class DerivedMasterList {

    public Dictionary<string, InventoryItem>    items { get { return _items; } }
    private Dictionary<string, InventoryItem>   _items;
    public Dictionary<string, RecipeKiln>       kilnRecipes { get { return _kilnRecipes; } }
    private Dictionary<string, RecipeKiln>      _kilnRecipes;

    public DerivedMasterList() {
        GenerateItems();
        GenerateKilnRecipes();
    }

    private void GenerateItems() {
        string rawText = File.ReadAllText(Application.streamingAssetsPath+"/JSON/Items.JSON");
        JSONNode json = JSON.Parse(rawText);
        _items = new Dictionary<string, InventoryItem>();
        for (int i = 0; i < json[Generic.ITEMS].Count; i++) {
            InventoryItem item = new InventoryItem(
                i,
                json[Generic.ITEMS][i][Generic.NAME],
                json[Generic.ITEMS][i][Generic.DISPLAY_NAME],
                json[Generic.ITEMS][i][Generic.STACK_MAX]);
            _items.Add(item.name, item);
        }
    }

    private void GenerateKilnRecipes() {
        string rawText = File.ReadAllText(Application.streamingAssetsPath+"/JSON/RecipesKiln.JSON");
        JSONNode json = JSON.Parse(rawText);
        _kilnRecipes = new Dictionary<string, RecipeKiln>();
        for (int i = 0; i < json[Generic.KILN].Count; i++) {
            InventoryItem input;
            InventoryItem output;
            if (_items.TryGetValue(json[Generic.KILN][i][Generic.INPUT], out input) &&
                _items.TryGetValue(json[Generic.KILN][i][Generic.OUTPUT], out output)) {
                RecipeKiln recipe = new RecipeKiln(input, output, json[Generic.KILN][i][Generic.LEY_SOL]);
                _kilnRecipes.Add(recipe.name, recipe);
                input.tags.Add(Generic.TAG_KILN);
            } else Debug.Log(json[Generic.KILN][i][Generic.INPUT]+" or "+json[Generic.KILN][i][Generic.OUTPUT]+" does not exist.");
        }
    }

    public string DEBUGPrintItemMasterList() {
        string itemMasterList = "Item Master List:\n";
        foreach (KeyValuePair<string, InventoryItem> pair in _items) {
            //itemMasterList += string.Format("\t"+pair.Value.id+" "+pair.Value.name+"\t"+pair.Value.displayName+"\t");
            string tagList = "";
            foreach (string tag in pair.Value.tags) tagList += (tag+" ");
            itemMasterList += string.Format("{0,-5} {1,-16} {2,-16} {3,-64}\n", pair.Value.id, pair.Value.name, pair.Value.displayName, tagList);
        }
        return(string.Format(itemMasterList));
    }
}