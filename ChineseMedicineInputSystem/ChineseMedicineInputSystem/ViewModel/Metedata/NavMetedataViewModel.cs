using ChineseMedicineInputSystem.Helpers;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseMedicineInputSystem.ViewModel.Metedata
{
    public class NavMetedataViewModel : NavigationViewModelBase
    {
        public static NavMetedataViewModel Create()
        {
            return ViewModelSource.Create(() => new NavMetedataViewModel());
        }

        protected NavMetedataViewModel() : base(ModuleType.Metedata) { }

        protected override void Initialize()
        {
            Header = "元数据";
            Icon = FilePathHelper.GetDXImageUri("Tasks/Task_32x32");
        }
        protected override void InitializeInDesignMode()
        {
            //ContentViewModel = TasksViewModel.Create();
        }
    }
}
