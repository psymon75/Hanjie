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
    class Puzzle
    {

        private List<Line> _lines;

        internal List<Line> Lines
        {
            get { return _lines; }
            set { _lines = value; }
        }

        public Puzzle()
        {
            this.Lines = new List<Line>();
        }

        public void Import(string xmlFilename)
        {
            XmlDocument xml = new XmlDocument();

            xml.LoadXml(File.ReadAllText(xmlFilename));
            XmlNodeList xnList = xml.SelectNodes("/PicrossPuzzle/Puzzle/Lines/Line");
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
                this.Lines.Add(new Line(int.Parse(xn.Attributes["index"].InnerText),xn["flush"].InnerText,xn["indices_string"].InnerText,xn["indices_string_separator"].InnerText,indiceposition));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bmp">Black/white image</param>
        public void FromImage(Bitmap bmp)
        {
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
            unsafe
            {
                byte* p = (byte*)(void*)Scan0;
                for (int y = 0; y < h; ++y)
                {
                    //p = (byte*)(void*)Scan0 + (y * stride); // p pointe au debut de la ligne ( equivalent a offset )
                    for (int x = 0; x < w; ++x)
                    {
                        int moyenne = (p[0] + p[1] + p[2]) / 3;
                        byte yolo = (byte)((moyenne >= 100) ? 255 : 0);
                        p[0] = yolo;
                        p[1] = yolo;
                        p[2] = yolo;
                        p += 3; // passage au pixel suivant
                    }
                    p += offset;
                }
            }
            bmp.UnlockBits(bmpData);
        }
    }
}
