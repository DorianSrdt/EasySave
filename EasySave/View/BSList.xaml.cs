using EasySave_V2.Service;
using EasySave_V2.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace EasySave_V2.View
{
    /// <summary>
    /// Logique d'interaction pour BSList.xaml
    /// </summary>
    public partial class BSList : Window
    {
        public BSList()
        {
            InitializeComponent();


            string repertory;
            string file;
            string fileToFollow;
            string findFile;
            string findFileToFollow;
            string[] readFile;
            var Dictionnary = GlobalLangParam.LangageDictionary;

            Add.Content = Dictionnary.Addbtn;
            AddBS.Content = Dictionnary.Addbtn;
            Remove.Content = Dictionnary.RemoveBtn;
            Confirm.Content = Dictionnary.ConfirmBtn;
            Available.Text = Dictionnary.SettingsBusinessAvailable;
            Selected.Text = Dictionnary.SettingsBusinessSelect;

            repertory = @".\Assets\";
            file = "BSList.txt";
            fileToFollow = "BSListToFollow.txt";

            findFile = repertory + file;
            findFileToFollow = repertory + fileToFollow;

            readFile = File.ReadAllLines(findFile);

            foreach (string line in readFile)
            {
                string text = line;

                LeftListBox.Items.Add(text);

                string alltext = text + text;
            }

            readFile = File.ReadAllLines(findFileToFollow);

            foreach (string line in readFile)
            {
                string text = line;

                RightListBox.Items.Add(text);
                File.AppendAllText(@".\Assets\BSList.txt", text);
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

        }
    }
}
