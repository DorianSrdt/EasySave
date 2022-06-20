using System.Windows;
using EasySave_V2.Service;
using EasySave_V2.ServiceConfirm;

namespace EasySave_V2.View
{

    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            var Dictionnary = GlobalLangParam.LangageDictionary;
            WelcomeText.Text = Dictionnary.HomeMessage;
            CreateButton.Content = Dictionnary.MenuCreate;
            ListButton.Content = Dictionnary.MenuDisplay;
            SettingButton.Content = Dictionnary.MenuSettings;
            ExitButton.Content = Dictionnary.MenuExit;
            HomeButton.Content = Dictionnary.MenuHome;
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
    }
}
