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
using Windows.Storage;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpyMaster.Pages
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class LoginPage : Page
	{
		private StorageFolder AppStorage;
		private ApplicationDataContainer AppSettings;
		private const string STATE_FILE = "state.bin";

		public LoginPage()
		{
			this.InitializeComponent();
			AppStorage = ApplicationData.Current.LocalFolder;
			AppSettings = ApplicationData.Current.LocalSettings;
		}
		
		private async void LoginBtn_Click(object sender, RoutedEventArgs e)
		{
			LoadingControl.IsLoading = true;
			LoginBtn.IsEnabled = false;

			try
			{

				Console.WriteLine("Starting demo of InstaSharper project");
				// create user session data and provide login details
				MainPage.SelfUser = new UserSessionData
				{
					UserName = UsernameBox.Text,
					Password = PasswordBox.Password
				};

				var delay = TimeSpan.FromSeconds(2);
				// create new InstaApi instance using Builder
				MainPage.InstaApi = InstaApiBuilder.CreateBuilder()
					.SetUser(MainPage.SelfUser)
					.UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
					.SetRequestDelay(delay)
					.Build();


				if (!MainPage.InstaApi.IsUserAuthenticated)
				{
					//delay.Disable();
					var logInResult = await MainPage.InstaApi.LoginAsync();
					//delay.Enable();
					if (!logInResult.Succeeded)
					{
						Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
						//return false;
						AppSettings.Values["LastUsername"] = UsernameBox.Text;
						AppSettings.Values["LastPassword"] = PasswordBox.Password;
						LoginBtn.IsEnabled = true;
						LoadingControl.IsLoading = false;
						return;
					}

					var stateFile = await AppStorage.CreateFileAsync(STATE_FILE);

					var state = MainPage.InstaApi.GetStateDataAsStream();
					//using (var fileStream = File.Create(stateFile))
					using (var fileStream = await stateFile.OpenStreamForWriteAsync())
					{
						state.Seek(0, SeekOrigin.Begin);
						state.CopyTo(fileStream);
					}

					App.MainFrame.Navigate(typeof(MainPage));
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

		private async void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Console.WriteLine("Starting demo of InstaSharper project");
			// create user session data and provide login details
			MainPage.SelfUser = new UserSessionData
			{
				UserName = (string)AppSettings.Values["LastUsername"],
				Password = (string)AppSettings.Values["LastPassword"]
			};

			var delay = TimeSpan.FromSeconds(2);
			// create new InstaApi instance using Builder
			MainPage.InstaApi = InstaApiBuilder.CreateBuilder()
				.SetUser(MainPage.SelfUser)
				.UseLogger(new DebugLogger(LogLevel.Exceptions)) // use logger for requests and debug messages
				.SetRequestDelay(delay)
				.Build();

			try
			{
				var stateFile = AppStorage.TryGetItemAsync(STATE_FILE);
				if (stateFile != null)
				{
					Console.WriteLine("Loading state from file");
					//using (var fs = File.OpenRead(stateFile))
					using (var fs = await AppStorage.OpenStreamForReadAsync(STATE_FILE))
					{
						MainPage.InstaApi.LoadStateDataFromStream(fs);
					}
				}
			}
			catch (Exception eev)
			{
				Console.WriteLine(eev);
			}


			if (MainPage.InstaApi.IsUserAuthenticated)
			{
				App.MainFrame.Navigate(typeof(MainPage));
			}

		}
	}
}
