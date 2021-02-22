using UnityEditor;
using UnityEngine;

public static class SelectionObjectValidateTrigger {
	[InitializeOnLoadMethod]
	static void Initialize() {
		Selection.selectionChanged -= OnSelectionChanged;
		Selection.selectionChanged += OnSelectionChanged;
	}

	static void OnSelectionChanged() {
		switch (Selection.activeObject) {
			case GameObject gameObject: {
				foreach ( var component in gameObject.GetComponentsInChildren<Component>(true) ) {
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
