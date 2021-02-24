/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;

namespace Andtech.MicroDatabase {

	/// <summary>
	/// Represents a single change to a database.
	/// </summary>
	/// <typeparam name="TData">The type of data in the database.</typeparam>
	public struct Changestep<TData> {
		/// <summary>
		/// The affected data.
		/// </summary>
		public readonly TData Data;
		/// <summary>
		/// The CRUD operation that was performed.
		/// </summary>
		public readonly CrudOperation Operation;
		/// <summary>
		/// Information about the operation (if <see cref="Operation"/> is <see cref="CrudOperation.Update"/>)
		/// </summary>
		public readonly IDiff Diff;
		/// <summary>
		/// When the operation was performed.
		/// </summary>
		public readonly DateTime Timestamp;

		public Changestep(TData data, CrudOperation operation, IDiff diff = default) : this(data, operation, diff, DateTime.UtcNow) { }

		public Changestep(TData data, CrudOperation operation, IDiff diff, DateTime timestamp) {
			Data = data;
			Operation = operation;
			Diff = diff;
			Timestamp = timestamp;
		}

		#region OVERRIDE
		public override string ToString() {
			string format = Operation.HasFlag(CrudOperation.Update) ? "{0} {1} {2}" : "{0} {1}";

			return string.Format(format, Data, Operation, Diff);
		}
		#endregion
	}
}

