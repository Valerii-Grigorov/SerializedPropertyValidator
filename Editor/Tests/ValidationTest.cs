using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

using NUnit.Framework;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor.Tests {
	public static class ValidationTest {
		[Test]
		public static void PrefabsValidation() {
			var prefabs = AssetDatabase.FindAssets("t:prefab");
			foreach ( var prefab in prefabs ) {
				var path = AssetDatabase.GUIDToAssetPath(prefab);
				var asset = AssetDatabase.LoadMainAssetAtPath(path);
				var gameObject = asset as GameObject;
				if ( !gameObject ) {
					continue;
				}

				Debug.Log($"Validate prefab [{path}]");
				ValidateComponents(gameObject.GetComponentsInChildren<Component>());
			}
		}

		[Test]
		public static void ScriptableObjectsValidation() {
			var assets = AssetDatabase.FindAssets("t:ScriptableObject");
			foreach ( var asset in assets ) {
				var path = AssetDatabase.GUIDToAssetPath(asset);
				var obj = AssetDatabase.LoadMainAssetAtPath(path);
				if ( !(obj is ScriptableObject scriptableObject) ) {
					continue;
				}

				Debug.Log($"Validate ScriptableObject [{path}]");
				Assert.IsTrue(Validator.Validate(scriptableObject));
			}
		}

		[Test]
		public static void ScenesValidation() {
			foreach ( var sceneInfo in EditorBuildSettings.scenes ) {
				Debug.Log($"CheckingScene {sceneInfo.path}");
				var scene = EditorSceneManager.OpenScene(sceneInfo.path);
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
}
