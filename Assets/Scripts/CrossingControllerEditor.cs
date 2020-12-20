using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CrossingController))]
public class CrossingControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        /*CrossingController crossing = (CrossingController)target;

        *//*string[] s = { "ble", "ale" };
        GUILayout.SelectionGrid(1, s, 1);*//*

        var obj = new SerializedObject(crossing);
        var stages = obj.FindProperty("stages");
        EditorGUILayout.BeginHorizontal();
        Debug.Log(stages.isArray);
        //EditorGUILayout.PropertyField(stages.isArray, new GUIContent("duration"));
        //EditorGUILayout.PropertyField(obj.FindProperty("duration"), new GUIContent("duration"));
        var ListSize = stages.arraySize;
        ListSize = EditorGUILayout.IntField("Stages Size", ListSize);
        for (int i = 0; i < stages.arraySize; i++)
        {
            stages.InsertArrayElementAtIndex(i);
        }
        //EditorGUILayout.PropertyField(o.FindProperty("isGreen"), GUIContent.none);
        //EditorGUILayout.PropertyField(o.FindProperty("canGoThrought"), GUIContent.none);
        EditorGUILayout.EndHorizontal();*/
    }
}
