using AdminClient.Communication;
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
using System.Windows.Controls;

namespace AdminClient.ViewModel
{
    public class LoginVm : ViewModelBase
    {
        private RelayCommand<object> loginBtnClickedCmd;
        private string loginMessage;
        private string userName;

        Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>();

        private ServiceCommunication client;

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

            //Crate Instance of Communication
            client = new ServiceCommunication();
            client.InitClient();

        }

        

        private void Login(object obj)
        {
            var passwordBox = obj as PasswordBox;
            var password = passwordBox.Password;

            //Verify Access Data
            if(client.LoginAdmin(UserName, password) == true)
            {
                //Activate Buttons
                Session.IsLoggedIn = true;
                
                //Setup Message Content
                MessageContent content = new MessageContent()
                {
                    ViewModelName = "ReportedPictures"
                };

                messenger.Send<PropertyChangedMessage<MessageContent>>(new PropertyChangedMessage<MessageContent>(null, content, ""), "ChangeVm");
            }else
            {
                LoginMessage = "Login fehlgeschlagen!";
                RaisePropertyChanged(null);
            }

            
        }
    }
}
