using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Tool_Transform_To_Interractible))]
[ExecuteInEditMode]
public class Editor_Interractible : Editor
{

    

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Tool_Transform_To_Interractible gameObjectToTransform = (Tool_Transform_To_Interractible)target;

        if (GUILayout.Button("Transform to Interractible Object"))
        {

            gameObjectToTransform.tag = "interactible";
            


            if (gameObjectToTransform.GetComponent<ItemInteraction>() == null)
            {
                gameObjectToTransform.gameObject.AddComponent<ItemInteraction>();
            }
            

            if (gameObjectToTransform.GetComponent<BoxCollider>() == null)
            {
                gameObjectToTransform.gameObject.AddComponent<BoxCollider>();
            }
        }
    }


}