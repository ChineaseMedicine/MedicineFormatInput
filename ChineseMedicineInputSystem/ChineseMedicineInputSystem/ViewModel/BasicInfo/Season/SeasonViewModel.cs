using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class SeasonViewModel : BasicInfoViewModel<SeasonBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static SeasonViewModel Create()
        {
            return ViewModelSource.Create(() => new SeasonViewModel());
        }
        protected SeasonViewModel() : base(ModuleType.Season) { }
        public SeasonBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private SeasonBo _currentBo = new SeasonBo();

        public SeasonBo CurrentSelectedBo
        {
            get { return _currentSelectBo; }
            set { _currentSelectBo = value; }
        }
        private SeasonBo _currentSelectBo = new SeasonBo();

        public void SaveNew()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    var handler = new SeasonHandler();

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
            var handler = new SeasonHandler();
            handler.DeleteRecord(CurrentSelectedBo.Id);
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
            var handler = new SeasonHandler();

            var ItemRecords = new List<SeasonBo>();

            var bos = handler.LoadBos();

            ItemsSource = new ObservableCollection<SeasonBo>(bos);

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
                SeasonHandler handler = new SeasonHandler();
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
            CurrentBo = new SeasonBo
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
