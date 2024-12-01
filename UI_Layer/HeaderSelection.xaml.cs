using Kreislauf;
using Kreislauf.Models;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UI_Layer
{
    /// <summary>
    /// Interaktionslogik für HeaderSelection.xaml
    /// </summary>
    public partial class HeaderSelection : Window
    {
        List<string> header;
        Dictionary<string, int> map;
        CSVReader<PersonFull> csv;
        List<PersonFull> persons;
        List<ComboBox> cmbs;
        string path;
        public List<PersonFull> Persons { get => persons; set => persons = value; }

        public HeaderSelection(string? path)
        {
            InitializeComponent();
            map = new Dictionary<string, int>();
            cmbs = new List<ComboBox>();
            this.path = path;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog ofp = new OpenFileDialog();
            ofp.Filter = "Files|*.csv;";
            if(ofp.ShowDialog() == true)
            {
                path = ofp.FileName;
            }
            else
            {
                this.Close();
                return;
            }
            csv = new CSVReader<PersonFull>(path);
            cmbAlter.Items.Add("NONE");
            cmbGeschlecht.Items.Add("NONE");
            foreach(UIElement element in gridy.Children)
            {
                if(element is ComboBox)
                {
                    cmbs.Add(element as ComboBox);
                }
            }
            header = csv.Readheader().ToList();
            
            foreach (string line in header)
            {
                foreach (ComboBox comboBox in cmbs)
                {
                    
                    comboBox.Items.Add(line);
                }
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

            map.Add("vorname", header.FindIndex(x => x == cmbVorname.SelectedItem.ToString()));
            map.Add("nachname", header.FindIndex(x => x == cmbNachname.SelectedItem.ToString()));
            map.Add("geburtsdatum", header.FindIndex(x => x == cmbAlter.SelectedItem.ToString()));
            map.Add("klasse", header.FindIndex(x => x == cmbKlasse.SelectedItem.ToString()));
            map.Add("geschlecht", header.FindIndex(x => x == cmbGeschlecht.SelectedItem.ToString()));
            Persons = csv.Read(map);
          

        }
    }
}
