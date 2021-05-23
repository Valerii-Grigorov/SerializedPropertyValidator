using System.Reflection;
using Grigorov.Unity.SerializedPropertyValidator.Attributes;
using UnityEditor;
using UnityEngine;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor {
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
			return !obj || Validate(new SerializedObject(obj));
		}

		static bool Validate(SerializedObject serializedObject) {
			var serializedProperty = serializedObject.GetIterator();
			var result = true;
			while ( serializedProperty.NextVisible(true) ) {
				var field = GetFieldInfo(serializedProperty);
				if ( field == null ) {
					continue;
				}

				var validationAttribute = field.GetCustomAttribute<ValidationAttribute>();
				if ( validationAttribute == null ) {
					continue;
				}
				
				if ( Validate(serializedProperty, field, validationAttribute) ) {
					continue;
				}
				
				result = false;
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
}