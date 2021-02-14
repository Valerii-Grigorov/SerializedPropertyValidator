using System;
using System.Reflection;
using UnityEditor;

public interface IValidator {
	Type AttributeType { get; }

	void Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute attribute);
}