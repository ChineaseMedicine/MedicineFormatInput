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
    public class DrugEffectViewModel : BasicInfoViewModel<DrugEffectBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DrugEffectViewModel Create()
        {
            return ViewModelSource.Create(() => new DrugEffectViewModel());
        }
        protected DrugEffectViewModel() : base(ModuleType.DrugEffect) { }
        public DrugEffectBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private DrugEffectBo _currentBo = new DrugEffectBo();

        public DrugEffectBo CurrentSelectedBo
        {
            get { return _currentSelecteBo; }
            set { _currentSelecteBo = value; }
        }
        private DrugEffectBo _currentSelecteBo = new DrugEffectBo();

        public void SaveNewDrugEffect()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DrugEffectHandler handler = new DrugEffectHandler();

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

        public void DeleteDurgTast()
        {
            DrugEffectHandler handler = new DrugEffectHandler();
            handler.DeleteBDrugEffectRecord(CurrentSelectedBo.Id);
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
            DrugEffectHandler handler = new DrugEffectHandler();
            var ItemRecords = new List<DrugEffectBo>();
            var records = handler.LoadBDrugEffectRecords();
            foreach (var record in records)
            {
                ItemRecords.Add(new DrugEffectBo
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
            ItemsSource = new ObservableCollection<DrugEffectBo>(ItemRecords);

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
                DrugEffectHandler handler = new DrugEffectHandler();
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

        public void CreateNewDrugTaste()
        {
            CurrentBo = new DrugEffectBo
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
