using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class DynastyViewModel : BasicInfoViewModel<DynastyBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DynastyViewModel Create()
        {
            return ViewModelSource.Create(() => new DynastyViewModel());
        }
        protected DynastyViewModel() : base(ModuleType.Dynasty) { }
        public DynastyBo CurrentDynastyBo
        {
            get { return _currentDynastyBo; }
            set { _currentDynastyBo = value; }
        }
        private DynastyBo _currentDynastyBo = new DynastyBo();

        public DynastyBo CurrentSelectedDynastyBo
        {
            get { return _currentSelectDynastyBo; }
            set { _currentSelectDynastyBo = value; }
        }
        private DynastyBo _currentSelectDynastyBo = new DynastyBo();

        public virtual int Count { get; set; }

        protected virtual void OnCountChanged()
        {

        }


        public void Delete()
        {
            DynastyHandler handler = new DynastyHandler();
            if (!handler.DeleteBDynastyRecord(CurrentSelectedDynastyBo.Id))
            {
                this.GetService<INotificationService>().CreatePredefinedNotification("Delete Result.", "删除失败，已经被应用于元数据", "").ShowAsync();
            }
            else
            {
                ItemsSource.Remove(CurrentSelectedDynastyBo);
            }
        }

        private void RefreshData()
        {
            DynastyHandler handler = new DynastyHandler();
            var ItemRecords = new List<DynastyBo>();
            var records = handler.LoadBDynastyRecords();
            foreach (var record in records)
            {
                ItemRecords.Add(new DynastyBo
                {
                    Id = record.Id,
                    CreateBy = record.CreateBy,
                    CreateTime = record.CreateTime,
                    IsActive = record.IsActive,
                    IsAncient = record.IsAncient,
                    Name = record.Name,
                    UpdateBy = record.UpdateBy,
                    UpdateTime = record.UpdateTime,
                });
            }
            ItemsSource = new ObservableCollection<DynastyBo>(ItemRecords);

            Count++;
        }
        public void TestLoaded()
        {
            UpdateItemsSource();
        }

        protected override void UpdateItemsSource()
        {
            RefreshData();
        }

        public void CreateNewDynasty()
        {
            CurrentDynastyBo = new DynastyBo
            {
                Name = string.Empty,
                IsActive = false,
                IsAncient = false,
                CreateBy = CurrentUser,
                CreateTime = DateTime.Now,
                UpdateBy = CurrentUser,
                UpdateTime = DateTime.Now,
            };

            ShowMessageWindow();
        }

        public void SaveNewDynasty()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DynastyHandler handler = new DynastyHandler();

                    CurrentDynastyBo.CreateTime = DateTime.Now;
                    CurrentDynastyBo.UpdateTime = DateTime.Now;

                    handler.SaveRecord(CurrentDynastyBo);

                    this.GetService<IWindowService>().Close();
                    this.GetService<INotificationService>().CreatePredefinedNotification("Save Result.", "该记录保存成功!", "").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void ColseWindow()
        {
            this.GetService<IWindowService>().Close();
        }
        public void DuplicateQuery(bool showResultMessage)
        {
            DuplicateQueryCheck(true);
        }
        private void ShowMessageWindow()
        {
            this.GetService<IWindowService>().Show(this);
        }
        private bool DuplicateQueryCheck(bool showResultMessage)
        {
            bool result = false;

            if (string.IsNullOrEmpty(CurrentDynastyBo.Name))
            {
                //show message can not be empty
                this.GetService<INotificationService>().CreatePredefinedNotification("Task Changed", "朝代名称不能为空", "").ShowAsync();
            }
            else
            {
                DynastyHandler handler = new DynastyHandler();
                result = handler.DuplicateQuery(CurrentDynastyBo.Name);

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

    }
}
