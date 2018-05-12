﻿using InstaSharper.API;
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

		public MainPage()
		{
			this.InitializeComponent();
		}

		protected async override void OnNavigatedTo(NavigationEventArgs e)
		{
			FollowersLoading.IsLoading = true;

			var res = await InstaApi.GetUserFollowersAsync("the_samimd", PaginationParameters.MaxPagesToLoad(1));

			FollowersList.ItemsSource = res.Value;

			FollowersLoading.IsLoading = false;

			Console.Write("he");
		}
	}
}
