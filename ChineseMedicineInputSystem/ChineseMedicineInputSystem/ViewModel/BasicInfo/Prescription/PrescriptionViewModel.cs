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
    public class PrescriptionViewModel : BasicInfoViewModel<PrescriptionBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static PrescriptionViewModel Create()
        {
            return ViewModelSource.Create(() => new PrescriptionViewModel());
        }
        protected PrescriptionViewModel() : base(ModuleType.Dosageforms) { }
        public PrescriptionBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private PrescriptionBo _currentBo = new PrescriptionBo();

        public PrescriptionBo CurrentSelectedBo
        {
            get { return _currentSelectBo; }
            set { _currentSelectBo = value; }
        }
        private PrescriptionBo _currentSelectBo = new PrescriptionBo();

        public void SaveNew()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    var handler = new PrescriptionHandler();

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
            var handler = new PrescriptionHandler();
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
            var handler = new PrescriptionHandler();

            var ItemRecords = new List<PrescriptionBo>();

            var bos = handler.LoadBos();

            ItemsSource = new ObservableCollection<PrescriptionBo>(bos);

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
                PrescriptionHandler handler = new PrescriptionHandler();
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
            CurrentBo = new PrescriptionBo
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
