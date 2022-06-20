using EasySave_V2.Model;
using EasySave_V2.Service;
using EasySave_V2.ServiceConfirm;
using System;
using System.IO;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace EasySave_V2.View
{

    public partial class List : Window
    {
        public List()
        {
            InitializeComponent();
            var Dictionnary = GlobalLangParam.LangageDictionary;
            ModifyButton.Content = Dictionnary.ModifyBtn;
            RemoveButton.Content = Dictionnary.RemoveBtn;
            CreateButton.Content = Dictionnary.MenuCreate;
            ListButton.Content = Dictionnary.MenuDisplay;
            SettingButton.Content = Dictionnary.MenuSettings;
            ExitButton.Content = Dictionnary.MenuExit;
            HomeButton.Content = Dictionnary.MenuHome;
            Title.Text = Dictionnary.MenuDisplay;
            var ListBackUp = ServiceBackUpList.GetBackUpList();

            foreach (var BackUp in ListBackUp)
            {
                lstview.Items.Add(new MyItem { Name = BackUp.Name, Date = Convert.ToDateTime(BackUp.CreationTime), SourcePath = BackUp.SourcePath, TargetPath = BackUp.DestinationPath, Type = BackUp.BackUpType });
            }
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


        private void Button_Click_OK(object sender, RoutedEventArgs e)
        {
            lstview.Items.Clear();

            //string path = MyTextBox.Text;
            //DirectoryInfo DI = new DirectoryInfo(path);
            //FileInfo[] Files = DI.GetFiles("*");

        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            MyItem myitem = (MyItem)lstview.SelectedItem;
            File.Delete(myitem.SourcePath + myitem.Name);
            //lstview.Items.Remove(myitem);


        }

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            Modify page = new();
            page.Show();
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Pause(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Stop(object sender, RoutedEventArgs e)
        {

        }

        public static IPEndPoint clientep;


        private void Connect_Button(object sender, RoutedEventArgs e)
        {
            Socket socket = SeConnecter();
            Socket client = AccepterConnection(socket);
            EcouterReseau(client);
            Deconnecter(socket);

        }
        public static Socket SeConnecter()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            newsock.Bind(ipep);

            newsock.Listen(10);

            MessageBox.Show("Serveur disponible à l'écoute ...");
            return newsock;

        }
        public Socket AccepterConnection(Socket socket)
        {
            Socket client = socket.Accept();

            clientep = (IPEndPoint)client.RemoteEndPoint;

            //Data.Text = "Connecté avec l'adresse 127.0.0.1 et port 8080";
            MessageBox.Show("Connecté avec l'adresse 127.0.0.1 et port 8080");
            return client;

        }

        public void EcouterReseau(Socket client)
        {
            if (lstview != null)
            {
                string input; int recv;
                string welcome = JsonConvert.SerializeObject(lstview.Items) + "<//<";

                byte[] data = Encoding.UTF8.GetBytes(welcome);

                client.Send(data, data.Length, SocketFlags.None);


                //while (true)
                //{

                //    // data = new byte[1024];

                //    recv = client.Receive(data);

                //    if (Encoding.UTF8.GetString(data, 0, recv) == "exit")

                //        break;

                //    MessageBox.Show("Client: " + Encoding.UTF8.GetString(data, 0, recv));

                //    //input = "oui";

                //    input = Data.Text;

                //    MessageBox.Show("Server : " + input);

                //    client.Send(Encoding.UTF8.GetBytes(input));
                //}

                if (true)
                {

                    // data = new byte[1024];

                    recv = client.Receive(data);

                    //if (Encoding.UTF8.GetString(data, 0, recv) == "exit")

                    //    break;

                    MessageBox.Show("Client: " + Encoding.UTF8.GetString(data, 0, recv));

                    //input = "oui";

                    input = Data.Text;

                    MessageBox.Show("Server : " + input);

                    client.Send(Encoding.UTF8.GetBytes(input));
                }

            }
        }
        public static void Deconnecter(Socket socket)
        {
            MessageBox.Show("Disconnected from 127.0.0.1");

            socket.Close();

            socket.Close();
        }
    }
}
