using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor.Menu {
	public static class MenuItems {
		[MenuItem("Assets/SerializedPropertyValidator/Validate")]
		public static void Validate() {
			var obj = Selection.activeObject;
			if ( obj == null ) {
				return;
			}
			
			var path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
			if ( path.Length <= 0 ) {
				return;
			}
			
			if ( Directory.Exists(path) ) {
				ValidateAssetsInFolder(path);
			} else {
				ValidateSingleAsset(path);
			}
		}

		static void ValidateAssetsInFolder(string path) {
			var scriptableObjects = AssetDatabase.FindAssets("t:ScriptableObject", new []{ path });
			var prefabs = AssetDatabase.FindAssets("t:prefab", new []{ path });
			var allGUIDs = scriptableObjects.Concat(prefabs);
			foreach ( var guid in allGUIDs ) {
				var obj = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(guid));
				Validate(obj);
			}
		}
		
		static void ValidateSingleAsset(string path) {
			var obj = AssetDatabase.LoadMainAssetAtPath(path);
			Validate(obj);
		}

		static void Validate(Object obj) {
			switch (obj) {
				case GameObject go: {
					var components = go.GetComponentsInChildren<Component>(true).ToList();
					components.ForEach(c => Validator.Validate(c));
					break;
				}
				case ScriptableObject so:
					Validator.Validate(so);
					break;
			}
		}
	}
}
