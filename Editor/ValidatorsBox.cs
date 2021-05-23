using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Grigorov.Unity.SerializedPropertyValidator.Editor.Validators;
using UnityEditor.Callbacks;

namespace Grigorov.Unity.SerializedPropertyValidator.Editor {
	public static class ValidatorBox {
		static List<IValidator> _validators = new List<IValidator>();

		public static List<IValidator> Validators {
			get {
				if ( _validators.Count == 0 ) {
					_validators = CreateValidators();
				}

				return _validators;
			}
		}

		static List<IValidator> CreateValidators() {
			return Assembly.GetExecutingAssembly()
				.GetTypes()
				.Where(CheckValidatorInterface)
				.Select(t => Activator.CreateInstance(t) as IValidator).ToList();
		}

		static bool CheckValidatorInterface(Type type) {
			var interfaces = type.GetInterfaces();
			var validatorInterface = typeof(IValidator);
			foreach ( var intr in interfaces ) {
				if ( intr == validatorInterface ) {
					return true;
				}
			}

			return false;
		}

		[DidReloadScripts]
		static void Reload() {
			_validators.Clear();
		}
	}
}