using EasySave_V2.Model;
using EasySave_V2.ServiceConfirm;
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

namespace EasySave_V2.View
{
    /// <summary>
    /// Logique d'interaction pour Multiple.xaml
    /// </summary>
    public partial class Multiple : Window
    {
        public Multiple()
        {
            InitializeComponent();
        }

        private void Button_Click_Create(object sender, RoutedEventArgs e)
        {
            Create page = new();
            page.Show();
            Close();
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

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            ParaSaveBackUp para = new();
            para.ExecutParallelBackup();
        }


        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {

            string source = MyTextBoxSource.Text;
            string name = MyTextBoxName.Text;

            string trg = MyTextBoxTarget.Text;
            string type = Type.Text;
            lstview.Items.Add(new MyItem { Name = name, SourcePath = source, TargetPath = trg, Type = type });

            MyTextBoxSource.Clear();
            MyTextBoxName.Clear();
            MyTextBoxTarget.Clear();

        }
    }
}