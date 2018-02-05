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
    public class ReportedPicturesVm : ViewModelBase
    {
        //Messenger
        Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>();

        //Guid of interested Dataset
        private string parameter = "";

        private RelayCommand showParamBtnClickedCommand;

        public ReportedPicturesVm()
        {
            //IMPORTANT!!!!!!!!!!!!!!!!!!!
            //Set identifier parameter to: "ViewModel + Identifier", eg.: "DemoReceiverIdentifier"
            messenger.Register<PropertyChangedMessage<MessageContent>>(this, "ReportedPicturesIdentifier", SetActiveGuid);

            ShowParamBtnClickedCommand = new RelayCommand(ShowParam);

        }

        private void ShowParam()
        {
            MessageBox.Show("Param: " + parameter);
        }

        private void SetActiveGuid(PropertyChangedMessage<MessageContent> obj)
        {
            parameter = obj.NewValue.Param;
        }

        public RelayCommand ShowParamBtnClickedCommand
        {
            get
            {
                return showParamBtnClickedCommand;
            }

            set
            {
                showParamBtnClickedCommand = value;
            }
        }
    }
}
