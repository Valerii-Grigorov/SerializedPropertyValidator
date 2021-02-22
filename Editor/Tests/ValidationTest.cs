using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class ValidationTest {
	[Test]
	public static void PrefabsValidation() {
		var prefabs = AssetDatabase.FindAssets("t:prefab");
		foreach ( var prefab in prefabs ) {
			var asset = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(prefab));
			var gameObject = asset as GameObject;
			if ( !gameObject ) {
				continue;
			}
			ValidateComponents(gameObject.GetComponentsInChildren<Component>());
		}
	}
	
	[Test]
	public static void ScriptableObjectsValidation() {
		var assets = AssetDatabase.FindAssets("t:asset");
		foreach ( var asset in assets ) {
			var obj = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(asset));
			if ( !(obj is ScriptableObject scriptableObject) ) {
				continue;
			}
			Assert.IsTrue(Validator.Validate(scriptableObject));
		}
	}
	
	[Test]
	public static void ScenesValidation() {
		var assets = AssetDatabase.FindAssets("t:Scene");
		foreach ( var asset in assets ) {
			var path = AssetDatabase.GUIDToAssetPath(asset);
			var scene = EditorSceneManager.OpenScene(path);
			var rootGameObjects = scene.GetRootGameObjects();
			foreach ( var gameObject in rootGameObjects ) {
				ValidateComponents(gameObject.GetComponentsInChildren<Component>());
			}
		}
	}

	static void ValidateComponents(Component[] components) {
		foreach ( var component in components ) {
			Assert.IsTrue(Validator.Validate(component));
		}
	}
}
