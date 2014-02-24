using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hanjie
{
    class Line
    {
        private int _index;

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private int _flush;

        public int Flush
        {
            get { return _flush; }
            set { _flush = value; }
        }

        private int _nbIndice;

        public int NbIndice
        {
            get { return _nbIndice; }
            set { _nbIndice = value; }
        }

        private int _separator;

        public int Separator
        {
            get { return _separator; }
            set { _separator = value; }
        }

        public Line()
        {

        }
    }
}
