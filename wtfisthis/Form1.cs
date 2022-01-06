using System.ComponentModel;
using System.Windows.Forms;

namespace wtfisthis
{
    public partial class Form1 : Form
    {
        public OpenFileDialog openFileDialog1;
        public Form1()
        {
            InitializeComponent();
            // 
            // vlcControl1
            //
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
            this.progressBar1.MarqueeAnimationSpeed = 0;
            openFileDialog1 = new OpenFileDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            vlcControl1.SetMedia(new FileInfo(openFileDialog1.FileName));
            vlcControl1.Play();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            vlcControl1.Play();
            timer1.Start();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            vlcControl1.Pause();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            vlcControl1.Stop();
            timer1.Stop();
            progressBar1.Value = 0;
            label1.Text = "0s";
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            Point CP = progressBar1.PointToClient(Cursor.Position);
            progressBar1.Value = progressBar1.Minimum + (progressBar1.Maximum - progressBar1.Minimum) * CP.X / progressBar1.Width;
            float test = Convert.ToSingle(progressBar1.Value);
            vlcControl1.Position = test / 1000;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int posint = Convert.ToInt32(vlcControl1.Position * 1000);
            string playtime = Convert.ToString(vlcControl1.Time / 1000);
            label1.Text = Convert.ToString(playtime) + "s";
            if (posint > 0 && posint < 1000) { 
            progressBar1.Value = posint;
            }
            else { 
                progressBar1.Value = 0;
                 }
        }
    }
}