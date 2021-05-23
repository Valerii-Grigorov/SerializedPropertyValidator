using System;
using System.Reflection;
using UnityEditor;
using Grigorov.Unity.SerializedPropertyValidator.Attributes;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor.Validators {
	public interface IValidator {
		Type AttributeType { get; }

		bool Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute attribute);
	}
}