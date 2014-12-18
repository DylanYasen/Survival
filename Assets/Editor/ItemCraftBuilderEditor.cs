using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CraftRecipeIO))]
public class ItemCraftBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CraftRecipeIO recipeBuilder = (CraftRecipeIO)target;

        if (GUILayout.Button("Add Recipe"))
        {
            recipeBuilder.AddRecipe();
        }
    }

}
