namespace hanjie
{
  partial class Form1
  {
    /// <summary>
    /// Variable nécessaire au concepteur.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Nettoyage des ressources utilisées.
    /// </summary>
    /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Code généré par le Concepteur Windows Form

    /// <summary>
    /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
    /// le contenu de cette méthode avec l'éditeur de code.
    /// </summary>
    private void InitializeComponent()
    {
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.importerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.partieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.OFD = new System.Windows.Forms.OpenFileDialog();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.menuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.SuspendLayout();
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.partieToolStripMenuItem});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(542, 24);
        this.menuStrip1.TabIndex = 1;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // fichierToolStripMenuItem
        // 
        this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importerToolStripMenuItem,
            this.quitterToolStripMenuItem});
        this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
        this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
        this.fichierToolStripMenuItem.Text = "Fichier";
        // 
        // importerToolStripMenuItem
        // 
        this.importerToolStripMenuItem.Name = "importerToolStripMenuItem";
        this.importerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        this.importerToolStripMenuItem.Text = "Importer...";
        this.importerToolStripMenuItem.Click += new System.EventHandler(this.importerToolStripMenuItem_Click);
        // 
        // quitterToolStripMenuItem
        // 
        this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
        this.quitterToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
        this.quitterToolStripMenuItem.Text = "Quitter";
        // 
        // partieToolStripMenuItem
        // 
        this.partieToolStripMenuItem.Name = "partieToolStripMenuItem";
        this.partieToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
        this.partieToolStripMenuItem.Text = "Partie";
        // 
        // OFD
        // 
        this.OFD.FileName = "openFileDialog1";
        // 
        // pictureBox1
        // 
        this.pictureBox1.Location = new System.Drawing.Point(12, 27);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(518, 422);
        this.pictureBox1.TabIndex = 2;
        this.pictureBox1.TabStop = false;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(542, 461);
        this.Controls.Add(this.pictureBox1);
        this.Controls.Add(this.menuStrip1);
        this.MainMenuStrip = this.menuStrip1;
        this.Name = "Form1";
        this.Text = "Form1";
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importerToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem partieToolStripMenuItem;
    private System.Windows.Forms.OpenFileDialog OFD;
    private System.Windows.Forms.PictureBox pictureBox1;
  }
}

