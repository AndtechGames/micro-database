/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Andtech.MicroDatabase {

	/// <summary>
	/// Data implementation with reflection features.
	/// </summary>
	public partial class Data : ICustomNotifyPropertyChanged {

		#region Reflection
		private bool TryGetPropertyInfo(string property, out PropertyInfo propertyInfo) {
			propertyInfo = GetType().GetProperty(property);

			return propertyInfo != null;
		}

		private bool TryGetFieldInfo(string field, out FieldInfo fieldInfo) {
			fieldInfo = GetType().GetField(field, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);

			return fieldInfo != null;
		}

		protected bool TryGetField<T>(string field, out T value) {
			// Try acquire field
			if (!TryGetFieldInfo(field, out FieldInfo info))
				goto Hell;

			// Retrieve value
			try {
				value = (T)info.GetValue(this);

				return true;
			}
			catch { goto Hell; }

			Hell:
			value = default;
			return false;
		}

		protected bool TrySetField(string field, object value) {
			// Try acquire field
			if (!TryGetFieldInfo(field, out FieldInfo info))
				return false;

			// Apply set
			info.SetValue(this, value);

			return true;
		}

		/// <summary>
		/// Helper method for setting backing stores from within a property.
		/// </summary>
		/// <param name="backingFieldName">The identifier of the backing store.</param>
		/// <param name="value">The value to set.</param>
		/// <param name="propertyName">The identifier of the property. (Typically, don't supply this)</param>
		/// <returns>The property equals the provided value.</returns>
		/// <remarks>The routine raises "property changed" events.</remarks>
		protected bool SetWithNotify(string backingFieldName, object value, [CallerMemberName] string propertyName = default) {
			// Obtain the property info (raw)
			if (!TryGetPropertyInfo(propertyName, out var propertyInfo))
				return false;

			// Compute participant values (1 of 2)
			var oldValue = propertyInfo.GetValue(this);
			// Apply the set operation
			if (!TrySetField(backingFieldName, value))
				return false;

			// Compute participant values (2 of 2)
			var newValue = propertyInfo.GetValue(this);
			if (DiffUtility.TryCreateDiff(propertyInfo.Name, oldValue, newValue, out var diff))
				propertyChanged?.Invoke(this, new CustomNotifyPropertyChangedEventArgs { Diff = diff });

			return true;
		}
		#endregion

		#region INTERFACE
		event EventHandler<CustomNotifyPropertyChangedEventArgs> ICustomNotifyPropertyChanged.PropertyChanged {
			add => propertyChanged += value;
			remove => propertyChanged -= value;
		}
		#endregion

		#region EVENT
		protected event EventHandler<CustomNotifyPropertyChangedEventArgs> propertyChanged;
		#endregion
	}
}
