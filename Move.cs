using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    internal class Move
    {
            public int YFrom, XFrom;
            public int YTo, XTo;
            public int yeat, xeat;
            public int Nikot;

            public Move firstMove;

            public Move()
            {
                YFrom = -1;
            }

            public Move(int _YFrom, int _XFrom, int _yeat, int _xeat, int _YTo, int _XTo, int _Nikot)
            {
                YFrom = _YFrom; XFrom = _XFrom;
                yeat = _yeat; xeat = _xeat;
                YTo = _YTo; XTo = _XTo;
                Nikot = _Nikot;
            }

        }
    
}
