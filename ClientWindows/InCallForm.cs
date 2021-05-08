﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shared;

namespace ClientWindows
{
    public delegate void ByteCallback(byte[] b);
    public partial class InCallForm : Form
    {

        private Id callId = null;
        private Boolean callStopped = false;
        private Call call = null;

        private int i = 0;


        public InCallForm()
        {
            InitializeComponent();
        }

        private void updateUsersInCall()
        {
            if(callUsersList_label.InvokeRequired)
            {
                this.callUsersList_label.Invoke(new MethodInvoker(() => { updateUsersInCall(); }));
                return;
            }
            String usersListStr = "";
            foreach (String u in call.usernames)
            {
                if (!usersListStr.Equals(""))
                {
                    usersListStr += ", ";
                }
                usersListStr += u;
            }
            this.callUsersList_label.Text = usersListStr;
        }

        public InCallForm(Call c)
        {
            
            this.call = c;
            this.callId = new Id(c.callId);
            InitializeComponent();
            //Task.Run(refreshFormLoop);
            //LoggedInForm.updateCallStatus();
            this.Text = "(" + Program.username + ") Aktywne połączenie";
            StringCallback callback2 = addUser;
            StringCallback callback3 = removeUser;
            LoggedInService.AddUserToCall = callback2;
            LoggedInService.RemoveUserFromCall = callback3;


            //Task.Run(progBarIncrease);
            updateUsersInCall();
            ByteCallback callback = incomingTraffic;
            CallProcessing.ReceiveMsgCallback = callback;
            CallProcessing.Start();
            Task.Run(sentBytes);
            Program.isInCall = true;
        }

        /*
        public InCallForm(Id id, Username user)
        {
            this.callId = id;
            InitializeComponent();
            this.Text = "("+ Program.username + ") Aktywne połączenie";
            this.callUsersList_label.Text = user.username;
            //Task.Run(progBarIncrease);

            ByteCallback callback = incomingTraffic;
            CallProcessing.ReceiveMsgCallback = callback;
            CallProcessing.Start();
            Task.Run(sentBytes);
            Program.isInCall = true;
        }
        */

        private void refreshFormLoop()
        {
            while (true)
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => { refreshForm(); }));
                    return;
                } else
                {
                    refreshForm();
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        public void refreshForm()
        {
            this.Refresh();
        }

        public void removeUser(string username)
        {
            this.call.removeUser(username);
            updateUsersInCall();
            if (this.call.usernames.Count == 0)
            {
                this.Close();
            }
        }

        public void addUser(string username)
        {
            this.call.addUser(username);
            updateUsersInCall();
        }

        private void InCallForm_Load(object sender, EventArgs e)
        {

        }

        private void leaveCall_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InCallForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.callId == null) return;
            callStopped = true;
            
            CallProcessing.SendMessages(BitConverter.GetBytes(callId.id));
            CallProcessing.SendMessages(BitConverter.GetBytes(callId.id));
            CallProcessing.SendMessages(BitConverter.GetBytes(callId.id));
            CallProcessing.Stop();
            LoggedInService.declineCall(this.callId);
            Program.isInCall = false;
        }

        private void incomingTraffic_label_Click(object sender, EventArgs e)
        {
            
        }

        /*private void progBarIncrease()
        {
            if(incomingTraffic_bar.InvokeRequired)
            {
                incomingTraffic_bar.Invoke(new MethodInvoker(() => { progBarIncrease(); }));
                return;
            }
            incomingTraffic_bar.Visible = true;
            incomingTraffic_bar.Minimum = 0;
            incomingTraffic_bar.Maximum = 1;
            incomingTraffic_bar.Value = 1;
            incomingTraffic_bar.Step = 1;
            while(true)
            {
                if(incomingTraffic_bar.Value==1)
                    incomingTraffic_bar.Value = 0;
                else
                    incomingTraffic_bar.PerformStep();
                System.Threading.Thread.Sleep(500);
            }
            
        }*/

        public void incomingTraffic(byte[] b)
        {
            if (incomingMsg_label.InvokeRequired)
            {
                incomingMsg_label.Invoke(new MethodInvoker(() => { incomingTraffic(b); }));
            }
            else
            {
                
                incomingMsg_label.Text = Encoding.ASCII.GetString(b);
            }
        }

        public void sentBytes()
        {
            do
            {
                char b = 'Z';
                switch (i)
                {
                    case 0:
                        b = 'A';
                        break;
                    case 1:
                        b = 'B';
                        break;
                    case 2:
                        b = 'C';
                        break;
                    case 3:
                        b = 'D';
                        break;
                }

                CallProcessing.SendMessages(Encoding.ASCII.GetBytes(Program.username + ": " + b));
                i++;
                if (i == 4)
                {
                    i = 0;
                }
                System.Threading.Thread.Sleep(250);
            } while (callStopped==false);
        }

        private void callUsersList_label_Click(object sender, EventArgs e)
        {

        }
    }
}