using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ChineseMedicineInputSystem.ViewModel.BasicInfo
{
    public interface IBasicInfoFolderDescription
    {
        BasicInfoFolderName Folder { get; }
        IEnumerable<IBasicInfoFolderDescription> GetSubFolders();
    }

    public class BasicInfoFolderViewModel : IBasicInfoFolderDescription
    {
        public static BasicInfoFolderViewModel Create(string name, Uri icon, BasicInfoFolderName folder)
        {
            return ViewModelSource.Create(() => new BasicInfoFolderViewModel()
            {
                Name = name,
                Icon = icon,
                Folder = folder
            });
        }

        public string Name { get; set; }
        public BasicInfoFolderName Folder { get; set; }
        public List<BasicInfoFolderViewModel> SubFolders { get; set; }
        public Uri Icon { get; set; }
        public virtual int UnreadCount { get; set; }
        protected BasicInfoFolderViewModel()
        {
            SubFolders = new List<BasicInfoFolderViewModel>();
        }
        IEnumerable<IBasicInfoFolderDescription> IBasicInfoFolderDescription.GetSubFolders()
        {
            return SubFolders;
        }
    }
    public class BasicInfoFoldersChildSelector : IChildNodesSelector
    {
        IEnumerable IChildNodesSelector.SelectChildren(object item)
        {
            if (item is IBasicInfoFolderDescription)
                return (item as IBasicInfoFolderDescription).GetSubFolders();

            return null;
        }
    }
}
