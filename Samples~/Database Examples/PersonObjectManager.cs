/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Andtech.MicroDatabase {

	public class PersonObjectManager : DatabaseCallbackTemplate<PersonSubsystem, PersonData> {
		[SerializeField]
		private PersonObject prefab;
		private Dictionary<PersonData, PersonObject> persons = new Dictionary<PersonData, PersonObject>();

		#region OVERRIDE
		protected override Func<PersonSubsystem, Database<PersonData>> GetDatabase => instance => instance.Database;

		protected override void OnCreate(Changestep<PersonData> step) {
			var personObject = Instantiate(prefab, transform);
			personObject.name = step.Data.Name;
			personObject.transform.position = new Vector3(2.0F * persons.Count, 0.0F, 0.0F);
			personObject.Data = step.Data;
			personObject.Download();

			persons.Add(step.Data, personObject);
		}

		protected override void OnUpdate(Changestep<PersonData> step) {
			var personObject = persons[step.Data];
			personObject.Download();
		}

		protected override void OnDelete(Changestep<PersonData> step) {
			var personObject = persons[step.Data];
			persons.Remove(step.Data);
			Destroy(personObject.gameObject);
		}
		#endregion
	}
}
