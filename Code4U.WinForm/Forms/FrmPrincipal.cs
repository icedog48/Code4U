using AutoMapper;
using Code4U.Commands;
using Code4U.Models;
using Code4U.WinForm.Forms.ViewModels;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code4U.WinForm
{
    public partial class FrmPrincipal : Form
    {
        private const string PROJECT_MODELS_NODE_TEXT = "Project Models";

        private FrmGetModelFromDatabaseSchemaSettings frmGetFromDatabase;
        private FrmGetModelFromAssemblySettings frmGetFromAssembly;

        private readonly IMediator mediator;

        private string filename;

        public ProjectViewModel Model { get; set; }

        public FrmPrincipal(FrmGetModelFromDatabaseSchemaSettings frmDatabaseSchemaSettings,
                            FrmGetModelFromAssemblySettings frmGetFromAssembly,
                            IMediator mediator)
        {
            InitializeComponent();

            this.frmGetFromDatabase = frmDatabaseSchemaSettings;
            this.frmGetFromAssembly = frmGetFromAssembly;

            this.mediator = mediator;
        }

        #region Handlers
        
        private void trvProject_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ShowProperties();
        }

        private void ptgProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var nodesIndex = GetNodesIndex(trvProject.SelectedNode);

            LoadTreeView();

            SelectNode(nodesIndex);
        }

        #region Menu

        private void fromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.frmGetFromDatabase.ShowDialog(this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

       

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void runToolStripButton_Click(object sender, EventArgs e)
        {
            RunTransformations();
        }        

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProject();
        }        

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveNewModelFile();
        }

        private void fromAssemblyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.frmGetFromAssembly.ShowDialog(this);
        }

        #endregion Menu

        #region Toolbar

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            NewProject();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void addToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void removeToolStripButton_Click(object sender, EventArgs e)
        {

        }

        #endregion Toolbar

        #endregion Handlers

        #region Methods

        public virtual void SetModel(Project project)
        {
            this.Model = Mapper.Map<ProjectViewModel>(project);

            LoadTreeView();
        }

        protected virtual void LoadTreeView()
        {
            trvProject.Nodes.Clear();

            if (this.Model != null)
            {
                var projectNode = trvProject.Nodes.Add(PROJECT_MODELS_NODE_TEXT);

                foreach (var entity in this.Model.Entities)
                {
                    var entityNode = projectNode.Nodes.Add(entity.Name);

                    foreach (var property in entity.Properties)
                    {
                        entityNode.Nodes.Add(property.Name);
                    }
                }

                projectNode.ExpandAll();

                trvProject.SelectedNode = projectNode;
            }
        }

        protected virtual void ShowProperties()
        {
            if (trvProject.SelectedNode.Level == 0)
            {
                ptgProperties.SelectedObject = this.Model;
            }
            else if (trvProject.SelectedNode.Level == 1)
            {
                var entity = this.Model.Entities.First(x => x.Name == trvProject.SelectedNode.Text);

                ptgProperties.SelectedObject = entity;
            }
            else
            {
                var entityName = trvProject.SelectedNode.Parent.Text;
                var propertyName = trvProject.SelectedNode.Text;

                var property = this.Model.Entities.First(x => x.Name == entityName)
                                            .Properties.First(x => x.Name == propertyName);

                ptgProperties.SelectedObject = property;
            }
        }

        protected virtual void SelectNode(IEnumerable<int> nodesIndex)
        {
            TreeNode node = null;

            foreach (var index in nodesIndex)
            {
                if (node == null)
                {
                    node = trvProject.Nodes[index];
                }
                else
                {
                    node = node.Nodes[index];
                }
            }

            trvProject.SelectedNode = node;
        }

        protected virtual IEnumerable<int> GetNodesIndex(TreeNode node)
        {
            var indexs = new List<int>();

            if (node.Parent != null)
            {
                indexs.AddRange(GetNodesIndex(node.Parent));
            }

            indexs.Add(node.Index);

            return indexs;
        }

        protected virtual void SaveNewModelFile()
        {
            saveFileDialog.Filter = "JSON File|*.json";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.filename = saveFileDialog.FileName;

                WriteModelToFile();
            }
        }

        protected virtual void WriteModelToFile()
        {
            var json = JsonConvert.SerializeObject(this.Model, Formatting.Indented);

            File.WriteAllText(this.filename, json);
        }

        protected virtual void OpenProject()
        {
            openFileDialog.Filter = "JSON File|*.json";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.filename = openFileDialog.FileName;

                var json = File.ReadAllText(this.filename);

                this.Model = JsonConvert.DeserializeObject<ProjectViewModel>(json);

                LoadTreeView();
            }
        }

        protected virtual void SaveProject()
        {
            if (string.IsNullOrEmpty(this.filename))
            {
                SaveNewModelFile();
            }
            else
            {
                WriteModelToFile();
            }
        }

        protected virtual void RunTransformations()
        {
            if (this.Model != null)
            {
                try
                {
                    this.mediator.Send(new RunTemplate()
                    {
                        Model = Mapper.Map<Project>(this.Model)
                    });

                    MessageBox.Show("Code generated !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Import or Open a model first!");
            }
        }

        protected virtual void NewProject()
        {
            this.filename = string.Empty;
            this.Model = null;
            this.ptgProperties.SelectedObject = null;

            LoadTreeView();
        }

        #endregion Methods
    }
}
