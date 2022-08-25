using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ClearNullScript : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("CleanUp/Remove Missing Scripts Recursively Visit Prefabs")]
    private static void FindAndRemoveMissingInSelected()
    {
        var deepSelection = EditorUtility.CollectDeepHierarchy(Selection.gameObjects);
        int compCount = 0;
        int goCount = 0;
        foreach (var o in deepSelection)
        {
            if (o is GameObject go)
            {
                int count = GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(go);
                if (count > 0)
                {
                    Undo.RegisterCreatedObjectUndo(go, "Remove missing Scripts");
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                    compCount += count;
                    goCount++;
                }
            }
        }
    }
#endif
}
