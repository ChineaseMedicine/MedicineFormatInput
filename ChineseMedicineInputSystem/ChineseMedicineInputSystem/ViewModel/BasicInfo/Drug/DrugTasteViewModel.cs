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
    public class DrugTasteViewModel : BasicInfoViewModel<DurgTasteBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static DrugTasteViewModel Create()
        {
            return ViewModelSource.Create(() => new DrugTasteViewModel());
        }
        protected DrugTasteViewModel() : base(ModuleType.DrugTaste) { }
        public DurgTasteBo CurrentBo
        {
            get { return _currentBo; }
            set { _currentBo = value; }
        }
        private DurgTasteBo _currentBo = new DurgTasteBo();

        public DurgTasteBo CurrentSelectedBo
        {
            get { return _currentSelecteBo; }
            set { _currentSelecteBo = value; }
        }
        private DurgTasteBo _currentSelecteBo = new DurgTasteBo();

        public void SaveNewDrugTaste()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    DrugTasteHandler handler = new DrugTasteHandler();

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
            DrugTasteHandler handler = new DrugTasteHandler();
            if (!handler.DeleteBDurgTasteRecord(CurrentSelectedBo.Id))
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
            DrugTasteHandler handler = new DrugTasteHandler();
            var ItemRecords = new List<DurgTasteBo>();
            var records = handler.LoadBDurgTasteRecords();
            foreach (var record in records)
            {
                ItemRecords.Add(new DurgTasteBo
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
            ItemsSource = new ObservableCollection<DurgTasteBo>(ItemRecords);

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
                DrugTasteHandler handler = new DrugTasteHandler();
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
            CurrentBo = new DurgTasteBo
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
