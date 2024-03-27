using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HotelBookingSystem.Utils.Exceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException(string name):base("Unable to Perform Operation "+ name.SplitCamelCase()) 
        {

        }
    }
}
