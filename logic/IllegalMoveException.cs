using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_games
{
    internal class IllegalMoveException : Exception
    {
        public IllegalMoveException(string msg) : base(msg) { }
    }
}
