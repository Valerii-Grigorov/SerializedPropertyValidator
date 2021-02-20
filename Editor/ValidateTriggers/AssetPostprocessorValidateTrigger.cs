using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetPostprocessorValidateTrigger : AssetPostprocessor {
	static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {
		Validate(importedAssets);
		Validate(movedAssets);
	}

	static void Validate(string[] assets) {
		if ( assets == null ) {
			return;
		}
		
		foreach ( var asset in assets ) {
			var obj = AssetDatabase.LoadMainAssetAtPath(asset);
			if ( obj is GameObject gameObject ) {
				var components = gameObject.GetComponentsInChildren<Component>(true).ToList();
				components.ForEach(Validator.Validate);
			} else {
				Validator.Validate(obj);
			}
		}
	}
}
