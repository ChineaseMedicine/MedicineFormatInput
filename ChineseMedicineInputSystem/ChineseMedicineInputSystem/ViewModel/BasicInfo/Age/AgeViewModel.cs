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
    public class AgeViewModel : BasicInfoViewModel<AgeBo>
    {
        IBasicInfoFolderDescription currentFolder;
        public static AgeViewModel Create()
        {
            return ViewModelSource.Create(() => new AgeViewModel());
        }
        protected AgeViewModel() : base(ModuleType.Age) { }
        public AgeBo CurrentAgeBo
        {
            get { return _currentAgeBo; }
            set { _currentAgeBo = value; }
        }
        private AgeBo _currentAgeBo = new AgeBo();

        public AgeBo CurrentSelectedAgeBo
        {
            get { return _currentSelectAgeyBo; }
            set { _currentSelectAgeyBo = value; }
        }
        private AgeBo _currentSelectAgeyBo = new AgeBo();

        public void SaveNewAge()
        {
            try
            {
                bool result = DuplicateQueryCheck(false);

                if (!result)
                {
                    AgeHandler handler = new AgeHandler();

                    CurrentAgeBo.CreateTime = DateTime.Now;
                    CurrentAgeBo.UpdateTime = DateTime.Now;
                    CurrentAgeBo.Name = CurrentAgeBo.Name.Trim();

                    handler.SaveRecord(CurrentAgeBo);

                    this.GetService<IWindowService>().Close();
                    this.GetService<INotificationService>().CreatePredefinedNotification("Save Result.", "该记录保存成功!", "").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteAge()
        {
            AgeHandler handler = new AgeHandler();
            if (!handler.DeleteBAgeRecord(CurrentSelectedAgeBo.Id))
            {
                this.GetService<INotificationService>().CreatePredefinedNotification("Delete Result.", "删除失败，已经被应用于元数据", "").ShowAsync();
            }
            else
            {
                ItemsSource.Remove(CurrentSelectedAgeBo);
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
            AgeHandler handler = new AgeHandler();
            var ItemRecords = new List<AgeBo>();
            var records = handler.LoadBAgeRecords();
            foreach (var record in records)
            {
                ItemRecords.Add(new AgeBo
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
            ItemsSource = new ObservableCollection<AgeBo>(ItemRecords);

        }
        private bool DuplicateQueryCheck(bool showResultMessage)
        {
            bool result = false;

            if (string.IsNullOrEmpty(CurrentAgeBo.Name))
            {
                //show message can not be empty
                this.GetService<INotificationService>().CreatePredefinedNotification("Task Changed", "年龄段名称不能为空", "").ShowAsync();
            }
            else
            {
                string ageName = CurrentAgeBo.Name.Trim();

                if (string.IsNullOrEmpty(ageName))
                {
                    this.GetService<INotificationService>().CreatePredefinedNotification("Task Changed", "年龄段名称不能为空", "").ShowAsync();
                }
                else
                {
                    var handler = new AgeHandler();
                    result = handler.DuplicateQuery(ageName);

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
            }

            return result;
        }

        public void CreateNewAge()
        {
            CurrentAgeBo = new AgeBo
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
