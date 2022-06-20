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
using System.IO;
using EasySave_V2.Service;
using EasySave_V2.Model;

namespace EasySave_V2.View
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Extfile : Window
    {
        public Extfile()
        {
            InitializeComponent();
            var Dictionnary = GlobalLangParam.LangageDictionary;

            Add.Content = Dictionnary.Addbtn;
            Crypt.Content = Dictionnary.CryptBtn;
            Remove.Content = Dictionnary.RemoveBtn;
            Confirm.Content = Dictionnary.ConfirmBtn;
            Available.Text = Dictionnary.SettingsExtAvailable;
            Selected.Text = Dictionnary.SettingsExtEncrypt;
            string repertory;
            string file;
            string fileToEncrypt;
            string findFile;
            string findFileToEncrypt;
            string[] readFile;

            repertory = @".\Assets\";
            file = "ExtFiles.txt";
            fileToEncrypt = "ExtFilesToEncrypt.txt";

            findFile = repertory + file;
            findFileToEncrypt = repertory + fileToEncrypt;

            readFile = File.ReadAllLines(findFile);

            foreach (string line in readFile)
            {
                string text = line;
                
                LeftListBox.Items.Add(text);

                string alltext = text + text;
            }

            readFile = File.ReadAllLines(findFileToEncrypt);

            foreach (string line in readFile)
            {
                string text = line;

                RightListBox.Items.Add(text);
                File.AppendAllText(@".\Assets\ExtFiles.txt", text);
                var Settings = new Settings(GlobalSettings.Language, GlobalSettings.LogExtension, GlobalSettings.ExtensionToCrypt, GlobalSettings.BusinessSoftwareFollow);

                Settings.UpdateSettingsJson();
                string alltext = text + text;
            }
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            LeftListBox.Items.Add(AddExt.Text);
            AddExt.Clear();
        }


        private void Button_Click_RemoveRight(object sender, RoutedEventArgs e)
        {
            RightListBox.Items.Add(LeftListBox.SelectedItem);
            LeftListBox.Items.Remove(LeftListBox.SelectedItem);
        }

        private void Button_Click_RemoveLeft(object sender, RoutedEventArgs e)
        {
            LeftListBox.Items.Add(RightListBox.SelectedItem);
            RightListBox.Items.Remove(RightListBox.SelectedItem);

        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            //string repertory;
            //string file;
            //string fileToEncrypt;
            //string findFile;
            //string findFileToEncrypt;
            //string[] readFile;

            //repertory = @"C:\Users\taind\Documents\Cesi\Cesi Année 3\Bloc Programmation système\Projet C#\";
            //file = "ExtFiles.txt";
            //fileToEncrypt = "ExtFilesToEncrypt.txt";

            //findFile = repertory + file;
            //findFileToEncrypt = repertory + fileToEncrypt;

            //readFile = File.ReadAllLines(findFile);
            //readFile = File.ReadAllLines(findFileToEncrypt);

            //LeftListBox.ReadLocalValue

            //foreach (LeftListBox.Items in LeftListBox)
            //{

            //}
        }

    }
}
