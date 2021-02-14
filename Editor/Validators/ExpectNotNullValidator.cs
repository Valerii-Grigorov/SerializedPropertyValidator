using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class ExpectNotNullValidator : IValidator {
	public Type AttributeType => typeof(ExpectNotNullAttribute);

	public void Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute attribute) {
		if ( property.propertyType != SerializedPropertyType.ObjectReference ) {
			return;
		}

		if ( !property.objectReferenceValue ) {
			Debug.LogError($"[{property.serializedObject.targetObject.GetType()}.{property.name}] is null");
		}
	}
}