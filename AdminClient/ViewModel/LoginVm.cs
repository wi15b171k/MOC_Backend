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
    public class LoginVm : ViewModelBase
    {
        private RelayCommand<object> loginBtnClickedCmd;

        Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>();

        private string loginMessage;
        private string userName;

        #region
        public RelayCommand<object> LoginBtnClickedCmd
        {
            get
            {
                return loginBtnClickedCmd;
            }

            set
            {
                loginBtnClickedCmd = value;
            }
        }

        public string LoginMessage
        {
            get
            {
                return loginMessage;
            }

            set
            {
                loginMessage = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
        #endregion

        public LoginVm()
        {
            LoginBtnClickedCmd = new RelayCommand<object>(Login);

        }

        

        private void Login(object obj)
        {
            MessageBox.Show("Login clicked");

            //Check if Login correcnt
            //...

            //Setup Message Content
            MessageContent content = new MessageContent()
            {
                ViewModelName = "ReportedPictures"
            };

            messenger.Send<PropertyChangedMessage<MessageContent>>(new PropertyChangedMessage<MessageContent>(null, content, ""), "ChangeVm");
        }
    }
}
