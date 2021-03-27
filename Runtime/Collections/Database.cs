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

	public class DatabaseEventArgs<TData> {
		public readonly Changestep<TData> Changestep;

		internal DatabaseEventArgs(Changestep<TData> changestep) {
			Changestep = changestep;
		}
	}

	public class Database<TData> : IEnumerable<TData> {
		private readonly EventCollection<TData> wrapper;

		public Database(ICollection<TData> collection) {
			var wrapper = new EventCollection<TData>(collection);
			wrapper.Added += HandleAdded;
			wrapper.Removed += HandleRemoved;

			this.wrapper = wrapper;
		}

		public void Add(TData data) => wrapper.Add(data);

		public void Remove(TData data) => wrapper.Remove(data);

		#region INTERFACE
		public IEnumerator<TData> GetEnumerator() => wrapper.collection.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
		#endregion

		#region CALLBACK
		private void HandleAdded(object sender, EventCollectionEventArgs<TData> e) {
			var data = e.Element;
			if (data is ICustomNotifyPropertyChanged x)
				x.PropertyChanged += HandlePropertyChanged;

			var step = new Changestep<TData>(data, CrudOperation.Create);
			Created?.Invoke(this, new DatabaseEventArgs<TData>(step));
		}

		private void HandleRemoved(object sender, EventCollectionEventArgs<TData> e) {
			var data = e.Element;
			if (data is ICustomNotifyPropertyChanged x)
				x.PropertyChanged -= HandlePropertyChanged;

			var step = new Changestep<TData>(data, CrudOperation.Delete);
			Deleted?.Invoke(this, new DatabaseEventArgs<TData>(step));
		}

		private void HandlePropertyChanged(object sender, CustomNotifyPropertyChangedEventArgs e) {
			if (sender is TData data) {
				var step = new Changestep<TData>(data, CrudOperation.Update, e.Diff);
				Updated?.Invoke(this, new DatabaseEventArgs<TData>(step));
			}
		}
		#endregion

		#region EVENT
		public event EventHandler<DatabaseEventArgs<TData>> Created;
		public event EventHandler<DatabaseEventArgs<TData>> Updated;
		public event EventHandler<DatabaseEventArgs<TData>> Deleted;
		#endregion
	}
}
