using System;

namespace Andtech.MicroDatabase {

	public class CustomNotifyPropertyChangedEventArgs : EventArgs {
		public IDiff Diff { get; set; }
	}

	public interface ICustomNotifyPropertyChanged {

		#region EVENT
		event EventHandler<CustomNotifyPropertyChangedEventArgs> PropertyChanged;
		#endregion
	}
}
