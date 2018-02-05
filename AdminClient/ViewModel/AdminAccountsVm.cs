using AdminClient.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
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

        //Guid of interested Dataset
        private string parameter = "";

        private RelayCommand anyBtnClickedCommand;

        public AdminAccountsVm()
        {
            //IMPORTANT!!!!!!!!!!!!!!!!!!!
            //Set identifier parameter to: "ViewModel + Identifier", eg.: "DemoReceiverIdentifier"
            messenger.Register<PropertyChangedMessage<MessageContent>>(this, "AdminAccountsIdentifier", GetParameter);

            AnyBtnClickedCommand = new RelayCommand(DoSomething);
        }

        private void GetParameter(PropertyChangedMessage<MessageContent> obj)
        {
            parameter = obj.NewValue.Param;
        }

        private void DoSomething()
        {
            MessageBox.Show("Hi");
        }

        public RelayCommand AnyBtnClickedCommand
        {
            get
            {
                return anyBtnClickedCommand;
            }

            set
            {
                anyBtnClickedCommand = value;
            }
        }
    }
}
