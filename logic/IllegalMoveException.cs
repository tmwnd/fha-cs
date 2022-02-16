using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Games
{
    internal class IllegalMoveException : Exception
    {
        public IllegalMoveException(string msg) : base(msg) { }
    }
}
