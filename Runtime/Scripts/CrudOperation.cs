/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;

namespace Andtech.MicroDatabase {

	/// <summary>
	/// Represents a standard database operation.
	/// </summary>
	[Flags]
	public enum CrudOperation {
		None = 0,
		Create = 1 << 0,
		Read = 1 << 1,
		Update = 1 << 2,
		Delete = 1 << 3,
	}
}
