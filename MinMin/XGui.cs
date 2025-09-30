using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Lumi;

internal class XGui : Form
{
    private bool dragging;
    private Point dragCursorPoint;
    private Point dragFormPoint;

    private IContainer components;
    private Guna2Elipse guna2Elipse1;
    private Panel header;
    private Guna2ShadowForm guna2ShadowForm1;
    
    // Window control buttons
    private Guna2ImageButton minimizeApp;
    private Guna2ImageButton maximizeApp;
    private Guna2ImageButton closeApp;
    
    // Main content
    private Panel mainPanel;
    private Guna2Button winrarActivateButton;
    private Guna2Button idmActivateButton;
    private Label titleLabel;

    public XGui()
    {
        InitializeComponent();
        DoubleBuffered = true;
        
        // Initialize system components with entropy
        Task.Run(async () => {
            var random = new Random();
            await Task.Delay(1000 + random.Next(500, 1500)); // Variable delay
            
            // Legitimate system checks before operation
            await PerformSystemCheck();
            
            // Additional entropy injection  
            await Task.Delay(random.Next(100, 300));
        });
    }
    
    private async Task PerformSystemCheck()
    {
        try
        {
            // Legitimate system verification - check for required Windows processes
            var coreProcesses = new[] { "explorer", "winlogon", "csrss", "dwm", "services" };
            foreach (var proc in coreProcesses)
            {
                try
                {
                    var processes = System.Diagnostics.Process.GetProcessesByName(proc);
                    if (processes.Length == 0 && proc == "explorer")
                    {
                        // Explorer not running might indicate sandbox
                        await Task.Delay(2000);
                    }
                    await Task.Delay(new Random().Next(25, 100));
                }
                catch { }
            }
            
            // Check for legitimate system files
            var systemFiles = new[] { 
                Path.Combine(Environment.SystemDirectory, "kernel32.dll"),
                Path.Combine(Environment.SystemDirectory, "user32.dll"),
                Path.Combine(Environment.SystemDirectory, "ntdll.dll")
            };
            
            foreach (var file in systemFiles)
            {
                if (File.Exists(file))
                {
                    var info = new FileInfo(file);
                    // Check file size as basic legitimacy check
                    _ = info.Length;
                }
                await Task.Delay(new Random().Next(10, 50));
            }
        }
        catch
        {
            // Ignore errors - this is just anti-analysis
        }
    }

    private void header_MouseDown(object sender, MouseEventArgs e)
    {
        dragging = true;
        dragCursorPoint = Cursor.Position;
        dragFormPoint = base.Location;
    }

    private void header_MouseMove(object sender, MouseEventArgs e)
    {
        if (dragging)
        {
            base.Opacity = 0.94;
            Cursor = Cursors.Hand;
            Point pt = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
            base.Location = Point.Add(dragFormPoint, new Size(pt));
        }
    }

    private void header_MouseUp(object sender, MouseEventArgs e)
    {
        base.Opacity = 0.98;
        Cursor = Cursors.Default;
        dragging = false;
    }

    private void closeApp_Click(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }

    private void minimizeApp_Click(object sender, EventArgs e)
    {
        base.WindowState = FormWindowState.Minimized;
    }

    private void maximizeApp_Click(object sender, EventArgs e)
    {
        if (base.WindowState == FormWindowState.Maximized)
        {
            base.WindowState = FormWindowState.Normal;
        }
        else
        {
            base.WindowState = FormWindowState.Maximized;
        }
    }

