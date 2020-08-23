using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(Conversation))]

public class ConversationEditor : Editor
{ 

    Conversation convo;
    SerializedObject targetObject;
    SerializedProperty targetList;
    int size;

    Font font;


    void OnEnable()
    {
        convo = (Conversation)target;
        targetObject = new SerializedObject(convo);
        targetList = targetObject.FindProperty("nodes"); // Find the List in our script and create a refrence of it
        font = AssetDatabase.LoadAssetAtPath<Font>("Assets/UI/Fonts/IBMPlexMono-Medium.ttf");
    }

    GUIStyle TextStyle()
    {
        GUIStyle textStyle = new GUIStyle(EditorStyles.textArea);
        textStyle.wordWrap = true;
        textStyle.font = font;
        return textStyle;
    }

    public override void OnInspectorGUI()
    {
        //Update our list
        targetObject.Update();

        //Display our list to the inspector window

        for (int i = 0; i < targetList.arraySize; i++)
        {
            SerializedProperty node = targetList.GetArrayElementAtIndex(i);
            SerializedProperty dialog = node.FindPropertyRelative("dialog");
            SerializedProperty profile = node.FindPropertyRelative("profile");

            dialog.stringValue = EditorGUILayout.TextArea(dialog.stringValue, TextStyle(), 
                GUILayout.Height(60), GUILayout.Width(184));
            EditorGUILayout.PropertyField(profile);

            //Remove this index from the List
            if (GUILayout.Button("X", GUILayout.Width(30)))
            {
                targetList.DeleteArrayElementAtIndex(i);
            }
            EditorGUILayout.Space();
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        //Resize our list
        if (GUILayout.Button("Add", GUILayout.Width(60)))
        {
            convo.nodes.Add(new ConversationNode());
        }

        size = targetList.arraySize;
        size = EditorGUILayout.IntField("Size", size);

        if (size != targetList.arraySize)
        {
            while (size > targetList.arraySize)
            {
                targetList.InsertArrayElementAtIndex(targetList.arraySize);
            }
            while (size < targetList.arraySize)
            {
                targetList.DeleteArrayElementAtIndex(targetList.arraySize - 1);
            }
        }

        //Apply the changes to our list
        targetObject.ApplyModifiedProperties();
    }
}