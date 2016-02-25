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
    public class EnvironmentViewModel : BasicInfoViewModel<EnvironmentBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static EnvironmentViewModel Create()
        {
            return ViewModelSource.Create(() => new EnvironmentViewModel());
        }
        protected EnvironmentViewModel() : base(ModuleType.Environment) { }
        public EnvironmentBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private EnvironmentBo _currentBo = new EnvironmentBo();

        public EnvironmentBo CurrentSelectedBo
        {
            get { return _currentSelectBo; }
            set { _currentSelectBo = value; }
        }
        private EnvironmentBo _currentSelectBo = new EnvironmentBo();

        public void SaveNew()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    var handler = new EnvironmentHandler();

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
            var handler = new EnvironmentHandler();
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
            var handler = new EnvironmentHandler();

            var ItemRecords = new List<EnvironmentBo>();

            var bos = handler.LoadBos();

            ItemsSource = new ObservableCollection<EnvironmentBo>(bos);

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
                EnvironmentHandler handler = new EnvironmentHandler();
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
            CurrentBo = new EnvironmentBo
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
