namespace wtfisthis
{
    public partial class Form1 : Form
    {
        public OpenFileDialog openFileDialog1;
        public Form1()
        {
            InitializeComponent();
            // vlcControl1
            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).BeginInit();
            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vlcControl1.Location = new System.Drawing.Point(0, 0);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(800, 450);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 14;
            this.vlcControl1.VlcMediaplayerOptions = null;
            this.vlcControl1.VlcLibDirectory = new DirectoryInfo(@".\libvlc\win-x64");
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).EndInit();
            this.Controls.Add(this.vlcControl1);
            // the Rest
            this.progressBar1.MarqueeAnimationSpeed = 0;
            openFileDialog1 = new OpenFileDialog();
        }
        private void progressBar1_Click(object sender, EventArgs e)
        {
            Point CP = progressBar1.PointToClient(Cursor.Position);
            progressBar1.Value = progressBar1.Minimum + (progressBar1.Maximum - progressBar1.Minimum) * CP.X / progressBar1.Width;
            float test = Convert.ToSingle(progressBar1.Value);
            vlcControl1.Position = test / 10000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int posint = Convert.ToInt32(vlcControl1.Position * 10000);
            string playtime = Convert.ToString(vlcControl1.Time / 1000);
            toolStripMenuItem1.Text = Convert.ToString(playtime) + "s";
            if (posint > 0 && posint < 10000)
            {
                progressBar1.Value = posint;
            }
            else
            {
                progressBar1.Value = 0;
            }
        }
        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            try { 
            vlcControl1.SetMedia(new FileInfo(openFileDialog1.FileName));
            vlcControl1.Play();
            timer1.Start();
            this.playToolStripMenuItem.Enabled = true;
            this.pauseToolStripMenuItem.Enabled = true;
            this.stopToolStripMenuItem.Enabled = true;
            }
            catch
            {
                MessageBox.Show("No file selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlcControl1.Play();
            timer1.Start();
            this.pauseToolStripMenuItem.Enabled = true;
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlcControl1.Pause();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vlcControl1.Stop();
            timer1.Stop();
            progressBar1.Value = 0;
            toolStripMenuItem1.Text = "0s";
            this.pauseToolStripMenuItem.Enabled = false;
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            var bar = (TrackBar)sender;
            if (bar.Value % bar.SmallChange != 0)
            {
                bar.Value = bar.SmallChange * ((bar.Value + bar.SmallChange / 2) / bar.SmallChange);
            }
            toolStripMenuItem1.Text = bar.Value.ToString();
            vlcControl1.Audio.Volume = bar.Value;
        }
        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(toolStripTextBox2.Text, out int vol);
            {
                vlcControl1.Audio.Volume = vol;
            }
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            vlcControl1.Audio.ToggleMute();
        }
    }
}
