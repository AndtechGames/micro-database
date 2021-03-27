
namespace Andtech.MicroDatabase {

	public interface ICrudObserver<TData> {

		void OnCreate(Changestep<TData> step);

		void OnUpdate(Changestep<TData> step);

		void OnDelete(Changestep<TData> step);
	}
}
