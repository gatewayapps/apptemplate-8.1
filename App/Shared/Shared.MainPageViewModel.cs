using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Windows.UI.Xaml.Controls;

namespace $safeprojectname$.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
		public INavigationService NavigationService { get; set; }
		public ISessionStateService SessionStateService { get; set; }


		public MainPageViewModel(INavigationService _navigationService, ISessionStateService _sessionStateService)
		{

			this.NavigationService = _navigationService;
			this.SessionStateService = _sessionStateService;



		}

    }
}
