using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text.RegularExpressions;

namespace CustomValidation
{
    public class Validation : IParameterInspector
    {

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
        }

        public object BeforeCall(string operationName, object[] inputs)
        {

            if (operationName == "AddRemarksById")
            {
                Regex Regex = new Regex("^[a-zA-Z ]+$");

                if ((int)inputs[0] < 0)
                {
                    throw new FaultException("Id invalid");
                }
                if (Regex.IsMatch(inputs[1].ToString()))
                {
                    return inputs;
                }
                else
                {
                    throw new FaultException("Not a valid alphabet");

                }
                
            }
            else if (operationName == "SearchById")
            {
                if ((int)inputs[0] < 0)
                {
                    throw new FaultException("Id invalid");
                }
              
            }
            else if (operationName == "SearchByName")
            {
                Regex Regex = new Regex("^[a-zA-Z ]+$");
                if (Regex.IsMatch(inputs[0].ToString()))
                {
                    return inputs;
                }
                else
                {
                    throw new FaultException("Not a valid alphabet");

                }
            }
            else if (operationName == "GetAllEmployeesHavingRemark")
            {
                Regex Regex = new Regex("^[a-zA-Z ]+$");
                if (Regex.IsMatch(inputs[0].ToString()))
                {
                    return inputs;
                }
                else
                {
                    throw new FaultException("Not a valid alphabet");

                }
            }

            return null;
        }
    }
    public class ValidationBehavior : IEndpointBehavior
    {
        private bool enabled;

        internal ValidationBehavior(bool enabled)
        {
            this.enabled = enabled;
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public void AddBindingParameters(ServiceEndpoint serviceEndpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior( ServiceEndpoint endpoint,ClientRuntime clientRuntime)
        {
            //If enable is not true in the config we do not apply the Parameter Inspector
            if (false == this.enabled)
            {
                return;
            }

            foreach (ClientOperation clientOperation in clientRuntime.Operations)
            {
                clientOperation.ParameterInspectors.Add(
                    new Validation());
            }

        }

       public void ApplyDispatchBehavior(ServiceEndpoint endpoint,EndpointDispatcher endpointDispatcher)
       {
            if (false == this.enabled)
            {
                return;
            }

            foreach (DispatchOperation dispatchOperation in endpointDispatcher.DispatchRuntime.Operations)
            {

                dispatchOperation.ParameterInspectors.Add(
                    new Validation());
            }

        }

        public void Validate(ServiceEndpoint serviceEndpoint)
        {

        }


    }
    public class CustomBehaviorSection : BehaviorExtensionElement
    {
        private const string EnabledAttributeName = "enabled";
        [ConfigurationProperty(EnabledAttributeName, DefaultValue = true, IsRequired = false)]
        public bool Enabled
        {
            get { return (bool)base[EnabledAttributeName]; }
            set { base[EnabledAttributeName] = value; }
        }

        protected override object CreateBehavior()
        {
            return new ValidationBehavior(this.Enabled);

        }
        public override Type BehaviorType
        {
            get { return typeof(ValidationBehavior); }
        }
    }
}
