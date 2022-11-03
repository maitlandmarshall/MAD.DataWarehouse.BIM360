using System;
using System.Runtime.Serialization;

namespace MAD.DataWarehouse.BIM360.Jobs
{
    [Serializable]
    internal class ReportRunStatusException : Exception
    {
        public ReportRunStatusException()
        {
        }

        public ReportRunStatusException(string message) : base(message)
        {
        }

        public ReportRunStatusException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReportRunStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}