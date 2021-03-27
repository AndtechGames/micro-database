/*
 *	Copyright (c) 2021, AndtechGames
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

using System.Collections;
using UnityEngine;

namespace Andtech.MicroDatabase {

	public class BufferingExample : SubsystemObserver<PersonSubsystem> {
		[SerializeField]
		private Color createColor = Color.green;
		[SerializeField]
		private Color updateColor = Color.yellow;
		[SerializeField]
		private Color deleteColor = Color.red;
		[SerializeField]
		private bool autoFlush;

		private BufferedCrudProcessor<PersonData> processor;
		private Coroutine checking;

		public void Flush() {
			processor.Flush();
		}

		#region MONOBEHAVIOUR
		protected virtual void Awake() {
			var logger = new CrudLogger<PersonData>() {
				CreateColor = createColor,
				UpdateColor = updateColor,
				DeleteColor = deleteColor
			};
			processor = BufferedCrudProcessor<PersonData>.Factory(logger);
		}
		#endregion

		#region OVERRIDE
		protected override void OnEnable() {
			base.OnEnable();

			if (autoFlush) {
				var routine = Checking();
				checking = StartCoroutine(routine);
			}
		}

		protected override void OnDisable() {
			base.OnDisable();

			if (checking != null) {
				StopCoroutine(checking);
				checking = null;
			}
		}

		protected override void OnRegister(PersonSubsystem instance) {
			processor.Link(instance.Database);
		}

		protected override void OnUnregister(PersonSubsystem instance) {
			processor.Unlink(instance.Database);
		}
		#endregion

		#region COROUTINE
		private IEnumerator Checking() {
			while (enabled) {
				yield return Yielders.WaitForPostFixedUpdate;

				processor.Flush();
			}
		}
		#endregion
	}
}
