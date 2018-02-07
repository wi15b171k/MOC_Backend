using AdminClient.Communication;
using AdminClient.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Shared.ServiceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private RelayCommand allowPictureBtnClickedCmd;
        private RelayCommand deletePictureBtnClickedCmd;

        private ObservableCollection<ReportVm> reports;
        private ReportVm selectedReport;
        private byte[] picture;

        private ServiceCommunication client;

        public ReportedPicturesVm()
        {
            //Get Params - optional - Set identifier parameter to: "ViewModel + Identifier", eg.: "DemoReceiverIdentifier"
            messenger.Register<PropertyChangedMessage<MessageContent>>(this, "ReportedPicturesIdentifier", SetParameter);

            AllowPictureBtnClickedCmd = new RelayCommand(PictureIsAllowed, CanExecuteAllowPictureBtn);
            DeletePictureBtnClickedCmd = new RelayCommand(PictureIsForbidden, CanExecuteDeletePictureBtn);

            client = new ServiceCommunication();
            Reports = new ObservableCollection<ReportVm>();

            //Get Data
            GetReports();
        }

        private bool CanExecuteDeletePictureBtn()
        {
            if(SelectedReport != null)
            {
                return true;
            }else
            {
                return false;
            }
        }

        private bool CanExecuteAllowPictureBtn()
        {
            if (SelectedReport != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetReports()
        {
            Reports.Clear();

            var result = client.GetActiveReports();
            foreach (var item in result)
            {
                Reports.Add(new ReportVm()
                {
                    Id = item.ReportId,
                    ReportedBy = item.FirstNameReportingUser + " " + item.LastNameReportingUser,
                    PictureOwner = item.FirstNameOwner + " " + item.LastNameOwner,
                    PictureId = item.PicId,
                    Comment = item.Comment
                });
            }
        }

        private void PictureIsAllowed()
        {
            //MessageBox.Show("Allowed" + SelectedReport.PictureId);
            client.UpdateReport(false, SelectedReport.Id);
            GetReports();

        }

        private void PictureIsForbidden()
        {
            //MessageBox.Show("Forbidden" + SelectedReport.PictureId);
            client.UpdateReport(true, SelectedReport.Id);
            GetReports();
        }

        public RelayCommand AllowPictureBtnClickedCmd
        {
            get
            {
                return allowPictureBtnClickedCmd;
            }

            set
            {
                allowPictureBtnClickedCmd = value;
            }
        }

        public RelayCommand DeletePictureBtnClickedCmd
        {
            get
            {
                return deletePictureBtnClickedCmd;
            }

            set
            {
                deletePictureBtnClickedCmd = value;
            }
        }

        public ObservableCollection<ReportVm> Reports
        {
            get
            {
                return reports;
            }

            set
            {
                reports = value;
            }
        }

        public byte[] Picture
        {
            get
            {
                return picture;
            }

            set
            {
                picture = value;
            }
        }

        public ReportVm SelectedReport
        {
            get
            {
                return selectedReport;
            }

            set
            {
                selectedReport = value;
                if(value != null)
                {
                    var result = client.GetPicture(value.PictureId, 630, 450);
                    Picture = result;
                }else
                {
                    Picture = null;
                }
                
                RaisePropertyChanged(null);
            }
        }

        private void SetParameter(PropertyChangedMessage<MessageContent> obj)
        {
            parameter = obj.NewValue.Param;
        }

    }
}
