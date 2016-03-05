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
        public virtual int CaseNumber { get; set; }

        public virtual MeteDataBo SelectedItem { get; set; }
        protected MetedataViewModel() : base(ModuleType.Metedata) { }

        public void SaveNew()
        {
            var handler = new MainSourceHandler();

            handler.SaveRecord(new MeteDataBo()
            {
                DiseaseCategoryName = SelectedDiseaseCategory,
                DynastyName = SelectedDynasty,
                DiseaseName = SelectedDiseaseName,
                DiseasePropertyName = SelectedDiseasePropertyName,
                AreaName = SelectedArea,
                SeasonName = SelectedSeason,
                EnvironmentName = SelectedEnvironment,
                AgeName = SelectedAge,
                SexName = SelectedSex,
                Symptoms = SelectedSymptoms,
                Syndromes = SelectedSyndromes,
                Prescriptions = SelectedPrescriptions,
                Dosageformses = SelectedDosageformses,
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
            LoadData();
            ShowMessageWindow();
        }

        public void Delete()
        {
            var handler = new MainSourceHandler();
            handler.DeleteRecord(Convert.ToInt64(SelectedItem.Id));

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

        private void LoadData()
        {
            var ageHandler = new AgeHandler();
            AgeList = ageHandler.LoadBAgeRecords().Select(o => o.Name as object).ToList();
            SelectedAge = null;

            var areaHandler = new AreaHandler();
            AreaList = areaHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedArea = null;

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
            DrugNameList = drugHandler.LoadBDrugRecords().Select(o => o.Name as object).ToList();
            SelectedDrugNames = new List<object>();

            var dynastyHandler = new DynastyHandler();
            DynastyList = dynastyHandler.LoadBDynastyRecords().Select(o => o.Name as object).ToList();
            SelectedDynasty = null;

            var environmentHandler = new EnvironmentHandler();
            EnvironmentList = environmentHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedEnvironment = null;

            var prescriptionHandler = new PrescriptionHandler();
            PrescriptionList = prescriptionHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedPrescriptions = new List<object>();

            var seasonHandler = new SeasonHandler();
            SeasonList = seasonHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSeason = null;

            var sexHandler = new SexHandler();
            SexList = sexHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSex = null;

            var symptomHandler = new SymptomHandler();
            SymptomList = symptomHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSymptoms = new List<object>();

            var syndromesHandler = new SyndromesHandler();
            SyndromeList = syndromesHandler.LoadBos().Select(o => o.Name as object).ToList();
            SelectedSyndromes = new List<object>();
        }

        private void ShowMessageWindow()
        {
            this.GetService<IWindowService>().Show(this);
        }
    }
}
