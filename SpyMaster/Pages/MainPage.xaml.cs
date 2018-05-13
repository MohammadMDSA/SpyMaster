using InstaSharper.API;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
		public static string SelfUsername { get; set; }

		public MainPage()
		{
			this.InitializeComponent();
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{

			SelfUsername = (string)Windows.Storage.ApplicationData.Current.LocalSettings.Values["LastUsername"];

			await LoadPropic();

			await LoadFollowers();

		}

		private async Task LoadFollowers()
		{
			var res = await InstaApi.GetUserFollowersAsync(SelfUsername, PaginationParameters.MaxPagesToLoad(1));

			FollowersList.ItemsSource = res.Value;
		}

		private async Task LoadPropic()
		{
			var rres = await InstaApi.GetUserAsync(SelfUsername);
			Propic.Source = rres.Value.ProfilePicture;
		}
	}
}
