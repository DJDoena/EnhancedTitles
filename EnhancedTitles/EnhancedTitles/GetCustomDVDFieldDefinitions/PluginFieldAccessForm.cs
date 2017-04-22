using Invelos.DVDProfilerPlugin;
using System;
using System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.GetCustomDVDFieldDefinitions
{
    public partial class PluginFieldAccessForm : Form
    {
        public PluginFieldAccessForm(CustomPluginDataDefinitions customPluginDataDefinitions)
        {
            TreeNode root;

            InitializeComponent();
            root = PluginTreeView.Nodes.Add("Plugins");
            if ((customPluginDataDefinitions.FieldDomainList != null) && (customPluginDataDefinitions.FieldDomainList.Length > 0))
            {
                foreach (FieldDomain plugin in customPluginDataDefinitions.FieldDomainList)
                {
                    AddPluginNode(root, plugin);
                }
            }
            PluginTreeView.ExpandAll();
        }

        private void AddPluginNode(TreeNode root, FieldDomain plugin)
        {
            TreeNode pluginNode;

            pluginNode = root.Nodes.Add("ID", "ID: " + plugin.ID);
            if ((plugin.FieldList != null) && (plugin.FieldList.Length > 0))
            {
                foreach (FieldData fieldData in plugin.FieldList)
                {
                    AddFieldNode(pluginNode, fieldData);
                }
            }
        }

        private void AddFieldNode(TreeNode pluginNode, FieldData fieldData)
        {
            TreeNode fieldNode;

            fieldNode = pluginNode.Nodes.Add("FieldName", "FieldName: " + fieldData.FieldName);
            fieldNode.Nodes.Add("FieldType", "FieldType: " + GetReadableFieldType(fieldData.FieldType));
            fieldNode.Nodes.Add("ReadKeyRequired", "ReadKeyRequired: " + fieldData.ReadKeyRequired.ToString());
            fieldNode.Nodes.Add("WriteKeyRequired", "WriteKeyRequired: " + fieldData.WriteKeyRequired.ToString());
            fieldNode.Nodes.Add("StoreInBackup", "StoreInBackup:" + fieldData.StoreInBackup.ToString());
            fieldNode.Nodes.Add("StoreInDPO", "StoreInDPO:" + fieldData.StoreInDPO.ToString());
        }

        private static String GetReadableFieldType(Int32 fieldType)
        {
            switch (fieldType)
            {
                case (PluginConstants.FIELD_TYPE_INT):
                    {
                        return ("FIELD_TYPE_INT");
                    }
                case (PluginConstants.FIELD_TYPE_STRING):
                    {
                        return ("FIELD_TYPE_STRING");
                    }
                case (PluginConstants.FIELD_TYPE_DATETIME):
                    {
                        return ("FIELD_TYPE_DATETIME");
                    }
                case (PluginConstants.FIELD_TYPE_CURRENCY):
                    {
                        return ("FIELD_TYPE_CURRENCY");
                    }
                case (PluginConstants.FIELD_TYPE_BYTE_ARRAY):
                    {
                        return ("FIELD_TYPE_BYTE_ARRAY");
                    }
                case (PluginConstants.FIELD_TYPE_BOOL):
                    {
                        return ("FIELD_TYPE_BYTE_BOOL");
                    }
                case (PluginConstants.FIELD_TYPE_INT_ARRAY):
                    {
                        return ("FIELD_TYPE_INT_ARRAY");
                    }
                case (PluginConstants.FIELD_TYPE_STRING_ARRAY):
                    {
                        return ("FIELD_TYPE_STRING_ARRAY");
                    }
                default:
                    {
                        return ("UNKNOWN");
                    }
            }
        }
    }
}