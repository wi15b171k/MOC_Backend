using AdminClient.Communication;
using AdminClient.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdminClient.ViewModel
{
    public class AdminAccountsVm : ViewModelBase
    {
        //Messenger
        Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>();

        //Optianal Parameter
        private string parameter = "";

        private RelayCommand saveBtnClickedCommand;
        private AdminVm newAdmin;

        private ServiceCommunication client;


        public AdminAccountsVm()
        {
            //Get Parameter - optional - Set identifier parameter to: "ViewModel + Identifier", eg.: "DemoReceiverIdentifier"
            messenger.Register<PropertyChangedMessage<MessageContent>>(this, "AdminAccountsIdentifier", GetParameter);

            NewAdmin = new AdminVm();
            SaveBtnClickedCommand = new RelayCommand(SaveAdmin, CanExecuteSaveBtn);

            client = new ServiceCommunication();
        }


        private void SaveAdmin()
        {

            client.AddNewAdminUser(new PersonAdminAddSM()
            {
                FirstName = NewAdmin.FirstName,
                LastName = NewAdmin.LastName,
                Email = NewAdmin.Email,
                Password = NewAdmin.Password,
                ConfirmPassword = NewAdmin.ConfirmPassword
            });

            NewAdmin.FirstName = "";
            NewAdmin.LastName = "";
            NewAdmin.Email = "";
            NewAdmin.Password = "";
            NewAdmin.ConfirmPassword = "";

            RaisePropertyChanged(null);
        }

        private bool CanExecuteSaveBtn()
        {
            return true;
        }

        public RelayCommand SaveBtnClickedCommand
        {
            get
            {
                return saveBtnClickedCommand;
            }

            set
            {
                saveBtnClickedCommand = value;
            }
        }

        public AdminVm NewAdmin
        {
            get
            {
                return newAdmin;
            }

            set
            {
                newAdmin = value;
            }
        }

        private void GetParameter(PropertyChangedMessage<MessageContent> obj)
        {
            parameter = obj.NewValue.Param;
        }

   
    }
}
