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

        protected enum Levels
        {
            Project = 0,
            Entity = 1,
            Property = 2
        }

        private FrmGetModelFromDatabaseSchemaSettings frmGetFromDatabase;
        private FrmGetModelFromAssemblySettings frmGetFromAssembly;

        private readonly IMediator mediator;

        private string filename;

        public ProjectViewModel Project { get; set; }

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
            if (this.Project != null && this.Project.FromAssembly != null)
            {
                this.frmGetFromAssembly.AssemblyFile = Project.FromAssembly.AssemblyFilename;
                this.frmGetFromAssembly.Namespace    = Project.FromAssembly.Namespace;
            }
            
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
            if (trvProject.SelectedNode != null)
            {
                var selectedNode = trvProject.SelectedNode;

                var level = (Levels)trvProject.SelectedNode.Level;

                if (level == Levels.Project)
                {
                    var entities = this.Project.Entities.ToList();

                    entities.Add(new EntityViewModel()
                    {
                        Name = "New entity"
                    });

                    this.Project.Entities = entities;

                    LoadTreeView();

                    trvProject.SelectedNode = trvProject.SelectedNode.LastNode;
                }
                else 
                {
                    var nodesIndex = GetNodesIndex(trvProject.SelectedNode);
                    
                    var entityName = trvProject.SelectedNode.Text;

                    if (level == Levels.Property)
                    {
                        entityName = trvProject.SelectedNode.Parent.Text;
                    }

                    var entity = this.Project.Entities.Last(x => x.Name == entityName);

                    var properties = entity.Properties.ToList();

                    properties.Add(new PropertyViewModel()
                    {
                        Name = "New Property"
                    });

                    entity.Properties = properties;

                    LoadTreeView();

                    SelectNode(nodesIndex);

                    if (level == Levels.Property)
                    {
                        trvProject.SelectedNode = trvProject.SelectedNode.Parent.LastNode;
                    }
                    else
                    {
                        trvProject.SelectedNode = trvProject.SelectedNode.LastNode;
                    }
                }
            }
        }

        private void removeToolStripButton_Click(object sender, EventArgs e)
        {
            if (trvProject.SelectedNode != null)
            {
                var nodesIndex = GetNodesIndex(trvProject.SelectedNode).ToList();

                var level = (Levels)trvProject.SelectedNode.Level;

                if (level == Levels.Entity)
                {
                    var entityName = trvProject.SelectedNode.Text;

                    var entity = this.Project.Entities.Last(x => x.Name == entityName);

                    var entities = this.Project.Entities.ToList();

                    entities.Remove(entity);

                    this.Project.Entities = entities;

                    LoadTreeView();
                }
                else if (level == Levels.Property)
                {
                    var entityName = trvProject.SelectedNode.Parent.Text;
                    var entity = this.Project.Entities.Last(x => x.Name == entityName);

                    var propertyName = trvProject.SelectedNode.Text;
                    var property = entity.Properties.Last(x => x.Name == propertyName);

                    var properties = entity.Properties.ToList();
                    properties.Remove(property);

                    entity.Properties = properties;

                    LoadTreeView();
                    
                    nodesIndex.RemoveAt(nodesIndex.Count - 1);

                    SelectNode(nodesIndex);
                }
            }
        }

        #endregion Toolbar

        #endregion Handlers

        #region Methods

        public virtual void SetModel(Project project, GetFromAssemblyViewModel getFromViewModel)
        {
            this.Project = Mapper.Map<ProjectViewModel>(project);

            this.Project.FromAssembly = getFromViewModel;

            LoadTreeView();
        }

        public virtual void SetModel(Project project, GetFromDatabaseViewModel getFromViewModel)
        {
            this.Project = Mapper.Map<ProjectViewModel>(project);

            this.Project.FromDatabase = getFromViewModel;

            LoadTreeView();
        }

        protected virtual void LoadTreeView()
        {
            var entityNodesExpanded = new List<NodeStateViewModel>();

            if (trvProject.TopNode != null)
            {
                entityNodesExpanded = trvProject.TopNode.Nodes.Cast<TreeNode>().Select(x => new NodeStateViewModel()
                {
                    Text = x.Text,
                    Expanded = x.IsExpanded
                }).ToList();
            }

            trvProject.Nodes.Clear();

            var projectNode = trvProject.Nodes.Add(PROJECT_MODELS_NODE_TEXT);

            if (this.Project != null)
            {
                foreach (var entity in this.Project.Entities)
                {
                    var entityNode = projectNode.Nodes.Add(entity.Name);

                    foreach (var property in entity.Properties)
                    {
                        entityNode.Nodes.Add(property.Name);
                    }

                    var nodeState = entityNodesExpanded.FirstOrDefault(x => x.Text == entity.Name);

                    if (nodeState != null && nodeState.Expanded) entityNode.Expand();
                }

                trvProject.SelectedNode = projectNode;
                trvProject.SelectedNode.Expand();
            }
        }

        protected virtual void ShowProperties()
        {
            if (trvProject.SelectedNode.Level == 0)
            {
                ptgProperties.SelectedObject = this.Project;
            }
            else if (trvProject.SelectedNode.Level == 1)
            {
                var entity = this.Project.Entities.First(x => x.Name == trvProject.SelectedNode.Text);

                ptgProperties.SelectedObject = entity;
            }
            else
            {
                var entityName = trvProject.SelectedNode.Parent.Text;
                var propertyName = trvProject.SelectedNode.Text;

                var property = this.Project.Entities.First(x => x.Name == entityName)
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

        protected virtual TreeNode GetNodeByText(string nodeText, Levels nodeLevel = Levels.Project, TreeNode node = null)
        {
            if (node == null) node = trvProject.TopNode;

            if (node.Text == nodeText && 
                node.Level == (int)nodeLevel) return node;

            TreeNode childNode = null;
            
            foreach (TreeNode currentNode in node.Nodes)
            {
                childNode = GetNodeByText(nodeText, nodeLevel, currentNode);

                if (childNode != null && 
                    childNode.Text == nodeText && 
                    childNode.Level == (int)nodeLevel) return childNode;
            }

            return childNode;
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
            saveFileDialog.Filter = "Code4U Project|*.4u";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                this.filename = saveFileDialog.FileName;

                WriteModelToFile();
            }
        }

        protected virtual void WriteModelToFile()
        {
            var json = JsonConvert.SerializeObject(this.Project, Formatting.Indented);

            File.WriteAllText(this.filename, json);
        }

        protected virtual void OpenProject()
        {
            openFileDialog.Filter = "Code4U Project|*.4u";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.filename = openFileDialog.FileName;

                var json = File.ReadAllText(this.filename);

                this.Project = JsonConvert.DeserializeObject<ProjectViewModel>(json);

                if (this.Project.Entities == null) this.Project.Entities = new List<EntityViewModel>();

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
            if (this.Project != null)
            {
                try
                {
                    this.mediator.Send(new RunTemplate()
                    {
                        Model = Mapper.Map<Project>(this.Project)
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
            this.Project = null;
            this.ptgProperties.SelectedObject = null;

            LoadTreeView();
        }

        #endregion Methods

        private void toolStripButtonUp_Click(object sender, EventArgs e)
        {
            if (trvProject.SelectedNode == null) return;

            var level = (Levels)trvProject.SelectedNode.Level;

            if (level != Levels.Entity) return;

            var entities = this.Project.Entities.ToList();

            var entityName = trvProject.SelectedNode.Text;

            var entity = entities.Last(x => x.Name == entityName);

            var entityIndex = entities.IndexOf(entity);

            if (entityIndex == 0) return;

            entities.Remove(entity);
            entities.Insert(entityIndex - 1, entity);

            this.Project.Entities = entities;

            LoadTreeView();

            var node = GetNodeByText(entity.Name, Levels.Entity);

            SelectNode(GetNodesIndex(node));
        }

        private void toolStripButtonDown_Click(object sender, EventArgs e)
        {
            if (trvProject.SelectedNode == null) return;

            var level = (Levels)trvProject.SelectedNode.Level;

            if (level != Levels.Entity) return;

            var entities = this.Project.Entities.ToList();

            var entityName = trvProject.SelectedNode.Text;

            var entity = entities.Last(x => x.Name == entityName);

            var entityIndex = entities.IndexOf(entity);

            if (entityIndex == entities.Count - 1) return;

            entities.Remove(entity);
            entities.Insert(entityIndex + 1, entity);

            this.Project.Entities = entities;

            LoadTreeView();

            var node = GetNodeByText(entity.Name, Levels.Entity);

            SelectNode(GetNodesIndex(node));
        }
    }
}
