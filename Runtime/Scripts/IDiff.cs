/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

namespace Andtech.MicroDatabase {

	public interface IDiff {
		object Key {
			get;
		}
		object OldValue {
			get;
		}
		object NewValue {
			get;
		}
	}

	public interface IDiff<TKey, TValue> : IDiff {
		new TKey Key {
			get;
		}
		new TValue OldValue {
			get;
		}
		new TValue NewValue {
			get;
		}
	}
}
