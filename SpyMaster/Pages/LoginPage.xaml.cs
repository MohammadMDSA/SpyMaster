using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
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
	public sealed partial class LoginPage : Page
	{
		public LoginPage()
		{
			this.InitializeComponent();
		}

		private async void LoginBtn_Click(object sender, RoutedEventArgs e)
		{
			LoadingControl.IsLoading = true;
			LoginBtn.IsEnabled = false;

			try
			{
				Console.WriteLine("Starting demo of InstaSharper project");
				// create user session data and provide login details
				var userSession = new UserSessionData
				{
					//UserName = "The_Samimd",
					UserName = UsernameBox.Text,
					//Password = "The2nd"
					Password = PasswordBox.Password
				};

				var delay = TimeSpan.FromSeconds(2);
				// create new InstaApi instance using Builder
				MainPage.InstaApi = InstaApiBuilder.CreateBuilder()
					.SetUser(userSession)
					.UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
					.SetRequestDelay(delay)
					.Build();
				//// create account
				//var username = "kajokoleha";
				//var password = "ramtinjokar";
				//var email = "ramtinak@live.com";
				//var firstName = "Ramtin";
				//var accountCreation = await _instaApi.CreateNewAccount(username, password, email, firstName);

				//const string stateFile = "state.bin";
				//try
				//{
				//	if (File.Exists(stateFile))
				//	{
				//		Console.WriteLine("Loading state from file");
				//		using (var fs = File.OpenRead(stateFile))
				//		{
				//			_instaApi.LoadStateDataFromStream(fs);
				//		}
				//	}
				//}
				//catch (Exception eev)
				//{
				//	Console.WriteLine(eev);
				//}

				if (!MainPage.InstaApi.IsUserAuthenticated)
				{
					// login
					Console.WriteLine($"Logging in as {userSession.UserName}");
					//delay.Disable();
					var logInResult = await MainPage.InstaApi.LoginAsync();
					//delay.Enable();
					if (!logInResult.Succeeded)
					{
						Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
						//return false;
						LoginBtn.IsEnabled = true;
						LoadingControl.IsLoading = false;
						return;
					}

					(Window.Current.Content as Frame).Navigate(typeof(MainPage));
					//var state = _instaApi.GetStateDataAsStream();
					//using (var fileStream = File.Create(stateFile))
					//{
					//	state.Seek(0, SeekOrigin.Begin);
					//	state.CopyTo(fileStream);
					//}
					
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				//	// perform that if user needs to logged out
				//	// var logoutResult = Task.Run(() => _instaApi.LogoutAsync()).GetAwaiter().GetResult();
				//	// if (logoutResult.Succeeded) Console.WriteLine("Logout succeed");
				LoginBtn.IsEnabled = true;
				LoadingControl.IsLoading = false;
			}
			//return false;

		}
	}
}
