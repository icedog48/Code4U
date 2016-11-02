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

namespace Code4U.WinForm
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();

            //TODO: Carregar de arquivo

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
            //TODO: Transferir todo o codigo desse metodo para um serviço em outra DLL
            //TODO: Descobrir como adicionar uma dll para a compilação dos templates, Verificar o Assembly.LoadFrom("MyNice.dll"); e o 
            
            var folders = DirSearch(txtTemplateFolder.Text);

            var config = new TemplateServiceConfiguration()
            {
                Debug = true,
                Language = Language.CSharp,
                TemplateManager = new ResolvePathTemplateManager(folders)
            };
            
            var service = RazorEngineService.Create(config);
            
            Engine.Razor = service;

            var server = txtServer.Text;
            var database = txtDatabase.Text;
            var user = txtUser.Text;
            var password = txtPassword.Text;

            var connectionString = $"Server={server};Database={database};User Id={user};Password={password};";

            var dbReader = new DatabaseReader(connectionString, "System.Data.SqlClient");

            var schema = dbReader.ReadAll();

            try
            {
                var viewBag = new DynamicViewBag();
                viewBag.AddValue("TemplateFolder", txtTemplateFolder.Text);
                viewBag.AddValue("GeneratedCodeFolder", txtGeneratedCodeFolder.Text);

                var templateFileName = Path.Combine(txtTemplateFolder.Text, "Index.cshtml");

                Engine.Razor.RunCompile(templateFileName, typeof(DatabaseSchema), schema, viewBag);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deu ruim.: " + ex.Message);
            }
        }

        public static IList<string> DirSearch(string baseDir)
        {
            var directories = new List<string>();

            directories.Add(baseDir);

            foreach (var directory in Directory.GetDirectories(baseDir))
            {
                directories.AddRange(DirSearch(directory));
            }

            return directories;
        }
    }
}
