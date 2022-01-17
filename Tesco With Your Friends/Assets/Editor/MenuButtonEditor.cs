using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Tesco.UI;

[CustomEditor(typeof(MenuButton))]
public class MenuButtonEditor : Editor {
    SerializedProperty mType;
    SerializedProperty windowToClose;
    SerializedProperty sceneToSwitch;
    SerializedProperty windowToSwitch;

    private void OnEnable() {
        mType = serializedObject.FindProperty("m_Type");
        windowToClose = serializedObject.FindProperty("m_WindowToClose");
        sceneToSwitch = serializedObject.FindProperty("m_SceneToSwitch");
        windowToSwitch = serializedObject.FindProperty("m_WindowToSwitchTo");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        EditorGUILayout.PropertyField(mType);
        MenuButtonType m_Type = (MenuButtonType)mType.enumValueIndex;

        switch (m_Type) {
            case MenuButtonType.Close:
                EditorGUILayout.PropertyField(windowToClose);
                break;
            case MenuButtonType.Quit:
                break;
            case MenuButtonType.SwitchScene:
                EditorGUILayout.PropertyField(sceneToSwitch);
                break;
            case MenuButtonType.SwitchUI:
                EditorGUILayout.PropertyField(windowToClose);
                EditorGUILayout.PropertyField(windowToSwitch);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
