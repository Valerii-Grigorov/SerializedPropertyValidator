using System;

namespace Grigorov.Unity.SerializedPropertyValidator.Attributes {
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class NotNullOrEmptyCollectionAttribute : ValidationAttribute {
		public bool AllowEmptyCollection { get; set; }
	}
}