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

namespace Code4U.WinForm
{
    public partial class FrmPrincipal : Form
    {
        private readonly IMediator mediator;

        public FrmPrincipal(IMediator mediator)
        {
            this.mediator = mediator;

            InitializeComponent();

            //TODO: Carregar de um arquivo de config. Permitir salvar tb.

            var codePath = System.AppDomain.CurrentDomain.BaseDirectory;

            txtTemplateFolder.Text = Path.Combine(@"C:\Projetos\Lucian\Code4U\Code4U.WinForm", "Templates");
            txtGeneratedCodeFolder.Text = Path.Combine(codePath, "GeneratedCode");
            txtServer.Text = @"localhost\SQLExpress";
            txtDatabase.Text = "MangaScrapper";
            txtUser.Text = @"sa";
            txtPassword.Text = @"123456";
        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            mediator.Send(new RunDatabaseSchemaTemplate()
            {
                TemplateFolder = txtTemplateFolder.Text,
                GeneratedCodeFolder = txtGeneratedCodeFolder.Text,
                Server = txtServer.Text,
                Database = txtDatabase.Text,
                User = txtUser.Text,
                Password = txtPassword.Text
            });
        }
    }
}
