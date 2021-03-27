/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System.Collections.Generic;

namespace Andtech.MicroDatabase {

	public class DeferBuffer<T> {
		/// <summary>
		/// Does the buffer require a flush?
		/// </summary>
		public bool Dirty => pending.Count > 0;
		public IEnumerable<T> Flushed => flushed;

		private readonly Queue<T> pending;
		private readonly Queue<T> flushed;

		public DeferBuffer() {
			pending = new Queue<T>();
			flushed = new Queue<T>();
		}

		public DeferBuffer(int capacity) {
			pending = new Queue<T>(capacity);
			flushed = new Queue<T>(capacity);
		}

		public bool Flush() {
			if (!Dirty)
				return false;

			flushed.Clear();
			foreach (var element in pending)
				flushed.Enqueue(element);
			pending.Clear();

			return true;
		}

		public void Add(T item) => pending.Enqueue(item);

		public void Clear() {
			pending.Clear();
			flushed.Clear();
		}
	}
}
