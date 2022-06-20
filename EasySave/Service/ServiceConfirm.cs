using System;
using System.Windows.Forms;
using EasySave_V2.View;

namespace EasySave_V2.ServiceConfirm
{
    public class Confirm
    {
        public void ConfirmChoice()
        {
            //Exit or return to the main menue

            DialogResult ConfirmChoice = MessageBox.Show("Do you want to exit ?",
              "EasySave_V2",
              MessageBoxButtons.OKCancel);
            if (ConfirmChoice == DialogResult.OK)
            {
                Environment.Exit(0);
            }
        }
    }
}

