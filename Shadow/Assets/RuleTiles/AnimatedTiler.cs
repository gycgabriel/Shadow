using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

#if UNITY_EDITOR

public class AnimatedTiler : EditorWindow
{
    /*
     * Modified from HenryStrattonFW 
     * https://forum.unity.com/threads/duplicate-or-copy-tiles-and-palettes.529256/#post-6333540
     */



    private RuleTile referenceTile;
    private Texture2D sprite;
    private int unitRows;
    private int unitCols;
    private int repeats;
    private int minSpeed;
    private int maxSpeed;
    private int missingTile;
    

    [MenuItem("Tiles/Animated RuleTiler")]
    public static void ShowTool()
    {
        GetWindow<AnimatedTiler>().Show();
    }
    
    public void OnGUI()
    {
        referenceTile = EditorGUILayout.ObjectField(referenceTile, typeof(RuleTile), false) as RuleTile;
        sprite = EditorGUILayout.ObjectField(sprite, typeof(Texture2D), false) as Texture2D;
        unitRows = EditorGUILayout.IntField("Unit rows: ", unitRows);
        unitCols = EditorGUILayout.IntField("Unit cols: ", unitCols);
        repeats = EditorGUILayout.IntField("Repeats: ", repeats);
        minSpeed = EditorGUILayout.IntField("Min Speed: ", minSpeed);
        maxSpeed = EditorGUILayout.IntField("Max Speed: ", maxSpeed);
        missingTile = EditorGUILayout.IntField("Missing Tile: ", missingTile);



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
        }
    }

    private void CloneBySpriteIndex(string spritePath, RuleTile newTile, RuleTile refTile)
    {
        // First load in the data for the reference tile.
        Texture2D refTex = newTile.m_TilingRules[0].m_Sprites[0].texture;
        string refPath = AssetDatabase.GetAssetPath(refTex);
        Sprite[] refSprites = AssetDatabase.LoadAllAssetsAtPath(refPath).OfType<Sprite>().ToArray();

        // New rule tile created, now to swap out the sprites.
        Sprite[] newSprites = AssetDatabase.LoadAllAssetsAtPath(spritePath).OfType<Sprite>().ToArray();

        int currRow = 0;
        int missingRow = unitRows - missingTile / unitCols - 1;     // count from 0
        for (int i = 0; i < newTile.m_TilingRules.Count; i++)
        {
            RuleTile.TilingRule rule = newTile.m_TilingRules[i];
            //int refIndex = FindIndex(refSprites, rule.m_Sprites[0]);    // get reference from nonanimated ruletile
            int refIndex = i + 1;

            if ((refIndex % unitCols) == 0)
            {
                currRow++;
            }

            refIndex += (repeats - 1) * (unitCols) * currRow;

            rule.m_Sprites = new Sprite[repeats];
            rule.m_Output = RuleTile.TilingRule.OutputSprite.Animation;
            rule.m_MinAnimationSpeed = minSpeed;
            rule.m_MaxAnimationSpeed = maxSpeed;


            

            for (int j = 0; j < repeats; j++)           // unit 1, unit 2, unit 3 ... unit j
            {
                int newIndex = refIndex + j * unitCols;
                if (currRow >= missingRow)
                {
                    newIndex = refIndex + j * (unitCols - missingTile);
                }

                rule.m_Sprites[j] = newSprites[newIndex];
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
            if (spriteArray[i] == sprite)
                return i;
        }

        return -1;
    }
}

#endif