﻿using System;
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
    public partial class Form1 : Form
    {
        const int DEFAULT_HEIGHT = 10;
        const int DEFAULT_WIDTH = 10;

        bool[,] imgMatrix;

        public Form1()
        {
            InitializeComponent();
        }

        private bool[,] ImgToMatrix(Bitmap bmp, int matrixHeight, int matrixWidth)
        {
            bool[,] matrix = new bool[matrixWidth, matrixHeight];
            int stepHeight = bmp.Height / matrixHeight;
            int stepWidth = bmp.Width / matrixWidth;
            bmp = niveauxDeGrisScan(bmp);
            pictureBox1.Image = (Image)bmp;
            //http://www.youtube.com/watch?v=DcaVYuyXTMs
            //Idée : redémensionner l'image en 10x10 puit récupérer les pixels pour remplir la matrix
            //Regarder les methode de Image pour faire du rééchantillionnage (image sampling)
            return matrix;
        }

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
                        byte yolo = (byte)((moyenne >= 100) ? 255 : 0);
                        p[0] = yolo;
                        p[1] = yolo;
                        p[2] = yolo;
                        p += ; // passage au pixel suivant
                    }
                    p += offset;
                }
            }
            bmp.UnlockBits(bmpData);
            return bmp;
        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OFD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap img = new Bitmap(OFD.FileName, false);
                this.imgMatrix = this.ImgToMatrix(img,DEFAULT_HEIGHT, DEFAULT_WIDTH);
            }
        }
    }
}