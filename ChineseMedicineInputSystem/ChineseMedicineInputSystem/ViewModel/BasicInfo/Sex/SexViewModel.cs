using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CMD.Contract.Models;
using CMD.DAL.DALs;

using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class SexViewModel : BasicInfoViewModel<SexBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static SexViewModel Create()
        {
            return ViewModelSource.Create(() => new SexViewModel());
        }
        protected SexViewModel() : base(ModuleType.Sex) { }
        public SexBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private SexBo _currentBo = new SexBo();

        public SexBo CurrentSelectedBo
        {
            get { return _currentSelectBo; }
            set { _currentSelectBo = value; }
        }
        private SexBo _currentSelectBo = new SexBo();

        public void SaveNew()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    var handler = new SexHandler();

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
            var handler = new SexHandler();
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
            var handler = new SexHandler();

            var ItemRecords = new List<SexBo>();

            var bos = handler.LoadBos();

            ItemsSource = new ObservableCollection<SexBo>(bos);

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
                SexHandler handler = new SexHandler();
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
            CurrentBo = new SexBo
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
