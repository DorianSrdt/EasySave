using EasySave_V2.Model;
using EasySave_V2.Service;
using EasySave_V2.ServiceConfirm;
using System;
using System.Windows;

namespace EasySave_V2.View
{
    public partial class Create : Window
    {
        public Create()
        {
            InitializeComponent();
            var Dictionnary = GlobalLangParam.LangageDictionary;

            Title.Text = Dictionnary.MenuCreate;
            CreateButton.Content = Dictionnary.MenuCreate;
            ListButton.Content = Dictionnary.MenuDisplay;
            SettingButton.Content = Dictionnary.MenuSettings;
            ExitButton.Content = Dictionnary.MenuExit;
            HomeButton.Content = Dictionnary.MenuHome;
            NameText.Text = Dictionnary.CreateName;
            SourceText.Text = Dictionnary.CreateSource;
            DestinationPath.Text = Dictionnary.CreateTarget;
            RadioComplete.Content = Dictionnary.TypeComplete;
            RadioDifferential.Content = Dictionnary.TypeDifferential;
            BrowserButton1.Content = Dictionnary.BrowserBtn;
            BrowserButton2.Content = Dictionnary.BrowserBtn;
            ConfirmButton.Content = Dictionnary.ConfirmBtn;
        }



        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            Home page = new();
            page.Show();
            Close();
        }

        private void Button_Click_List(object sender, RoutedEventArgs e)
        {
            List page = new();
            page.Show();
            Close();
        }
        private void Button_Click_Setting(object sender, RoutedEventArgs e)
        {
            Setting page = new();
            page.Show();
            Close();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Confirm exit = new Confirm();
            exit.ConfirmChoice();
        }

        private void Button_Click_Browser1(object sender, RoutedEventArgs e)//
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                SourcePath.Text = dialog.SelectedPath;
            }
        }


        private void Button_Click_Browser2(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                TargetPath.Text = dialog.SelectedPath + "\\";
            }
        }

        private void Button_Click_MultipleBK(object sender, RoutedEventArgs e)
        {
            Multiple page = new();
            page.Show();
            Close();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(BackUpName.Text) && !String.IsNullOrEmpty(SourcePath.Text) && !String.IsNullOrEmpty(TargetPath.Text))
            {
                if (!BusinessSoftwareDetection.Detection())
                {
                    if (RadioComplete.IsChecked == true)
                    {
                        CompleteBackUp NewBackUp = new(BackUpName.Text, SourcePath.Text, TargetPath.Text);
                        NewBackUp.MakeBackup();
                    }
                    else
                    {
                        DifferentialBackUp NewBackUp = new(BackUpName.Text, SourcePath.Text, TargetPath.Text);
                        NewBackUp.MakeBackup();
                    }
                }

            }

        }

    }
}