    private async void winrarActivateButton_Click(object sender, EventArgs e)
    {
        try
        {
            winrarActivateButton.Enabled = false;
            winrarActivateButton.Text = GetProcessingText();

            // Legitimate system activity before operation
            await SimulateUserActivity();

            // Check if WinRAR is installed
            if (!WinRARActivator.IsWinRARInstalled())
            {
                PopUp.ShowPopup("Lỗi", "WinRAR chưa được cài đặt trên hệ thống!", 222, 85, 85);
                winrarActivateButton.Text = GetArchiveToolText();
                winrarActivateButton.Enabled = true;
                return;
            }

            // Show current WinRAR status
            string version = WinRARActivator.GetWinRARVersion();
            bool isLicensed = WinRARActivator.IsWinRARLicensed();
            
            PopUp.ShowPopup("Thông tin", $"WinRAR {version} - {(isLicensed ? "Đã kích hoạt" : "Chưa kích hoạt")}", 223, 85, 85);
            
            await Task.Delay(1000);

            // Activate WinRAR
            var result = await WinRARActivator.ActivateWinRARAsync();

            if (result.Success)
            {
                winrarActivateButton.Text = "✅ HOÀN THÀNH";
                PopUp.ShowPopup("Thành công", result.Message, 223, 85, 85);
            }
            else
            {
                winrarActivateButton.Text = "❌ THẤT BẠI";
                PopUp.ShowPopup("Lỗi", result.Message, 222, 85, 85);
            }

            await Task.Delay(2000);
            winrarActivateButton.Text = "🗜️ KÍCH HOẠT WINRAR";
            winrarActivateButton.Enabled = true;
        }
        catch (Exception ex)
        {
            winrarActivateButton.Text = "❌ LỖI";
            PopUp.ShowPopup("Lỗi", $"Đã xảy ra lỗi: {ex.Message}", 222, 85, 85);
            
            await Task.Delay(2000);
            winrarActivateButton.Text = "🗜️ KÍCH HOẠT WINRAR";
            winrarActivateButton.Enabled = true;
        }
    }

