using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hanjie
{
    class Line
    {
        private int _index;
        private string _indiceString;
        private string _indiceSeparator;
        private string _flush;
        private Dictionary<int, int> _indicePosition;

        public Dictionary<int, int> IndicePosition
        {
            get { return _indicePosition; }
            set { _indicePosition = value; }
        }

        public string IndiceString
        {
            get { return _indiceString; }
            set { _indiceString = value; }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public string Flush
        {
            get { return _flush; }
            set { _flush = value; }
        }

        public string IndiceSeparator
        {
            get { return _indiceSeparator; }
            set { _indiceSeparator = value; }
        }

       

        public Line(int index, string flush, string indice, string separator, Dictionary<int,int> indicepositon)
        {
            this.Index = index;
            this.Flush = flush;
            this.IndiceString = indice;
            this.IndiceSeparator = separator;
            this.IndicePosition = indicepositon;
        }
    }
}
