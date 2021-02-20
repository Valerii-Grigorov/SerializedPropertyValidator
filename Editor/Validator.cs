using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class Validator {
	public static void Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute validationAttribute) {
		var attributeType = validationAttribute.GetType();
		var validators = ValidatorBox.Validators.FindAll(validator => validator.AttributeType == attributeType);
		validators.ForEach(validator => validator.Validate(property, fieldInfo, validationAttribute));
	}
	
	public static void Validate(Object obj) {
		Validate(new SerializedObject(obj));
	}

	static void Validate(SerializedObject serializedObject) {
		var serializedProperty = serializedObject.GetIterator();
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
				Validate(serializedProperty, field, validationAttribute);
			}
		}
	}
	
	static FieldInfo GetFieldInfo(SerializedProperty property) {
		var parentType = property.serializedObject.targetObject.GetType();
		var fieldInfo = parentType.GetField(property.propertyPath,
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		return fieldInfo;
	}
}