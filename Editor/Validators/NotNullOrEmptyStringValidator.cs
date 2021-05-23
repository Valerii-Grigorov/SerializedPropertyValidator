using System;
using System.Reflection;
using UnityEditor;
using Grigorov.Unity.SerializedPropertyValidator.Attributes;
using UnityEngine;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor.Validators {
	public class NotNullOrEmptyStringValidator : IValidator {
		public Type AttributeType => typeof(NotNullOrEmptyStringAttribute);

		public bool Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute attribute) {
			if ( fieldInfo.FieldType != typeof(string) ) {
				return true;
			}
			
			var context = property.serializedObject.targetObject;
			var value = fieldInfo.GetValue(context);
			if ( !(value is string str) ) {
				return true;
			}

			if ( string.IsNullOrEmpty(str) ) {
				Debug.LogError($"[{context.GetType()}.{property.name}] string is null or empty");
			}
			
			return false;
		}
	}
}