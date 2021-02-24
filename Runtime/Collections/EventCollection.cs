/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;
using System.Collections.Generic;

namespace Andtech.MicroDatabase {

	public class EventCollectionEventArgs<T> : EventArgs {
		public readonly T Element;

		public EventCollectionEventArgs(T element) => Element = element;
	}

	internal class EventCollection<T> {
		internal readonly ICollection<T> collection;

		public EventCollection(ICollection<T> collection) {
			this.collection = collection;
		}

		#region INTERFACE
		public void Add(T item) {
			collection.Add(item);

			added?.Invoke(this, new EventCollectionEventArgs<T>(item));
		}

		public bool Remove(T item) {
			bool result = collection.Remove(item);
			if (result)
				removed?.Invoke(this, new EventCollectionEventArgs<T>(item));

			return result;
		}

		public event EventHandler<EventCollectionEventArgs<T>> Added {
			add => added += value;
			remove => added -= value;
		}
		public event EventHandler<EventCollectionEventArgs<T>> Removed {
			add => removed += value;
			remove => removed -= value;
		}
		#endregion

		#region EVENT
		private event EventHandler<EventCollectionEventArgs<T>> added;
		private event EventHandler<EventCollectionEventArgs<T>> removed;
		#endregion
	}
}
