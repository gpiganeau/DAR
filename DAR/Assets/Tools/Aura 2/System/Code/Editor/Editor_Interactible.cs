/*using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Transform))]
[ExecuteInEditMode]

public class Editor_Interactible : Editor
{

    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("-- TOOLS --", EditorStyles.boldLabel);

        Transform gameObjectToTransform = (Transform)target;

        if (GUILayout.Button("Transform to Interractible Object"))
        {

            gameObjectToTransform.tag = "interactible";
            gameObjectToTransform.gameObject.isStatic = true;

            if (gameObjectToTransform.GetComponent<ItemInteraction>() == null)
            {
                gameObjectToTransform.gameObject.AddComponent<ItemInteraction>();
            }

            if (gameObjectToTransform.GetComponent<BoxCollider>() == null)
            {
                gameObjectToTransform.gameObject.AddComponent<BoxCollider>();
            }
        }

        if (GUILayout.Button("Tool n°2 - Placeholder"))
        {
        }
    }
}*/