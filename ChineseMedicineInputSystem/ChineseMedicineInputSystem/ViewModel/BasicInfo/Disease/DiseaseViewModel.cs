using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class DiseaseViewModel : BasicInfoViewModel<DiseaseBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DiseaseViewModel Create()
        {
            return ViewModelSource.Create(() => new DiseaseViewModel());
        }
        protected DiseaseViewModel() : base(ModuleType.Disease) { }
        public DiseaseBo CurrentDiseaseBo
        {
            get { return _currentDiseaseBo; }
            set { _currentDiseaseBo = value; }
        }
        private DiseaseBo _currentDiseaseBo = new DiseaseBo();

        public DiseaseBo CurrentSelectedBo
        {
            get { return _currentSelectedDiseaseBo; }
            set { _currentSelectedDiseaseBo = value; }
        }
        private DiseaseBo _currentSelectedDiseaseBo = new DiseaseBo();

        public void SaveNewDisease()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DiseaseHandler handler = new DiseaseHandler();

                    CurrentDiseaseBo.CreateTime = DateTime.Now;
                    CurrentDiseaseBo.UpdateTime = DateTime.Now;

                    handler.SaveRecord(CurrentDiseaseBo);

                    this.GetService<IWindowService>().Close();
                    this.GetService<INotificationService>().CreatePredefinedNotification("Save Result.", "该记录保存成功!", "").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteDisease()
        {
            DiseaseHandler handler = new DiseaseHandler();
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
            DiseaseHandler handler = new DiseaseHandler();
            var ItemRecords = new List<DiseaseBo>();
            var records = handler.LoadBos();
            foreach (var record in records)
            {
                ItemRecords.Add(new DiseaseBo
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
            ItemsSource = new ObservableCollection<DiseaseBo>(ItemRecords);

        }
        private bool DuplicateQueryCheck(bool showResultMessage)
        {
            bool result = false;

            if (string.IsNullOrEmpty(CurrentDiseaseBo.Name))
            {
                //show message can not be empty
                this.GetService<INotificationService>().CreatePredefinedNotification("Task Changed", "年龄段名称不能为空", "").ShowAsync();
            }
            else
            {
                DiseaseHandler handler = new DiseaseHandler();
                result = handler.DuplicateQuery(CurrentDiseaseBo.Name);

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

        public void CreateNew()
        {
            CurrentDiseaseBo = new DiseaseBo
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
