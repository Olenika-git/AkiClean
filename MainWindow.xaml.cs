using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
        //---------------------------------------------------
        //             Declaration Username Variable        |
        //---------------------------------------------------
        public string username = Environment.UserName;

        //---------------------------------------------------
        //     Declaration Temp Windows Folders Variable    |
        //---------------------------------------------------
        public DirectoryInfo tempWin;
        public DirectoryInfo tempApp;
        public DirectoryInfo tempUsrApp;

        //---------------------------------------------------
        //         Declaration Temp Internet Variable       |
        //---------------------------------------------------
        public DirectoryInfo tempFirefox;
        public DirectoryInfo tempGoogle;
        public DirectoryInfo tempEdge;
        public DirectoryInfo tempIE1;
        public DirectoryInfo tempIE2;
        public DirectoryInfo tempSafariHistory;
        public DirectoryInfo tempSafariCache;
        public DirectoryInfo tempSafariCookies;
        //---------------------------------------------------
        //         Declaration LocalVersion Variable        |
        //---------------------------------------------------
        string LocalVersion = "1.0.0";


        public MainWindow()
        {
            InitializeComponent();

            //---------------------------------------------------
            //               Windows Folders Property           |
            //---------------------------------------------------
            tempWin = new DirectoryInfo(@"C:\Windows\Temp");
            tempApp = new DirectoryInfo(System.IO.Path.GetTempPath());
            tempUsrApp = new DirectoryInfo(@"C:\Users\"+username+@"\AppData\Local\Temp");

            //---------------------------------------------------
            //               Internet Folders Property          |
            //---------------------------------------------------
            tempFirefox = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Mozilla\Firefox\Profiles");
            tempGoogle = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Google\Chrome\User Data\Default");
            tempEdge = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Microsoft\Edge");
            tempIE1 = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Microsoft\Windows\WebCache");
            tempIE2 = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Microsoft\Internet Explorer\Recovery");
            tempSafariHistory = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Apple Computer\Safari\");
            tempSafariCache = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Local\Apple Computer\Safari\");
            tempSafariCookies = new DirectoryInfo(@"C:\Users\" + username + @"\AppData\Roaming\Apple Computer\Safari\Cookies\");

            //---------------------------------------------------
            //               News and Update Sections           |
            //---------------------------------------------------
            CheckNews();
            CheckUpdate();
        }

        

        //---------------------------------------------------
        //                   Event on Buttons               |
        //---------------------------------------------------

        //  Update Button
        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            CheckVersion();
        }

        //  History Button
        private void Button_History_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Historique à crée !", "Historique", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        
        //  DeleteWebFiles Button
        private void Button_DeleteWeb_Click(object sender, RoutedEventArgs e)
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

        //  Analyze Button
        private void Button_Analyze_Click(object sender, RoutedEventArgs e)
        {
            AnalyzeFolders();
            titre.Content = "Analyse Effectué";
        }

        //  DeleteFolder Button
        private void Button_DeleteFolders_Click(object sender, RoutedEventArgs e)
        {
            btnCleanFolders.Content = "Nettoyage en cours ...";
            imgCleanFolders.Visibility = Visibility.Hidden;
            titre.Content = "Nettoyage Effectué";
            espace.Content = "0 Mb";
            //Task.Delay(2000).ContinueWith(t => imgCleanFolders.Visibility = Visibility.Visible);

            try
            {
                var nbFiles = DeleteTempData(tempWin); //Delete Files and Folders and get the counter of this deleted items.
                label1.Content = "Fichier Supprimé : ";
                espace.Content = nbFiles + " fichiers";
            }   //  abstract pour définir un moule (classe qui ne s'instancie pas, utilisé pour l'hétitage)
                //  Atribut statique qui est stocké dans la classe et non dans les instances. et methoque statique, pas besoin d'instancier pour l'appeller exemple (classe Console et méthode Rightline)
                //  Constructeur = methode appellée automatiquement lors de l'instanciation d'un objet
                //  propriété raccourcis de l'attribut avec le getter et le setter
            catch (Exception error)
            {

                Console.WriteLine(error + " erreur ");
            }
        }
        //---------------------------------------------------
        //                      Functions                   |
        //---------------------------------------------------

        //  -- Function AnalyzeFolders -- analyze Folders Weight and set a date for the last Analyze
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

        // -- Function DirectorySize - get the weight of a folder --
        public long DirectorySize(DirectoryInfo directory)
        {
            return directory.GetFiles().Sum(files => files.Length) + directory.GetDirectories().Sum(dir => DirectorySize(dir));
        }

        //  -- Function DeleteTempData delete every files and directory in a choosen Directory --
        public static int DeleteTempData(DirectoryInfo directory)
        {
            int countFiles = 0;
            //  Loop to remove each files in the directory
            foreach (FileInfo file in directory.GetFiles())
            {
                try
                {
                    file.Delete();
                    Console.WriteLine(file.FullName);
                    countFiles++; //gérer le compteur avec un passage par référence ref countFiles
                }
                catch (Exception error)
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
                    dir.Delete(true); //true its recursive parameter(delete everything inside the folder)
                    Console.WriteLine(dir.FullName);
                    countFiles++;
                }
                catch (Exception error)
                {
                    Console.WriteLine("Error : " + error);
                    continue;
                }
            }
            return countFiles;
        }

        //  -- Function CheckNews get news on a file stored on a Server --
        public void CheckNews()
        {
            string link = "http://127.0.0.1/AkiClean/News.txt";
            using (WebClient client = new WebClient())
            {
                string News = client.DownloadString(link);

                if(News != string.Empty)
                {
                    news.Content = News;
                    news.Visibility = Visibility.Visible;
                    rectNews.Visibility = Visibility.Visible;
                }
                else
                {
                    news.Visibility = Visibility.Hidden;
                    rectNews.Visibility = Visibility.Hidden;
                }
            }
        }

        //  -- Function CheckUpdate get software update stored on a Server --
        public void CheckUpdate()
        {
            string link = "http://127.0.0.1/AkiClean/Update.txt";
            using (WebClient client = new WebClient())
            {

            }
        }

        //  -- Function CheckVersion get software version stored on a Server --
        public void CheckVersion()
        {
            string link = "http://127.0.0.1/AkiClean/Version.txt";
            using (WebClient client = new WebClient())
            {
                string DistantVersion = client.DownloadString(link);

                if(LocalVersion != DistantVersion)
                {
                    MessageBox.Show("Nouvelle Version Disponible : "+DistantVersion+" !", "Mise à jour . . .", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Logiciel à Jour, Version : "+LocalVersion, "Mise à jour . . .", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
