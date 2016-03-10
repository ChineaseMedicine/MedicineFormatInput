﻿using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChineseMedicineInputSystem.View
{
    /// <summary>
    /// Interaction logic for CreateMetedataDoseView.xaml
    /// </summary>
    public partial class CreateMetedataDoseView : UserControl
    {
        public CreateMetedataDoseView()
        {
            InitializeComponent();
        }


        void view_InitNewRow(object sender, DevExpress.Xpf.Grid.InitNewRowEventArgs e)
        {
            //grid.SetCellValue(e.RowHandle, ColDrugName, 1);
            //grid.SetCellValue(e.RowHandle, ColCount, 1);
            //grid.SetCellValue(e.RowHandle, colDiscount, 0);
            //grid.SetCellValue(e.RowHandle, colOrderID, newRowID++);
        }
        void newItemRowPositionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (view.NewItemRowPosition != NewItemRowPosition.None)
            {
                view.FocusedRowHandle = GridControl.NewItemRowHandle;
                view.ScrollIntoView(view.FocusedRowHandle);
            }
        }
    }
}
