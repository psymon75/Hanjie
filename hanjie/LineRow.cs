using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hanjie
{
    class LineRow
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

        public void GenerateFromFlush(string flush)
        {
            this.IndiceString = "";
            this.IndiceSeparator = "";
            this.IndicePosition = new Dictionary<int, int>();
            int count = 0;
            int indexSequence = 0;
            bool isInSequence = false;
            foreach (char c in flush)
            {
                if (c == '1')
                {
                    isInSequence = true;
                    count++;
                }
                else if(c == '0' && count != 0)
                {
                    isInSequence = false;
                    this.IndiceString += count.ToString() + " ";
                    this.IndiceSeparator += count.ToString() + ", ";
                    this.IndicePosition.Add(indexSequence, count);
                    count = 0;
                    indexSequence++;
                }
            }
            this.IndiceString = this.IndiceString.TrimEnd();
            if(this.IndiceSeparator != "")
                this.IndiceSeparator = this.IndiceSeparator.Remove(this.IndiceSeparator.LastIndexOf(','));

        }

        public LineRow(int index, string flush, string indice, string separator, Dictionary<int,int> indicepositon)
        {
            this.Index = index;
            this.Flush = flush;
            this.IndiceString = indice;
            this.IndiceSeparator = separator;
            this.IndicePosition = indicepositon;
        }

        public LineRow(int index)
        {
            this.Index = index;
            this.Flush = "";
            this.IndiceString = "";
            this.IndiceSeparator = "";
            this.IndicePosition = new Dictionary<int, int>();
        }
    }
}
