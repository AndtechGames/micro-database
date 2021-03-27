/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

namespace Andtech.MicroDatabase {

	public class CrudProcessor<TData> {
		public readonly ICrudObserver<TData> Target;

		public CrudProcessor(ICrudObserver<TData> target) {
			Target = target;
		}

		public void Process(Changestep<TData> step) {
			switch (step.Operation) {
				case CrudOperation.Create:
					Target.OnCreate(step);
					break;
				case CrudOperation.Update:
					Target.OnUpdate(step);
					break;
				case CrudOperation.Delete:
					Target.OnDelete(step);
					break;
			}
		}

		public void Link(Database<TData> database) {
			database.Created += HandleDatabaseChanged;
			database.Updated += HandleDatabaseChanged;
			database.Deleted += HandleDatabaseChanged;
		}

		public void Unlink(Database<TData> database) {
			database.Created -= HandleDatabaseChanged;
			database.Updated -= HandleDatabaseChanged;
			database.Deleted -= HandleDatabaseChanged;
		}

		private void HandleDatabaseChanged(object sender, DatabaseEventArgs<TData> e) {
			Process(e.Changestep);
		}
	}
}
