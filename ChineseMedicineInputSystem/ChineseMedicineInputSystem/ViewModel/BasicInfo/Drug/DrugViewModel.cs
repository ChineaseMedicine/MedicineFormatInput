using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class DrugViewModel : BasicInfoViewModel<DrugBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DrugViewModel Create()
        {
            return ViewModelSource.Create(() => new DrugViewModel());
        }
        protected DrugViewModel() : base(ModuleType.DrugEffect) { }
        public DrugBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private DrugBo _currentBo = new DrugBo();

        public DrugBo CurrentSelectedBo
        {
            get { return _currentSelecteBo; }
            set { _currentSelecteBo = value; }
        }
        private DrugBo _currentSelecteBo = new DrugBo();

        public virtual ObservableCollection<DrugEffectBo> DrugEffectBos { get; set; }
        public DrugEffectBo DrugEffectSelectedBo { get; set; }
        public virtual ObservableCollection<DrugEffectCategroyBo> DrugEffectCategroyBos { get; set; }
        public DrugEffectCategroyBo SelectedDrugEffectCategroyBo { get; set; }
        public virtual ObservableCollection<DurgTasteBo> DurgTasteBos { get; set; }
        public DurgTasteBo DurgTasteSelectedBos { get; set; }
        public void ColseWindow()
        {
            this.GetService<IWindowService>().Close();
        }
        public void SaveNewDrug()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DrugHandler handler = new DrugHandler();

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

        public void DeleteDrug()
        {
            DrugHandler handler = new DrugHandler();
            handler.DeleteBDrugRecord(CurrentSelectedBo.Id);
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
            DrugHandler handler = new DrugHandler();
            var ItemRecords = new List<DrugBo>();
            var records = handler.LoadBDrugRecords();
            foreach (var record in records)
            {
                ItemRecords.Add(new DrugBo
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
            ItemsSource = new ObservableCollection<DrugBo>(ItemRecords);
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
                DrugHandler handler = new DrugHandler();
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

        public void CreateNewDrug()
        {
            CurrentBo = new DrugBo
            {
                Name = string.Empty,
                IsActive = false,
                CreateBy = CurrentUser,
                CreateTime = DateTime.Now,
                UpdateBy = CurrentUser,
                UpdateTime = DateTime.Now,
            };

            LoadDrugEffectCategroys();
            LoadDrugEffects();
            LoadDurgTastes();

            ShowMessageWindow();
        }

        #region load data for create drug
        private void LoadDrugEffectCategroys()
        {
            DrugEffectCategroyHandler _drugEffectCategroyHandler = new DrugEffectCategroyHandler();
            var records = _drugEffectCategroyHandler.LoadBDrugEffectCategroyRecords();
            var drugEffectCategroyBos = new List<DrugEffectCategroyBo>();
            foreach (var record in records)
            {
                drugEffectCategroyBos.Add(new DrugEffectCategroyBo
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
            DrugEffectCategroyBos = new ObservableCollection<DrugEffectCategroyBo>(drugEffectCategroyBos);
        }
        private void LoadDrugEffects()
        {
            DrugEffectHandler _drugEffectHandler = new DrugEffectHandler();
            var records = _drugEffectHandler.LoadBDrugEffectRecords();
            var drugEffectBos = new List<DrugEffectBo>();
            foreach (var record in records)
            {
                drugEffectBos.Add(new DrugEffectBo
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
            DrugEffectBos = new ObservableCollection<DrugEffectBo>(drugEffectBos);
        }
        private void LoadDurgTastes()
        {
            DrugTasteHandler _drugTasteHandler = new DrugTasteHandler();
            var records = _drugTasteHandler.LoadBDurgTasteRecords();
            var drugTasteBos = new List<DurgTasteBo>();
            foreach (var record in records)
            {
                drugTasteBos.Add(new DurgTasteBo
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
            DurgTasteBos = new ObservableCollection<DurgTasteBo>(drugTasteBos);
        }
        #endregion
        
        private void ShowMessageWindow()
        {
            this.GetService<IWindowService>().Show(this);
        }
    }
}
