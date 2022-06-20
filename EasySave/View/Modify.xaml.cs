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
using EasySave_V2.Model;
using EasySave_V2.Service;

namespace EasySave_V2.View
{
    /// <summary>
    /// Logique d'interaction pour Modify.xaml
    /// </summary>
    public partial class Modify : Window
    {
        public Modify()
        {
            InitializeComponent();
            var Dictionnary = GlobalLangParam.LangageDictionary;
            Nametext.Text = Dictionnary.ChangeName;
            TargetText.Text = Dictionnary.ChangeTarget;
            Type.Text = Dictionnary.Type;
            RadioComplete.Content = Dictionnary.TypeComplete;
            RadioDifferential.Content = Dictionnary.TypeDifferential;
            OkBtn1.Content = Dictionnary.OkBtn;
            OkBtn2.Content = Dictionnary.OkBtn;
            BrowserButton2.Content = Dictionnary.BrowserBtn;
            //Chemin d'affichage par défaut
            string Path = @".\Assets\";
            DirectoryInfo DI = new DirectoryInfo(Path);
            FileInfo[] Files = DI.GetFiles("*");
            //Completer le tableau 
            foreach (FileInfo file in Files)
            {
                listview.Items.Add(new MyItem { Name = file.Name, Date = file.CreationTime, SourcePath = Path, TargetPath = "r", Type = "g" });
            }
        }

        public void RenameFile(object sender, RoutedEventArgs e)
        {
            MyItem myitem = (MyItem)listview.SelectedItem;
            string pathbackup = myitem.SourcePath;
            string oldName = @myitem.SourcePath + myitem.Name;
            string newName = myitem.SourcePath + TextboxName.Text;

            //File.Move(oldName, newName);
            MessageBox.Show("Your backup as been renamed");
        }

        public void MoveFile(object sender, RoutedEventArgs e)
        {
            MyItem myitem = (MyItem)listview.SelectedItem;
            string sourceFile = @myitem.SourcePath + myitem.Name;
            string destinationFile = TextboxTarget.Text + myitem.Name;

            // To move a file or folder to a new location:
            System.IO.File.Move(sourceFile, destinationFile);
            MessageBox.Show("Your backup as been moved");
        }

        private void Button_Click_Browser(object sender, RoutedEventArgs e)
        {
            var dialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            if (dialog.ShowDialog(this).GetValueOrDefault())
            {
                TextboxTarget.Text = dialog.SelectedPath + "\\";
            }
        }

        private void Button_Click_Return(object sender, RoutedEventArgs e)
        {
            List page = new();
            page.Show();
            Close();
        }
    }
 }