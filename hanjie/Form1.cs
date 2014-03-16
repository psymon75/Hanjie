////////////////////////////////////////////////////////////////////////////////////////////////////
/// \file   Form1.cs
///
/// \brief  Implements the form 1 class.
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace hanjie
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// \class  Form1
    ///
    /// \brief  A form 1.
    ///
    /// \author Simon Menetrey
    /// \date   16.03.2014
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class Form1 : Form
    {
        /// \brief  The default height.
        const int DEFAULT_HEIGHT = 10;
        /// \brief  The default width.
        const int DEFAULT_WIDTH = 10;
        /// \brief  The default cell size.
        const int DEFAULT_CELL_SIZE = 10;
        /// \brief  The default picross height.
        const int DEFAULT_PICROSS_HEIGHT = 30;
        /// \brief  The default picross width.
        const int DEFAULT_PICROSS_WIDTH = 30;


        /// \brief  The g.
        Graphics _g;

        /// \brief  The puzzle.
        Puzzle _puzzle;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \property   internal Puzzle Puzzle
        ///
        /// \brief  Gets or sets the puzzle.
        ///
        /// \return The puzzle.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        internal Puzzle Puzzle
        {
            get { return _puzzle; }
            set { _puzzle = value; }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn public Form1()
        ///
        /// \brief  Default constructor.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Form1()
        {
            InitializeComponent();
            this.Puzzle = new Puzzle();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private bool[,] ImgToMatrix(Bitmap bmp, int matrixHeight, int matrixWidth)
        ///
        /// \brief  Image to matrix.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  bmp             The bitmap.
        /// \param  matrixHeight    Height of the matrix.
        /// \param  matrixWidth     Width of the matrix.
        ///
        /// \return A bool[,].
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool[,] ImgToMatrix(Bitmap bmp, int matrixHeight, int matrixWidth)
        {
            bool[,] matrix = new bool[matrixWidth, matrixHeight];
            int stepHeight = bmp.Height / matrixHeight;
            int stepWidth = bmp.Width / matrixWidth;
            bmp = niveauxDeGrisScan(bmp);
            Bitmap bad = ImageUtilities.ResizeImage((Image)bmp, 10, 10);

            //http://www.youtube.com/watch?v=DcaVYuyXTMs
            //Idée : redémensionner l'image en 10x10 puit récupérer les pixels pour remplir la matrix
            //Regarder les methode de Image pour faire du rééchantillionnage (image sampling)
            return matrix;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private Bitmap niveauxDeGrisScan(Bitmap bmp)
        ///
        /// \brief  Niveaux de gris scan.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \exception  ArgumentException   Thrown when one or more arguments have unsupported or illegal
        ///                                 values.
        ///
        /// \param  bmp The bitmap.
        ///
        /// \return A Bitmap.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private Bitmap niveauxDeGrisScan(Bitmap bmp)
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
                        byte yolo = (byte)((moyenne >= 120) ? 255 : 0);
                        p[0] = yolo;
                        p[1] = yolo;
                        p[2] = yolo;
                        p += 3; // passage au pixel suivant
                    }
                    p += offset;
                }
            }
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void panel1_MouseDown(object sender, MouseEventArgs e)
        ///
        /// \brief  Event handler. Called by panel1 for mouse down events.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  sender  Source of the event.
        /// \param  e       Mouse event information.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                _g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(e.X - e.X % DEFAULT_CELL_SIZE - 1, e.Y - e.Y % DEFAULT_CELL_SIZE - 1, DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE));
                this.Puzzle.UpdatePixel(ConvertPixelPoint(e.Location),true);
             
                
                //_g.DrawRectangle(new Pen(Color.Black), new Rectangle(e.X - e.X % DEFAULT_CELL_SIZE, e.Y - e.Y % DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE));
            }
            else if (e.Button == MouseButtons.Right)
            {
                _g.FillRectangle(new SolidBrush(panel1.BackColor), new Rectangle(e.X - e.X % DEFAULT_CELL_SIZE - 1, e.Y - e.Y % DEFAULT_CELL_SIZE - 1, DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE));
                _g.DrawRectangle(new Pen(Color.Gray), new Rectangle(e.X - e.X % DEFAULT_CELL_SIZE - 1, e.Y - e.Y % DEFAULT_CELL_SIZE - 1, DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE));
                this.Puzzle.UpdatePixel(ConvertPixelPoint(e.Location),false);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private Point ConvertPixelPoint(Point pixelpoint)
        ///
        /// \brief  Convert pixel point.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  pixelpoint  The pixelpoint.
        ///
        /// \return The pixel converted point.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private Point ConvertPixelPoint(Point pixelpoint)
        {
          return new Point(((pixelpoint.X) - (pixelpoint.X  % DEFAULT_CELL_SIZE - 1)) / DEFAULT_CELL_SIZE, ((pixelpoint.Y) - (pixelpoint.Y % DEFAULT_CELL_SIZE - 1)) / DEFAULT_CELL_SIZE);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void Form1_Load(object sender, EventArgs e)
        ///
        /// \brief  Event handler. Called by Form1 for load events.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  sender  Source of the event.
        /// \param  e       Event information.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void Form1_Load(object sender, EventArgs e)
        {
            _g = panel1.CreateGraphics();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void panel1_Paint(object sender, PaintEventArgs e)
        ///
        /// \brief  Event handler. Called by panel1 for paint events.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  sender  Source of the event.
        /// \param  e       Paint event information.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            for (int y = -1; y <= (DEFAULT_CELL_SIZE * DEFAULT_PICROSS_HEIGHT); y += DEFAULT_CELL_SIZE)
                for (int x = -1; x <= (DEFAULT_CELL_SIZE *DEFAULT_PICROSS_WIDTH ); x += DEFAULT_CELL_SIZE)
                    _g.DrawRectangle(new Pen(Color.Gray), new Rectangle(x,y, DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void DrawAtPosition(int x, int y)
        ///
        /// \brief  Draw at position.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  x   The x coordinate.
        /// \param  y   The y coordinate.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void DrawAtPosition(int x, int y)
        {
            _g.FillRectangle(new SolidBrush(Color.Black), new Rectangle(x * DEFAULT_CELL_SIZE, y * DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE, DEFAULT_CELL_SIZE));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void DrawPuzzle()
        ///
        /// \brief  Draw puzzle.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void DrawPuzzle()
        {
            Font drawFont = new Font("Arial", 8);
            StringFormat drawFormatVertical = new StringFormat(StringFormatFlags.DirectionVertical);
            int countLine = 0;
            int countColums = 0;
            foreach (LineRow line in Puzzle.Lines)
            {
                countColums = 0;
                foreach (char c in line.Flush)
                {
                    if (c == '1')
                        DrawAtPosition(countColums,countLine);
                   
                    countColums++;
                            
                }
                _g.DrawString(line.IndiceSeparator, drawFont, new SolidBrush(Color.Black), (float)((DEFAULT_CELL_SIZE * DEFAULT_PICROSS_WIDTH) + DEFAULT_CELL_SIZE), (float)(DEFAULT_CELL_SIZE * countLine));
                countLine++;
            }

            countColums = 0;
            foreach (LineRow col in Puzzle.Colums)
            {
                 _g.DrawString(col.IndiceSeparator, drawFont, new SolidBrush(Color.Black), (float)((DEFAULT_CELL_SIZE * countColums) - 1), (float)((DEFAULT_CELL_SIZE * DEFAULT_PICROSS_HEIGHT) + DEFAULT_CELL_SIZE), drawFormatVertical);
                 countColums++;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        ///
        /// \brief  Event handler. Called by exporterToolStripMenuItem for click events.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  sender  Source of the event.
        /// \param  e       Event information.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Puzzle.Export(SFD.FileName);
                MessageBox.Show("Exportation réussie !");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        ///
        /// \brief  Event handler. Called by imageToolStripMenuItem for click events.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  sender  Source of the event.
        /// \param  e       Event information.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap img = new Bitmap(OFD.FileName, false);
                Bitmap orig = ImageUtilities.ResizeImage((Image)img, 30, 30);
                Bitmap clone = new Bitmap(orig.Width, orig.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                
                using (Graphics gr = Graphics.FromImage(clone))
                {
                    gr.DrawImage(orig, new Rectangle(0, 0, clone.Width, clone.Height));
                }
                this.Puzzle.FromImage(clone,OFD.FileName);
                DrawPuzzle();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// \fn private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        ///
        /// \brief  Event handler. Called by xMLToolStripMenuItem for click events.
        ///
        /// \author Simon Menetrey
        /// \date   16.03.2014
        ///
        /// \param  sender  Source of the event.
        /// \param  e       Event information.
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Puzzle.Import(OFD.FileName);
                DrawPuzzle();
            }
        }
    }
}
