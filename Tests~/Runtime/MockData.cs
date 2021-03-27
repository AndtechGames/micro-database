/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;

namespace Andtech.MicroDatabase.Tests {

	public class MockData : Data {
		public float Health {
			get => health;
			set => SetWithNotify(nameof(health), value);
		}

		private float health;

		public MockData(Guid id) : base(id) { }
	}
}
