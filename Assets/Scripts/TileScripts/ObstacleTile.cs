using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class ObstacleTile : Tile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/ObstacleTile")]
    public static void Create()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Obstacle", "ObstacleTile", "asset", "Save obstacletile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<ObstacleTile>(), path);
    }
#endif
}
