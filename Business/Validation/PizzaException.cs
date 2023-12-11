using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    [Serializable]
    public class PizzaException : Exception
    {
        private static readonly string DefaultMessage = "Pizza exception was thrown.";

        public PizzaException() : base(DefaultMessage)
        {

        }

        public PizzaException(string message) : base(message)
        {

        }

        public PizzaException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected PizzaException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
