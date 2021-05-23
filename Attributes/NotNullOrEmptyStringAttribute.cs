using System;

namespace Grigorov.Unity.SerializedPropertyValidator.Attributes {
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class NotNullOrEmptyStringAttribute : ValidationAttribute {
	}
}