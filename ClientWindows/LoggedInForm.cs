﻿using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace ClientWindows
{

    public delegate void StringCallback(String message);
    public delegate void InvitationCallback(Invitation inv);
    public delegate void UsernameCallback(Username u);
    public delegate void IdCallback(Id id);
    public partial class LoggedInForm : Form
    {
        //public static List<Friend> callbackFriendsContainer;

        private static List<Friend> friendsContainer = new List<Friend>();
        private static List<Invitation> invitationContainer = new List<Invitation>();
        private static Id lastCallId = null;
        private Hashtable friendListDetails = new Hashtable(){
            {"friendsStart", 0},
            {"friendsAmount", 0},
            {"invitationsStart", 0},
            {"invitationsAmount", 0}
        };
        public LoggedInForm()
        {
            InitializeComponent();

            UsernameCallback callback4 = newInactiveFriendFunc;
            UsernameCallback callback5 = newActiveFriendFunc;
            IdCallback callback6 = callUserReply;
            BooleanCallback callback7 = callUserReplyFromUser;
            LoggedInService.NewInactiveFriend = callback4;
            LoggedInService.NewActiveFriend = callback5;
            LoggedInService.InviteToConversationReplyOk = callback6;
            LoggedInService.InviteToConversationReplyFromUser = callback7;

            StringCallback callback2 = writeToInvitingList;
            LoggedInService.NewInvitationCallback = callback2;
            this.signedInLogin_Text.Text = Program.username;

            StringCallback callback = writeToFriendContainer;
            LoggedInService.GetFriendsCallback = callback;

            InvitationCallback callback3 = removeFromInvitingList;
            LoggedInService.InvitationProcessedCallback = callback3;

            LoggedInService.getFriends();
        }

        public void updateInvitationButton()
        {
            if (this.invitingList_button.InvokeRequired)
            {
                friendsList.Invoke(new MethodInvoker(() => { updateInvitationButton(); }));
                return;
            }
            else
            {
                int amount = 0;
                foreach (Invitation inv in invitationContainer)
                {
                    if (inv.status < 2)
                    {
                        amount++;
                    }
                }
                this.invitingList_button.Text = "Zaproszenia (" + amount.ToString() + ")";
            }
        }

        public void updateFriendList()
        {
            if (friendsList.InvokeRequired)
            {
                friendsList.Invoke(new MethodInvoker(() => { updateFriendList(); }));
                return;
            }
            else
            {
                friendListDetails["friendsStart"] = 0;
                friendListDetails["friendsAmount"] = 0;
                friendListDetails["invitationsStart"] = 0;
                friendListDetails["invitationsAmount"] = 0;
                friendsList.Items.Clear();
                //friendsList.Items.Add("ZNAJOMI:");
                int i = 1;
                if (friendsContainer == null || friendsContainer.Count == 0)
                {
                    i++;
                    //friendsList.Items.Add("- Nie masz znajomych");
                }
                else
                {
                    foreach (Friend fr in friendsContainer)
                    {
                        i++;
                        friendListDetails["friendsAmount"] = (int)friendListDetails["friendsAmount"] + 1;
                        friendsList.Items.Add(fr);
                    }
                }
                friendListDetails["invitationsStart"] = i;
                //friendsList.Items.Add("ZAPROSZENIA:");
                /*
                if (invitationContainer == null || invitationContainer.Count == 0)
                {
                    friendsList.Items.Add("- Nie masz zaproszeń");
                }
                else {
                    foreach (Invitation inv in invitationContainer)
                    {
                        friendListDetails["invitationsAmount"] = (int)friendListDetails["invitationsAmount"]+1;
                        friendsList.Items.Add(inv);
                    }
                }
                */
                return;
            }
        }

        public void writeToFriendContainer(String message)
        {
            String[] replySplit = message.Split(new String[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
            String frListStr = replySplit[1].Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries)[1];
            List<Friend> friends = MessageProccesing.DeserializeObjectOnErrorCode(Options.GET_FRIENDS, frListStr) as List<Friend>;
            friendsContainer.Clear();
            foreach (Friend fr in friends)
            {
                friendsContainer.Add(fr);
            }

            updateFriendList();
            //MessageBox.Show("Friends updated v2");

            //if (null == friendsContainer) return;

            // also make this threadsafe:
            /*if (callbackFriendsContainer.InvokeRequired)
            {
                callbackFriendsContainer.Invoke(new MethodInvoker(() => { writeToFriendList(message); }));
            }
            else
            {
                String[] replySplit = message.Split(new String[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
                String frListStr = replySplit[1].Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries)[1];
                List<Friend> friends = MessageProccesing.DeserializeObject(Options.GET_FRIENDS, frListStr) as List<Friend>;
                callbackFriendsContainer.Items.Clear();
                friendsContainer.Clear();
                if (friends == null || friends.Count == 0)
                {
                    callbackFriendsContainer.Items.Add("Nie masz znajomych");
                    return;
                }

                foreach (Friend fr in friends)
                {
                    friendsContainer.Add(fr);
                    callbackFriendsList.Items.Add(fr.username);
                }
            */
            updateFriendList();
                //callbackFriendsList.Items.Add(s);
                //callbackFriendsList.TopIndex = callbackFriendsList.Items.Count - (callbackFriendsList.Height / callbackFriendsList.ItemHeight);
            //}
        }

        public void addToFriendContainer(Friend fr)
        {
            friendsContainer.Add(fr);
            updateFriendList();
        }

        public void writeToInvitingList(String message)
        {
            String[] replySplit = message.Split(new String[] { "$$" }, StringSplitOptions.RemoveEmptyEntries);
            String frListStr = replySplit[1].Split(new char[] { ':' }, 2, StringSplitOptions.RemoveEmptyEntries)[1];
            List<Invitation> invitations = MessageProccesing.DeserializeObject(Options.FRIEND_INVITATIONS, frListStr) as List<Invitation>;
            foreach (Invitation inv in invitations)
            {
                if(inv.status<2)
                {
                    invitationContainer.Add(inv);
                } else if(inv.status==2)
                {
                    Friend newFr = new Friend(inv.inviteeUsername, 1);
                    friendsContainer.Add(newFr);
                }
            }
            updateInvitationButton();
            //MessageBox.Show("Invitations updated v2");
        }

        public void removeFromInvitingList(Invitation inv)
        {
            invitationContainer.Remove(inv);
            if(inv.status==2)
            {
                friendsContainer.Add(new Friend(inv.username, 1));
            }
            updateInvitationButton();

        }

        public void newActiveFriendFunc(Username u)
        {
            foreach(Friend fr in friendsContainer)
            {
                if (fr.username == u.username)
                    fr.active = 1;
            }
            updateFriendList();
            updateFriendWindowStatus();
        }

        public void newInactiveFriendFunc(Username u)
        {
            foreach (Friend fr in friendsContainer)
            {
                if (fr.username == u.username)
                    fr.active = 0;
            }
            updateFriendList();
            updateFriendWindowStatus();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void openFriend_button_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void friendList_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index == -1) return;
            ListBox lb = sender as ListBox;
            if (lb.Items.Count < 1) return;
            
            Friend fr = lb.Items[e.Index] as Friend;
            if (fr == null) return;

            SizeF txt_size = e.Graphics.MeasureString(fr.username, this.Font);

            e.ItemHeight = (int)txt_size.Height + 2*5;
            e.ItemWidth = (int)txt_size.Width;
        }

        private void friendList_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) return;
            // Get the ListBox and the item.
            ListBox lst = sender as ListBox;
            if (lst.Items.Count < 1) return;
            Friend fr = lst.Items[e.Index] as Friend;
            if (fr == null) return;

            SolidBrush redDot = new SolidBrush(Color.Red);
            SolidBrush greenDot = new SolidBrush(Color.Green);

            // Draw the background.
            e.DrawBackground();


            e.DrawFocusRectangle();
            //e.Graphics.DrawEllipse(Pens.Blue, new Rectangle(1, 1 + e.Index * 15, 100, 10));
            //e.Graphics.DrawString(l.Items[e.Index].ToString(), new Font(FontFamily.GenericSansSerif, 9, FontStyle.Regular), Brushes.Red, e.Bounds);


            // See if the item is selected.
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                if(fr.active==1)
                    e.Graphics.FillEllipse(greenDot, 4, e.Bounds.Top+4, 15, 15);
                else
                    e.Graphics.FillEllipse(redDot, 4, e.Bounds.Top+4, 15, 15);

                e.Graphics.DrawString(fr.username, this.Font, SystemBrushes.HighlightText, 23, e.Bounds.Top+5);
            }
            else
            {
                // Not selected. Draw with ListBox's foreground color.
                using (SolidBrush br = new SolidBrush(e.ForeColor))
                {
                    if (fr.active == 1)
                        e.Graphics.FillEllipse(greenDot, 4, e.Bounds.Top+4, 15, 15);
                    else
                        e.Graphics.FillEllipse(redDot, 4, e.Bounds.Top+4, 15, 15);

                    e.Graphics.DrawString(fr.username, this.Font, br, 23, e.Bounds.Top+5);
                }
            }

            // Draw the focus rectangle if appropriate.
            e.DrawFocusRectangle();
        }

        private void closeApp_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoggedInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoggedInService.logout();
        }

        private void addFriend_button_Click(object sender, EventArgs e)
        {
            AddFriendForm aff = new AddFriendForm();
            aff.ShowDialog();
        }

        private void showFriendContext()
        {
            activeUserWindow.Visible = true;
            activeFriendStatus_Label.Visible = true;
            callUser.Visible = true;
        }

        private void friendsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;

            /*int startFriends = (int)friendListDetails["friendsStart"]+1;
            int endFriends = (int)friendListDetails["friendsStart"] + (int)friendListDetails["friendsAmount"];
            int startInvitations = (int)friendListDetails["invitationsStart"]+1;
            int endInvitations = (int)friendListDetails["invitationsStart"] + (int)friendListDetails["invitationsAmount"];

            if (lb.SelectedIndex==0||
                lb.SelectedIndex==(int)friendListDetails["friendsStart"]||
                lb.SelectedIndex == (int)friendListDetails["invitationsStart"]) { return; }
            
            if(lb.SelectedIndex>=startFriends&&lb.SelectedIndex<=endFriends)
            {*/
                Friend fr = (Friend)lb.Items[lb.SelectedIndex];
                activeUserWindow.Text = fr.username;
                callUser.Enabled = (fr.active==1);
                if (callUser.Enabled)
                {
                    activeFriendStatus_Label.Text = "Aktywny";
                    activeFriendStatus_Label.ForeColor = Color.Green;
                }
                else
                {
                    activeFriendStatus_Label.Text = "Nieaktywny";
                    activeFriendStatus_Label.ForeColor = Color.Red;
                }
            showFriendContext();
            /*}
            if (lb.SelectedIndex >= startInvitations && lb.SelectedIndex <= endInvitations)
            {
                Invitation inv = (Invitation)lb.Items[lb.SelectedIndex];
                SelectInvitationForm sif = new SelectInvitationForm(inv);
                sif.ShowDialog();
            }*/
        }

        private void invitingList_button_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void updateFriendWindowStatus()
        {
            foreach(Friend fr in friendsContainer)
            {
                if(fr.username==activeUserWindow.Text)
                {
                    callUser.Enabled = (fr.active == 1);
                    if(callUser.Enabled)
                    {
                        activeFriendStatus_Label.Text = "Aktywny";
                        activeFriendStatus_Label.ForeColor = Color.Green;
                    } else
                    {
                        activeFriendStatus_Label.Text = "Nieaktywny";
                        activeFriendStatus_Label.ForeColor = Color.Red;
                    }
                    return;
                }
            }
        }

        private void callUser_Click(object sender, EventArgs e)
        {
            LoggedInService.inviteToConversation(new Username(activeUserWindow.Text));
        }

        public void callUserReply(Id callId)
        {
            if (callingStatusLabel.InvokeRequired)
            {
                callingStatusLabel.Invoke(new MethodInvoker(() => { callUserReply(callId); }));
            }
            else
            {
                lastCallId = callId;
                callUser.Enabled = false;
                callingStatusLabel.Visible = true;
                callingStatusLabel.Text = "Oczekuje na odpowiedź...";
                callingStatusLabel.ForeColor = Color.Green;
            }
        }

        public void callUserReplyFromUser(Boolean reply)
        {
            if (callingStatusLabel.InvokeRequired)
            {
                callingStatusLabel.Invoke(new MethodInvoker(() => { callUserReplyFromUser(reply); }));
            }
            else
            {
                callingStatusLabel.Visible = true;
                if (reply==true)
                {
                    callUser.Enabled = false;
                    callingStatusLabel.Text = "Zaakceptowano!";
                    callingStatusLabel.ForeColor = Color.Green;
                    if (reply)
                    {
                        InCallForm icf = new InCallForm(lastCallId);
                        icf.ShowDialog();
                    }
                } else
                {
                    callUser.Enabled = true;
                    callingStatusLabel.Text = "Odrzucono połączenie!";
                    callingStatusLabel.ForeColor = Color.Red;
                }
                
            }
        }

        private void LoggedInForm_Load(object sender, EventArgs e)
        {

        }
    }
}