//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Soap.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost", ConfigurationName="ServiceReference1.PurchaseJournalPost_Port")]
    public interface PurchaseJournalPost_Port {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost:DeleteCurrentBatch", ReplyAction="*")]
        Soap.ServiceReference1.DeleteCurrentBatch_Result DeleteCurrentBatch(Soap.ServiceReference1.DeleteCurrentBatch request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost:DeleteCurrentBatch", ReplyAction="*")]
        System.Threading.Tasks.Task<Soap.ServiceReference1.DeleteCurrentBatch_Result> DeleteCurrentBatchAsync(Soap.ServiceReference1.DeleteCurrentBatch request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost:RunCodeUnit", ReplyAction="*")]
        Soap.ServiceReference1.RunCodeUnit_Result RunCodeUnit(Soap.ServiceReference1.RunCodeUnit request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost:RunCodeUnit", ReplyAction="*")]
        System.Threading.Tasks.Task<Soap.ServiceReference1.RunCodeUnit_Result> RunCodeUnitAsync(Soap.ServiceReference1.RunCodeUnit request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DeleteCurrentBatch {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DeleteCurrentBatch", Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost", Order=0)]
        public Soap.ServiceReference1.DeleteCurrentBatchBody Body;
        
        public DeleteCurrentBatch() {
        }
        
        public DeleteCurrentBatch(Soap.ServiceReference1.DeleteCurrentBatchBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost")]
    public partial class DeleteCurrentBatchBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string batchname;
        
        public DeleteCurrentBatchBody() {
        }
        
        public DeleteCurrentBatchBody(string batchname) {
            this.batchname = batchname;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class DeleteCurrentBatch_Result {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="DeleteCurrentBatch_Result", Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost", Order=0)]
        public Soap.ServiceReference1.DeleteCurrentBatch_ResultBody Body;
        
        public DeleteCurrentBatch_Result() {
        }
        
        public DeleteCurrentBatch_Result(Soap.ServiceReference1.DeleteCurrentBatch_ResultBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost")]
    public partial class DeleteCurrentBatch_ResultBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string batchname;
        
        public DeleteCurrentBatch_ResultBody() {
        }
        
        public DeleteCurrentBatch_ResultBody(string batchname) {
            this.batchname = batchname;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RunCodeUnit {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RunCodeUnit", Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost", Order=0)]
        public Soap.ServiceReference1.RunCodeUnitBody Body;
        
        public RunCodeUnit() {
        }
        
        public RunCodeUnit(Soap.ServiceReference1.RunCodeUnitBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost")]
    public partial class RunCodeUnitBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string batchname;
        
        public RunCodeUnitBody() {
        }
        
        public RunCodeUnitBody(string batchname) {
            this.batchname = batchname;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RunCodeUnit_Result {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RunCodeUnit_Result", Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost", Order=0)]
        public Soap.ServiceReference1.RunCodeUnit_ResultBody Body;
        
        public RunCodeUnit_Result() {
        }
        
        public RunCodeUnit_Result(Soap.ServiceReference1.RunCodeUnit_ResultBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="urn:microsoft-dynamics-schemas/codeunit/PurchaseJournalPost")]
    public partial class RunCodeUnit_ResultBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string return_value;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string batchname;
        
        public RunCodeUnit_ResultBody() {
        }
        
        public RunCodeUnit_ResultBody(string return_value, string batchname) {
            this.return_value = return_value;
            this.batchname = batchname;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PurchaseJournalPost_PortChannel : Soap.ServiceReference1.PurchaseJournalPost_Port, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PurchaseJournalPost_PortClient : System.ServiceModel.ClientBase<Soap.ServiceReference1.PurchaseJournalPost_Port>, Soap.ServiceReference1.PurchaseJournalPost_Port {
        
        public PurchaseJournalPost_PortClient() {
        }
        
        public PurchaseJournalPost_PortClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PurchaseJournalPost_PortClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PurchaseJournalPost_PortClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PurchaseJournalPost_PortClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Soap.ServiceReference1.DeleteCurrentBatch_Result Soap.ServiceReference1.PurchaseJournalPost_Port.DeleteCurrentBatch(Soap.ServiceReference1.DeleteCurrentBatch request) {
            return base.Channel.DeleteCurrentBatch(request);
        }
        
        public void DeleteCurrentBatch(ref string batchname) {
            Soap.ServiceReference1.DeleteCurrentBatch inValue = new Soap.ServiceReference1.DeleteCurrentBatch();
            inValue.Body = new Soap.ServiceReference1.DeleteCurrentBatchBody();
            inValue.Body.batchname = batchname;
            Soap.ServiceReference1.DeleteCurrentBatch_Result retVal = ((Soap.ServiceReference1.PurchaseJournalPost_Port)(this)).DeleteCurrentBatch(inValue);
            batchname = retVal.Body.batchname;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Soap.ServiceReference1.DeleteCurrentBatch_Result> Soap.ServiceReference1.PurchaseJournalPost_Port.DeleteCurrentBatchAsync(Soap.ServiceReference1.DeleteCurrentBatch request) {
            return base.Channel.DeleteCurrentBatchAsync(request);
        }
        
        public System.Threading.Tasks.Task<Soap.ServiceReference1.DeleteCurrentBatch_Result> DeleteCurrentBatchAsync(string batchname) {
            Soap.ServiceReference1.DeleteCurrentBatch inValue = new Soap.ServiceReference1.DeleteCurrentBatch();
            inValue.Body = new Soap.ServiceReference1.DeleteCurrentBatchBody();
            inValue.Body.batchname = batchname;
            return ((Soap.ServiceReference1.PurchaseJournalPost_Port)(this)).DeleteCurrentBatchAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Soap.ServiceReference1.RunCodeUnit_Result Soap.ServiceReference1.PurchaseJournalPost_Port.RunCodeUnit(Soap.ServiceReference1.RunCodeUnit request) {
            return base.Channel.RunCodeUnit(request);
        }
        
        public string RunCodeUnit(ref string batchname) {
            Soap.ServiceReference1.RunCodeUnit inValue = new Soap.ServiceReference1.RunCodeUnit();
            inValue.Body = new Soap.ServiceReference1.RunCodeUnitBody();
            inValue.Body.batchname = batchname;
            Soap.ServiceReference1.RunCodeUnit_Result retVal = ((Soap.ServiceReference1.PurchaseJournalPost_Port)(this)).RunCodeUnit(inValue);
            batchname = retVal.Body.batchname;
            return retVal.Body.return_value;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Soap.ServiceReference1.RunCodeUnit_Result> Soap.ServiceReference1.PurchaseJournalPost_Port.RunCodeUnitAsync(Soap.ServiceReference1.RunCodeUnit request) {
            return base.Channel.RunCodeUnitAsync(request);
        }
        
        public System.Threading.Tasks.Task<Soap.ServiceReference1.RunCodeUnit_Result> RunCodeUnitAsync(string batchname) {
            Soap.ServiceReference1.RunCodeUnit inValue = new Soap.ServiceReference1.RunCodeUnit();
            inValue.Body = new Soap.ServiceReference1.RunCodeUnitBody();
            inValue.Body.batchname = batchname;
            return ((Soap.ServiceReference1.PurchaseJournalPost_Port)(this)).RunCodeUnitAsync(inValue);
        }
    }
}
