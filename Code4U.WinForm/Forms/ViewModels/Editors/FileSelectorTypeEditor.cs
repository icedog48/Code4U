//
// FileSelectorTypeEditor.cs
//
// Author:
//    Tomas Restrepo (tomasr@mvps.org)
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Code4U.WinForm.Forms.ViewModels.TypeEditors
{
    /// <summary>
    /// Customer UITypeEditor that pops up a
    /// file selector dialog
    /// </summary>
    public class FileSelectorTypeEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context == null || context.Instance == null) return base.GetEditStyle(context);

            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService;

            if (context == null || context.Instance == null || provider == null) return value;

            try
            {
                // get the editor service, just like in windows forms
                editorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));

                var dlg = new FolderBrowserDialog();

                var selectedPath = (string)value;

                if (!Directory.Exists(selectedPath)) selectedPath = null;

                dlg.SelectedPath = selectedPath;

                using (dlg)
                {
                    DialogResult res = dlg.ShowDialog();

                    if (res == DialogResult.OK)
                    {
                        selectedPath = dlg.SelectedPath;
                    }
                }

                return selectedPath;
            }
            finally
            {
                editorService = null;
            }
        }
    } 
} 