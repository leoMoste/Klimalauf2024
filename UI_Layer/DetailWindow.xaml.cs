using Kreislauf.Models;
using MvvM.ViewModel;
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
    /// Interaktionslogik für DetailWindow.xaml
    /// </summary>
    public partial class DetailWindow : Window
    {
        private readonly PersonFull _personFull;

        public DetailWindow()
        {
            InitializeComponent();
        }

        public DetailWindow(DetailViewModel viewModel)
        {
           
            InitializeComponent();
            DataContext = viewModel;
           // BindData();
        }

        private void BindData()
        {
            //lblVorname.Text = _personFull.Person.Vorname;
            //lblNachname.Text = _personFull.Person.Nachname;
            //lblAlter.Text = _personFull.Person.Lebensalter.ToString();
            //lblRunden.Text = _personFull.Lauf.RundenAnzahl.ToString();
            //lblsSchule.Text = _personFull.Schule.Name;
            //lblKlasse.Text = _personFull.Klasse.Name;

          /*  lblVorname.Text = "";
            lblNachname.Text = "";
            lblAlter.Text = "";
            lblRunden.Text = "";
            lblsSchule.Text = "";
            lblKlasse.Text = "";*/
        }
    }
}
