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
using Code4U.WinForm.Forms.ViewModels;

namespace Code4U.WinForm
{
    public partial class FrmGetModelFromAssemblySettings : Form
    {
        private readonly IMediator mediator;

        public Project Model { get; set; }

        public string AssemblyFile
        {
            get
            {
                return txtAssemblyFile.Text;
            }
            set
            {
                txtAssemblyFile.Text = value;
            }
        }

        public string Namespace
        {
            get
            {
                return txtNamespace.Text;
            }
            set
            {
                txtNamespace.Text = value;
            }
        }

        public FrmGetModelFromAssemblySettings(IMediator mediator)
        {
            InitializeComponent();

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

            if (frmPrincipal.Project != null)
            {
                this.Model.Name = frmPrincipal.Project.Name;
                this.Model.TemplateFolder = frmPrincipal.Project.TemplateFolder;
                this.Model.GeneratedCodeFolder = frmPrincipal.Project.GeneratedCodeFolder;
            }

            frmPrincipal.SetModel(this.Model, new GetFromAssemblyViewModel()
            {
                AssemblyFilename = txtAssemblyFile.Text,
                Namespace = txtNamespace.Text
            });

            this.Close();
        }
    }
}
