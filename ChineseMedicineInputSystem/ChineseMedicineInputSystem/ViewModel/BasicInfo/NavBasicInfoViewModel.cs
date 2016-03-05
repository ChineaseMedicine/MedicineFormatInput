using ChineseMedicineInputSystem.Helpers;
using ChineseMedicineInputSystem.ViewModel.BasicInfo;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public class NavBasicInfoViewModel : NavigationViewModelBase
    {
        public static NavBasicInfoViewModel Create()
        {
            return ViewModelSource.Create(() => new NavBasicInfoViewModel());
        }
        public IList<IBasicInfoFolderDescription> Folders { get; private set; }
        public virtual IBasicInfoFolderDescription CurrentFolder { get; set; }
        protected NavBasicInfoViewModel() : base(ModuleType.BasicInfo) { }

        protected override void Initialize()
        {
            Header = "基础信息";
            Icon = FilePathHelper.GetDXImageUri("Mail/Mail_32x32");
            FillFolders();
            CurrentFolder = GetFolderByFolderDescription(BasicInfoFolderName.Announcements, Folders);
        }
        protected override void InitializeInDesignMode()
        {
            ContentViewModel = BasicInfoViewModel<object>.Create();
        }
        protected override void OnContentViewModelChanged(object oldValue)
        {
            //if (oldValue != null)
            //    (oldValue as BasicInfoViewModel).AssignMailFolders(null);
            //(ContentViewModel as BasicInfoViewModel).SetCurrentFolder(CurrentFolder);
            //(ContentViewModel as BasicInfoViewModel).AssignMailFolders(Folders);
        }
        protected void OnCurrentFolderChanged()
        {
            if (ContentViewModel == null)
                return;

            (ContentViewModel as BasicInfoViewModel<object>).SetCurrentFolder(CurrentFolder);

            if (CurrentFolder.Folder == BasicInfoFolderName.Dynasty)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Dynasty);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Age)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Age);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.DrugTaste)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.DrugTaste);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Herbs)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Herbs);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.DrugEffect)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.DrugEffect);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.DrugEffectCategroy)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.DrugEffectCategroy);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Drug)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Drug);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Disease)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Disease);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.DiseaseCategory)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.DiseaseCategory);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.DiseaseProperty)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.DiseaseProperty);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Dosageforms)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Dosageforms);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Prescription)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Prescription);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Area)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Area);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Season)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Season);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Environment)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Environment);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Sex)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Sex);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Symptom)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Symptom);
            }
            else if (CurrentFolder.Folder == BasicInfoFolderName.Syndromes)
            {
                ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType.Syndromes);
            }
        }
        void FillFolders()
        {
            Folders = new List<IBasicInfoFolderDescription>();
            BasicInfoFolderViewModel mainFolder = BasicInfoFolderViewModel.Create("基础信息", FilePathHelper.GetAppImageUri("FoldersIcons/Customer"), BasicInfoFolderName.Root);

            BasicInfoFolderViewModel drugFolder = BasicInfoFolderViewModel.Create("药品", FilePathHelper.GetAppImageUri("FoldersIcons/IDE"), BasicInfoFolderName.Drug);
            drugFolder.SubFolders.Add(BasicInfoFolderViewModel.Create("药性", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Herbs));
            drugFolder.SubFolders.Add(BasicInfoFolderViewModel.Create("药味", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.DrugTaste));
            drugFolder.SubFolders.Add(BasicInfoFolderViewModel.Create("药品功效", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.DrugEffect));
            drugFolder.SubFolders.Add(BasicInfoFolderViewModel.Create("药品功效大类", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.DrugEffectCategroy));

            BasicInfoFolderViewModel diseaseFolder = BasicInfoFolderViewModel.Create("疾病", FilePathHelper.GetAppImageUri("FoldersIcons/IDE"), BasicInfoFolderName.Disease);
            diseaseFolder.SubFolders.Add(BasicInfoFolderViewModel.Create("疾病大类", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.DiseaseCategory));
            diseaseFolder.SubFolders.Add(BasicInfoFolderViewModel.Create("疾病属性", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.DiseaseProperty));
            
            BasicInfoFolderViewModel symptomFolder = BasicInfoFolderViewModel.Create("症状", FilePathHelper.GetAppImageUri("FoldersIcons/IDE"), BasicInfoFolderName.Symptom);
            symptomFolder.SubFolders.Add(BasicInfoFolderViewModel.Create("证型", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Syndromes));

            BasicInfoFolderViewModel prescriptionFoler = BasicInfoFolderViewModel.Create("方剂", FilePathHelper.GetAppImageUri("FoldersIcons/IDE"), BasicInfoFolderName.Prescription);
            prescriptionFoler.SubFolders.Add(BasicInfoFolderViewModel.Create("剂型", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Dosageforms));

            BasicInfoFolderViewModel otherFoler = BasicInfoFolderViewModel.Create("其他", FilePathHelper.GetAppImageUri("FoldersIcons/IDE"), BasicInfoFolderName.All);
            otherFoler.SubFolders.Add(BasicInfoFolderViewModel.Create("朝代", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Dynasty));
            otherFoler.SubFolders.Add(BasicInfoFolderViewModel.Create("年龄层次", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Age));
            otherFoler.SubFolders.Add(BasicInfoFolderViewModel.Create("地域", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Area));
            otherFoler.SubFolders.Add(BasicInfoFolderViewModel.Create("季节", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Season));
            otherFoler.SubFolders.Add(BasicInfoFolderViewModel.Create("环境", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Environment));
            otherFoler.SubFolders.Add(BasicInfoFolderViewModel.Create("性别", FilePathHelper.GetAppImageUri("FoldersIcons/Frameworks"), BasicInfoFolderName.Sex));

            mainFolder.SubFolders.Add(drugFolder);
            mainFolder.SubFolders.Add(diseaseFolder);
            mainFolder.SubFolders.Add(symptomFolder);
            mainFolder.SubFolders.Add(prescriptionFoler);
            mainFolder.SubFolders.Add(otherFoler);
            Folders.Add(mainFolder);
        }
        IBasicInfoFolderDescription GetFolderByFolderDescription(BasicInfoFolderName name, IEnumerable<IBasicInfoFolderDescription> folders)
        {
            foreach (IBasicInfoFolderDescription folder in folders)
            {
                if (folder.Folder == name)
                    return folder;

                if (folder.GetSubFolders() != null)
                {
                    IBasicInfoFolderDescription subFolder = GetFolderByFolderDescription(name, folder.GetSubFolders());
                    if (subFolder != null)
                        return subFolder;
                }
            }
            return null;
        }
    }
}
