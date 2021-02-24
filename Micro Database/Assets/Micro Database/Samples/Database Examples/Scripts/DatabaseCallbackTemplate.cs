using System;
using UnityEngine;

namespace Andtech.MicroDatabase {

	/// <summary>
	/// Base class for (1) safely registering to a subsystem and (2) receiving callback events.
	/// </summary>
	/// <typeparam name="TSubsystem">The type of the target subsystem.</typeparam>
	/// <typeparam name="TData">The type of the data.</typeparam>
	public abstract class DatabaseCallbackTemplate<TSubsystem, TData> : SubsystemObserver<TSubsystem>, ICrudObserver<TData> where TSubsystem : SubsystemBehaviour<TSubsystem> {
		[SerializeField]
		private CrudOperation crudMask = CrudOperation.Create | CrudOperation.Update | CrudOperation.Delete;
		private CrudProcessor<TData> processor;

		protected virtual void Awake() => processor = new CrudProcessor<TData>(this);

		#region OVERRIDE
		protected override void OnRegister(TSubsystem instance) {
			processor.Link(GetDatabase(instance));
		}

		protected override void OnUnregister(TSubsystem instance) {
			processor.Unlink(GetDatabase(instance));
		}
		#endregion

		#region ABSTRACT
		protected abstract Func<TSubsystem, Database<TData>> GetDatabase { get; }

		protected abstract void OnCreate(Changestep<TData> step);

		protected abstract void OnUpdate(Changestep<TData> step);

		protected abstract void OnDelete(Changestep<TData> step);
		#endregion

		#region INTERFACE
		void ICrudObserver<TData>.OnCreate(Changestep<TData> step) {
			if (crudMask.HasFlag(step.Operation))
				OnCreate(step);
		}

		void ICrudObserver<TData>.OnUpdate(Changestep<TData> step) {
			if (crudMask.HasFlag(step.Operation))
				OnUpdate(step);
		}

		void ICrudObserver<TData>.OnDelete(Changestep<TData> step) {
			if (crudMask.HasFlag(step.Operation))
				OnDelete(step);
		}
		#endregion

		#region PIPELINE
		protected void Log(object message, CrudOperation crudOperation = CrudOperation.None) {
			Color color;
			switch (crudOperation) {
				case CrudOperation.Create:
					color = Color.green;
					break;
				case CrudOperation.Update:
					color = Color.yellow;
					break;
				case CrudOperation.Delete:
					color = Color.red;
					break;
				default:
					color = Color.white;
					break;
			}
			Color blendColor;
#if UNITY_PRO_LICENSE
			blendColor = Color.white;
#else
			blendColor = Color.black;
#endif
			Debug.Log(message.ToString().Color(Color.Lerp(color, blendColor, 0.5F)));
		}
		#endregion
	}
}
