using CMD.Contract.Models;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public enum LayoutMode { Normal, Flipped }
    public enum BasicInfoFolderName
    {
        All, Announcements, ASP, WinForms, IDETools,
        Frameworks, Root, Dynasty, Age, DrugTaste, DrugEffect, DrugEffectCategroy, Drug,
        Disease, DiseaseCategory, DiseaseProperty, Dosageforms, Prescription, Area, Season,
        Environment, Sex, Symptom, Syndromes, Herbs
    };

    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class BasicInfoViewModel<T> : ContentViewModelBase<T>
    {
        IBasicInfoFolderDescription currentFolder;

        private string _currentUser = string.Empty;

        public string CurrentUser
        {
            get
            {
                if (string.IsNullOrEmpty(_currentUser))
                {
                    _currentUser = ConfigurationManager.AppSettings["UserId"];
                }
                return _currentUser;
            }
        }

        public static BasicInfoViewModel<T> Create()
        {
            return ViewModelSource.Create(() => new BasicInfoViewModel<T>(ModuleType.BasicInfo));
        }
        protected BasicInfoViewModel(ModuleType moduleType) : base(moduleType) { }

        [Command(false)]
        public void SetCurrentFolder(IBasicInfoFolderDescription folder)
        {
            currentFolder = folder;
            UpdateItemsSource();
        }
    }
}
