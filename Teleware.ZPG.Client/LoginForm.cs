﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin.SkinControl;

namespace Teleware.ZPG.Client
{
    public partial class LoginForm : SkinForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            SetLoginUsers();
        }

        //加载Id下拉框
        Random rnd = new Random();
        private void SetLoginUsers()
        {
            menuUsers.Height = 6;
            int menuItemHeight = 30;
            for (int i = 0; i < 6; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.AutoSize = false;
                item.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
                item.Size = new System.Drawing.Size(182, menuItemHeight);
                item.Tag = rnd.Next(1000, 10000).ToString();
                item.Text = item.Tag.ToString();
                item.TextAlign = ContentAlignment.BottomCenter;
                item.Click += new EventHandler(item_Click);
                menuUsers.Height += menuItemHeight;
                menuUsers.Items.Add(item);
            }
        }

        //状态菜单中的Item选择事件
        void item_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            txtId.SkinTxt.Text = item.Tag.ToString();
        }

        private bool loginFlag = true;
        private void btnDl_Click(object sender, EventArgs e)
        {
            if (loginFlag)
            {
                this.InvalidateWhenLoginError();
            }
            else
            {
                this.InvalidateWhenLoginSuccess();
            }
            
            loginFlag = !loginFlag;

            new MainForm().Show();
            this.Hide();
        }

        private void InvalidateWhenLoginSuccess()
        {
            this.panelError.Visible = false;
            this.imgLoadding.Visible = true;
        }

        private void InvalidateWhenLoginError()
        {
            this.imgLoadding.Visible = false;
            this.panelError.Visible = true;
        }

        private void btn_allowUp_Click(object sender, EventArgs e)
        {
            this.InvalidateWhenLoginSuccess();
            loginFlag = !loginFlag;
        }

        private bool keyBoardFormShow = false;
        private void txtPwd_IconClick(object sender, EventArgs e)
        {
            if (!keyBoardFormShow)
            {
                var keyBoardFrom = new KeyBoardForm(this.Left + txtPwd.Left - 25, this.Top + txtPwd.Bottom,this.txtPwd.SkinTxt);
                keyBoardFrom.Show(this);
            }
            
            keyBoardFormShow = !keyBoardFormShow;
        }

        private void btnId_MouseDown(object sender, MouseEventArgs e)
        {
            menuUsers.Show(txtId, 1, txtId.Height + 1);
            btnId.StopState = StopStates.Pressed;
            btnId.Enabled = false;
            txtId.MouseState = CCWin.SkinClass.ControlState.Hover;
        }

        private void btnId_MouseEnter(object sender, EventArgs e)
        {
            txtId.MouseState = CCWin.SkinClass.ControlState.Hover;
        }

        private void btnId_MouseLeave(object sender, EventArgs e)
        {
            txtId.MouseState = CCWin.SkinClass.ControlState.Normal;
        }

        private void menuUsers_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            btnId.Enabled = true;
            btnId.StopState = StopStates.NoStop;
            btnId.ControlState = txtId.MouseState = CCWin.SkinClass.ControlState.Normal;
        }
    }
}
