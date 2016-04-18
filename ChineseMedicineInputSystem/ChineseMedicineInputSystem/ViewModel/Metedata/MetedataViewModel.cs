using CMD.Contract.Models;
using CMD.DAL.DALs;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ChineseMedicineInputSystem.ViewModel.Metedata
{
    public class MetedataViewModel : ContentViewModelBase<MeteDataBo>
    {
        public static MetedataViewModel Create()
        {
            return ViewModelSource.Create(() => new MetedataViewModel());
        }
        public virtual string TitleName { get; set; }
        private bool IsCreateMode = true;
        public virtual List<object> DiseaseCategoryList { get; set; }
        public virtual object SelectedDiseaseCategory { get; set; }
        public virtual List<object> DynastyList { get; set; }
        public virtual object SelectedDynasty { get; set; }
        public virtual List<object> DiseaseNameList { get; set; }
        public virtual object SelectedDiseaseName { get; set; }
        public virtual List<object> DiseasePropertyList { get; set; }
        public virtual object SelectedDiseasePropertyName { get; set; }
        public virtual List<object> AreaList { get; set; }
        public virtual object SelectedArea { get; set; }
        public virtual List<object> SeasonList { get; set; }
        public virtual object SelectedSeason { get; set; }
        public virtual List<object> EnvironmentList { get; set; }
        public virtual object SelectedEnvironment { get; set; }
        public virtual List<object> AgeList { get; set; }
        public virtual object SelectedAge { get; set; }
        public virtual List<object> SexList { get; set; }
        public virtual object SelectedSex { get; set; }
        public virtual List<object> SymptomList { get; set; }
        public virtual List<object> SelectedSymptoms { get; set; }
        public virtual List<object> SyndromeList { get; set; }
        public virtual List<object> SelectedSyndromes { get; set; }
        public virtual List<object> PrescriptionList { get; set; }
        public virtual List<object> SelectedPrescriptions { get; set; }
        public virtual List<object> DosageformsList { get; set; }
        public virtual List<object> SelectedDosageformses { get; set; }
        public virtual List<object> DrugNameList { get; set; }
        public virtual List<object> SelectedDrugNames { get; set; }
        public virtual ObservableCollection<DrugBoInput> DrugBoInputs { get; set; }
        public virtual int CaseNumber { get; set; }
        public virtual long SelectedLineNumber { get; set; }

        public virtual MeteDataBo SelectedItem { get; set; }
        protected MetedataViewModel() : base(ModuleType.Metedata) { }

        public void SaveNew()
        {
            if (!CheckEmpty())
            {
                return;
            }
            var handler = new MainSourceHandler();
            if (!IsCreateMode)
            {
                handler.DeleteRecord(Convert.ToInt64(SelectedItem.Id), false);
            }
            SelectedDrugNames = DrugBoInputs.Distinct().Select(o => o as object).ToList();

            handler.SaveRecord(new MeteDataBo()
            {
                LineNumber = SelectedLineNumber,
                Id = SelectedItem == null ? 0 : SelectedItem.Id,
                DiseaseCategoryName = SelectedDiseaseCategory,
                DynastyName = SelectedDynasty,
                DiseaseName = SelectedDiseaseName,
                DiseasePropertyName = SelectedDiseasePropertyName,
                AreaName = SelectedArea,
                SeasonName = SelectedSeason,
                EnvironmentName = SelectedEnvironment,
                AgeName = SelectedAge,
                SexName = SelectedSex,
                Symptoms = SelectedSymptoms.Distinct().ToList(),
                Syndromes = SelectedSyndromes.Distinct().ToList(),
                Prescriptions = SelectedPrescriptions.Distinct().ToList(),
                Dosageformses = SelectedDosageformses.Distinct().ToList(),
                DrugNames = SelectedDrugNames,
                CaseNumber = CaseNumber,
                CreateBy = ConfigurationManager.AppSettings["UserId"],
                UpdateBy = ConfigurationManager.AppSettings["UserId"],
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            });

            this.GetService<IWindowService>().Close();
            this.GetService<INotificationService>().CreatePredefinedNotification("Save Result.", "该记录保存成功!", "").ShowAsync();
            CleanUp();
        }

        private bool CheckEmpty()
        {
            if (SelectedAge == null)
            {
                this.GetService<INotificationService>()
                    .CreatePredefinedNotification("Query Result.", "年龄层次不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedArea == null)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "地域不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedDiseaseCategory == null)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "疾病大类不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedDiseaseName == null)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "疾病名不能为空", "").ShowAsync();
                return false;
            }
            //if (SelectedDiseasePropertyName == null)
            //{
            //    this.GetService<INotificationService>()
            //       .CreatePredefinedNotification("Query Result.", "年龄层次不能为空", "").ShowAsync();
            //    return false;
            //}
            if (SelectedDosageformses == null || SelectedDosageformses.Count <= 0)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "剂型不能为空", "").ShowAsync();
                return false;
            }
            if (DrugBoInputs == null || DrugBoInputs.Count <= 0)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "药品名不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedDynasty == null)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "朝代不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedEnvironment == null)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "环境不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedPrescriptions == null || SelectedPrescriptions.Count <= 0)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "处方不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedSeason == null)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "季节不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedSex == null)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "性别不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedSymptoms == null || SelectedSymptoms.Count <= 0)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "症状不能为空", "").ShowAsync();
                return false;
            }
            if (SelectedSyndromes == null || SelectedSyndromes.Count <= 0)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "证型不能为空", "").ShowAsync();
                return false;
            }
            if (CaseNumber <= 0)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "病例数不能为空", "").ShowAsync();
                return false;
            }

            if (SelectedLineNumber <= 0)
            {
                this.GetService<INotificationService>()
                   .CreatePredefinedNotification("Query Result.", "Excel行号不能为空", "").ShowAsync();
                return false;
            }

            if (IsCreateMode)
            {
                MainSourceHandler handler = new MainSourceHandler();
                if (handler.CheckDuplicateExcelRowNumber(SelectedLineNumber))
                {
                    this.GetService<INotificationService>()
                      .CreatePredefinedNotification("Query Result.", "Excel行号不能重复", "").ShowAsync();
                    return false;
                }
            }

            return true;
        }
        public void AddDose()
        {
            this.GetService<IWindowService>().Show("bcdeView", this);
        }
        private void CleanUp()
        {
            SelectedAge = null;
            SelectedArea = null;
            SelectedDiseaseCategory = null;
            SelectedDiseaseName = null;
            SelectedDiseasePropertyName = null;
            SelectedDosageformses = null;
            SelectedDrugNames = null;
            SelectedDynasty = null;
            SelectedEnvironment = null;
            SelectedPrescriptions = null;
            SelectedSeason = null;
            SelectedSex = null;
            SelectedSymptoms = null;
            SelectedSyndromes = null;
            CaseNumber = 0;
        }

        public void CreateNew()
        {
            TitleName = "新建元数据窗口";
            IsCreateMode = true;
            LoadData();
            ShowMessageWindow();
        }

        public void Edit()
        {
            if (SelectedItem == null && Convert.ToInt64(SelectedItem.Id) > 0)
            {
                return;
            }
            TitleName = "修改元数据窗口";
            IsCreateMode = false;
            LoadData();
            SetData();
            ShowMessageWindow();
            RefreshItemSource();
        }

        public void Delete()
        {
            var handler = new MainSourceHandler();
            handler.DeleteRecord(Convert.ToInt64(SelectedItem.Id), true);

            ItemsSource.Remove(SelectedItem);
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
            var handler = new MainSourceHandler();

            var ItemRecords = new List<MeteDataBo>();

            var bos = handler.LoadBos();

            ItemsSource = new ObservableCollection<MeteDataBo>(bos);
        }
        private void SetData()
        {
            SelectedAge = SelectedItem.AgeName;
            SelectedArea = SelectedItem.AreaName;
            SelectedDiseaseCategory = SelectedItem.DiseaseCategoryName;
            SelectedDiseaseName = SelectedItem.DiseaseName;
            SelectedDiseasePropertyName = SelectedItem.DiseasePropertyName;
            SelectedDosageformses = SelectedItem.Dosageformses;
            DrugBoInputs = new ObservableCollection<DrugBoInput>(SelectedItem.DrugNames.Cast<DrugBoInput>());
            SelectedDynasty = SelectedItem.DynastyName;
            SelectedEnvironment = SelectedItem.EnvironmentName;
            SelectedPrescriptions = SelectedItem.Prescriptions;
            SelectedSeason = SelectedItem.SeasonName;
            SelectedSex = SelectedItem.SexName;
            SelectedSymptoms = SelectedItem.Symptoms;
            SelectedSyndromes = SelectedItem.Syndromes;
            CaseNumber = SelectedItem.CaseNumber;
            SelectedLineNumber = SelectedItem.LineNumber;
        }
        private void LoadData()
        {
            var ageHandler = new AgeHandler();
            AgeList = ageHandler.LoadBAgeRecords().Select(o => o.Name as object).ToList();
            SelectedAge = ageHandler.LoadDefaultBo().Name;

            var areaHandler = new AreaHandler();
            AreaList = areaHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedArea = areaHandler.LoadDefaultBo().Name;

            var diseaseCategoryHandler = new DiseaseCategoryHandler();
            DiseaseCategoryList = diseaseCategoryHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedDiseaseCategory = null;

            var diseaseHandler = new DiseaseHandler();
            DiseaseNameList = diseaseHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedDiseaseName = null;

            var diseasePropertyHandler = new DiseasePropertyHandler();
            DiseasePropertyList = diseasePropertyHandler.LoadRecords().Select(o => o.Name as object).ToList();
            SelectedDiseasePropertyName = null;

            var dosageformHandler = new DosageformsHandler();
            DosageformsList = dosageformHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedDosageformses = new List<object>();

            var drugEffectCategoryHandler = new DrugEffectCategroyHandler();
            var drugEffectHandler = new DrugEffectHandler();
            var drugTasteHandler = new DrugTasteHandler();
            var drugHandler = new DrugHandler();
            DrugNameList = drugHandler.LoadBDrugRecords().Select(o => o as object).ToList();
            SelectedDrugNames = new List<object>();
            DrugBoInputs = new ObservableCollection<DrugBoInput>();

            var dynastyHandler = new DynastyHandler();
            DynastyList = dynastyHandler.LoadBDynastyRecords().Select(o => o.Name as object).ToList();
            SelectedDynasty = null;

            var environmentHandler = new EnvironmentHandler();
            EnvironmentList = environmentHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedEnvironment = environmentHandler.LoadDefaultBo().Name;

            var prescriptionHandler = new PrescriptionHandler();
            PrescriptionList = prescriptionHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedPrescriptions = new List<object>();

            var seasonHandler = new SeasonHandler();
            SeasonList = seasonHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSeason = seasonHandler.LoadDefaultBo().Name;

            var sexHandler = new SexHandler();
            SexList = sexHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSex = sexHandler.LoadDefaultBo().Name;

            var symptomHandler = new SymptomHandler();
            SymptomList = symptomHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSymptoms = new List<object>();

            var syndromesHandler = new SyndromesHandler();
            SyndromeList = syndromesHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSyndromes = new List<object>() { syndromesHandler.LoadDefaultBo().Name };
        }

        private void ShowMessageWindow()
        {
            this.GetService<IWindowService>().Show(this);
        }
    }
}
