using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace SpyMaster.Controls
{
	public sealed partial class UserBanner : UserControl
	{
		public string Username { get => UsernameLabel.Text; set => UsernameLabel.Text = value; }
		public string FullName { get => FullNameLabel.Text; set => FullNameLabel.Text = value; }
		public object PicSource { get => Pic.Source; set => Pic.Source = value; }

		public UserBanner()
		{
			this.InitializeComponent();
		}
	}
}
