﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.1.
// 
namespace DoenaSoft.DVDProfiler.GetCustomDVDFieldDefinitions
{
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class CustomPluginDataDefinitions
    {

        private FieldDomain[] fieldDomainField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FieldDomain")]
        public FieldDomain[] FieldDomainList
        {
            get
            {
                return this.fieldDomainField;
            }
            set
            {
                this.fieldDomainField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FieldDomain
    {

        private FieldData[] fieldField;

        private string idField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Field")]
        public FieldData[] FieldList
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.1")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FieldData
    {

        private string fieldNameField;

        private int fieldTypeField;

        private bool readKeyRequiredField;

        private bool writeKeyRequiredField;

        private bool storeInBackupField;

        private bool storeInDPOField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FieldName
        {
            get
            {
                return this.fieldNameField;
            }
            set
            {
                this.fieldNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int FieldType
        {
            get
            {
                return this.fieldTypeField;
            }
            set
            {
                this.fieldTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool ReadKeyRequired
        {
            get
            {
                return this.readKeyRequiredField;
            }
            set
            {
                this.readKeyRequiredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool WriteKeyRequired
        {
            get
            {
                return this.writeKeyRequiredField;
            }
            set
            {
                this.writeKeyRequiredField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool StoreInBackup
        {
            get
            {
                return this.storeInBackupField;
            }
            set
            {
                this.storeInBackupField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool StoreInDPO
        {
            get
            {
                return this.storeInDPOField;
            }
            set
            {
                this.storeInDPOField = value;
            }
        }
    }
}