
namespace Andtech.MicroDatabase {

	public interface IDiff {
		object Key {
			get;
		}
		object OldValue {
			get;
		}
		object NewValue {
			get;
		}
	}

	public interface IDiff<TKey, TValue> : IDiff {
		new TKey Key {
			get;
		}
		new TValue OldValue {
			get;
		}
		new TValue NewValue {
			get;
		}
	}
}
