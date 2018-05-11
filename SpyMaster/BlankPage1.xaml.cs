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
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpyMaster
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class BlankPage1 : Page
	{
		private IInstaApi _instaApi;

		public BlankPage1()
		{
			this.InitializeComponent();
		}

		private async void BeginBtn_Click(object sender, RoutedEventArgs eventArgs)
		{
			try
			{
				Console.WriteLine("Starting demo of InstaSharper project");
				// create user session data and provide login details
				var userSession = new UserSessionData
				{
					UserName = "The_Samimd",
					Password = "The2nd"
				};

				var delay = TimeSpan.FromSeconds(2);
				// create new InstaApi instance using Builder
				_instaApi = InstaApiBuilder.CreateBuilder()
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

				const string stateFile = "state.bin";
				try
				{
					if (File.Exists(stateFile))
					{
						Console.WriteLine("Loading state from file");
						using (var fs = File.OpenRead(stateFile))
						{
							_instaApi.LoadStateDataFromStream(fs);
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}

				if (!_instaApi.IsUserAuthenticated)
				{
					// login
					Console.WriteLine($"Logging in as {userSession.UserName}");
					//delay.Disable();
					var logInResult = await _instaApi.LoginAsync();
					//delay.Enable();
					if (!logInResult.Succeeded)
					{
						Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
						//return false;
						return;
					}

					//var state = _instaApi.GetStateDataAsStream();
					//using (var fileStream = File.Create(stateFile))
					//{
					//	state.Seek(0, SeekOrigin.Begin);
					//	state.CopyTo(fileStream);
					//}


					var res = await _instaApi.GetUserFollowersAsync("the_samimd", PaginationParameters.MaxPagesToLoad(2));

					Console.Write("he");

					//	Console.WriteLine("Press 1 to start basic demo samples");
					//	Console.WriteLine("Press 2 to start upload photo demo sample");
					//	Console.WriteLine("Press 3 to start comment media demo sample");
					//	Console.WriteLine("Press 4 to start stories demo sample");
					//	Console.WriteLine("Press 5 to start demo with saving state of API instance");
					//	Console.WriteLine("Press 6 to start messaging demo sample");
					//	Console.WriteLine("Press 7 to start location demo sample");
					//	Console.WriteLine("Press 8 to start collections demo sample");
					//	Console.WriteLine("Press 9 to start upload video demo sample");

					//	var samplesMap = new Dictionary<ConsoleKey, IDemoSample>
					//	{
					//		[ConsoleKey.D1] = new Basics(_instaApi),
					//		[ConsoleKey.D2] = new UploadPhoto(_instaApi),
					//		[ConsoleKey.D3] = new CommentMedia(_instaApi),
					//		[ConsoleKey.D4] = new Stories(_instaApi),
					//		[ConsoleKey.D5] = new SaveLoadState(_instaApi),
					//		[ConsoleKey.D6] = new Messaging(_instaApi),
					//		[ConsoleKey.D7] = new LocationSample(_instaApi),
					//		[ConsoleKey.D8] = new CollectionSample(_instaApi),
					//		[ConsoleKey.D9] = new UploadVideo(_instaApi)


					//	};
					//	var key = Console.ReadKey();
					//	Console.WriteLine(Environment.NewLine);
					//	if (samplesMap.ContainsKey(key.Key))
					//		await samplesMap[key.Key].DoShow();
					//	Console.WriteLine("Done. Press esc key to exit...");

					//	key = Console.ReadKey();
					//	return key.Key == ConsoleKey.Escape;
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
			}
			//return false;
		}
	}
}
