using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Grigorov.Unity.SerializedPropertyValidator.Attributes;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor.Validators {
	public class NotNullOrEmptyCollectionValidator : IValidator {
		public Type AttributeType => typeof(NotNullOrEmptyCollectionAttribute);
		
		public bool Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute attribute) {
			if ( !property.isArray ) {
				return true;
			}

			var context = property.serializedObject.targetObject;
			if ( fieldInfo.GetValue(context) == null ) {
				Debug.LogError($"[{context.GetType()}.{property.name}] is null");
				return false;
			}
			
			var allowEmptyCollection = (attribute as NotNullOrEmptyCollectionAttribute)?.AllowEmptyCollection ?? false;
			if ( !allowEmptyCollection && (property.arraySize <= 0) ) {
				Debug.LogError($"[{context.GetType()}.{property.name}] is empty");
				return false;
			}

			var isObjectReference = (property.arraySize > 0) && (property.GetArrayElementAtIndex(0).propertyType == SerializedPropertyType.ObjectReference);
			if ( !isObjectReference ) {
				return true;
			}
			
			var result = true;
			for ( var i = 0; i < property.arraySize; i++ ) {
				var obj = property.GetArrayElementAtIndex(i).objectReferenceValue;
				if ( obj ) {
					continue;
				}
				result = false;
				Debug.LogError($"[{context.GetType()}.{property.name}] element[{i}] is null");
			}
			
			return result;
		}
	}
}