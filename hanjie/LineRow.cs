////////////////////////////////////////////////////////////////////////////////////////////////////
/// \file   LineRow.cs
///
/// \brief  Implements the line row class.
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hanjie
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// \class  LineRow
    ///
    /// \brief  A line row.
    ///
    /// \author Simon Menetrey
    /// \date   16.03.2014
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class LineRow
    {
        /// \brief  The index.
        private int _index;
        /// \brief  The indice string.
        private string _indiceString;
        /// \brief  The indice separator.
        private string _indiceSeparator;
        /// \brief  The flush.
        private string _flush;
        /// \brief  The indice position.
        private Dictionary<int, int> _indicePosition;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public Dictionary<int, int> IndicePosition
        ///
        /// \brief  Gets or sets the indice position.
        ///
        /// \return The indice position.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Dictionary<int, int> IndicePosition
        {
            get { return _indicePosition; }
            set { _indicePosition = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public string IndiceString
        ///
        /// \brief  Gets or sets the indice string.
        ///
        /// \return The indice string.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string IndiceString
        {
            get { return _indiceString; }
            set { _indiceString = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public int Index
        ///
        /// \brief  Gets or sets the zero-based index of this object.
        ///
        /// \return The index.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public string Flush
        ///
        /// \brief  Gets or sets the flush.
        ///
        /// \return The flush.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Flush
        {
            get { return _flush; }
            set { _flush = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public string IndiceSeparator
        ///
        /// \brief  Gets or sets the indice separator.
        ///
        /// \return The indice separator.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string IndiceSeparator
        {
            get { return _indiceSeparator; }
            set { _indiceSeparator = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public void GenerateFromFlush(string flush)
        ///
        /// \brief  Generates from flush.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  flush   The flush.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public LineRow(int index, string flush, string indice, string separator, Dictionary<int,int> indicepositon)
        ///
        /// \brief  Constructor.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  index           Zero-based index of the.
        /// \param  flush           The flush.
        /// \param  indice          The indice.
        /// \param  separator       The separator.
        /// \param  indicepositon   The indicepositon.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public LineRow(int index, string flush, string indice, string separator, Dictionary<int,int> indicepositon)
        {
            this.Index = index;
            this.Flush = flush;
            this.IndiceString = indice;
            this.IndiceSeparator = separator;
            this.IndicePosition = indicepositon;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public LineRow(int index)
        ///
        /// \brief  Constructor.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  index   Zero-based index of the.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

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
