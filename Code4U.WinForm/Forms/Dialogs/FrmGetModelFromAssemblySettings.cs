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
    public partial class FrmGetModelFromAssemblySettings : Form
    {
        private readonly IMediator mediator;

        public Project Model { get; set; }

        public FrmGetModelFromAssemblySettings(IMediator mediator)
        {
            InitializeComponent();
            
            txtAssemblyFile.Text = @"C:\Projetos\Lucian\Code4U\Code4U\bin\Debug\Code4U.dll";
            txtNamespace.Text = "Code4U.Models";

            this.mediator = mediator;
        }

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            this.Model = mediator.Send(new GetModelFromAssembly()
            {
                ProjectName = "<Project Name>",
                AssemblyFilename = txtAssemblyFile.Text,
                Namespace = txtNamespace.Text
            });

            var frmPrincipal = (FrmPrincipal)this.Owner;

            frmPrincipal.SetModel(this.Model);

            this.Close();
        }
    }
}
