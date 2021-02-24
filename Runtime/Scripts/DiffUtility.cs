/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System.Collections.Generic;

namespace Andtech.MicroDatabase {

	public static class DiffUtility {

		/// <summary>
		/// Creates a diff from the specified arguments.
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="name">The name of the property.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new (incoming) value.</param>
		/// <param name="diff">The resultant diff.</param>
		/// <returns>The diff was created successfully.</returns>
		public static bool TryCreateDiff<T>(string name, T oldValue, T newValue, out Diff<string, T> diff) {
			if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
				goto Hell;

			diff = new Diff<string, T>(name, oldValue, newValue);
			return true;

			Hell:
			diff = default;
			return false;
		}
	}
}
