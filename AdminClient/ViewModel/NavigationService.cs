using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.ViewModel
{
    public class NavigationService
    {
        private string lastviewId = "";
        private string currentviewId = null;

        public ViewModelBase GoBack()
        {
            if (lastviewId != "")
            {
                return SimpleFactory(lastviewId);
            }
            {
                return SimpleFactory(currentviewId);
            }

        }

        public ViewModelBase NavigateTo(string viewId)
        {
            return SimpleFactory(viewId);
        }

        //TODO: if that gets bigger move it to an own class
        private ViewModelBase SimpleFactory(string viewId)
        {
            lastviewId = currentviewId;
            switch (viewId)
            {
                case "Login":
                    currentviewId = viewId;
                    return SimpleIoc.Default.GetInstance<LoginVm>();

                case "ReportedPictures":
                    currentviewId = viewId;
                    return SimpleIoc.Default.GetInstance<ReportedPicturesVm>();

                case "AdminAccounts":
                    currentviewId = viewId;
                    return SimpleIoc.Default.GetInstance<AdminAccountsVm>();



                default:
                    return SimpleFactory(currentviewId);

            }
        }

    }
}
