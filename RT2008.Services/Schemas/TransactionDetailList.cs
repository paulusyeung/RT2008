﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Runtime.Serialization.ContractNamespaceAttribute("http://synergyis.biz/TransactionDetailList.xsd", ClrNamespace="synergyis.biz.TransactionDetailList.xsd")]

namespace RT2008.Services.Model
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TransactionDetailList", Namespace="http://synergyis.biz/TransactionDetailList.xsd")]
    public partial class TransactionDetailList : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private string TxNumberField;
        
        private string TxTypeField;
        
        private string TxDateField;
        
        private string ProductIdField;
        
        private double QtyField;
        
        private string WorkplaceField;
        
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string TxNumber
        {
            get
            {
                return this.TxNumberField;
            }
            set
            {
                this.TxNumberField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string TxType
        {
            get
            {
                return this.TxTypeField;
            }
            set
            {
                this.TxTypeField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string TxDate
        {
            get
            {
                return this.TxDateField;
            }
            set
            {
                this.TxDateField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string ProductId
        {
            get
            {
                return this.ProductIdField;
            }
            set
            {
                this.ProductIdField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public double Qty
        {
            get
            {
                return this.QtyField;
            }
            set
            {
                this.QtyField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string Workplace
        {
            get
            {
                return this.WorkplaceField;
            }
            set
            {
                this.WorkplaceField = value;
            }
        }
    }
}
