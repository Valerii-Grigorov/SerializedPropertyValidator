using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class Validator {
	public static bool Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute validationAttribute) {
		var attributeType = validationAttribute.GetType();
		var validators = ValidatorBox.Validators.FindAll(validator => validator.AttributeType == attributeType);

		var result = true;
		foreach ( var validator in validators ) {
			if ( validator.Validate(property, fieldInfo, validationAttribute) ) {
				continue;
			}
			result = false;
		}
		return result;
	}
	
	public static bool Validate(Object obj) {
		return Validate(new SerializedObject(obj));
	}

	static bool Validate(SerializedObject serializedObject) {
		var serializedProperty = serializedObject.GetIterator();
		var result = true;
		while ( serializedProperty.NextVisible(true) ) {
			var field = GetFieldInfo(serializedProperty);
			if ( field == null ) {
				continue;
			}

			var attributes = field.CustomAttributes;
			foreach ( var attr in attributes ) {
				var attribute = field.GetCustomAttribute(attr.AttributeType);
				if ( !(attribute is ValidationAttribute validationAttribute) ) {
					continue;
				}
				if ( Validate(serializedProperty, field, validationAttribute) ) {
					continue;
				}
				result = false;
			}
		}
		return result;
	}
	
	static FieldInfo GetFieldInfo(SerializedProperty property) {
		var parentType = property.serializedObject.targetObject.GetType();
		var fieldInfo = parentType.GetField(property.propertyPath,
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		return fieldInfo;
	}
}