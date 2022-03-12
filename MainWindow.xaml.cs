using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        //  Declaration Username Variable
        public string username = Environment.UserName;

        //  Declaration Temp Windows Folders Variable
        public DirectoryInfo tempWin;
        public DirectoryInfo tempApp;
        public DirectoryInfo tempUsrApp;

        //  Declaration Temp Internet Navigator Variable
        public DirectoryInfo tempFirefox;
        public DirectoryInfo tempGoogle;
        public DirectoryInfo tempEdge;
        public DirectoryInfo tempIE1;
        public DirectoryInfo tempIE2;
        public DirectoryInfo tempSafariHistory;
        public DirectoryInfo tempSafariCache;
        public DirectoryInfo tempSafariCookies;

        public MainWindow()
        {
            InitializeComponent();

            //  Temp Windows Folders
            tempWin = new DirectoryInfo(@"C:\Windows\Temp");
            tempApp = new DirectoryInfo(System.IO.Path.GetTempPath());
            tempUsrApp = new DirectoryInfo(@"C:\Users\"+username+@"\AppData\Local\Temp");

            //  Internet Nagivator Folders
            tempFirefox = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Mozilla\Firefox\Profiles");
            tempGoogle = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Google\Chrome\User Data\Default");
            tempEdge = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Microsoft\Edge");
            tempIE1 = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Microsoft\Windows\WebCache");
            tempIE2 = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Microsoft\Internet Explorer\Recovery");
            tempSafariHistory = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Apple Computer\Safari\");
            tempSafariCache = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Apple Computer\Safari\");
            tempSafariCookies = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Apple Computer\Safari\Cookies\");
        }

        // Function to get the weight of a folder
        public long DirectorySize(DirectoryInfo directory)
        {
            return directory.GetFiles().Sum(files => files.Length) + directory.GetDirectories().Sum(dir => DirectorySize(dir));
        }

        //  Function to delete every files in a choosen Directory
        public static void DeleteTempData(DirectoryInfo directory)
        {
            //  Loop to remove each files in the directory
            foreach (FileInfo file in directory.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                    //totalFilesRemoved++;
                }
                catch(Exception error)
                {
                    Console.WriteLine("Error : " + error);
                    continue;
                }
            }
            //  Loop to remove each directory in the directory
            foreach (DirectoryInfo dir in directory.GetDirectories())
            {
                try
                {
                    dir.Delete(true);
                    Console.WriteLine(dir.FullName);
                }
                catch (Exception error)
                {
                    Console.WriteLine("Error : "+error);
                   continue;
                }
            }
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

        private void Button_Analyze_Click(object sender, RoutedEventArgs e)
        {
            AnalyzeFolders();
        }
        //---------------------------------------------------

        //  Function to analyze Folders Weight and date for last Analyze
        public void AnalyzeFolders()
        {
            Console.WriteLine("Début de l'analyse ...");
            long totalSize = 0;

            try
            {
                totalSize = (DirectorySize(tempWin) / 1000000) + (DirectorySize(tempApp) / 1000000) + (DirectorySize(tempUsrApp) / 1000000);
            }
            catch (Exception error)
            {

                Console.WriteLine("Error : Folder Analyze Denied " + error.Message);
            }
            
            espace.Content = totalSize + " Mb";
            date.Content = DateTime.Now;
        }
    }
}
