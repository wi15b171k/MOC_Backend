using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using AdminClient.Helpers;

namespace AdminClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        //Detail Views
        private ViewModelBase currentVm;
        private NavigationService navService = SimpleIoc.Default.GetInstance<NavigationService>();

        //Messengers
        Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>();

        private RelayCommand adminAccountsBtnClickedCmd;
        private RelayCommand reportedPicturesBtnClickedCmd;

        #region PROPERTIES
        public ViewModelBase CurrentVm
        {
            get
            {
                return currentVm;
            }

            set
            {
                currentVm = value;
                RaisePropertyChanged(null);
            }
        }

        public RelayCommand AdminAccountsBtnClickedCmd
        {
            get
            {
                return adminAccountsBtnClickedCmd;
            }

            set
            {
                adminAccountsBtnClickedCmd = value;
            }
        }

        public RelayCommand ReportedPicturesBtnClickedCmd
        {
            get
            {
                return reportedPicturesBtnClickedCmd;
            }

            set
            {
                reportedPicturesBtnClickedCmd = value;
            }
        }

        


        #endregion

        public MainViewModel()
        {
            //Statup VM setzen
            CurrentVm = navService.NavigateTo("Login");

            //Messenger
            messenger.Register<PropertyChangedMessage<MessageContent>>(this, "ChangeVm", ChangeDetailView);

            AdminAccountsBtnClickedCmd = new RelayCommand(ShowViewAdminAccounts, CanExecuteAdminAccountsBtn);
            ReportedPicturesBtnClickedCmd = new RelayCommand(ShowViewReportedPictures, CanExecuteReportedPicturesBtn);


        }

        private void ChangeDetailView(PropertyChangedMessage<MessageContent> obj)
        {
            //Change ViewModle
            CurrentVm = navService.NavigateTo(obj.NewValue.ViewModelName);

            //Pass bearer Token
            if(obj.NewValue.Param != null)
            {
                //Send Guid to ViewModel -- die Zielview hört auf den identifier "VieModelNameIdentifier"
                messenger.Send<PropertyChangedMessage<MessageContent>>(new PropertyChangedMessage<MessageContent>(null, obj.NewValue, ""), obj.NewValue.ViewModelName + "Identifier");

            }
        }

        private bool CanExecuteReportedPicturesBtn()
        {
            return true;
        }

        private void ShowViewReportedPictures()
        {
            MessageBox.Show("Reported Pictures");
            CurrentVm = navService.NavigateTo("ReportedPictures");

        }

        private bool CanExecuteAdminAccountsBtn()
        {
            return true;
        }

        private void ShowViewAdminAccounts()
        {
            MessageBox.Show("Admin Accounts");
            CurrentVm = navService.NavigateTo("AdminAccounts");
        }
    }
}