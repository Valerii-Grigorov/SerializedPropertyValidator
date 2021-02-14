using System.Reflection;
using UnityEditor;

public static class Validator {
	public static void Validate(SerializedProperty property, FieldInfo fieldInfo, ValidationAttribute validationAttribute) {
		var attributeType = validationAttribute.GetType();
		var validators = ValidatorBox.Validators.FindAll(validator => validator.AttributeType == attributeType);
		validators.ForEach(validator => validator.Validate(property, fieldInfo, validationAttribute));
	}
}