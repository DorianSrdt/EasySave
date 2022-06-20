using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.IO;

using MessageBox = System.Windows.Forms.MessageBox;

namespace EasySave_V2.Service
{
    class BusinessSoftwareDetection
    {

        public static bool Detection()
        {
            bool logicielMetier = false;
            string repertory;
            string fileToFollow;
            string findFileToFollow;
            string[] readFile;

            repertory = @".\Assets\";
            fileToFollow = "BSListToFollow.txt";

            findFileToFollow = repertory + fileToFollow;

            readFile = File.ReadAllLines(findFileToFollow);

            foreach (string line in readFile)
            {

                Process[] processlist = Process.GetProcesses();
                foreach (Process theprocess in processlist)
                {
                    string text = theprocess.ProcessName;
                    bool contains = text.Contains(line);
  
                    if (contains)
                    {
                        logicielMetier = true;
                    }
                }
            }

            // detection if an app is running
            if (logicielMetier == true)
            {
                DialogResult BusinessSoftware = MessageBox.Show("Business Software detected, pause process", "EasySave_V2", MessageBoxButtons.OK);
            }
            return logicielMetier;
        }
    }
}
