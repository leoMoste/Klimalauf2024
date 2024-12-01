using Kreislauf.Data;
using Kreislauf.Models;
using Microsoft.EntityFrameworkCore;
using ScottPlot;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UI_Layer
{
    /// <summary>
    /// Interaktionslogik für StatisticWindow.xaml
    /// </summary>
    public partial class StatisticWindow : Window
    {
        private List<string> charts = new List<string> { "Pie", "Scatter", "Bar"};
        TextBox textVal1;
        TextBox textVal2;

        ScottPlot.Plot p;

        public StatisticWindow()
        {

            
            InitializeComponent();
            textVal1 = new TextBox { Width = Val1.Width, Height = Val2.Height, Margin = Val1.Margin };
            charts.ForEach(chart =>
            {
                Chart_Typ.Items.Add(chart);
            });
            p = Statisktik_Plot.Plot;
            
        }

        private void Chart_Typ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Val1.Items.Clear();
            Val2.Items.Clear();
            Gruppen.Items.Clear();
            Gruppen.IsEnabled = true;
            List<Klasse> klasses = getGruppe();
            ComboboxItem item = new ComboboxItem { Text = "Alle Klassen", Value = klasses };
            Gruppen.Items.Add(item); //schule mode / klass mode?
            foreach (Klasse klass in klasses)
            {
                item = new ComboboxItem { Text = klass.Name, Value = klass };
                Gruppen.Items.Add(item);
            }
        }
        

        private void Gruppen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Val1 != null && Val2 != null)
            {
                Val1.IsEnabled = true;
                Val2.IsEnabled = true;
                Val1.Items.Add(new ComboboxItem { Text="Alter", Value = "Lebensalter" });
                Val2.Items.Add("No Second Option");
                Val2.SelectedIndex = 0;
                Val2.Items.Add(new ComboboxItem { Text="Runden", Value = "RundenAnzahl" });
            }
            
        }

        private void Val1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (Val2.SelectedItem != "No Second Option" && Val1.SelectedItem != null)
            {
                makeChart();
            }
            
        }

        private void Val1_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void Gruppen_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private void Val2_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }







        private void makeChart()
        {
            p.Clear();
            ComboboxItem item = Val1.SelectedItem as ComboboxItem;
            var val1 = getVal<Person>(item.Value.ToString());
            var val2 = new List<object>();
            if (Val2.SelectedIndex != 0)
            {
                item = Val2.SelectedItem as ComboboxItem;
                val2 = getVal<Barcode>(item.Value.ToString());
            }
            List<Klasse> klassen = getGruppe();
            switch (Chart_Typ.SelectedIndex) {
                case 0:
                    if (int.TryParse(val2[0].ToString(), out int i))
                    {
                        makePie(val1.ConvertAll<double>(x => Double.Parse(x.ToString())), null, null);
                    }
                    makePie(val1.ConvertAll<double>(x => Double.Parse(x.ToString())), val1.ConvertAll<string>(x => x.ToString()), null);
                    
                    
                    break;
                case 1:
                    makeScatter(val1.ConvertAll<double>(x => Double.Parse(x.ToString())), val2.ConvertAll<double>(x => Double.Parse(x.ToString())), null);
                    break;
                case 2:
                    makeBar(val1.ConvertAll<double>(x => Double.Parse(x.ToString())));
                    break;


                    
            }
          
        }
        private List<object> getVal<T>(string filter) where T: class
        {
            List<T> list;
            using(AppDbContext con = new AppDbContext())
            {
                 list = con.Set<T>().ToList();
            }

            List<object> listfiltered = new List<object>();
            foreach(object val in list)
            {
                listfiltered.Add(val.GetType().GetProperty(filter).GetValue(val));
            }
            return listfiltered;
        }
        private List<Klasse> getGruppe()
        {     
            List<Klasse> klassen = new List<Klasse>();
                using(AppDbContext con = new AppDbContext())
                {
                     klassen = con.Klassen.AsEnumerable().ToList();
                }
                return klassen;
        }
        private class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void Val2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Val2.SelectedItem != "No Second Option" && Val2.SelectedItem != null)
            {
                makeChart();
            }
        }
        private void makePie(List<double> val1, List<string>? val2, List<ScottPlot.Colors>? colors)
        {
            
            p.Add.Pie(val1);
            
        }
        private void makeScatter(List<double> val1, List<double> val2, ScottPlot.Color? color)
        {
             
            color = color.GetValueOrDefault();
            p.Add.Scatter(val1, val2, color);
        }
        private void makeBar(List<double> val1)
        {
            List<Bar> bars = new List<Bar>();
            foreach (double val in val1)
            {
                bars.Add(new Bar {Value = val});
            }
            p.Add.Bars(val1.ToArray());
            p.Axes.Margins(bottom: 0);
            
        }

       
    }
}
