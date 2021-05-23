using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Grigorov.Unity.SerializedPropertyValidator.Attributes;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor.Validators {
	public class ExpectNotNullValidator : IValidator {
		public Type AttributeType => typeof(ExpectNotNullAttribute);

		public bool Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute attribute) {
			if ( property.propertyType != SerializedPropertyType.ObjectReference ) {
				return true;
			}

			if ( property.objectReferenceValue ) {
				return true;
			}

			Debug.LogError($"[{property.serializedObject.targetObject.GetType()}.{property.name}] is null");
			return false;
		}
	}
}