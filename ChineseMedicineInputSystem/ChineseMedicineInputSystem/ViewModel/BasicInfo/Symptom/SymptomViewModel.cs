using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using CMD.Contract.Models;
using CMD.DAL.DALs;

using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class SymptomViewModel : BasicInfoViewModel<SymptomBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static SymptomViewModel Create()
        {
            return ViewModelSource.Create(() => new SymptomViewModel());
        }
        protected SymptomViewModel() : base(ModuleType.Symptom) { }
        public SymptomBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private SymptomBo _currentBo = new SymptomBo();

        public SymptomBo CurrentSelectedBo
        {
            get { return _currentSelectBo; }
            set { _currentSelectBo = value; }
        }
        private SymptomBo _currentSelectBo = new SymptomBo();

        public void SaveNew()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    var handler = new SymptomHandler();

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
            var handler = new SymptomHandler();
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
            var handler = new SymptomHandler();

            var ItemRecords = new List<SymptomBo>();

            var bos = handler.LoadBos();

            ItemsSource = new ObservableCollection<SymptomBo>(bos);

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
                SymptomHandler handler = new SymptomHandler();
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
            CurrentBo = new SymptomBo
            {
                Name = string.Empty,
                IsActive = false,
                CreateBy = "SYSTEM_SET",
                CreateTime = DateTime.Now,
                UpdateBy = "SYSTEM_SET",
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
