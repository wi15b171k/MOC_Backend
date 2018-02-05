/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AdminClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;

namespace AdminClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //VM Registrieren
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginVm>();
            SimpleIoc.Default.Register<ReportedPicturesVm>();
            SimpleIoc.Default.Register<AdminAccountsVm>();

            //Messenger
            SimpleIoc.Default.Register<Messenger>();

            //VM wechseln
            SimpleIoc.Default.Register<NavigationService>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginVm Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginVm>();
            }
        }

        public ReportedPicturesVm ReportedPictures
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ReportedPicturesVm>();
            }
        }

        public AdminAccountsVm AdminAccounts
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdminAccountsVm>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}