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
        private string _message;
        
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public INavigationService NavigationService { get; set; }
        public ISessionStateService SessionStateService { get; set; }

        public MainPageViewModel(INavigationService _navigationService, ISessionStateService _sessionStateService)
		{
			this.NavigationService = _navigationService;
			this.SessionStateService = _sessionStateService;

            Message = "Hello, Gateway.";
		}

        // default constructor used for design time binding
        public MainPageViewModel()
        {
            Message = "Hello, Gateway.";
        }
    }
}
