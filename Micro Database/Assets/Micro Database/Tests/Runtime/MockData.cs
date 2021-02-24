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
