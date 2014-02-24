using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hanjie
{
    class Puzzle
    {

        private List<Line> _lines;

        internal List<Line> Lines
        {
            get { return _lines; }
            set { _lines = value; }
        }
    }
}
