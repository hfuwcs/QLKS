﻿using QLKS.Models;
using QLKS.Models.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKS.Forms
{
    public partial class FormLogin : Form
    {
        static DbContext db = new DbContext(DbContext.ConnectionType.ConfigurationManager, "DefaultConnection");

        Account account;

        public Account Account => account;

        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Length == 0 || txtPassword.Text.Length == 0 ) {
                MessageBox.Show("Username và Password không được để trống!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                Account curr = EmployeeExts.GetAccount(txtUsername.Text.Replace(" ", ""), db);
                if (curr != null) { 
                    if (txtPassword.Text.HashSHA256().Equals(curr.Password))
                    {
                        account = curr;
                        Close();
                    } else
                    {
                        MessageBox.Show("Username hoặc Password không đúng!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                } else
                {
                    MessageBox.Show("Tài khoản này không tồn tại!", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
