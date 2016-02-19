using ChineseMedicineInputSystem.Helpers;
using ChineseMedicineInputSystem.View.BasicInfo;
using ChineseMedicineInputSystem.ViewModel;
using ChineseMedicineInputSystem.ViewModel.BasicInfo;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChineseMedicineInputSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static void Initialize()
        {
            DevExpress.Images.ImagesAssemblyLoader.Load();
            ExceptionHelper.Initialize();
            ServiceContainer.Default.RegisterService(new ApplicationJumpListService());
            ThemeManager.ApplicationThemeName = Theme.Office2013Name;
            InitModules();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            Initialize();
            base.OnStartup(e);
        }

        static void InitModules()
        {
            ViewInjectionManager.Default.Inject(Regions.Navigation, ModuleType.BasicInfo,
                () => NavBasicInfoViewModel.Create(), typeof(NavBasicInfoView));
            //ViewInjectionManager.Default.Inject(Regions.Navigation, ModuleType.Tasks,
            //    () => TasksNavigationViewModel.Create(), typeof(NavBarTasksView));
            //ViewInjectionManager.Default.Inject(Regions.Navigation, ModuleType.Calendar,
            //    () => CalendarNavigationViewModel.Create(), typeof(NavBarCalendarView));
            //ViewInjectionManager.Default.Inject(Regions.Navigation, ModuleType.Contacts,
            //    () => ContactsNavigationViewModel.Create(), typeof(NavBarContactsView));

            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.BasicInfo, () => BasicInfoViewModel<object>.Create(), typeof(BasicInfoView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Dynasty, () => DynastyViewModel.Create(), typeof(DynastyView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Age, () => AgeViewModel.Create(), typeof(AgeView));

            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.DrugTaste, () => DrugTasteViewModel.Create(), typeof(DrugTasteView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.DrugEffect, () => DrugEffectViewModel.Create(), typeof(DrugEffectView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.DrugEffectCategroy, () => DrugEffectCategroyViewModel.Create(), typeof(DrugEffectCategoryView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Drug, () => DrugViewModel.Create(), typeof(DrugView));

            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Disease, () => DiseaseViewModel.Create(), typeof(DiseaseView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.DiseaseCategory, () => DiseaseCategoryViewModel.Create(), typeof(DiseaseCategoryView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.DiseaseProperty, () => DiseasePropertyViewModel.Create(), typeof(DiseasePropertyView));

            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Dosageforms, () => DosageformsViewModel.Create(), typeof(DosageformsView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Prescription, () => PrescriptionViewModel.Create(), typeof(PrescriptionView));

            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Area, () => AreaViewModel.Create(), typeof(AreaView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Season, () => SeasonViewModel.Create(), typeof(SeasonView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Environment, () => EnvironmentViewModel.Create(), typeof(EnvironmentView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Sex, () => SexViewModel.Create(), typeof(SexView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Symptom, () => SymptomViewModel.Create(), typeof(SymptomView));
            ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Syndromes, () => SyndromesViewModel.Create(), typeof(SyndromesView));
            //ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Tasks, () => TasksViewModel.Create(), typeof(TasksView));
            //ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Calendar, () => CalendarViewModel.Create(), typeof(CalendarView));
            //ViewInjectionManager.Default.Inject(Regions.Main, ModuleType.Contacts, () => ContactsViewModel.Create(), typeof(ContactsView));
        }
    }
}
