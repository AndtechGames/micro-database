/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;
using System.Collections;
using System.Collections.Generic;

namespace Andtech.MicroDatabase {

	/// <summary>
	/// Standard storage location for the core system.
	/// </summary>
	public class GuidSet<T> : ICollection<T> where T : IGuidIndexable {
		public T this[Guid key] {
			get => elements[key];
			set => elements[key] = value;
		}

		private readonly Dictionary<Guid, T> elements = new Dictionary<Guid, T>();

		public void Add(T element) => elements.Add(element.Guid, element);

		/// <summary>
		/// Removes the element from the set.
		/// </summary>
		/// <param name="guid">The GUID of the element to remove.</param>
		/// <returns>Was the element removed successfully?</returns>
		public bool Remove(Guid guid) => elements.Remove(guid);

		public void Clear() => elements.Clear();

		/// <summary>
		/// Tests whether the set contains a certain element.
		/// </summary>
		/// <param name="guid">The GUID of the element to search for.</param>
		/// <returns>Is the element in the set?</returns>
		public bool ContainsID(Guid guid) => elements.ContainsKey(guid);

		#region INTERFACE
		#region ICollection
		public bool Contains(T item) => ContainsID(item.Guid);

		public void CopyTo(T[] array, int arrayIndex) => elements.Values.CopyTo(array, arrayIndex);

		public bool Remove(T item) => Remove(item.Guid);

		public int Count => elements.Count;
		public bool IsReadOnly => false;

		public IEnumerator<T> GetEnumerator() => elements.Values.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		#endregion
		#endregion
	}
}
