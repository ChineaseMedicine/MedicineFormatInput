using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class DrugHerbsViewModel : BasicInfoViewModel<HerbsBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DrugHerbsViewModel Create()
        {
            return ViewModelSource.Create(() => new DrugHerbsViewModel());
        }
        protected DrugHerbsViewModel() : base(ModuleType.Herbs) { }
        public HerbsBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private HerbsBo _currentBo = new HerbsBo();

        public HerbsBo CurrentSelectedBo
        {
            get { return _currentSelecteBo; }
            set { _currentSelecteBo = value; }
        }
        private HerbsBo _currentSelecteBo = new HerbsBo();

        public void SaveNew()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    var handler = new HerbsHandler();

                    CurrentBo.CreateTime = DateTime.Now;
                    CurrentBo.UpdateTime = DateTime.Now;

                    handler.SaveRecord(CurrentBo);

                    this.GetService<IWindowService>().Close();
                    this.GetService<INotificationService>().CreatePredefinedNotification("Save Result.", "该记录保存成功!", "").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete()
        {
            var handler = new HerbsHandler();
            if (!handler.DeleteRecord(CurrentSelectedBo.Id))
            {
                this.GetService<INotificationService>().CreatePredefinedNotification("Delete Result.", "删除失败，已经被应用于元数据", "").ShowAsync();
            }
            else
            {
                ItemsSource.Remove(CurrentSelectedBo);
            }
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
            HerbsHandler handler = new HerbsHandler();
            var ItemRecords = new List<HerbsBo>();
            var records = handler.LoadBos();
            foreach (var record in records)
            {
                ItemRecords.Add(new HerbsBo
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
            ItemsSource = new ObservableCollection<HerbsBo>(ItemRecords);

        }
        private bool DuplicateQueryCheck(bool showResultMessage)
        {
            bool result = false;

            if (string.IsNullOrEmpty(CurrentBo.Name))
            {
                //show message can not be empty
                this.GetService<INotificationService>().CreatePredefinedNotification("Task Changed", "年龄段名称不能为空", "").ShowAsync();
            }
            else
            {
                HerbsHandler handler = new HerbsHandler();
                result = handler.DuplicateQuery(CurrentBo.Name);

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
            CurrentBo = new HerbsBo
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
