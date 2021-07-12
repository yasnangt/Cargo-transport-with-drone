using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
 


namespace WindowsFormsApp3
{

    public partial class Form1 : Form
    {
        string isim = null;
        string command;
        string c1;
        UDPSocket s = new UDPSocket();
        UDPSocket c = new UDPSocket();
        List<Panel> listPanel = new List<Panel>();
        int[,] droneBaslangic = { { 20, 20 } };


        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.IsMdiContainer = true;
        }
        // SQL BAĞLANTI
        //static string constring = "Data Source=DESKTOP-OF6J8I3;Initial Catalog=project;Integrated Security=True";
        static string constring = "Data Source=DESKTOP-C98HBFA;Initial Catalog=Kisiler;Integrated Security=True";
        // Yasin DB Data Source=DESKTOP-C98HBFA;Initial Catalog=Kisiler;Integrated Security=True
        SqlConnection connect = new SqlConnection(constring);
        SqlConnection connect2 = new SqlConnection(constring);

        int position = 0;
        SerialPort SerialPort = new SerialPort();
        private void Form1_Load(object sender, EventArgs e)
        {
            var ports = SerialPort.GetPortNames(); //Seri portları diziye ekleme
            foreach (var port in ports)
                comboBox1.Items.Add(port);
            //TrackBar ayarları
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 180;
        }
        private void write_angle(int value)
        {
            try
            {
                if (SerialPort.IsOpen)
                {
                    SerialPort.WriteLine(value.ToString()); //Değeri port üzerinden gönder
                    label2.Text = "Açı: " + value.ToString() + "°"; //Güncel değeri label2'ye yaz
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message, "Hata"); //Hata mesajı
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPort.PortName = comboBox1.SelectedItem.ToString();
            SerialPort.Open();
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (SerialPort.IsOpen) SerialPort.Close(); //Seri port açıksa kapat

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!SerialPort.IsOpen)
                {
                    /* Seri Port Ayarları */
                    SerialPort.PortName = comboBox1.Text;
                    SerialPort.BaudRate = 9600;
                    SerialPort.Parity = Parity.None;
                    SerialPort.DataBits = 8;
                    SerialPort.StopBits = StopBits.One;
                    SerialPort.Open(); //Seri portu aç
                    label3.Text = "Bağlantı Sağlandı.";
                    label3.ForeColor = System.Drawing.Color.Green;
                    button1.Text = "KES"; //Buton1 yazısını değiştir
                }
                else
                {
                    label3.Text = "Bağlantı Kesildi.";
                    label3.ForeColor = System.Drawing.Color.Red;
                    button1.Text = "BAĞLAN"; //Buton1 yazısını değiştir
                    SerialPort.Close(); //Seri portu kapa
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata"); //Hata mesajı
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            position = 30;
            write_angle(position);
            label2.Text = "Açı: " + position.ToString() + "°"; //Güncel değeri label2'ye yaz
        }
        private void button3_Click(object sender, EventArgs e)
        {
            position = 90;
            write_angle(position);
            label2.Text = "Açı: " + position.ToString() + "°"; //Güncel değeri label2'ye yaz
        }
        private void button4_Click(object sender, EventArgs e)
        {
            position = 120;
            write_angle(position);
            label2.Text = "Açı: " + position.ToString() + "°"; //Güncel değeri label2'ye yaz
        }
        private void button5_Click(object sender, EventArgs e)
        {
            position = 180;
            write_angle(position);
            label2.Text = "Açı: " + position.ToString() + "°"; //Güncel değeri label2'ye yaz
        }
        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            position = trackBar1.Value;
            write_angle(position);
            label2.Text = "Açı: " + position.ToString() + "°"; //Güncel değeri label2'ye yaz
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            listPanel.Add(panel1);
            listPanel.Add(panel2);           
            listPanel.Add(panel6);
            listPanel.Add(panel5);
            
            connect.Open();
            SqlCommand getir = new SqlCommand("Select isim from projectTable", connect);
            SqlDataReader oku = getir.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku["isim"].ToString());
            }

            connect.Close();


            label24.Text = droneBaslangic[0, 0].ToString();
            label25.Text = droneBaslangic[0, 1].ToString();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button6_Click(object sender, EventArgs e)
        {
            listPanel[1].BringToFront();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            listPanel[0].BringToFront();
        }
        private void button25_Click(object sender, EventArgs e)
        {
            listPanel[2].BringToFront();
            listPanel[3].BringToFront();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void button8_Click(object sender, EventArgs e)
        {
            int pos;
            pos = position;
            pos += 5;
            position = pos;
            label2.Text = "Açı: " + position.ToString() + "°"; //Güncel değeri label2'ye yaz
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (position != 0)
            {
                int pos;
                pos = position;
                pos -= 5;
                position = pos;
                label2.Text = "Açı: " + position.ToString() + "°"; //Güncel değeri label2'ye yaz
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                c.Client("192.168.10.1", 8889);
                s.Server("0.0.0.0", 8890);
                label4.Text = "Bağlantı Sağlandı.";
                label4.ForeColor = System.Drawing.Color.Green;
                //button1.Text = "KES"; //Buton1 yazısını değiştir

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata"); //Hata mesajı
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            c.Disconnect();
        }

        
        private void button12_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            label5.Text = btn.Text;
            c.Send(btn.Text);
	        //System.Threading.Thread.Sleep(2000);
            //c1 = c.Receivee();
            //label8.Text = c1;
            //listBox1.Items.Add(c1);
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string com = textBox1.Text;
            c.Send(com);
            System.Threading.Thread.Sleep(2000);
            //c1 = c.Receivee();
            command = textBox1.Text;
            label5.Text = command;
            //label8.Text = c1;
            //listBox1.Items.Add(c1);
            //string com = textBox1.Text;
            //c.Send(com);
            //label5.Text = textBox1.Text;            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            c.Send(textBox2.Text);
	        //System.Threading.Thread.Sleep(2000);
            //string s2 = s.Receivee();
            //label7.Text= s2;
            //label8.Text = c1;
	        //listBox1.Items.Add(s2);

        }

        int counter = 0;


        private void button24_Click(object sender, EventArgs e)
        {
            #region Deneme            

            Forward();

            Up();

            Up();

            Down();

            Up();

            Land();

            #endregion


            //timer1.Start();
            

           }        
        string num = null;
        private void DroneHazırla(object sender, EventArgs e)
        {
            c.Send("command");
            c.Send("takeoff");
        }

        private void kayitbitir_btn(object sender, EventArgs e)
        {
            try
            {
                if (connect.State == ConnectionState.Closed)
                    connect.Open();
                //KAYIT EKLEME
                string kayit = "insert into projectTable (isim,telefon,il,ilce,mahalle,sokak,bina,tip) values (@isim,@telefon,@il,@ilce,@mahalle,@sokak,@bina,@tip)";
                SqlCommand komut = new SqlCommand(kayit, connect);

                komut.Parameters.AddWithValue("@isim", textBox3.Text);
                komut.Parameters.AddWithValue("@telefon", textBox4.Text);
                komut.Parameters.AddWithValue("@il", comboBox6.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@ilce", comboBox5.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@mahalle", comboBox4.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@sokak", comboBox3.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@bina", comboBox7.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@tip", comboBox8.SelectedItem.ToString());
                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Kayıt Eklendi");
                //mevcut kayıtları silme

                connect2.Open();
                SqlCommand getiri = new SqlCommand("Select isim from projectTable", connect2);
                SqlDataReader okur = getiri.ExecuteReader();
                while (okur.Read())
                {

                    comboBox2.Items.Remove(okur["isim"].ToString());

                }
                connect2.Close();

                //Kayıt okuma
                connect.Open();
                SqlCommand getir = new SqlCommand("Select isim from projectTable", connect);
                SqlDataReader oku = getir.ExecuteReader();
                while (oku.Read())
                {
                    
                    comboBox2.Items.Add(oku["isim"].ToString());

                }

                connect.Close();




            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata Meydana Geldi " + hata.Message);
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void button28_Click(object sender, EventArgs e)
        {
            ////
            //BİLGİ YAZISI
            ////
            connect.Open();
            string name = comboBox2.SelectedItem.ToString();
            SqlCommand getir = new SqlCommand("Select bina, mahalle, sokak,tip , il, ilce from projectTable where isim = '"+ name+"'", connect);
            SqlDataReader oku = getir.ExecuteReader();
            while (oku.Read())
            {
                label10.Text = oku["mahalle"].ToString()+" "+ oku["sokak"].ToString()+" "+ oku["bina"].ToString()+Environment.NewLine+ oku["tip"].ToString() +Environment.NewLine + oku["il"].ToString()+"/"+ oku["ilce"].ToString();
            }
            connect.Close();
            connect.Open();
            connect2.Open();
            ////
            //GPS YÖNLENDİRME
            ////
            
           
            SqlDataReader okur = getir.ExecuteReader();

           

            int x=0, y=0;
            int sonuc;
            int[ , ]drone =  { { 20, 20 } };

            int xGidis, yGidis;
            while (okur.Read()) {          
                SqlCommand getiri = new SqlCommand("Select binaİsim, x, z from map where binaİsim = '" + okur["bina"].ToString() + "'", connect2);
                SqlDataReader okur2 = getiri.ExecuteReader();
                
                while (okur2.Read())
                { 
                    
                    string x2 = okur2["x"].ToString();
                    x = Int32.Parse(x2);
                    string z2 = okur2["z"].ToString();
                    y = Int32.Parse(z2);
                    label28.Text = x.ToString();
                    label29.Text = y.ToString();
                   
                }
                connect2.Close();
                xGidis = x - drone[0, 0];
                yGidis = y - drone[0, 1];

                MessageBox.Show("x: " + x + " ve y: " + y + "hedef " + xGidis + " " + yGidis);

                int ySag = yGidis * -1;
                int ySol = yGidis;
                int xDonus = xGidis * -1;
                  if (xGidis > 0)
                    {
                        if (yGidis > 0)//4 ve 5. bina
                        {
                            AzBekle();
                            MessageBox.Show("4-5 İşlem yapıldı.");
                            Komut("go " + xGidis + " " + ySag + " 0 15");
                            drone[0, 0] += xGidis;
                            drone[0, 1] += yGidis;
                            label24.Text = drone[0, 0].ToString();
                            label25.Text = drone[0, 1].ToString();
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("down 40");
                            MessageBox.Show("İşlem yapıldı.");
                            Bekle();
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("up 40");
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("cw 180");
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("go " + xDonus + " " + ySag + " 0 15");
                            drone[0, 0] -= xGidis;
                            drone[0, 1] -= yGidis;
                            label24.Text = drone[0, 0].ToString();
                            label25.Text = drone[0, 1].ToString();
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("land");
                            MessageBox.Show("İşlem yapıldı.");
                        }
                        else //1. ve 2. bina
                        {
                            AzBekle();
                            MessageBox.Show("1-2 İşlem yapıldı.");
                            Komut("go " + xGidis + " 0 0 15");
                            drone[0, 0] += xGidis;
                            label24.Text = drone[0, 0].ToString();
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("down 40");
                            MessageBox.Show("İşlem yapıldı.");
                            Bekle();
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("up 40");
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("cw 180");
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("go " + xGidis + " 0 0 15");
                            drone[0, 0] -= xGidis;
                            label24.Text = drone[0, 0].ToString();
                            MessageBox.Show("İşlem yapıldı.");
                            Komut("land");
                            MessageBox.Show("İşlem yapıldı.");                            
                        }
                    }
                    else if (yGidis > 0) //3.Bina
                    {
                        AzBekle();
                        MessageBox.Show("3-İşlem yapıldı.");
                        Komut("go 0" + ySag + " 0 15");
                        drone[0, 1] += yGidis;
                        label25.Text = drone[0, 1].ToString();
                        MessageBox.Show("İşlem yapıldı.");
                        Komut("down 40");
                        MessageBox.Show("İşlem yapıldı.");
                        Bekle();
                        MessageBox.Show("İşlem yapıldı.");
                        Komut("up 40");
                        MessageBox.Show("İşlem yapıldı.");
                        Komut("cw 180");
                        MessageBox.Show("İşlem yapıldı.");
                        Komut("go 0" + ySag + " 0 15");
                        drone[0, 1] -= yGidis;
                        label25.Text = drone[0, 1].ToString();
                        MessageBox.Show("İşlem yapıldı.");
                        Komut("land");
                        MessageBox.Show("İşlem yapıldı.");
                    }
                    else
                    {
                        MessageBox.Show("Hata!!!!");
                    }
            }
            connect.Close();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Command()
        {
            c.Send("command");
            Thread.Sleep(3000);
        }

        public void TakeOff()
        {
            c.Send("takeoff");
            Thread.Sleep(3000);
        }

        public void Forward()
        {
            c.Send("forward 30");
            Thread.Sleep(3000);
        }
        
        public void Up()
        {
            c.Send("up 30");
            Thread.Sleep(3000);
        }

        public void Down()
        {
            c.Send("down 30");
            Thread.Sleep(3000);
        }

        public void Land()
        {
            c.Send("land");
            Thread.Sleep(3000);
        }

        public void Komut(string com1,int value)
        {
            c.Send(com1 + " " + value);
            Thread.Sleep(3000);
        }

        public void Komut(string com1)
        {
            c.Send(com1);
            Thread.Sleep(3000);
        }

      


        //private void timer1_Tick_1(object sender, EventArgs e)
        //{
        //    if (counter == 20)
        //    {
        //        timer1.Stop();
        //        timer2.Start();
        //    }
        //    label26.Text = counter.ToString();
        //    counter++;
        //}


        //private void timer2_Tick(object sender, EventArgs e)
        //{
        //    if (counter == 45)
        //    {
        //        timer2.Stop();
        //    }
        //    else
        //    {
        //        label27.Text = counter.ToString();
        //        counter++;
        //    }
        //}
    }

        
}