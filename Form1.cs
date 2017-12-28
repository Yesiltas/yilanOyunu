using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace yilan
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        int buyuk=0;
        yilan yılanımız = new yilan();
        Yon yonumuz;
        PictureBox[] pb_yilanparcalari;
        bool yem_varmı = false;
        Random rastgl = new Random();
        int skor = 0;
        PictureBox pb_yem;
        private void Form1_Load(object sender, EventArgs e)
        {
            yeni_oyun();
        }
        private void yeni_oyun()
        {
            yem_varmı = false;
            skor = 0;
            yılanımız = new yilan();
            yonumuz = new Yon(-10, 0);
            pb_yilanparcalari = new PictureBox[0];
            for (int i = 0; i < 3; i++)
            {
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);
                pb_yilanparcalari[i] = pb_ekle();
            }
            timer1.Start();
            button1.Enabled = false;


        }
        private PictureBox pb_ekle()
        {

            PictureBox pb = new PictureBox();
            pb.Size = new Size(10, 10);
            pb.BackColor = Color.Purple;
            pb.Location = yılanımız.GetPos(pb_yilanparcalari.Length - 1);
            panel1.Controls.Add(pb);
            return pb;
        }
        private void Pb_guncelle()
        {
            for (int i = 0; i < pb_yilanparcalari.Length; i++)
            {
                pb_yilanparcalari[i].Location = yılanımız.GetPos(i);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (yonumuz._y != 10)
                {
                    yonumuz = new Yon(0, -10);
                }
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.W)
            {
                if (yonumuz._y != -10)
                {
                    yonumuz = new Yon(0, 10);
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (yonumuz._x != 10)
                {
                    yonumuz = new Yon(-10, 0);
                }
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (yonumuz._x != -10)
                {
                    yonumuz = new Yon(10, 0);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "" + skor.ToString();
            yılanımız.ilerle(yonumuz);
            Pb_guncelle();
            yem_olustur();
            yem_yedi_mi();
            yılan_kendine_carptı();
            yılan_duvara_carptı();
        }
        public void yem_olustur()
        {
            if (!yem_varmı)
            {
                PictureBox pb = new PictureBox();
                pb.BackColor = Color.Aqua;
                pb.Size = new Size(10, 10);
                pb.Location = new Point(rastgl.Next(panel1.Width / 10) * 10, rastgl.Next(panel1.Height / 10) * 10);
                pb_yem = pb;
                yem_varmı = true;
                panel1.Controls.Add(pb);
            }
        }
        public void yem_yedi_mi()
        {
            if(yılanımız.GetPos(0)==pb_yem.Location)
            {
                skor += 10;
                yılanımız.buyu();
                Array.Resize(ref pb_yilanparcalari, pb_yilanparcalari.Length + 1);
                pb_yilanparcalari[pb_yilanparcalari.Length - 1] = pb_ekle();
                yem_varmı = false;
                panel1.Controls.Remove(pb_yem);
            }
        }
        public void yılan_kendine_carptı()
        {
            for(int i = 1; i < yılanımız.yılan_buyuklugu; i++)
            {
                if (yılanımız.GetPos(0) == yılanımız.GetPos(i))
                {
                    yenildi();
                }
            }
        }
        public void yılan_duvara_carptı()
        {
            Point p = yılanımız.GetPos(0);
            if (p.X < 0 || p.X > panel1.Width - 10 || p.Y < 0 || p.Y > panel1.Height-10)
            {
                yenildi();
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void yenildi()
        {
            
            timer1.Stop();
            MessageBox.Show("Oyun Bitdi !  KAYBETDİNİZ!!!");
            button1.Enabled = true;
            if (skor > buyuk)
            {
                buyuk = skor;
                label3.Text= "En Yuksek Skor =" + buyuk.ToString();
            }
            
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            yeni_oyun();
            
        }
    }
}
