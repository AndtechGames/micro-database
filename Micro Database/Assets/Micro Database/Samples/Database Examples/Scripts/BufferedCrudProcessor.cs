namespace Andtech.MicroDatabase {

	public class BufferedCrudProcessor<TData> : ICrudObserver<TData> {
		private readonly ICrudObserver<TData> Target;
		private readonly DeferBuffer<Changestep<TData>> Buffer;

		private BufferedCrudProcessor(ICrudObserver<TData> target, DeferBuffer<Changestep<TData>> buffer) {
			Target = target;
			Buffer = buffer;
		}

		public void Flush() {
			if (Buffer.Flush()) {
				foreach (var step in Buffer.Flushed) {
					Process(step);
				}
				Buffer.Clear();
			}
		}

		public void Process(Changestep<TData> step) {
			switch (step.Operation) {
				case CrudOperation.Create:
					Target.OnCreate(step);
					break;
				case CrudOperation.Update:
					Target.OnUpdate(step);
					break;
				case CrudOperation.Delete:
					Target.OnDelete(step);
					break;
			}
		}

		public void Link(Database<TData> database) {
			database.Created += HandleDatabaseChanged;
			database.Updated += HandleDatabaseChanged;
			database.Deleted += HandleDatabaseChanged;
		}

		public void Unlink(Database<TData> database) {
			database.Created -= HandleDatabaseChanged;
			database.Updated -= HandleDatabaseChanged;
			database.Deleted -= HandleDatabaseChanged;
		}

		#region INTERFACE
		void ICrudObserver<TData>.OnCreate(Changestep<TData> step) {
			Buffer.Add(step);
		}

		void ICrudObserver<TData>.OnUpdate(Changestep<TData> step) {
			Buffer.Add(step);
		}

		void ICrudObserver<TData>.OnDelete(Changestep<TData> step) {
			Buffer.Add(step);
		}
		#endregion

		public static BufferedCrudProcessor<TData> Factory(ICrudObserver<TData> target) {
			return new BufferedCrudProcessor<TData>(target, new DeferBuffer<Changestep<TData>>());
		}

		#region CALLBACK
		private void HandleDatabaseChanged(object sender, DatabaseEventArgs<TData> e) {
			Buffer.Add(e.Changestep);
		}
		#endregion
	}
}
