using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using Mindscape.Raygun4Net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace $ext_safeprojectname$
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	public sealed partial class App : MvvmAppBase
	{

		#region Settings

		/// <summary>
		/// Get local settings value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T LocalSettings<T>(string key)
		{

			var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
			if (localSettings.Values.ContainsKey(key))
			{
				var serialized = localSettings.Values[key].ToString();
				var retVal = JsonConvert.DeserializeObject<T>(serialized);
				return retVal;
			}
			return default(T);
		}

		/// <summary>
		/// Save local settings value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void LocalSettings<T>(string key, T value)
		{
			var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
			localSettings.Values[key] = JsonConvert.SerializeObject(value);
		}

		/// <summary>
		/// Gets roaming settings value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <returns></returns>
		public static T RoamingSettings<T>(string key)
		{
			var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
			if (roamingSettings.Values.ContainsKey(key))
			{
				var serialized = roamingSettings.Values[key].ToString();
				var retVal = JsonConvert.DeserializeObject<T>(serialized);
				return retVal;
			}
			return default(T);
		}


		/// <summary>
		/// Saves roaming settings value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public static void RoamingSettings<T>(string key, T value)
		{
			var roamingSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
			roamingSettings.Values[key] = JsonConvert.SerializeObject(value);
		}

		#endregion
		#region Resources
		/// <summary>
		/// Gets static resource as T
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="resourceName"></param>
		/// <returns></returns>
		public static T GetResource<T>(String resourceName) where T : class
		{
			if (App.Current.Resources.ContainsKey(resourceName))
			{
				try
				{
					return App.Current.Resources[resourceName] as T;
				}
				catch
				{
					return null;
				}
			}
			return null;
		}
		#endregion

		public static IUnityContainer _container = new UnityContainer();

#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			

			this.InitializeComponent();
		}



		protected override void OnActivated(IActivatedEventArgs args)
		{
			if (args.Kind == ActivationKind.Protocol)
			{
				var protocolArgs = args as ProtocolActivatedEventArgs;
				//Handle protocol activation
			}

			base.OnActivated(args);
		}

		protected override Task OnInitializeAsync(IActivatedEventArgs args)
		{

			_container.RegisterInstance<INavigationService>(NavigationService);
			_container.RegisterInstance<ISessionStateService>(SessionStateService);

			ViewModelLocationProvider.SetDefaultViewModelFactory((type) =>
			{
				return _container.Resolve(type);
			});

			return base.OnInitializeAsync(args);
		}



		protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
		{
			RaygunClient.Attach(GetResource<String>("raygunApiKey"));
			NavigationService.Navigate("Main", null);

			return Task.FromResult<object>(null);
		}






	}
}