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
            
            //vlcControl1.VlcLibDirectory = new DirectoryInfo(@"C:\Users\butzt\source\repos\wtfisthis\wtfisthis\bin\Debug\net6.0-windows\libvlc\win-x64");
            openFileDialog1 = new OpenFileDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            vlcControl1.SetMedia(new FileInfo(openFileDialog1.FileName));
            vlcControl1.Play();
            timer1.Start();
            //backgroundWorker2.WorkerReportsProgress = true;
            //backgroundWorker2.RunWorkerAsync();
            //backgroundWorker2.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker2_ProgressChanged);
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
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            Point CP = progressBar1.PointToClient(Cursor.Position);
            progressBar1.Value = progressBar1.Minimum + (progressBar1.Maximum - progressBar1.Minimum) * CP.X / progressBar1.Width;
            //string stri = Convert.ToString(progressBar1.Value);
            //MessageBox.Show(stri);
            //string floa = Convert.ToString(vlcControl1.Position);
            //MessageBox.Show(floa);
            //vlcControl1.Position = progressBar1.Value / 100;
            //vlcControl1.Pause();
            float test = Convert.ToSingle(progressBar1.Value);
            //string test2 = Convert.ToString(test);
            //MessageBox.Show(test2);
            vlcControl1.Position = test / 100;
            //string floa2 = Convert.ToString(vlcControl1.Position);
            //MessageBox.Show(floa2);
            //vlcControl1.Pause();
            //MessageBox.Show(floa2);
            
            

        }


        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            int total = (int)vlcControl1.Position;
            System.Threading.Thread.Sleep(100);
            int percents = (total * 100);
            backgroundWorker2.ReportProgress(percents);
        }
        void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar1.Value = e.ProgressPercentage;
            //textBox1.Text = "A";
            //Thread.Sleep(1000);
            //textBox1.Text = "B";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            int posint = Convert.ToInt32(vlcControl1.Position * 100);
            string playtime = Convert.ToString(vlcControl1.Time / 1000);
            label1.Text = Convert.ToString(playtime) + "s";
            if (posint < 0 || posint > 100) { 
            progressBar1.Value = posint;
            }
            else { 
                progressBar1.Value = 0;
                 }
        }
    }
}