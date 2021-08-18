using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;    // ports(serial)
using System.Diagnostics; // processstartinfo
using System.Windows;
using System.IO;
using Microsoft.Win32; //Get python path(registry)
using System.Threading; //쓰레드 사용
using System.Runtime.InteropServices;
using NAudio.CoreAudioApi;

namespace LED_GUI
{
    public partial class Form1 : Form
    {

        private bool flag = true; //Thread Method 탈출 플래그
        private Thread th;
        private bool OnOff = true;

        // ????
        delegate void setTextCallback();

        // serial port 설정
        SerialPort serialPort1 = new SerialPort();

        //RGB와 각각의 Color에 해당하는 정보를 저장하는 객체
        public class ControlColor
        {
            private string red, green, blue;
            private string rgb;

            public void init()
            {
                this.red = this.green = this.blue = this.rgb = this.dirRgb = "";
            }
            public void setRgb(string red, string green, string blue)
            {
                if (red != "")
                {
                    if (int.Parse(red) < 0)
                    {
                        this.red = "0";
                    }
                    else if (int.Parse(red) > 255)
                    {
                        this.red = "255";
                    }
                    else
                    {
                        this.red = red;
                    }
                }
                else
                {
                    this.red = "0";
                }

                if (green != "")
                {
                    if(int.Parse(green) < 0)
                    {
                        this.green = "0";
                    }
                    else if (int.Parse(green) > 255)
                    {
                        this.green = "255";
                    }
                    else
                    {
                        this.green = green;
                    }
                }
                else
                {
                    this.green = "0";
                }
                
                if (blue != "")
                {
                    if (int.Parse(blue) < 0)
                    {
                        this.blue = "0";
                    }
                    else if(int.Parse(blue) > 255)
                    {
                        this.blue = "255";
                    }
                    else
                    {
                        this.blue = blue;
                    }
                }
                else
                {
                    this.blue = blue;
                }
                    
            }
            public string getRgb()
            {
                rgb = "";
                if(int.Parse(red) < 10)
                {
                    red = "00" + red;
                }
                else if(int.Parse(red)>9 && int.Parse(red) < 100)
                {
                    red = "0" + red;
                }

                if(int.Parse(green) < 10)
                {
                    green = "00" + green;
                }
                else if(int.Parse(green)>9 && int.Parse(green) < 100)
                {
                    green = "0" + green;
                }

                if(int.Parse(blue) < 10)
                {
                    blue = "00" + blue;
                }
                else if(int.Parse(blue)>9 && int.Parse(blue) < 100)
                {
                    blue = "0" + blue;
                }
                rgb += red + green + blue;
                return rgb;
            }
            public string dirRgb
            {
                get; set;
            }
            
        }

        //Color 객체 생성
        ControlColor controlColor = new ControlColor();

        //Form1에서의 초기화
        public Form1()
        {
            InitializeComponent();
            Text = "LED";
            controlColor.init();
            liveButton.Enabled = false;
            moodButton.Enabled = false;
            closerButton.Enabled = false;
        }

        //Port 설정(불러오는) 버튼
        private void portButton_Click(object sender, EventArgs e)
        {
            try
            {
                portComboBox.Items.Clear();
                foreach (string comport in SerialPort.GetPortNames())
                {
                    portComboBox.Items.Add(comport);
                }
            }
            catch (UnauthorizedAccessException)
            {
                portComboBox.Text = "접근 실패";
            }
        }

