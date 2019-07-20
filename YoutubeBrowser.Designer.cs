namespace YoutubeChannelDumper
{
    partial class YoutubeBrowser
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.VideoListBox = new System.Windows.Forms.ListView();
            this.ChannelLinkBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ProcessChannel = new System.Windows.Forms.Button();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.DeselectButton = new System.Windows.Forms.Button();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.DownloadPathBox = new System.Windows.Forms.TextBox();
            this.LogBox = new System.Windows.Forms.RichTextBox();
            this.MuxedBestButton = new System.Windows.Forms.RadioButton();
            this.SeparateBestButt = new System.Windows.Forms.RadioButton();
            this.ChangeExtBox = new System.Windows.Forms.CheckBox();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.AltProceedButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // VideoListBox
            // 
            this.VideoListBox.Location = new System.Drawing.Point(12, 12);
            this.VideoListBox.Name = "VideoListBox";
            this.VideoListBox.Size = new System.Drawing.Size(394, 597);
            this.VideoListBox.TabIndex = 0;
            this.VideoListBox.UseCompatibleStateImageBehavior = false;
            this.VideoListBox.View = System.Windows.Forms.View.List;
            // 
            // ChannelLinkBox
            // 
            this.ChannelLinkBox.Location = new System.Drawing.Point(412, 36);
            this.ChannelLinkBox.Name = "ChannelLinkBox";
            this.ChannelLinkBox.Size = new System.Drawing.Size(303, 20);
            this.ChannelLinkBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(413, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Channel link:";
            // 
            // ProcessChannel
            // 
            this.ProcessChannel.Location = new System.Drawing.Point(721, 34);
            this.ProcessChannel.Name = "ProcessChannel";
            this.ProcessChannel.Size = new System.Drawing.Size(114, 23);
            this.ProcessChannel.TabIndex = 3;
            this.ProcessChannel.Text = "Load by username";
            this.ProcessChannel.UseVisualStyleBackColor = true;
            this.ProcessChannel.Click += new System.EventHandler(this.ProcessChannel_Click);
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.Location = new System.Drawing.Point(12, 615);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAllButton.TabIndex = 4;
            this.SelectAllButton.Text = "Select all";
            this.SelectAllButton.UseVisualStyleBackColor = true;
            this.SelectAllButton.Click += new System.EventHandler(this.SelectAllButton_Click);
            // 
            // DeselectButton
            // 
            this.DeselectButton.Location = new System.Drawing.Point(93, 615);
            this.DeselectButton.Name = "DeselectButton";
            this.DeselectButton.Size = new System.Drawing.Size(75, 23);
            this.DeselectButton.TabIndex = 5;
            this.DeselectButton.Text = "Deselect all";
            this.DeselectButton.UseVisualStyleBackColor = true;
            this.DeselectButton.Click += new System.EventHandler(this.DeselectButton_Click);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(721, 88);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(75, 23);
            this.DownloadButton.TabIndex = 6;
            this.DownloadButton.Text = "Download";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(413, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Path:";
            // 
            // DownloadPathBox
            // 
            this.DownloadPathBox.Location = new System.Drawing.Point(412, 90);
            this.DownloadPathBox.Name = "DownloadPathBox";
            this.DownloadPathBox.Size = new System.Drawing.Size(303, 20);
            this.DownloadPathBox.TabIndex = 7;
            // 
            // LogBox
            // 
            this.LogBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LogBox.Location = new System.Drawing.Point(416, 116);
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.Size = new System.Drawing.Size(694, 522);
            this.LogBox.TabIndex = 9;
            this.LogBox.Text = "";
            // 
            // MuxedBestButton
            // 
            this.MuxedBestButton.AutoSize = true;
            this.MuxedBestButton.Location = new System.Drawing.Point(922, 69);
            this.MuxedBestButton.Name = "MuxedBestButton";
            this.MuxedBestButton.Size = new System.Drawing.Size(109, 17);
            this.MuxedBestButton.TabIndex = 10;
            this.MuxedBestButton.Text = "Best muxed audio";
            this.MuxedBestButton.UseVisualStyleBackColor = true;
            // 
            // SeparateBestButt
            // 
            this.SeparateBestButt.AutoSize = true;
            this.SeparateBestButt.Checked = true;
            this.SeparateBestButt.Location = new System.Drawing.Point(922, 94);
            this.SeparateBestButt.Name = "SeparateBestButt";
            this.SeparateBestButt.Size = new System.Drawing.Size(188, 17);
            this.SeparateBestButt.TabIndex = 11;
            this.SeparateBestButt.TabStop = true;
            this.SeparateBestButt.Text = "Best separate audio (better quality)";
            this.SeparateBestButt.UseVisualStyleBackColor = true;
            // 
            // ChangeExtBox
            // 
            this.ChangeExtBox.AutoSize = true;
            this.ChangeExtBox.Checked = true;
            this.ChangeExtBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChangeExtBox.Location = new System.Drawing.Point(949, 46);
            this.ChangeExtBox.Name = "ChangeExtBox";
            this.ChangeExtBox.Size = new System.Drawing.Size(161, 17);
            this.ChangeExtBox.TabIndex = 12;
            this.ChangeExtBox.Text = "Force .m4a rename for audio";
            this.ChangeExtBox.UseVisualStyleBackColor = true;
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.Location = new System.Drawing.Point(335, 615);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(75, 23);
            this.ClearLogButton.TabIndex = 13;
            this.ClearLogButton.Text = "Clear log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // AltProceedButton
            // 
            this.AltProceedButton.Location = new System.Drawing.Point(841, 34);
            this.AltProceedButton.Name = "AltProceedButton";
            this.AltProceedButton.Size = new System.Drawing.Size(75, 23);
            this.AltProceedButton.TabIndex = 14;
            this.AltProceedButton.Text = "Load by ID";
            this.AltProceedButton.UseVisualStyleBackColor = true;
            this.AltProceedButton.Click += new System.EventHandler(this.AltProceedButton_Click);
            // 
            // YoutubeBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 650);
            this.Controls.Add(this.AltProceedButton);
            this.Controls.Add(this.ClearLogButton);
            this.Controls.Add(this.ChangeExtBox);
            this.Controls.Add(this.SeparateBestButt);
            this.Controls.Add(this.MuxedBestButton);
            this.Controls.Add(this.LogBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DownloadPathBox);
            this.Controls.Add(this.DownloadButton);
            this.Controls.Add(this.DeselectButton);
            this.Controls.Add(this.SelectAllButton);
            this.Controls.Add(this.ProcessChannel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChannelLinkBox);
            this.Controls.Add(this.VideoListBox);
            this.Name = "YoutubeBrowser";
            this.Text = "Youtube browser";
            this.Load += new System.EventHandler(this.YoutubeBrowser_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView VideoListBox;
        private System.Windows.Forms.TextBox ChannelLinkBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ProcessChannel;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.Button DeselectButton;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox DownloadPathBox;
        private System.Windows.Forms.RichTextBox LogBox;
        private System.Windows.Forms.RadioButton MuxedBestButton;
        private System.Windows.Forms.RadioButton SeparateBestButt;
        private System.Windows.Forms.CheckBox ChangeExtBox;
        private System.Windows.Forms.Button ClearLogButton;
        private System.Windows.Forms.Button AltProceedButton;
    }
}

