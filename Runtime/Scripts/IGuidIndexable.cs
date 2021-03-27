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
	/// Indicates that the type can be indexed by GUID.
	/// </summary>
	public interface IGuidIndexable {

		/// <summary>
		/// The GUID associated with the instance.
		/// </summary>
		Guid Guid {
			get;
		}
	}
}
