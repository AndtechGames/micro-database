/*
 *	Copyright (c) 2020, <AUTHOR>
 *	All rights reserved.
 *	
 *	This source code is licensed under the BSD-style license found in the
 *	LICENSE file in the root directory of this source tree
 */

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
