using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lumi;

internal class PopUp : Form
{
	private IContainer components;

	private Panel header;

	private Label headerTitle;

	private ProgressBar progressBar1;

	private PictureBox pictureBox1;

	private Label content;

	public static int R { get; set; }

	public static int G { get; set; }

	public static int B { get; set; }

	public static string Title { get; set; }

	public static string Content { get; set; }

	public PopUp()
	{
		InitializeComponent();
	}

    public static void ShowPopup(string title, string content, int r, int g, int b)
    {
        Title = title;
        Content = content;
        R = r;
        G = g;
        B = b;

        PopUp popup = new PopUp();
        popup.Show();
    }

    private async void PopUp_Load(object sender, EventArgs e)
	{
		headerTitle.ForeColor = Color.FromArgb(R, G, B);
		progressBar1.ForeColor = Color.FromArgb(R, G, B);
		// Position popup in center
		this.StartPosition = FormStartPosition.CenterScreen;
		headerTitle.Text = Title;
		content.Text = Content;
		await Task.Delay(5000);
		Close();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
            this.header = new System.Windows.Forms.Panel();
            this.headerTitle = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.content = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // header
            // 
            this.header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.header.Controls.Add(this.headerTitle);
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(440, 40);
            this.header.TabIndex = 8;
            // 
            // headerTitle
            // 
            this.headerTitle.BackColor = System.Drawing.Color.Transparent;
            this.headerTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.headerTitle.Location = new System.Drawing.Point(12, 9);
            this.headerTitle.Name = "headerTitle";
            this.headerTitle.Size = new System.Drawing.Size(420, 18);
            this.headerTitle.TabIndex = 2;
            this.headerTitle.Text = "TITLE";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 118);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(440, 2);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 10;
            // 
            // content
            // 
            this.content.BackColor = System.Drawing.Color.Transparent;
            this.content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.content.ForeColor = System.Drawing.Color.White;
            this.content.Location = new System.Drawing.Point(71, 70);
            this.content.Name = "content";
            this.content.Size = new System.Drawing.Size(350, 18);
            this.content.TabIndex = 11;
            this.content.Text = "CONTENT";
            this.content.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Location = new System.Drawing.Point(17, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // PopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(440, 120);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.content);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(440, 120);
            this.Name = "PopUp";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PopUp";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PopUp_Load);
            this.header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

	}
}
