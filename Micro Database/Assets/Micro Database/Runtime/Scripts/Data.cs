using System;
using UnityEngine;

namespace Andtech.MicroDatabase {

	/// <summary>
	/// Standard base class for defining data classes.
	/// </summary>
	public abstract partial class Data : ICloneable, IGuidIndexable, ISerializationCallbackReceiver {
		public Guid ID { get; private set; }

		protected Data(Guid id) {
			ID = id;
		}

		[SerializeField]
		private string idString;

		#region INTERFACE
		Guid IGuidIndexable.Guid => ID;

		object ICloneable.Clone() => MemberwiseClone();

		void ISerializationCallbackReceiver.OnBeforeSerialize() => idString = ID.ToString();

		void ISerializationCallbackReceiver.OnAfterDeserialize() => ID = Guid.Parse(idString);
		#endregion
	}
}
