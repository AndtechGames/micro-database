﻿using UnityEngine;

namespace Andtech.MicroDatabase {

	public class CrudLogger<T> : ICrudObserver<T> {
		public Color CreateColor { get; set; } = Color.green;
		public Color UpdateColor { get; set; } = Color.yellow;
		public Color DeleteColor { get; set; } = Color.red;

		#region INTERFACE
		void ICrudObserver<T>.OnCreate(Changestep<T> step) {
			Debug.LogFormat("CREATE {0}".Color(CreateColor), step);
		}

		void ICrudObserver<T>.OnUpdate(Changestep<T> step) {
			Debug.LogFormat("UPDATE {0}".Color(UpdateColor), step);
		}

		void ICrudObserver<T>.OnDelete(Changestep<T> step) {
			Debug.LogFormat("DELETE {0}".Color(DeleteColor), step);
		}
		#endregion
	}
}
