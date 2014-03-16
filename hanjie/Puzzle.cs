////////////////////////////////////////////////////////////////////////////////////////////////////
/// \file   Puzzle.cs
///
/// \brief  Implements the puzzle class.
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace hanjie
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// \class  Puzzle
    ///
    /// \brief  A puzzle.
    ///
    /// \author Simon Menetrey
    /// \date   16.03.2014
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    class Puzzle
    {

        /// \brief  The lines.
        private List<LineRow> _lines;
        /// \brief  The colums.
        private List<LineRow> _colums;
        /// \brief  The nb lines.
        int _nbLines;
        /// \brief  The nb colums.
        int _nbColums;
        /// \brief  The name.
        string _name;

        #region Properties

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public string Name
        ///
        /// \brief  Gets or sets the name.
        ///
        /// \return The name.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public int NbColums
        ///
        /// \brief  Gets or sets the nb colums.
        ///
        /// \return The nb colums.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int NbColums
        {
            get { return _nbColums; }
            set { _nbColums = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   public int NbLines
        ///
        /// \brief  Gets or sets the nb lines.
        ///
        /// \return The nb lines.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int NbLines
        {
            get { return _nbLines; }
            set { _nbLines = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   internal List<LineRow> Colums
        ///
        /// \brief  Gets or sets the colums.
        ///
        /// \return The colums.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal List<LineRow> Colums
        {
            get { return _colums; }
            set { _colums = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   internal List<LineRow> Lines
        ///
        /// \brief  Gets or sets the lines.
        ///
        /// \return The lines.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal List<LineRow> Lines
        {
            get { return _lines; }
            set { _lines = value; }
        }
        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public Puzzle()
        ///
        /// \brief  Default constructor.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Puzzle()
        {
            this.Lines = new List<LineRow>();
            this.Colums = new List<LineRow>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public void Import(string xmlFilename)
        ///
        /// \brief  Imports puzzle from xml.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  xmlFilename Filename of the XML file.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Import(string xmlFilename)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(File.ReadAllText(xmlFilename));
            XmlNodeList xnList = xml.SelectNodes("/PicrossPuzzle/Informations");
            foreach (XmlNode xn in xnList)
            {
                this.Name = xn["Name"].InnerText;
                this.NbLines = int.Parse(xn["NbLines"].Attributes["dim"].InnerText);
                this.NbColums = int.Parse(xn["NbRows"].Attributes["dim"].InnerText);
            }

            xnList = xml.SelectNodes("/PicrossPuzzle/Puzzle/Lines/Line");
            foreach (XmlNode xn in xnList)
            {
                Dictionary<int, int> indiceposition = new Dictionary<int,int>();
                if(xn["indices_string"].InnerText != "")
                {
                    
                    string[] indices = xn["indices_string"].InnerText.Split(' ');
                    int index = 0;
                    foreach (string indice in indices)
	                {
		                indiceposition.Add(index,int.Parse(indice));
                        index++;
	                }
                }
                this.Lines.Add(new LineRow(int.Parse(xn.Attributes["index"].InnerText),xn["flush"].InnerText,xn["indices_string"].InnerText,xn["indices_string_separator"].InnerText,indiceposition));
            }
            xnList = xml.SelectNodes("/PicrossPuzzle/Puzzle/Rows/Row");
            foreach (XmlNode xn in xnList)
            {
                Dictionary<int, int> indiceposition = new Dictionary<int, int>();
                if (xn["indices_string"].InnerText != "")
                {

                    string[] indices = xn["indices_string"].InnerText.Split(' ');
                    int index = 0;
                    foreach (string indice in indices)
                    {
                        indiceposition.Add(index, int.Parse(indice));
                        index++;
                    }
                }
                this.Colums.Add(new LineRow(int.Parse(xn.Attributes["index"].InnerText), xn["flush"].InnerText, xn["indices_string"].InnerText, xn["indices_string_separator"].InnerText, indiceposition));
            }

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public void Export(string filename)
        ///
        /// \brief  Exports to xml.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  filename    Filename of the file.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Export(string filename)
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("PicrossPuzzle");
            XmlElement puzzle = doc.CreateElement("Puzzle");

            //INFORMATIONS
            XmlElement informations = doc.CreateElement("Informations");
            XmlElement name = doc.CreateElement("Name");
            name.InnerText = this.Name;
            informations.AppendChild(name);
            XmlElement createddate = doc.CreateElement("CreatedDate");
            createddate.SetAttribute("date", DateTime.Now.ToString());
            createddate.InnerText = DateTime.Now.ToString();
            informations.AppendChild(createddate);
            XmlElement createdby = doc.CreateElement("CreatedBy");
            createdby.SetAttribute("version", "1.0");
            createdby.SetAttribute("name", "PSN");
            informations.AppendChild(createdby);
            XmlElement nbLines = doc.CreateElement("NbLines");
            nbLines.SetAttribute("dim", this.NbLines.ToString());
            informations.AppendChild(nbLines);
            XmlElement nbColumns = doc.CreateElement("NbRows");
            nbColumns.SetAttribute("dim", this.NbColums.ToString());
            informations.AppendChild(nbColumns);
            root.AppendChild(informations);
            //

            XmlElement lines = doc.CreateElement("Lines");
            int count = 0;

            foreach (LineRow line in this.Lines)
            {
                XmlElement element = doc.CreateElement("Line");
                element.SetAttribute("index", count.ToString());

                XmlElement flush = doc.CreateElement("flush");
                flush.InnerText = line.Flush;
                element.AppendChild(flush);
                XmlElement indices_string = doc.CreateElement("indices_string");
                indices_string.InnerText = line.IndiceString;
                element.AppendChild(indices_string);
                XmlElement indices_string_seprator = doc.CreateElement("indices_string_separator");
                indices_string_seprator.InnerText = line.IndiceSeparator;
                element.AppendChild(indices_string_seprator);
                for (int i = 0; i < line.IndicePosition.Count; i++)
                {
                    XmlElement indice = doc.CreateElement("indice");
                    indice.SetAttribute("position",i.ToString());
                    indice.InnerText = line.IndicePosition[i].ToString();
                    element.AppendChild(indice);
                }

                lines.AppendChild(element);
                count++;
            }
            puzzle.AppendChild(lines);
            count = 0;
            XmlElement columns = doc.CreateElement("Rows");
            foreach (LineRow column in this.Colums)
            {
                XmlElement element = doc.CreateElement("Row");
                element.SetAttribute("index", count.ToString());

                XmlElement flush = doc.CreateElement("flush");
                flush.InnerText = column.Flush;
                element.AppendChild(flush);
                XmlElement indices_string = doc.CreateElement("indices_string");
                indices_string.InnerText = column.IndiceString;
                element.AppendChild(indices_string);
                XmlElement indices_string_seprator = doc.CreateElement("indices_string_separator");
                indices_string_seprator.InnerText = column.IndiceSeparator;
                element.AppendChild(indices_string_seprator);
                for (int i = 0; i < column.IndicePosition.Count; i++)
                {
                    XmlElement indice = doc.CreateElement("indice");
                    indice.SetAttribute("position",i.ToString());
                    indice.InnerText = column.IndicePosition[i].ToString();
                    element.AppendChild(indice);
                }

                columns.AppendChild(element);
                count++;
            }
            puzzle.AppendChild(columns);

            
            root.AppendChild(puzzle);
            
            doc.AppendChild(root);
            doc.Save(filename);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public void FromImage(Bitmap bmp, string filename)
        ///
        /// \brief  Import from image.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \exception  ArgumentException   Thrown when one or more arguments have unsupported or illegal
        ///                                 values.
        ///
        /// \param  bmp         Black/white image.
        /// \param  filename    Filename of the file.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void FromImage(Bitmap bmp, string filename)
        {
            this.Lines.Clear();
            this.Colums.Clear();
            this.Name = Path.GetFileNameWithoutExtension(filename);
            this.NbColums = bmp.Width;
            this.NbLines = bmp.Height;

            if (bmp.PixelFormat != PixelFormat.Format24bppRgb)
            {
                throw new ArgumentException("bmp.PixelFormat != PixelFormat.Format24bppRgb");
            }
            int w = bmp.Width;
            int h = bmp.Height;
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, w, h),
            ImageLockMode.ReadWrite,
            bmp.PixelFormat);
            int stride = bmpData.Stride; // largeur d'une ligne de pixels
            int offset = (stride - (w * 3));
            System.IntPtr Scan0 = bmpData.Scan0; // scan0 pointe vers les données
           
            // du bitmap
            // //Create collums
            for (int i = 0; i < w; i++)
            {
                this.Colums.Add(new LineRow(i));
            }
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                for (int y = 0; y < h; ++y)
                {
                    LineRow line = new LineRow(y);
                    //p = (byte*)(void*)Scan0 + (y * stride); // p pointe au debut de la ligne ( equivalent a offset )
                    for (int x = 0; x < w; ++x)
                    {
                        
                        int moyenne = (p[0] + p[1] + p[2]) / 3;
                        this.Colums[x].Flush += ((moyenne >= 100) ? "0" : "1");
                        line.Flush += ((moyenne >= 100) ? "0" : "1");
                        p += 3; // passage au pixel suivant
                    }
                    line.GenerateFromFlush(line.Flush);
                    this.Lines.Add(line);
                    p += offset;
                }
            }
            for (int i = 0; i < this.Colums.Count; i++)
            {
                this.Colums[i].GenerateFromFlush(this.Colums[i].Flush);
            }
            bmp.UnlockBits(bmpData);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public void UpdatePixel(Point position, bool enable)
        ///
        /// \brief  Updates the pixel.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  position    The position.
        /// \param  enable      true to enable, false to disable.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void UpdatePixel(Point position, bool enable)
        {
            this.Lines[position.Y].Flush = this.Lines[position.Y].Flush.Remove(position.X, 1);
            this.Lines[position.Y].Flush = this.Lines[position.Y].Flush.Insert(position.X, (enable) ? "1" : "0");
            this.Lines[position.Y].GenerateFromFlush(this.Lines[position.Y].Flush);


            this.Colums[position.X].Flush = this.Colums[position.X].Flush.Remove(position.Y, 1);
            this.Colums[position.X].Flush = this.Colums[position.X].Flush.Insert(position.Y, (enable) ? "1" : "0");
            this.Colums[position.X].GenerateFromFlush(this.Lines[position.X].Flush);
        }
    }
}