        //불러온 Port를 ComboBox에 표시하고 선택한 Port와 연결함.
        private void portComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            flag = false;
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                }
                serialPort1.PortName = portComboBox.Text;
                serialPort1.BaudRate = (int)9600;
                serialPort1.Open();
                liveButton.Enabled = true;
                moodButton.Enabled = true;
                closerButton.Enabled = true;
            }
            catch
            {
                portComboBox.Text = "연결 실패";
            }
        }

        //포트 해제버튼 ComboBox또한 클리어 함
        private void closerButton_Click(object sender, EventArgs e)
        {
            flag = false;
            if (serialPort1.IsOpen)
            {
                //포트를 해제 하기전에 Led를 꺼준다.
                for(int i = 0; i < 2; i++)
                {
                    colorSet("000000000", 'L');
                    colorSet("000000000", 'R');
                    colorSet("000000000", 'Q');
                    colorSet("000000000", 'W');
                }
                serialPort1.Close();
            }
            submitButton.Enabled = false;
            closerButton.Enabled = false;
            liveButton.Enabled = false;
            moodButton.Enabled = false;
            portButton.Enabled = true;
            portComboBox.Items.Clear();
        }

        //Live Mode Select
        private void liveButton_Click(object sender, EventArgs e)
        {
            //Live Mode로 진입하면서 submit기능을 false 해준다.
            submitButton.Enabled = false;

            flag = true;

            ThreadStart ts = new ThreadStart(TransColor);

            //Thread 생성
            th = new Thread(ts);

            th.IsBackground = true;

            th.Start();
            
        }

        private void TransColor()
        {
            //L(왼쪽 아래), R(오른쪽 아래), Q(왼쪽 위), W(오른쪽 위)
            while (flag)
            {
                liveColorSet('L');
                liveColorSet('R');
                liveColorSet('Q');
                liveColorSet('W');
                liveVolume();
                
                
            }
        }

        //Mood Mode에서 입력받은 RGB값을 LED에 전송
        private void colorSet(string RGB, char direction)
        {
            string rgbBuf = direction + RGB + '\n';
            byte[] data = StringToByte(rgbBuf);
            serialPort1.Write(data, 0, data.Length);
        }

        //Mood Mode Select
        private void moodButton_Click(object sender, EventArgs e)
        {
            //Mood Mode를 활성화 하면서 submit 버튼도 함께 활성화 해줌.
            submitButton.Enabled = true;
            flag = false;
        }

        //입력한 RGB값을 Serial Port로 전송
        private void submitButton_Click(object sender, EventArgs e)
        {
            controlColor.setRgb(redBox.Text, greenBox.Text, blueBox.Text);
            string submitC = controlColor.getRgb();
            if (serialPort1.IsOpen)
            {
                colorSet(submitC, 'L');
                colorSet(submitC, 'R');
                colorSet(submitC, 'Q');
                colorSet(submitC, 'W');
            }
        }

        //UTF8로 인코딩
        private byte[] StringToByte(string _str)
        {
            byte[] tmpeBytes = Encoding.UTF8.GetBytes(_str);
            return tmpeBytes;
        }

        //Serial Port로 값을 받지만 필요없는 Function 삭제예정
        private void serialPort1_dataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                string data = serialPort1.ReadExisting();

                if (data != string.Empty)
                {
                    char[] values = data.ToCharArray();
                    int value = Convert.ToInt32(values[0]);

                    dataProcessing(data);
                }
            }
        }

        //받은 Data를 처리해 저장하지만 필요없는 Function 삭제예정
        private void dataProcessing(string Text)
        {
            controlColor.dirRgb = Text;
        }

        //Delay를 주기 위해 만든 함수
        private static DateTime Delay(int MS)
        {
            // Thread 와 Timer보다 효율 적으로 사용할 수 있음.
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }

        //Live Color를 Serial Port로 전송하는 함수
        private void liveColorSet(char direction)
        {
            //Programm이 Run되는 Path를 설정(main.py의 경로를 설정해주기 위함)
            string path = AppDomain.CurrentDomain.BaseDirectory;
            string pathP = pythonPath("where python");

            for(int i = 0; i < pathP.Length; i++)
            {
                if (pathP[i] == '\\')
                {
                    string tmp = pathP.Substring(0, i);
                    string temp = pathP.Substring(i, pathP.Length - i);
                    pathP = tmp + '\\' + temp;
                    i++;
                }
            }

            //Process 생성 + Python.exe의 위치 설정
            var psi = new ProcessStartInfo();
            //psi.FileName = @"C:\Users\kingm\AppData\Local\Programs\Python\Python38-32\python.exe";
            psi.FileName = string.Format(@"{0}", pathP);

            //script위치, 해상도, LED좌표 Argument로 사용
            var script = AppDomain.CurrentDomain.BaseDirectory + "main.py";
            var width = Screen.PrimaryScreen.Bounds.Width;
            var height = Screen.PrimaryScreen.Bounds.Height;
            psi.Arguments = $"\"{script}\" \"{width}\" \"{height}\" \"{direction}\"";

            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var erros = "";
            var results = "";

            //Programm 읽어오기
            using (var process = Process.Start(psi))
            {
                erros = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            //결과값 ASCII로 인코딩
            byte[] buf = Encoding.ASCII.GetBytes(results, 0, results.Length);
            string convert = Encoding.ASCII.GetString(buf, 0, buf.Length);

            //인코딩한 데이터를 Serial Port로 전송
            string rgbBuf = direction + convert;
            byte[] data = StringToByte(rgbBuf);
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(data, 0, data.Length);
            }
            

        }

        //Python의 Path를 찾아주는 Function
        private string pythonPath(string command)
        {
            System.Diagnostics.ProcessStartInfo proinfo = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process pro = new System.Diagnostics.Process();

            proinfo.FileName = @"cmd.exe";
            proinfo.CreateNoWindow = true;
            proinfo.UseShellExecute = false;
            proinfo.RedirectStandardOutput = true;
            proinfo.RedirectStandardInput = true;
            proinfo.RedirectStandardError = true;
            pro.StartInfo = proinfo;
            pro.Start();

            pro.StandardInput.Write(command + Environment.NewLine);
            pro.StandardInput.Close();
            pro.WaitForExit();

            string readline = "";

            for (int i = 0; i < 5; i++)
            {
                readline = pro.StandardOutput.ReadLine();
            }

            string result = pro.StandardOutput.ReadToEnd();
            //Console.WriteLine(result);
            pro.Close();
            return readline;
        }

        private void liveVolume()
        {
            if (OnOff == true)
            {
                MMDeviceEnumerator devEnum = new MMDeviceEnumerator();
                MMDevice defaultDevice = devEnum.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                string masterV = defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar.ToString();
                string currVolume = defaultDevice.AudioMeterInformation.MasterPeakValue.ToString();

                double percentageV;
                if (double.Parse(masterV) != 0)
                {
                    //percentageV = double.Parse(currVolume) / double.Parse(masterV);
                    percentageV = double.Parse(currVolume) / 1;
                }
                else
                {
                    percentageV = 0;
                }

                percentageV *= 100;
                int percentageT = (int)percentageV;
                string percentageS = 'V' + percentageT.ToString() + '\n';

                /*
                if (portComboBox.InvokeRequired)
                {
                    portComboBox.Invoke(new setTextCallback(liveVolume));
                }
                else
                {
                    portComboBox.Text = percentageS;
                    //portComboBox.Text = masterV;
                }
                */

                byte[] data = StringToByte(percentageS);
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(data, 0, data.Length);
                }
            }
            else
            {
                string zero = 'V' + "0" + '\n';
                byte[] data = StringToByte(zero);
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(data, 0, data.Length);
                }
            }
        }

        //box 설정
        private void redBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8) //8:백스페이스,45:마이너스,46:소수점
            {
                e.Handled = true;
            }
        }

        private void greenBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8) //8:백스페이스,45:마이너스,46:소수점
            {
                e.Handled = true;
            }
        }

        private void blueBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)) && e.KeyChar != 8) //8:백스페이스,45:마이너스,46:소수점
            {
                e.Handled = true;
            }
        }

        private void wiingbutton_Click(object sender, EventArgs e)
        {
            if(OnOff == true)
            {
                OnOff = false;
            }
            else
            {
                OnOff = true;
            }
        }
    }
}

