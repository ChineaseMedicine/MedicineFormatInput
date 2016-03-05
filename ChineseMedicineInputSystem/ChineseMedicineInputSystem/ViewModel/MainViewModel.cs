using ChineseMedicineInputSystem.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ChineseMedicineInputSystem.ViewModel
{
    public class MainViewModel
    {
        public static MainViewModel Create()
        {
            return ViewModelSource.Create(() => new MainViewModel()); ;
        }

        protected MainViewModel()
        {
            if (!ViewModelBase.IsInDesignMode)
                RegisterJumpList();
        }
        [Command(false)]
        public void RegisterJumpList()
        {
            this.GetService<IApplicationJumpListService>().Items.AddOrReplace("Navigation", "Basic Info", new BitmapImage(FilePathHelper.GetDXImageUri("Mail/Mail_16x16")), OpenMail);
            this.GetService<IApplicationJumpListService>().Items.AddOrReplace("Navigation", "Calendar", new BitmapImage(FilePathHelper.GetDXImageUri("Scheduling/Today_16x16")), OpenCalendar);
            this.GetService<IApplicationJumpListService>().Items.AddOrReplace("Navigation", "Contacts", new BitmapImage(FilePathHelper.GetDXImageUri("Mail/Contact_16x16")), OpenContacts);
            this.GetService<IApplicationJumpListService>().Items.AddOrReplace("Navigation", "Tasks", new BitmapImage(FilePathHelper.GetDXImageUri("Tasks/Task_16x16")), OpenTasks);
            this.GetService<IApplicationJumpListService>().Items.AddOrReplace("Tasks", "New Mail", new BitmapImage(FilePathHelper.GetDXImageUri("Mail/NewMail_16x16")), OpenMailAndCreateNewMessage);
            this.GetService<IApplicationJumpListService>().Apply();
        }

        public void OpenMail()
        {
            ViewInjectionManager.Default.Navigate(Regions.Navigation, ModuleType.Mail);
        }
        public void OpenCalendar()
        {
            ViewInjectionManager.Default.Navigate(Regions.Navigation, ModuleType.Calendar);
        }
        public void OpenContacts()
        {
            ViewInjectionManager.Default.Navigate(Regions.Navigation, ModuleType.Contacts);
        }
        public void OpenTasks()
        {
            ViewInjectionManager.Default.Navigate(Regions.Navigation, ModuleType.Metedata);
        }
        public void ShowAbout()
        {
            //DevExpress.Xpf.About.ShowAbout(DevExpress.Utils.About.ProductKind.DXperienceWPF);
        }
        public void Exit()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }
        void OpenMailAndCreateNewMessage()
        {
            OpenMail();
            //(ViewInjectionManager.Default.GetService(Regions.Main).GetViewModel(ModuleType.Mail) as MailViewModel).CreateNewMessage();
        }
    }
}
