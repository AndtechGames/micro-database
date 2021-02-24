/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;

namespace Andtech.MicroDatabase {

	public class PersonSubsystem : SubsystemBehaviour<PersonSubsystem> {
		public Database<PersonData> Database { get; private set; }

		#region MONOBEHAVIOUR
		protected virtual void Awake() {
			Database = new Database<PersonData>(new GuidSet<PersonData>());
		}

		protected virtual void Start() {
			var person0 = MakePerson("Artie", 16, 1.0F);
			var person1 = MakePerson("Bernie", 21, 0.0F);
			var person2 = MakePerson("Charlie", 55, 0.5F);

			Database.Add(person0);
			Database.Add(person1);
			person0.Age = 17;
			Database.Add(person2);
			Database.Remove(person1);

			PersonData MakePerson(string name, int age, float health) {
				return new PersonData(Guid.NewGuid()) {
					Name = name,
					Health = health,
					Age = age
				};
			}
		}
		#endregion
	}
}
