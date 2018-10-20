using System;
using System.Collections.Generic;
using System.Text;

namespace Daisy.Exceptions
{
    class MergeException : Exception
    {
        public MergeException(string message) : base(message)
        {
        }
    }
}
