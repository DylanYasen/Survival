
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

[System.Serializable]
public struct RecipeItem
{
    public string ItemName;

    [HideInInspector]
    public int ItemID;

    public int RequiredAmount;
}

public class CraftRecipeIO : MonoBehaviour
{
    public static CraftRecipeIO instance;

    public string ResultItemName;
    public int ResultItemAmount;

    public RecipeItem[] ComponentItems;

    public string m_recipeData { get; private set; }


    private string RecipeRaw;
    private JSONNode RecipeData;
    public Dictionary<int, List<RecipeItem>> craftRecipes;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        LoadRecipeData();
        ProcessRecipeData();

    }

    private void LoadRecipeData()
    {
        if (File.Exists("Assets/Resources/IO/CraftingRecipes.recipes"))
        {

            var file = File.OpenText("Assets/Resources/IO/CraftingRecipes.recipes");
            RecipeRaw = file.ReadToEnd();
            Debug.Log(RecipeRaw);
            file.Close();

            RecipeData = JSONNode.LoadFromBase64(RecipeRaw);

            //var data = JSONNode.LoadFromFile("Assets/Resources/IO/CraftingRecipes.recipes");

            //RecipeData = JSONNode.LoadFromFile("Assets/Resources/IO/CraftingRecipes.recipes");

            Debug.Log(RecipeData.ToString());

        }
        else
        {
            Debug.Log(" doesn't exist.");
        }
    }

    private void ProcessRecipeData()
    {
        craftRecipes = new Dictionary<int, List<RecipeItem>>();

        string resultItemName = RecipeData["Result Item Name"];
        int resultItemID = ItemDatabase.instance.GetItemIDFromName(resultItemName);
        int resultAmount = int.Parse(RecipeData["Result Item Amount"]);

        List<RecipeItem> recipeItems = new List<RecipeItem>();

        // process components
        int ComponentAmount = RecipeData["Components"].Count;
        for (int i = 0; i < ComponentAmount; i++)
        {
            RecipeItem item;
            item.ItemName = RecipeData["Components"][i]["Item Name"];
            item.ItemID = ItemDatabase.instance.GetItemIDFromName(item.ItemName);
            item.RequiredAmount = int.Parse(RecipeData["Components"][i]["Required Amount"]);

            recipeItems.Add(item);
        }

        craftRecipes.Add(resultItemID, recipeItems);

        Debug.Log(resultItemID);
        Debug.Log(craftRecipes[resultItemID]);

        for (int i = 0; i < craftRecipes[resultItemID].Count; i++)
            Debug.Log(craftRecipes[resultItemID][i].ItemID);
    }

    public void AddRecipe()
    {
        m_recipeData = "";

        int index = 0;


        string data = "{\"Result Item Name\":\"test\",\"Result Item Amount\":\"test\",\"Components\":[,{\"Item Name\":\"value\",\"Required Amount\":\"value\"}]}";

        //string data = "{\"Recipe\":[\"Result Item Name\":\"name\",\"Result Item Amount\":\"amount\"]}";

        var ParsedData = JSONNode.Parse(data);

        ParsedData["Result Item Name"] = ResultItemName;
        ParsedData["Result Item Amount"] = ResultItemAmount.ToString();

        foreach (RecipeItem item in ComponentItems)
        {
            ParsedData["Components"][index]["Item Name"] = item.ItemName;
            ParsedData["Components"][index]["Required Amount"] = item.RequiredAmount.ToString();

            index++;
        }

        Parse(ParsedData.ToString(""));
        Debug.Log(m_recipeData);

        SaveMapData(ParsedData);
    }

    void Parse(string text)
    {
        m_recipeData += text + "\n";
    }


    void SaveMapData(JSONNode data)
    {
#if UNITY_EDITOR

        /*
        if (File.Exists("Assets/Resources/IO/" + "CraftingRecipes.recipes"))
        {
            Debug.Log(LevelName + " already exist.");
            return;
        }
        */

        // encode later
        var file = File.CreateText("Assets/Resources/IO/CraftingRecipes.recipes");


        //var file = File("Assets/Resources/IO/" + "CraftingRecipes.recipes",FileMode.Append);

        //file.Write(data.ToString());

        file.Write(data.SaveToBase64());

        //var f = File.CreateText("Assets/Resources/IO/CraftingRecipes.recipes");

        //data.SaveToFile("Assets/Resources/IO/CraftingRecipes.recipes");

        //file.Write(data.SaveToBase64());
        file.Close();

#endif
    }
}

