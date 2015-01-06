using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System.IO;

public class Test : MonoBehaviour
{

    void Start()
    {
        TextAsset text = Resources.Load("IO/CraftingRecipes") as TextAsset;

        //JSONNode RecipeData = JSONNode.LoadFromBase64(text.ToString());

        JSONNode RecipeData = JSONNode.Parse(text.text);
        //text.ToString();

        //JSONNode RecipeData = JSONNode.LoadFromFile("Resources/IO/CraftingRecipes.json");

        //Debug.Log(text.text);

        Debug.Log(RecipeData);

        Debug.Log(RecipeData[0]);
        Debug.Log(RecipeData[0][0]);

        Debug.Log(RecipeData["Recipes"]);
    }

    void Update()
    {

    }
}
