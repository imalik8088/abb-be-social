﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Transformation_Layer.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetHumanInformation_Result", Namespace="http://schemas.datacontract.org/2004/07/ABBConnectWCF.APP_DATA")]
    [System.SerializableAttribute()]
    public partial class GetHumanInformation_Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneNumberField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneNumber {
            get {
                return this.PhoneNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneNumberField, value) != true)) {
                    this.PhoneNumberField = value;
                    this.RaisePropertyChanged("PhoneNumber");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetHumanInformationByUsername_Result", Namespace="http://schemas.datacontract.org/2004/07/ABBConnectWCF.APP_DATA")]
    [System.SerializableAttribute()]
    public partial class GetHumanInformationByUsername_Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneNumberField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PhoneNumber {
            get {
                return this.PhoneNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneNumberField, value) != true)) {
                    this.PhoneNumberField = value;
                    this.RaisePropertyChanged("PhoneNumber");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetLatestXFeeds_Result", Namespace="http://schemas.datacontract.org/2004/07/ABBConnectWCF.APP_DATA")]
    [System.SerializableAttribute()]
    public partial class GetLatestXFeeds_Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreationTimeStampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FeedIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FilePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrioCategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PrioValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreationTimeStamp {
            get {
                return this.CreationTimeStampField;
            }
            set {
                if ((this.CreationTimeStampField.Equals(value) != true)) {
                    this.CreationTimeStampField = value;
                    this.RaisePropertyChanged("CreationTimeStamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FeedId {
            get {
                return this.FeedIdField;
            }
            set {
                if ((this.FeedIdField.Equals(value) != true)) {
                    this.FeedIdField = value;
                    this.RaisePropertyChanged("FeedId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FilePath {
            get {
                return this.FilePathField;
            }
            set {
                if ((object.ReferenceEquals(this.FilePathField, value) != true)) {
                    this.FilePathField = value;
                    this.RaisePropertyChanged("FilePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PrioCategory {
            get {
                return this.PrioCategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.PrioCategoryField, value) != true)) {
                    this.PrioCategoryField = value;
                    this.RaisePropertyChanged("PrioCategory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PrioValue {
            get {
                return this.PrioValueField;
            }
            set {
                if ((this.PrioValueField.Equals(value) != true)) {
                    this.PrioValueField = value;
                    this.RaisePropertyChanged("PrioValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GetLatestXFeedsFromId_Result", Namespace="http://schemas.datacontract.org/2004/07/ABBConnectWCF.APP_DATA")]
    [System.SerializableAttribute()]
    public partial class GetLatestXFeedsFromId_Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreationTimeStampField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FeedIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FilePathField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PrioCategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PrioValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int UserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UsernameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime CreationTimeStamp {
            get {
                return this.CreationTimeStampField;
            }
            set {
                if ((this.CreationTimeStampField.Equals(value) != true)) {
                    this.CreationTimeStampField = value;
                    this.RaisePropertyChanged("CreationTimeStamp");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int FeedId {
            get {
                return this.FeedIdField;
            }
            set {
                if ((this.FeedIdField.Equals(value) != true)) {
                    this.FeedIdField = value;
                    this.RaisePropertyChanged("FeedId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FilePath {
            get {
                return this.FilePathField;
            }
            set {
                if ((object.ReferenceEquals(this.FilePathField, value) != true)) {
                    this.FilePathField = value;
                    this.RaisePropertyChanged("FilePath");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PrioCategory {
            get {
                return this.PrioCategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.PrioCategoryField, value) != true)) {
                    this.PrioCategoryField = value;
                    this.RaisePropertyChanged("PrioCategory");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int PrioValue {
            get {
                return this.PrioValueField;
            }
            set {
                if ((this.PrioValueField.Equals(value) != true)) {
                    this.PrioValueField = value;
                    this.RaisePropertyChanged("PrioValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Text {
            get {
                return this.TextField;
            }
            set {
                if ((object.ReferenceEquals(this.TextField, value) != true)) {
                    this.TextField = value;
                    this.RaisePropertyChanged("Text");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((this.UserIdField.Equals(value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Username {
            get {
                return this.UsernameField;
            }
            set {
                if ((object.ReferenceEquals(this.UsernameField, value) != true)) {
                    this.UsernameField = value;
                    this.RaisePropertyChanged("Username");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IABBConnectWCF")]
    public interface IABBConnectWCF {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/LogIn", ReplyAction="http://tempuri.org/IABBConnectWCF/LogInResponse")]
        bool LogIn(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/LogIn", ReplyAction="http://tempuri.org/IABBConnectWCF/LogInResponse")]
        System.Threading.Tasks.Task<bool> LogInAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetHumanInformation", ReplyAction="http://tempuri.org/IABBConnectWCF/GetHumanInformationResponse")]
        Transformation_Layer.ServiceReference1.GetHumanInformation_Result GetHumanInformation(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetHumanInformation", ReplyAction="http://tempuri.org/IABBConnectWCF/GetHumanInformationResponse")]
        System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetHumanInformation_Result> GetHumanInformationAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetHumanInformationByUsername", ReplyAction="http://tempuri.org/IABBConnectWCF/GetHumanInformationByUsernameResponse")]
        Transformation_Layer.ServiceReference1.GetHumanInformationByUsername_Result GetHumanInformationByUsername(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetHumanInformationByUsername", ReplyAction="http://tempuri.org/IABBConnectWCF/GetHumanInformationByUsernameResponse")]
        System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetHumanInformationByUsername_Result> GetHumanInformationByUsernameAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetLatestXFeeds", ReplyAction="http://tempuri.org/IABBConnectWCF/GetLatestXFeedsResponse")]
        Transformation_Layer.ServiceReference1.GetLatestXFeeds_Result[] GetLatestXFeeds(string X);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetLatestXFeeds", ReplyAction="http://tempuri.org/IABBConnectWCF/GetLatestXFeedsResponse")]
        System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetLatestXFeeds_Result[]> GetLatestXFeedsAsync(string X);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetLatestXFeedsFromId", ReplyAction="http://tempuri.org/IABBConnectWCF/GetLatestXFeedsFromIdResponse")]
        Transformation_Layer.ServiceReference1.GetLatestXFeedsFromId_Result[] GetLatestXFeedsFromId(string X, string Id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IABBConnectWCF/GetLatestXFeedsFromId", ReplyAction="http://tempuri.org/IABBConnectWCF/GetLatestXFeedsFromIdResponse")]
        System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetLatestXFeedsFromId_Result[]> GetLatestXFeedsFromIdAsync(string X, string Id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IABBConnectWCFChannel : Transformation_Layer.ServiceReference1.IABBConnectWCF, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ABBConnectWCFClient : System.ServiceModel.ClientBase<Transformation_Layer.ServiceReference1.IABBConnectWCF>, Transformation_Layer.ServiceReference1.IABBConnectWCF {
        
        public ABBConnectWCFClient() {
        }
        
        public ABBConnectWCFClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ABBConnectWCFClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ABBConnectWCFClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ABBConnectWCFClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool LogIn(string username, string password) {
            return base.Channel.LogIn(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> LogInAsync(string username, string password) {
            return base.Channel.LogInAsync(username, password);
        }
        
        public Transformation_Layer.ServiceReference1.GetHumanInformation_Result GetHumanInformation(string id) {
            return base.Channel.GetHumanInformation(id);
        }
        
        public System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetHumanInformation_Result> GetHumanInformationAsync(string id) {
            return base.Channel.GetHumanInformationAsync(id);
        }
        
        public Transformation_Layer.ServiceReference1.GetHumanInformationByUsername_Result GetHumanInformationByUsername(string username) {
            return base.Channel.GetHumanInformationByUsername(username);
        }
        
        public System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetHumanInformationByUsername_Result> GetHumanInformationByUsernameAsync(string username) {
            return base.Channel.GetHumanInformationByUsernameAsync(username);
        }
        
        public Transformation_Layer.ServiceReference1.GetLatestXFeeds_Result[] GetLatestXFeeds(string X) {
            return base.Channel.GetLatestXFeeds(X);
        }
        
        public System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetLatestXFeeds_Result[]> GetLatestXFeedsAsync(string X) {
            return base.Channel.GetLatestXFeedsAsync(X);
        }
        
        public Transformation_Layer.ServiceReference1.GetLatestXFeedsFromId_Result[] GetLatestXFeedsFromId(string X, string Id) {
            return base.Channel.GetLatestXFeedsFromId(X, Id);
        }
        
        public System.Threading.Tasks.Task<Transformation_Layer.ServiceReference1.GetLatestXFeedsFromId_Result[]> GetLatestXFeedsFromIdAsync(string X, string Id) {
            return base.Channel.GetLatestXFeedsFromIdAsync(X, Id);
        }
    }
}
