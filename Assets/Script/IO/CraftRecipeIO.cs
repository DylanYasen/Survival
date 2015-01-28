
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

public struct RecipeData
{
    public int resultItemAmount;
    public List<int> componentsID;
}

public class CraftRecipeIO : MonoBehaviour
{
    private const string recipeFilePath = "Assets/Resources/IO/CraftingRecipes.json";

    public static CraftRecipeIO instance;

    public string ResultItemName;
    public int ResultItemAmount;

    public RecipeItem[] ComponentItems;

    public string m_recipeData { get; private set; }

    private string RecipeRaw;
    private JSONNode RecipeData;

    public Dictionary<int, RecipeData> craftRecipes = new Dictionary<int, RecipeData>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        RecipeData = LoadRecipeData();
        ProcessRecipeData(RecipeData);

    }

    JSONNode LoadRecipeData()
    {
        //#if UNITY_WEBPLAYER

        TextAsset text = Resources.Load("IO/CraftingRecipes") as TextAsset;

        //Debug.Log(text);
        //Debug.Log(text.ToString());
        //Debug.Log(text.ToString());

        //return JSONNode.LoadFromBase64(text.ToString());

        return JSONNode.Parse(text.ToString());

        //RecipeData = text.text;


        /*
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
        */
    }

    private void ProcessRecipeData(JSONNode data)
    {

        // recipe data
        for (int i = 0; i < data[0].Count; i++)
        {
            string resultItemName = data[0][i]["Result Item Name"];
            int resultItemID = ItemDatabase.instance.GetItemIDFromName(resultItemName);
            int resultAmount = int.Parse(data[0][i]["Result Item Amount"]);

            int ComponentAmount = data[0][i]["Components"].Count;

            RecipeData recipeData = new RecipeData();
            recipeData.componentsID = new List<int>();

            recipeData.resultItemAmount = resultAmount;


            for (int j = 0; j < ComponentAmount; j++)
            {
                string componentItemName = data[0][i]["Components"][j]["Item Name"];
                int componentItemID = ItemDatabase.instance.GetItemIDFromName(componentItemName);
                int requiredAmount = int.Parse(data[0][i]["Components"][j]["Required Amount"]);

                for (int z = 0; z < requiredAmount; z++)
                    recipeData.componentsID.Add(componentItemID);
            }

            Debug.Log(resultItemName);

            // store the recipe data
            craftRecipes.Add(resultItemID, recipeData);
        }
     
        foreach (KeyValuePair<int, RecipeData> entry in craftRecipes)
        {
            Debug.Log("result item ID: " + entry.Key);

            RecipeData value = entry.Value;

            Debug.Log("result item amount: " + value.resultItemAmount);

            foreach (int i in value.componentsID)
                Debug.Log("component item id: " + i);

            Debug.Log("*************");
        }
    }

        //string resultItemName = RecipeData["Result Item Name"];
        //int resultItemID = ItemDatabase.instance.GetItemIDFromName(resultItemName);
        //int resultAmount = int.Parse(RecipeData["Result Item Amount"]);
        /*
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
         
        */

   

    public void AddRecipe()
    {
#if !UNITY_WEBPLAYER
        m_recipeData = "";

        int index = 0;


        if (!File.Exists(recipeFilePath))
            InitRecipeFile();

        //string data = "{\"Recipes\":[{\"Result Item Name\":\"test\",\"Result Item Amount\":\"test\",\"Components\":[,{\"Item Name\":\"value\",\"Required Amount\":\"value\"}]}]}";
        string data = "{\"Result Item Name\":\"test\",\"Result Item Amount\":\"test\",\"Components\":[,{\"Item Name\":\"value\",\"Required Amount\":\"value\"}]}";

        //string data = "{\"Recipe\":[\"Result Item Name\":\"name\",\"Result Item Amount\":\"amount\"]}";

        var ParsedData = JSONNode.Parse(data + "\n");

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

        //LoadRecipeData();

        string dataText = File.ReadAllText(recipeFilePath);


        var newData = JSONNode.Parse(dataText);

        Debug.Log(newData.ToString());

        //newData.Add("Recipes", ParsedData);
        newData["Recipes"].Add(ParsedData);

        Debug.Log(newData.ToString());

        //SaveMapData(ParsedData);
        SaveMapData(newData);

#endif

    }

    void InitRecipeFile()
    {
        //string data = "{\"Recipes\":[]}";
        //        string data = "{\"Recipes\"}";
        //string data = "\"Recipes\"";

        string data = "{\"Recipes\"}";

        var ParsedData = JSONNode.Parse(data);

        SaveMapData(ParsedData);
    }

    void Parse(string text)
    {
        m_recipeData += text + "\n";
    }

    void SaveMapData(JSONNode data)
    {
#if UNITY_EDITOR

        // encode later
        var file = File.CreateText(recipeFilePath);
        file.Write(data.ToString());
        file.Close();

#endif
        //string filePath = "Assets/Resources/IO/CraftingRecipes.json";
        //StreamWriter file;
        //File.AppendAllText(filePath, data.SaveToBase64());
        //File.AppendAllText(filePath, data.ToString());
        //file.Write(data.SaveToBase64());
        /*
        if (File.Exists("Assets/Resources/IO/" + "CraftingRecipes.recipes"))
        {
            Debug.Log(LevelName + " already exist.");
            return;
        }
        */
        //if (File.Exists(filePath))
        //{
        //}
        //Debug.Log("file exists, create new file");
        //var file = File("Assets/Resources/IO/" + "CraftingRecipes.recipes",FileMode.Append);
        //var f = File.CreateText("Assets/Resources/IO/CraftingRecipes.recipes");
        //data.SaveToFile("Assets/Resources/IO/CraftingRecipes.recipes");
        //file.Write(data.SaveToBase64());


    }
}