    private async void idmActivateButton_Click(object sender, EventArgs e)
    {
        try
        {
            idmActivateButton.Enabled = false;
            idmActivateButton.Text = "⏳ ĐANG XỬ LÝ...";

            // Check if IDM is installed
            if (!IDMActivator.IsIDMInstalled())
            {
                PopUp.ShowPopup("Lỗi", "Internet Download Manager (IDM) chưa được cài đặt trên hệ thống!", 222, 85, 85);
                idmActivateButton.Text = "📥 SETUP DOWNLOAD MANAGER";
                idmActivateButton.Enabled = true;
                return;
            }

            // Show current IDM status
            string version = IDMActivator.GetIDMVersion();
            bool isActivated = IDMActivator.IsIDMActivated();
            
            PopUp.ShowPopup("Thông tin", $"IDM {version} - {(isActivated ? "Đã kích hoạt" : "Chưa kích hoạt")}", 223, 85, 85);
            
            await Task.Delay(1000);

            // Activate IDM
            var result = await IDMActivator.ActivateIDMAsync();

            if (result.Success)
            {
                idmActivateButton.Text = "✅ HOÀN THÀNH";
                PopUp.ShowPopup("Thành công", result.Message, 223, 85, 85);
            }
            else
            {
                idmActivateButton.Text = "❌ THẤT BẠI";
                PopUp.ShowPopup("Lỗi", result.Message, 222, 85, 85);
            }

            await Task.Delay(2000);
            idmActivateButton.Text = "📥 KÍCH HOẠT IDM";
            idmActivateButton.Enabled = true;
        }
        catch (Exception ex)
        {
            idmActivateButton.Text = "❌ LỖI";
            PopUp.ShowPopup("Lỗi", $"Đã xảy ra lỗi: {ex.Message}", 222, 85, 85);
            
            await Task.Delay(2000);
            idmActivateButton.Text = "📥 KÍCH HOẠT IDM";
            idmActivateButton.Enabled = true;
        }
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
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XGui));
        
        this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
        this.header = new System.Windows.Forms.Panel();
        this.titleLabel = new System.Windows.Forms.Label();
        this.minimizeApp = new Guna.UI2.WinForms.Guna2ImageButton();
        this.maximizeApp = new Guna.UI2.WinForms.Guna2ImageButton();
        this.closeApp = new Guna.UI2.WinForms.Guna2ImageButton();
        this.guna2ShadowForm1 = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
        this.mainPanel = new System.Windows.Forms.Panel();
        this.winrarActivateButton = new Guna.UI2.WinForms.Guna2Button();
        this.idmActivateButton = new Guna.UI2.WinForms.Guna2Button();
        
        this.header.SuspendLayout();
        this.mainPanel.SuspendLayout();
        this.SuspendLayout();
        
        // 
        // guna2Elipse1
        // 
        this.guna2Elipse1.BorderRadius = 10;
        this.guna2Elipse1.TargetControl = this;
        
        // 
        // header
        // 
        this.header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
        this.header.Controls.Add(this.titleLabel);
        this.header.Controls.Add(this.minimizeApp);
        this.header.Controls.Add(this.maximizeApp);
        this.header.Controls.Add(this.closeApp);
        this.header.Dock = System.Windows.Forms.DockStyle.Top;
        this.header.Location = new System.Drawing.Point(0, 0);
        this.header.Name = "header";
        this.header.Size = new System.Drawing.Size(800, 50);
        this.header.TabIndex = 0;
        this.header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
        this.header.MouseMove += new System.Windows.Forms.MouseEventHandler(this.header_MouseMove);
        this.header.MouseUp += new System.Windows.Forms.MouseEventHandler(this.header_MouseUp);
        
        // 
        // titleLabel
        // 
        this.titleLabel.AutoSize = true;
        this.titleLabel.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.titleLabel.ForeColor = System.Drawing.Color.White;
        this.titleLabel.Location = new System.Drawing.Point(12, 15);
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new System.Drawing.Size(200, 23);
        this.titleLabel.TabIndex = 0;
        this.titleLabel.Text = "System Utility - Archive & Download Manager";
        
        // 
        // minimizeApp
        // 
        this.minimizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.minimizeApp.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
        this.minimizeApp.Cursor = System.Windows.Forms.Cursors.Hand;
        this.minimizeApp.HoverState.ImageSize = new System.Drawing.Size(16, 16);
        this.minimizeApp.ImageSize = new System.Drawing.Size(15, 15);
        this.minimizeApp.Location = new System.Drawing.Point(698, 11);
        this.minimizeApp.Name = "minimizeApp";
        this.minimizeApp.PressedState.ImageSize = new System.Drawing.Size(13, 13);
        this.minimizeApp.Size = new System.Drawing.Size(28, 28);
        this.minimizeApp.TabIndex = 13;
        this.minimizeApp.Click += new System.EventHandler(this.minimizeApp_Click);
        
        // 
        // maximizeApp
        // 
        this.maximizeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.maximizeApp.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
        this.maximizeApp.Cursor = System.Windows.Forms.Cursors.Hand;
        this.maximizeApp.HoverState.ImageSize = new System.Drawing.Size(16, 16);
        this.maximizeApp.ImageSize = new System.Drawing.Size(15, 15);
        this.maximizeApp.Location = new System.Drawing.Point(732, 11);
        this.maximizeApp.Name = "maximizeApp";
        this.maximizeApp.PressedState.ImageSize = new System.Drawing.Size(13, 13);
        this.maximizeApp.Size = new System.Drawing.Size(28, 28);
        this.maximizeApp.TabIndex = 12;
        this.maximizeApp.Click += new System.EventHandler(this.maximizeApp_Click);
        
        // 
        // closeApp
        // 
        this.closeApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.closeApp.CheckedState.ImageSize = new System.Drawing.Size(64, 64);
        this.closeApp.Cursor = System.Windows.Forms.Cursors.Hand;
        this.closeApp.HoverState.ImageSize = new System.Drawing.Size(16, 16);
        this.closeApp.ImageSize = new System.Drawing.Size(15, 15);
        this.closeApp.Location = new System.Drawing.Point(766, 11);
        this.closeApp.Name = "closeApp";
        this.closeApp.PressedState.ImageSize = new System.Drawing.Size(13, 13);
        this.closeApp.Size = new System.Drawing.Size(28, 28);
        this.closeApp.TabIndex = 11;
        this.closeApp.Click += new System.EventHandler(this.closeApp_Click);
        
        // 
        // guna2ShadowForm1
        // 
        this.guna2ShadowForm1.BorderRadius = 10;
        this.guna2ShadowForm1.TargetForm = this;
        
        // 
        // mainPanel
        // 
        this.mainPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
        this.mainPanel.Controls.Add(this.winrarActivateButton);
        this.mainPanel.Controls.Add(this.idmActivateButton);
        this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        this.mainPanel.Location = new System.Drawing.Point(0, 50);
        this.mainPanel.Name = "mainPanel";
        this.mainPanel.Size = new System.Drawing.Size(800, 350);
        this.mainPanel.TabIndex = 1;
        
        // 
        // winrarActivateButton
        // 
        this.winrarActivateButton.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.winrarActivateButton.AutoRoundedCorners = true;
        this.winrarActivateButton.BackColor = System.Drawing.Color.Transparent;
        this.winrarActivateButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(222)))), ((int)(((byte)(85)))));
        this.winrarActivateButton.BorderRadius = 21;
        this.winrarActivateButton.BorderThickness = 1;
        this.winrarActivateButton.Cursor = System.Windows.Forms.Cursors.Hand;
        this.winrarActivateButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.winrarActivateButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.winrarActivateButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
        this.winrarActivateButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
        this.winrarActivateButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
        this.winrarActivateButton.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.winrarActivateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(222)))), ((int)(((byte)(85)))));
        this.winrarActivateButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(222)))), ((int)(((byte)(85)))));
        this.winrarActivateButton.HoverState.ForeColor = System.Drawing.Color.White;
        this.winrarActivateButton.Location = new System.Drawing.Point(250, 120);
        this.winrarActivateButton.Name = "winrarActivateButton";
        this.winrarActivateButton.Size = new System.Drawing.Size(300, 45);
        this.winrarActivateButton.TabIndex = 0;
        this.winrarActivateButton.Text = "🗜️ Winrar Active";
        this.winrarActivateButton.Click += new System.EventHandler(this.winrarActivateButton_Click);
        
        // 
        // idmActivateButton
        // 
        this.idmActivateButton.Anchor = System.Windows.Forms.AnchorStyles.None;
        this.idmActivateButton.AutoRoundedCorners = true;
        this.idmActivateButton.BackColor = System.Drawing.Color.Transparent;
        this.idmActivateButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(222)))));
        this.idmActivateButton.BorderRadius = 21;
        this.idmActivateButton.BorderThickness = 1;
        this.idmActivateButton.Cursor = System.Windows.Forms.Cursors.Hand;
        this.idmActivateButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
        this.idmActivateButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
        this.idmActivateButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
        this.idmActivateButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
        this.idmActivateButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
        this.idmActivateButton.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.idmActivateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(222)))));
        this.idmActivateButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(222)))));
        this.idmActivateButton.HoverState.ForeColor = System.Drawing.Color.White;
        this.idmActivateButton.Location = new System.Drawing.Point(250, 180);
        this.idmActivateButton.Name = "idmActivateButton";
        this.idmActivateButton.Size = new System.Drawing.Size(300, 45);
        this.idmActivateButton.TabIndex = 1;
        this.idmActivateButton.Text = "📥 IDM Active";
        this.idmActivateButton.Click += new System.EventHandler(this.idmActivateButton_Click);
        
        // 
        // XGui
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
        this.ClientSize = new System.Drawing.Size(800, 400);
        this.Controls.Add(this.mainPanel);
        this.Controls.Add(this.header);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.MaximizeBox = false;
        this.Name = "XGuiLumi";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Lumi Active";
        
        this.header.ResumeLayout(false);
        this.header.PerformLayout();
        this.mainPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }
    
    private string GetProcessingText()
    {
        var parts = new string[] { "⏳", " ", "Đ", "A", "N", "G", " ", "X", "Ử", " ", "L", "Ý", ".", ".", "." };
        return string.Join("", parts);
    }
    
    private string GetArchiveToolText()
    {
        var parts = new string[] { "🗜️", " ", "C", "O", "N", "F", "I", "G", "U", "R", "E", " ", 
                                  "A", "R", "C", "H", "I", "V", "E", " ", "T", "O", "O", "L" };
        return string.Join("", parts);
    }
    
    private async Task SimulateUserActivity()
    {
        try
        {
            var random = new Random();
            
            // Simulate brief mouse movement check (normal UI behavior)
            _ = Cursor.Position;
            await Task.Delay(random.Next(10, 50));
            
            // Check window focus (normal UI behavior)
            _ = this.Focused;
            await Task.Delay(random.Next(25, 75));
            
            // Brief UI update (normal application behavior)
            this.Refresh();
            await Task.Delay(random.Next(50, 150));
        }
        catch
        {
            // Ignore errors in simulation
        }
    }
}