/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

namespace Andtech.MicroDatabase {

	public interface ICrudObserver<TData> {

		void OnCreate(Changestep<TData> step);

		void OnUpdate(Changestep<TData> step);

		void OnDelete(Changestep<TData> step);
	}
}
