using Kreislauf.Models;
using MvvM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI_Layer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PersonFullViewModel _viewModel;
        private readonly MainViewModel _mainViewModel;


        public MainWindow()
        {

            _mainViewModel = new MainViewModel();
            InitializeComponent();

            //  _viewModel = new PersonFullViewModel();
           
            DataContext = _mainViewModel;

        }

        private void btnStatisticGen_Click(object sender, RoutedEventArgs e)
        {

            StatisticWindow stati = new StatisticWindow();
            stati.Show();

        }

        public void InitializeDataGrid()
        {

            PersonFullViewModel personFullViewModel = new PersonFullViewModel();
            DataContext = personFullViewModel;

            dataGrid.AutoGenerateColumns = false;

            // Create and bind DataGrid columns programmatically
            DataGridTextColumn idColumn = new DataGridTextColumn
            {
                Header = "Id",
                Binding = new System.Windows.Data.Binding("Person.Id")
            };

            DataGridTextColumn vornameColumn = new DataGridTextColumn
            {
                Header = "Vorname",
                Binding = new System.Windows.Data.Binding("Person.Vorname")
            };

            DataGridTextColumn nachnameColumn = new DataGridTextColumn
            {
                Header = "Nachname",
                Binding = new System.Windows.Data.Binding("Person.Nachname")
            };

            DataGridTextColumn klasseColumn = new DataGridTextColumn
            {
                Header = "Klasse",
                Binding = new System.Windows.Data.Binding("Klasse.Name")
            };

            dataGrid.Columns.Add(idColumn);
            dataGrid.Columns.Add(vornameColumn);
            dataGrid.Columns.Add(nachnameColumn);
            dataGrid.Columns.Add(klasseColumn);

            dataGrid.ItemsSource = personFullViewModel.PersonenFull;



        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // InitializeDataGrid();
            
        }

        private void btnBarcodeGen_Click(object sender, RoutedEventArgs e)
            {
            if (_mainViewModel.SelectedPersonFull != null)
            {
                ScanPopUp p = new ScanPopUp(_mainViewModel.SelectedPersonFull);
                p.Show();
            }
            else
            {
                MessageBox.Show("Please select a person first.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HeaderSelection head = new HeaderSelection(@"C:\Users\sandork\Documents\RunStat\Krsilaufdb\bin\Debug\net6.0\fasz.csv");
            head.Show();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Options options = new Options();
            options.Show();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SavePDF save = new SavePDF();
            save.Show();
        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {

            var viewModel = DataContext as MainViewModel;
            if (viewModel?.SelectedPersonFull != null)
            {
                var detailViewModel = new DetailViewModel(viewModel.SelectedPersonFull);
                var detailWindow = new DetailWindow(detailViewModel);
                detailWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select a person first.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

       /* private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedPerson = e.AddedItems[0] as PersonFull;
                if (selectedPerson != null)
                {
                    var detailViewModel = new DetailViewModel(selectedPerson);
                    var detailWindow = new DetailWindow(detailViewModel);
                    detailWindow.Show();
                }
            }
        }*/

        private async void testBtn_Click(object sender, RoutedEventArgs e)
        {
            BarcodeSession w = new BarcodeSession();
            w.Show();
        }

        private async void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            await _mainViewModel.FilterPersonsByNameAsync(tbName.Text);
        }

        
    }
    } 

