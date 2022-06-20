using EasySave_V2.Model;
using EasySave_V2.Service;
using EasySave_V2.ServiceConfirm;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace EasySave_V2.View
{

    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
            var Dictionnary = GlobalLangParam.LangageDictionary;

            CreateButton.Content = Dictionnary.MenuCreate;
            ListButton.Content = Dictionnary.MenuDisplay;
            SettingButton.Content = Dictionnary.MenuSettings;
            ExitButton.Content = Dictionnary.MenuExit;
            HomeButton.Content = Dictionnary.MenuHome;
            SelectLang.Text = Dictionnary.SettingsLang;
            ChooseExtension.Text = Dictionnary.SettingsEncryptExt;
            ChooseBusiness.Text = Dictionnary.SettingBusiness;
            FolderButton.Content = Dictionnary.ConfirmBtn;
            LogType.Text = Dictionnary.LogType;
            Title.Text = Dictionnary.MenuSettings;

            foreach (var Lang in GlobalLangParam.ListLanguage)
            {
                LangChoice.Items.Add(Lang);
            }
            LangChoice.SelectedItem =GlobalSettings.Language;
        }
        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            Home page = new();
            page.Show();
            Close();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            Create page = new();
            page.Show();
            Close();
        }

        private void Button_Click_List(object sender, RoutedEventArgs e)
        {
            List page = new();
            page.Show();
            Close();
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Confirm exit = new Confirm();
            exit.ConfirmChoice();
        }

        private void Button_Click_Ext(object sender, RoutedEventArgs e)
        {
            Extfile page = new();
            page.Show();
        }

        private void Button_Click_BSList(object sender, RoutedEventArgs e)
        {
            BSList page = new();
            page.Show();
        }
        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            Setting page = new();
            Close();
            page.Show();
        }
        private void LangChoice_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var LangDictionary = File.ReadAllText($"../../../Translate/{LangChoice.SelectedItem.ToString()}.json");

            GlobalSettings.Language = LangChoice.SelectedItem.ToString();
            GlobalLangParam.LangageDictionary = JsonConvert.DeserializeObject<Language>(LangDictionary);

            var Settings = new Settings(GlobalSettings.Language, GlobalSettings.LogExtension, GlobalSettings.ExtensionToCrypt, GlobalSettings.BusinessSoftwareFollow);

            Settings.UpdateSettingsJson();
        }

        private void RadioJSON_Checked(object sender, RoutedEventArgs e)
        {
            GlobalSettings.LogExtension = "json";
            var Settings = new Settings(GlobalSettings.Language, GlobalSettings.LogExtension, GlobalSettings.ExtensionToCrypt, GlobalSettings.BusinessSoftwareFollow);

            Settings.UpdateSettingsJson();
        }

        private void RadioXAML_Checked(object sender, RoutedEventArgs e)
        {
            GlobalSettings.LogExtension = "xml";
            var Settings = new Settings(GlobalSettings.Language, GlobalSettings.LogExtension, GlobalSettings.ExtensionToCrypt, GlobalSettings.BusinessSoftwareFollow);

            Settings.UpdateSettingsJson();
        }
    }
}
