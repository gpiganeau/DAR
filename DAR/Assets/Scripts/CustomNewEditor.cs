using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PlayerInventoryManager))]
[ExecuteInEditMode]
public class CustomNewEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PlayerInventoryManager manager = (PlayerInventoryManager)target;
        if (GUILayout.Button("Mettre les sprites"))
        {
            
        }
    }


}