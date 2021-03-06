/**
  * Modified from HenryStrattonFW 
  * https://forum.unity.com/threads/duplicate-or-copy-tiles-and-palettes.529256/#post-6333540
  */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

#if UNITY_EDITOR
public class RuleTileCloner : EditorWindow
{
    private RuleTile referenceTile;
    private Texture2D sprite;
 
    [MenuItem("Tools/RuleTile Cloner")]
    public static void ShowTool()
    {
        GetWindow<RuleTileCloner>().Show();
    }
 
    public void OnGUI()
    {
        referenceTile = EditorGUILayout.ObjectField(referenceTile, typeof(RuleTile), false) as RuleTile;
        sprite = EditorGUILayout.ObjectField(sprite, typeof(Texture2D), false) as Texture2D;
 
        if (GUILayout.Button("Clone"))
        {
            string origPath = AssetDatabase.GetAssetPath(referenceTile);
            string spritePath = AssetDatabase.GetAssetPath(sprite);
            string targetPath = $"{origPath.Substring(0, origPath.LastIndexOf('/'))}/{sprite.name}.asset";
 
            AssetDatabase.CopyAsset(origPath, targetPath);
            RuleTile oldTile = AssetDatabase.LoadAssetAtPath<RuleTile>(origPath);
            RuleTile newTile = AssetDatabase.LoadAssetAtPath<RuleTile>(targetPath);
 
            if (newTile != null)
            {
                CloneBySpriteIndex(spritePath, newTile, oldTile);
            }
 
            AssetDatabase.SaveAssets();

            Debug.Log("Saved to " + targetPath);
        }
    }
 
    private void CloneBySpriteIndex(string spritePath, RuleTile newTile, RuleTile refTile)
    {
        // First load in the data for the reference tile.
        Texture2D refTex = newTile.m_TilingRules[0].m_Sprites[0].texture;
        string refPath = AssetDatabase.GetAssetPath(refTex);

        /**
         * LoadAllAssetsAtPath has arbituary order. LoadAll Asset Representations loads subassets only which is what I want
         */
        
        Sprite[] refSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(refPath).OfType<Sprite>().ToArray();
 
        // New rule tile created, now to swap out the sprites.
        Sprite[] newSprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(spritePath).OfType<Sprite>().ToArray();
        for (int i = 0; i < newTile.m_TilingRules.Count; i++)
        {
            RuleTile.TilingRule rule = newTile.m_TilingRules[i];
 
            for (int j = 0; j < rule.m_Sprites.Length; j++)
            {
                int refIndex = FindIndex(refSprites, rule.m_Sprites[j]);
                rule.m_Sprites[j] = newSprites[refIndex];
            }
        }
 
        if (refTile.m_DefaultSprite != null)
        {
            newTile.m_DefaultSprite = newSprites[0];
        }
    }
 
    private int FindIndex(Sprite[] spriteArray, Sprite sprite)
    {
        for (int i = 0; i < spriteArray.Length; i++)
        {
            //Debug.Log("--------------No. " + i + "--------------------");
            //Debug.Log(spriteArray[i]);
            //Debug.Log(sprite);
            //Debug.Log("-----------------------------------------------");
            if (spriteArray[i] == sprite)
                return i;
        }
 
        return -1;
    }
}
#endif