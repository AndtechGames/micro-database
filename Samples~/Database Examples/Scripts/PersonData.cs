/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;

namespace Andtech.MicroDatabase {

	public class PersonData : Data {
		public string Name {
			get => name;
			set => SetWithNotify(nameof(name), value);
		}
		public int Age {
			get => age;
			set => SetWithNotify(nameof(age), value);
		}
		public float Health {
			get => health;
			set => SetWithNotify(nameof(health), value);
		}

		private string name;
		private int age;
		private float health;

		public PersonData(Guid id) : base(id) { }

		public override string ToString() {
			return Name;
		}
	}
}
