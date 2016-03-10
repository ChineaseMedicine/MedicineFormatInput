using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChineseMedicineInputSystem.ViewModel
{
    public enum ModuleType
    {
        Mail, Tasks, Calendar, Contacts, BasicInfo, Dynasty, Age,
        DrugTaste, DrugEffect, DrugEffectCategroy, Drug, Disease,
        DiseaseProperty, DiseaseCategory, Dosageforms, Prescription,
        Area, Season, Environment, Sex, Symptom, Syndromes, Herbs,
        Metedata
    }
    public static class Regions
    {
        public static string Navigation { get { return "NavigationRegion"; } }
        public static string Main { get { return "MainRegion"; } }
    }

    public class ContentViewModelInjectedMessage
    {
        public ModuleType ModuleType { get; private set; }
        public object ViewModel { get; private set; }

        public ContentViewModelInjectedMessage(ModuleType moduleType, object viewModel)
        {
            ModuleType = moduleType;
            ViewModel = viewModel;
        }
    }
    public abstract class NavigationViewModelBase
    {
        public Uri Icon { get; protected set; }
        public string Header { get; protected set; }
        public virtual object ContentViewModel { get; protected set; }
        protected ModuleType ModuleType { get; private set; }

        protected NavigationViewModelBase(ModuleType moduleType)
        {
            ModuleType = moduleType;
            Initialize();
            if (this.IsInDesignMode())
                InitializeInDesignMode();
            Messenger.Default.Register<ContentViewModelInjectedMessage>(this, OnViewModelInjectedMessage);
            ViewInjectionManager.Default.RegisterNavigatedEventHandler(this, NavigateToMainModule);
        }

        protected virtual void Initialize() { }
        protected virtual void InitializeInDesignMode() { }
        protected virtual void OnContentViewModelChanged(object oldValue) { }
        void NavigateToMainModule()
        {
            ViewInjectionManager.Default.Navigate(Regions.Main, ModuleType);
        }
        void OnViewModelInjectedMessage(ContentViewModelInjectedMessage message)
        {
            if (ModuleType == message.ModuleType)
                ContentViewModel = message.ViewModel;
        }
    }
    public abstract class ContentViewModelBase<T> //: NotificationObject
    {
        public virtual ObservableCollection<T> ItemsSource { get; protected set; }

        //protected IDataProvider DataProvider { get; private set; }
        protected IList<T> Items { get; private set; }
        protected ModuleType ModuleType { get; private set; }

        protected ContentViewModelBase(ModuleType moduleType)
        {
            ModuleType = moduleType;
            InitializeDataProvider();
            InitializeItems();
            //UpdateItemsSource();
            if (this.IsInDesignMode())
                InitializeInDesignMode();
            else
                Messenger.Default.Send(new ContentViewModelInjectedMessage(ModuleType, this));
        }

        protected virtual void InitializeInDesignMode() { }
        protected virtual void InitializeItems()
        {
            //Items = DataProvider.GetItems<T>().ToList();
        }
        protected virtual void InitializeDataProvider()
        {
            //if (this.IsInDesignMode())
            //DataProvider = new DesignTimeDataProvider();
            //else
            // DataProvider = DataSource.GetDefaultDataProvider();
        }
        protected virtual void UpdateItemsSource()
        {
            //ItemsSource = new ObservableCollection<T>(Items);
        }
    }
}
