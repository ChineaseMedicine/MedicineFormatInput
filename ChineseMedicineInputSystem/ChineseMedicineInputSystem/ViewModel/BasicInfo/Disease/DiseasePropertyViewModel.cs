using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class DiseasePropertyViewModel : BasicInfoViewModel<DiseasePropertyBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DiseasePropertyViewModel Create()
        {
            return ViewModelSource.Create(() => new DiseasePropertyViewModel());
        }
        protected DiseasePropertyViewModel() : base(ModuleType.DiseaseProperty) { }
        public DiseasePropertyBo CurrentDiseasePropertyBo
        {
            get { return _currentDiseasePropertyBo; }
            set { _currentDiseasePropertyBo = value; }
        }
        private DiseasePropertyBo _currentDiseasePropertyBo = new DiseasePropertyBo();

        public DiseasePropertyBo CurrentSelectedBo
        {
            get { return _currentSelectedBo; }
            set { _currentSelectedBo = value; }
        }
        private DiseasePropertyBo _currentSelectedBo = new DiseasePropertyBo();

        public void SaveNewDiseaseProperty()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DiseasePropertyHandler handler = new DiseasePropertyHandler();

                    CurrentDiseasePropertyBo.CreateTime = DateTime.Now;
                    CurrentDiseasePropertyBo.UpdateTime = DateTime.Now;

                    handler.SaveRecord(CurrentDiseasePropertyBo);

                    this.GetService<IWindowService>().Close();
                    this.GetService<INotificationService>().CreatePredefinedNotification("Save Result.", "该记录保存成功!", "").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteDiseaseProperty()
        {
            DiseasePropertyHandler handler = new DiseasePropertyHandler();
            handler.DeleteBAgeRecord(CurrentSelectedBo.Id);
            ItemsSource.Remove(CurrentSelectedBo);
        }

        public void RefreshItemSource()
        {
            UpdateItemsSource();
        }
        protected override void UpdateItemsSource()
        {
            RefreshData();
        }
        private void RefreshData()
        {
            DiseasePropertyHandler handler = new DiseasePropertyHandler();
            var ItemRecords = new List<DiseasePropertyBo>();
            var records = handler.LoadRecords();
            foreach (var record in records)
            {
                ItemRecords.Add(new DiseasePropertyBo
                {
                    Id = record.Id,
                    CreateBy = record.CreateBy,
                    CreateTime = record.CreateTime,
                    IsActive = record.IsActive,
                    Name = record.Name,
                    UpdateBy = record.UpdateBy,
                    UpdateTime = record.UpdateTime,
                });
            }
            ItemsSource = new ObservableCollection<DiseasePropertyBo>(ItemRecords);

        }
        private bool DuplicateQueryCheck(bool showResultMessage)
        {
            bool result = false;

            if (string.IsNullOrEmpty(CurrentDiseasePropertyBo.Name))
            {
                //show message can not be empty
                this.GetService<INotificationService>().CreatePredefinedNotification("Task Changed", "年龄段名称不能为空", "").ShowAsync();
            }
            else
            {
                DiseasePropertyHandler handler = new DiseasePropertyHandler();
                result = handler.DuplicateQuery(CurrentDiseasePropertyBo.Name);

                if (result)
                {
                    this.GetService<INotificationService>().CreatePredefinedNotification("Query Result.", "审核不通过,该名称已经被输入", "").ShowAsync();
                }
                else
                {
                    if (showResultMessage)
                    {
                        this.GetService<INotificationService>().CreatePredefinedNotification("Query Result.", "审核通过,可以保存。", "").ShowAsync();
                    }
                }
            }

            return result;
        }

        public void CreateNewDiseaseProperty()
        {
            CurrentDiseasePropertyBo = new DiseasePropertyBo
            {
                Name = string.Empty,
                IsActive = false,
                CreateBy = CurrentUser,
                CreateTime = DateTime.Now,
                UpdateBy = CurrentUser,
                UpdateTime = DateTime.Now,
            };

            ShowMessageWindow();
        }

        private void ShowMessageWindow()
        {
            this.GetService<IWindowService>().Show(this);
        }
    }
}
