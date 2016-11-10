using DatabaseSchemaReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RazorEngine;
using RazorEngine.Templating;
using DatabaseSchemaReader.DataSchema;
using RazorEngine.Configuration;
using System.IO;
using System.Reflection;
using Code4U.Helpers;
using MediatR;
using Code4U.Commands;
using Code4U.Models;

namespace Code4U.WinForm
{
    public partial class FrmDatabaseSchemaSettings : Form
    {
        private readonly IMediator mediator;

        public Project Model { get; set; }

        public FrmDatabaseSchemaSettings(IMediator mediator)
        {
            this.mediator = mediator;

            InitializeComponent();
            
            txtServer.Text = @"localhost\SQLExpress";
            txtDatabase.Text = "MangaScrapper";
            txtUser.Text = @"sa";
            txtPassword.Text = @"123456";
        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            this.Model = mediator.Send(new GetModelFromDatabaseSchema()
            {
                ProjectName = "<Project Name>",
                Server = txtServer.Text,
                Database = txtDatabase.Text,
                User = txtUser.Text,
                Password = txtPassword.Text
            });

            this.Close();
        }
    }
}
