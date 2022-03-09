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

namespace AkiClean
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //  Event on Button
        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logiciel à jour !", "Mise à jour . . .",MessageBoxButton.OK,MessageBoxImage.Information);

        }

        private void Button_History_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Historique à crée !", "Historique", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Web_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("www.google.com")
                {
                    UseShellExecute = true
                });
            }
            catch (Exception error)
            {

                Console.WriteLine("Erreur : " +error.Message);
            }
        }
        //---------------------------------------------------
    }
}
