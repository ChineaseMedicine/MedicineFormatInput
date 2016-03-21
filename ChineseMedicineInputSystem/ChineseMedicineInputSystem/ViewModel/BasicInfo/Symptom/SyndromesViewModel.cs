using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class SyndromesViewModel : BasicInfoViewModel<SyndromesBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static SyndromesViewModel Create()
        {
            return ViewModelSource.Create(() => new SyndromesViewModel());
        }
        protected SyndromesViewModel() : base(ModuleType.Syndromes) { }
        public SyndromesBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private SyndromesBo _currentBo = new SyndromesBo();

        public SyndromesBo CurrentSelectedBo
        {
            get { return _currentSelectBo; }
            set { _currentSelectBo = value; }
        }
        private SyndromesBo _currentSelectBo = new SyndromesBo();

        public void SaveNew()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    var handler = new SyndromesHandler();

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
            var handler = new SyndromesHandler();
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
            var handler = new SyndromesHandler();

            var ItemRecords = new List<SyndromesBo>();

            var bos = handler.LoadBos();

            ItemsSource = new ObservableCollection<SyndromesBo>(bos);

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
                SyndromesHandler handler = new SyndromesHandler();
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
            CurrentBo = new SyndromesBo
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
