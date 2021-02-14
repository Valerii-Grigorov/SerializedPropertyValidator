using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public sealed class ExpectNotNullAttribute : ValidationAttribute {
}