using System;
using System.Runtime.Serialization;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    [Serializable]
    internal class DesignAutomationStateException : Exception
    {
        public DesignAutomationStateException()
        {
        }

        public DesignAutomationStateException(string message) : base(message)
        {
        }

        public DesignAutomationStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DesignAutomationStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}