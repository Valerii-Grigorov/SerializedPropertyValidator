﻿using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ValidationAttribute), true)]
public class PropertyDrawerValidateTrigger : PropertyDrawer {
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		EditorGUI.BeginChangeCheck();
		EditorGUI.PropertyField(position, property, label, true);
		if ( EditorGUI.EndChangeCheck() ) {
			ValidateProperty(property, fieldInfo, attribute as ValidationAttribute);
		}
	}
 
	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
		return EditorGUI.GetPropertyHeight(property);
	}

	void ValidateProperty(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute validationAttribute) {
		Validator.Validate(property, fieldInfo, validationAttribute);
	}
}
