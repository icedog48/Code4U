using AutoMapper;
using Code4U.Models;
using Code4U.WinForm.Forms.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code4U.WinForm
{
    public class NodeCoordinate
    {
        public TreeNode ParentNode { get; set; }

        public int Index { get; set; }
    }

    public partial class FrmPrincipal : Form
    {
        private const string PROJECT_MODELS_NODE_TEXT = "Project Models";

        private FrmDatabaseSchemaSettings frmDatabaseSchemaSettings;

        public ProjectViewModel Model { get; set; }

        public FrmPrincipal(FrmDatabaseSchemaSettings frmDatabaseSchemaSettings)
        {
            InitializeComponent();

            this.frmDatabaseSchemaSettings = frmDatabaseSchemaSettings;
        }

        #region Handlers

        private void menuLoadModelFromDatabase_Click(object sender, EventArgs e)
        {
            this.frmDatabaseSchemaSettings.ShowDialog(this);

            this.Model = Mapper.Map<ProjectViewModel>(this.frmDatabaseSchemaSettings.Model);

            LoadTreeView();
        }
        
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

        #endregion Handlers

        #region Methods

        protected virtual void LoadTreeView()
        {
            trvProject.Nodes.Clear();

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

        #endregion Methods
    }
}
