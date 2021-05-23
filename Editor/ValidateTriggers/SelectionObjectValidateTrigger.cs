using UnityEditor;
using UnityEngine;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor.ValidateTriggers {
	public static class SelectionObjectValidateTrigger {
		[InitializeOnLoadMethod]
		static void Initialize() {
			Selection.selectionChanged -= OnSelectionChanged;
			Selection.selectionChanged += OnSelectionChanged;
		}

		static void OnSelectionChanged() {
			switch (Selection.activeObject) {
				case GameObject gameObject: {
					var components = gameObject.GetComponentsInChildren<Component>(true);
					foreach ( var component in components ) {
						Validator.Validate(component);
					}
					break;
				}
				case ScriptableObject scriptableObject: {
					Validator.Validate(scriptableObject);
					break;
				}
			}
		}
	}
}
