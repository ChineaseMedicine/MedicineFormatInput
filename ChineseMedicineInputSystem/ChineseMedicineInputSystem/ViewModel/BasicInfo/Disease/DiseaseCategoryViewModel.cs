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
    public class DiseaseCategoryViewModel : BasicInfoViewModel<DiseaseCategoryBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DiseaseCategoryViewModel Create()
        {
            return ViewModelSource.Create(() => new DiseaseCategoryViewModel());
        }
        protected DiseaseCategoryViewModel() : base(ModuleType.DiseaseCategory) { }
        public DiseaseCategoryBo CurrentDiseaseCategoryBo
        {
            get { return _currentDiseaseCategoryBo; }
            set { _currentDiseaseCategoryBo = value; }
        }
        private DiseaseCategoryBo _currentDiseaseCategoryBo = new DiseaseCategoryBo();

        public DiseaseCategoryBo CurrentSelectedDiseaseCategoryBo
        {
            get { return _currentSelectedDiseaseCategoryBo; }
            set { _currentSelectedDiseaseCategoryBo = value; }
        }
        private DiseaseCategoryBo _currentSelectedDiseaseCategoryBo = new DiseaseCategoryBo();

        public void SaveNewDiseaseCategory()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DiseaseCategoryHandler handler = new DiseaseCategoryHandler();

                    CurrentDiseaseCategoryBo.CreateTime = DateTime.Now;
                    CurrentDiseaseCategoryBo.UpdateTime = DateTime.Now;

                    handler.SaveRecord(CurrentDiseaseCategoryBo);

                    this.GetService<IWindowService>().Close();
                    this.GetService<INotificationService>().CreatePredefinedNotification("Save Result.", "该记录保存成功!", "").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteDiseaseCategory()
        {
            DiseaseCategoryHandler handler = new DiseaseCategoryHandler();
            handler.DeleteBAgeRecord(CurrentSelectedDiseaseCategoryBo.Id);
            ItemsSource.Remove(CurrentSelectedDiseaseCategoryBo);
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
            DiseaseCategoryHandler handler = new DiseaseCategoryHandler();
            var ItemRecords = new List<DiseaseCategoryBo>();
            var records = handler.LoadBos();
            foreach (var record in records)
            {
                ItemRecords.Add(new DiseaseCategoryBo
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
            ItemsSource = new ObservableCollection<DiseaseCategoryBo>(ItemRecords);

        }
        private bool DuplicateQueryCheck(bool showResultMessage)
        {
            bool result = false;

            if (string.IsNullOrEmpty(CurrentDiseaseCategoryBo.Name))
            {
                //show message can not be empty
                this.GetService<INotificationService>().CreatePredefinedNotification("Task Changed", "年龄段名称不能为空", "").ShowAsync();
            }
            else
            {
                DiseaseCategoryHandler handler = new DiseaseCategoryHandler();
                result = handler.DuplicateQuery(CurrentDiseaseCategoryBo.Name);

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

        public void CreateNewDiseaseCategory()
        {
            CurrentDiseaseCategoryBo = new DiseaseCategoryBo
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
