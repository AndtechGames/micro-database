/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using UnityEngine;
using UnityEngine.UI;

namespace Andtech.MicroDatabase {

	public class PersonObject : MonoBehaviour {
		public PersonData Data { get; set; }

		[SerializeField]
		private Text nameText;

		public void Download() {
			nameText.text = string.Format("{0}\n{1} y/o\n{2}%", Data.Name, Data.Age, Data.Health);
		}
	}
}
