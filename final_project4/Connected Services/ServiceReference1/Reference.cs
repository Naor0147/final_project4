﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 17.0.34511.75
// 
namespace final_project4.ServiceReference1 {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/FInalProject")]
    public partial class User : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string MailField;
        
        private string NameField;
        
        private string PasswordField;
        
        private int YearBornField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Mail {
            get {
                return this.MailField;
            }
            set {
                if ((object.ReferenceEquals(this.MailField, value) != true)) {
                    this.MailField = value;
                    this.RaisePropertyChanged("Mail");
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
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int YearBorn {
            get {
                return this.YearBornField;
            }
            set {
                if ((this.YearBornField.Equals(value) != true)) {
                    this.YearBornField = value;
                    this.RaisePropertyChanged("YearBorn");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="LevelStats", Namespace="http://schemas.datacontract.org/2004/07/FInalProject")]
    public partial class LevelStats : object, System.ComponentModel.INotifyPropertyChanged {
        
        private int CoinsField;
        
        private int IdField;
        
        private int LevelIdField;
        
        private string NameField;
        
        private int NumberOfCoinsField;
        
        private int TimeClickedField;
        
        private int TimePassedField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Coins {
            get {
                return this.CoinsField;
            }
            set {
                if ((this.CoinsField.Equals(value) != true)) {
                    this.CoinsField = value;
                    this.RaisePropertyChanged("Coins");
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
        public int LevelId {
            get {
                return this.LevelIdField;
            }
            set {
                if ((this.LevelIdField.Equals(value) != true)) {
                    this.LevelIdField = value;
                    this.RaisePropertyChanged("LevelId");
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
        public int NumberOfCoins {
            get {
                return this.NumberOfCoinsField;
            }
            set {
                if ((this.NumberOfCoinsField.Equals(value) != true)) {
                    this.NumberOfCoinsField = value;
                    this.RaisePropertyChanged("NumberOfCoins");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TimeClicked {
            get {
                return this.TimeClickedField;
            }
            set {
                if ((this.TimeClickedField.Equals(value) != true)) {
                    this.TimeClickedField = value;
                    this.RaisePropertyChanged("TimeClicked");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TimePassed {
            get {
                return this.TimePassedField;
            }
            set {
                if ((this.TimePassedField.Equals(value) != true)) {
                    this.TimePassedField = value;
                    this.RaisePropertyChanged("TimePassed");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="UserAndLevel", Namespace="http://schemas.datacontract.org/2004/07/FInalProject.classes")]
    public partial class UserAndLevel : object, System.ComponentModel.INotifyPropertyChanged {
        
        private final_project4.ServiceReference1.LevelStats LevelStatsField;
        
        private final_project4.ServiceReference1.User UserField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public final_project4.ServiceReference1.LevelStats LevelStats {
            get {
                return this.LevelStatsField;
            }
            set {
                if ((object.ReferenceEquals(this.LevelStatsField, value) != true)) {
                    this.LevelStatsField = value;
                    this.RaisePropertyChanged("LevelStats");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public final_project4.ServiceReference1.User User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/HasUsername", ReplyAction="http://tempuri.org/IService1/HasUsernameResponse")]
        System.Threading.Tasks.Task<bool> HasUsernameAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/RegisterUser", ReplyAction="http://tempuri.org/IService1/RegisterUserResponse")]
        System.Threading.Tasks.Task<bool> RegisterUserAsync(final_project4.ServiceReference1.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ValidateUser", ReplyAction="http://tempuri.org/IService1/ValidateUserResponse")]
        System.Threading.Tasks.Task<bool> ValidateUserAsync(final_project4.ServiceReference1.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/AddStats", ReplyAction="http://tempuri.org/IService1/AddStatsResponse")]
        System.Threading.Tasks.Task<bool> AddStatsAsync(final_project4.ServiceReference1.LevelStats levelStats);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/FindUser", ReplyAction="http://tempuri.org/IService1/FindUserResponse")]
        System.Threading.Tasks.Task<final_project4.ServiceReference1.User> FindUserAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetUsers", ReplyAction="http://tempuri.org/IService1/GetUsersResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.User>> GetUsersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLevelStatsById", ReplyAction="http://tempuri.org/IService1/GetLevelStatsByIdResponse")]
        System.Threading.Tasks.Task<final_project4.ServiceReference1.LevelStats> GetLevelStatsByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLevelStats", ReplyAction="http://tempuri.org/IService1/GetLevelStatsResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.LevelStats>> GetLevelStatsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLevelStatsPerUser", ReplyAction="http://tempuri.org/IService1/GetLevelStatsPerUserResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.LevelStats>> GetLevelStatsPerUserAsync(string Name);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetLevelStatsByLevelId", ReplyAction="http://tempuri.org/IService1/GetLevelStatsByLevelIdResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.LevelStats>> GetLevelStatsByLevelIdAsync(int LevelId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetAvgScore", ReplyAction="http://tempuri.org/IService1/GetAvgScoreResponse")]
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.UserAndLevel>> GetAvgScoreAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/test", ReplyAction="http://tempuri.org/IService1/testResponse")]
        System.Threading.Tasks.Task<int> testAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/black", ReplyAction="http://tempuri.org/IService1/blackResponse")]
        System.Threading.Tasks.Task<bool> blackAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : final_project4.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<final_project4.ServiceReference1.IService1>, final_project4.ServiceReference1.IService1 {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public Service1Client() : 
                base(Service1Client.GetDefaultBinding(), Service1Client.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IService1.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), Service1Client.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<bool> HasUsernameAsync(string username) {
            return base.Channel.HasUsernameAsync(username);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterUserAsync(final_project4.ServiceReference1.User user) {
            return base.Channel.RegisterUserAsync(user);
        }
        
        public System.Threading.Tasks.Task<bool> ValidateUserAsync(final_project4.ServiceReference1.User user) {
            return base.Channel.ValidateUserAsync(user);
        }
        
        public System.Threading.Tasks.Task<bool> AddStatsAsync(final_project4.ServiceReference1.LevelStats levelStats) {
            return base.Channel.AddStatsAsync(levelStats);
        }
        
        public System.Threading.Tasks.Task<final_project4.ServiceReference1.User> FindUserAsync(string username) {
            return base.Channel.FindUserAsync(username);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.User>> GetUsersAsync() {
            return base.Channel.GetUsersAsync();
        }
        
        public System.Threading.Tasks.Task<final_project4.ServiceReference1.LevelStats> GetLevelStatsByIdAsync(int id) {
            return base.Channel.GetLevelStatsByIdAsync(id);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.LevelStats>> GetLevelStatsAsync() {
            return base.Channel.GetLevelStatsAsync();
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.LevelStats>> GetLevelStatsPerUserAsync(string Name) {
            return base.Channel.GetLevelStatsPerUserAsync(Name);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.LevelStats>> GetLevelStatsByLevelIdAsync(int LevelId) {
            return base.Channel.GetLevelStatsByLevelIdAsync(LevelId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<final_project4.ServiceReference1.UserAndLevel>> GetAvgScoreAsync() {
            return base.Channel.GetAvgScoreAsync();
        }
        
        public System.Threading.Tasks.Task<int> testAsync() {
            return base.Channel.testAsync();
        }
        
        public System.Threading.Tasks.Task<bool> blackAsync() {
            return base.Channel.blackAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1)) {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/Design_Time_Addresses/FInalProject/Service1/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return Service1Client.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return Service1Client.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        public enum EndpointConfiguration {
            
            BasicHttpBinding_IService1,
        }
    }
}
