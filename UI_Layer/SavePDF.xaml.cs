using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI_Layer
{
    /// <summary>
    /// Interaktionslogik für SavePDF.xaml
    /// </summary>
    public partial class SavePDF : Window
    {
        string dir ="";

        public SavePDF()
        {
            InitializeComponent();

            if (dir != null)
            {
                pdf_path.Text = dir;
            }

        }
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();


            dialog.Multiselect = false;
            dialog.Title = "Select a folder";
            dialog.IsFolderPicker = true;



            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dialog.FileName;
                pdf_path.Text = folder;
                dir = folder;
            }
           
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
