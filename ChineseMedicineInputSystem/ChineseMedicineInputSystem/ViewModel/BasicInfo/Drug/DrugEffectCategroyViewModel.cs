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
    public class DrugEffectCategroyViewModel : BasicInfoViewModel<DrugEffectCategroyBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DrugEffectCategroyViewModel Create()
        {
            return ViewModelSource.Create(() => new DrugEffectCategroyViewModel());
        }
        protected DrugEffectCategroyViewModel() : base(ModuleType.DrugEffect) { }
        public DrugEffectCategroyBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private DrugEffectCategroyBo _currentBo = new DrugEffectCategroyBo();

        public DrugEffectCategroyBo CurrentSelectedBo
        {
            get { return _currentSelecteBo; }
            set { _currentSelecteBo = value; }
        }
        private DrugEffectCategroyBo _currentSelecteBo = new DrugEffectCategroyBo();

        public void SaveNewDrugEffectCategroy()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DrugEffectCategroyHandler handler = new DrugEffectCategroyHandler();

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

        public void DeleteDrugEffectCategroy()
        {
            DrugEffectCategroyHandler handler = new DrugEffectCategroyHandler();
            if (!handler.DeleteBAgeRecord(CurrentSelectedBo.Id))
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
            DrugEffectCategroyHandler handler = new DrugEffectCategroyHandler();
            var ItemRecords = new List<DrugEffectCategroyBo>();
            var records = handler.LoadBDrugEffectCategroyRecords();
            foreach (var record in records)
            {
                ItemRecords.Add(new DrugEffectCategroyBo
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
            ItemsSource = new ObservableCollection<DrugEffectCategroyBo>(ItemRecords);

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
                DrugEffectCategroyHandler handler = new DrugEffectCategroyHandler();
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

        public void CreateNewDrugEffectCategroy()
        {
            CurrentBo = new DrugEffectCategroyBo
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
