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
using Microsoft.EntityFrameworkCore;
using Repo.models;

namespace TanciskolaGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VizsgaContext tarolo = new VizsgaContext();
        public MainWindow()
        {
            InitializeComponent();
            cb1.ItemsSource = tarolo.Tancs.OrderBy(x => x.TancTipus).ToList();
            cb1.DisplayMemberPath = "TancTipus";
            cb2.IsEnabled = false;
            l2.IsEnabled = false;
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lb1.Items.Clear();
            var selTanc = cb1.SelectedItem as Tanc;

            if (selTanc != null)
            {
                l2.IsEnabled = true;
                cb2.IsEnabled = true;
                var tanarok1 = tarolo.Orarends
                    .Include(x => x.TancNavigation)
                    .Include(x => x.Tanar1Navigation)
                    .Where(x => x.Tanc == selTanc.TancId)
                    .Select(x => x.Tanar1Navigation)
                    .OrderBy(x => x.Nev);
                ;

                //var tanarok2 = tarolo.Orarends
                //    .Include(x => x.TancNavigation)
                //    .Include(x => x.Tanar2Navigation)
                //    .ToList().Where(x => x.Tanc == selTanc.TancId)
                //    .Select(x => x.Tanar2Navigation)
                //    .OrderBy(x => x.Nev);
                //    ;
                //cb2.ItemsSource = tanarok1.Union(tanarok2).Distinct();

                cb2.ItemsSource = tanarok1.Distinct().ToList();

                cb2.DisplayMemberPath = "Nev";
            }
        }

        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selTanc = cb1.SelectedItem as Tanc;
            var selTanar = cb2.SelectedItem as Tanar;

            if (selTanar != null) 
            {
                lb1.Items.Clear();
                var x = tarolo.Orarends
                    .Include(x => x.TancNavigation)
                    .Include(x => x.Tanar1Navigation)
                    .Include(x => x.SzintNavigation)
                    .Where(x => x.Tanc == selTanc.TancId && x.Tanar1 == selTanar.TanarId );
                foreach (var item in x)
                {
                    lb1.Items.Add($"{item.Nap} {item.KezdoIdopont}, {item.Hossz}, {item.SzintNavigation.Kategoria}");
                }
                
            }
        }
    }
}