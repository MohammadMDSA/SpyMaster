using InstaSharper.API;
using InstaSharper.Classes;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpyMaster.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public static IInstaApi InstaApi { get; set; }
		public static UserSessionData SelfUser { get; set; }

		public MainPage()
		{
			this.InitializeComponent();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{

			LoadFollowers();
			
		}

		private async void LoadFollowers()
		{
			var res = await InstaApi.GetUserFollowersAsync(SelfUser.UserName, PaginationParameters.MaxPagesToLoad(1));

			FollowersList.ItemsSource = res.Value;
		}

		private async void LoadPropic()
		{
			var res = await InstaApi.GetUserAsync()
		}
	}
}
