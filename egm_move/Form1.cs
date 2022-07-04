using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
//using System.Threading.Tasks;
using abb.egm;
using JoyKeys.Voluntary;

namespace egm_move
{
    public partial class Form1 : Form
    {
        public Sensor s1;
        public static Joystick joystick;
        public static bool Set_position = false;
        public static bool get_initial_position = true;
        //4600机器人向固定位置（工作位置，停止位置）移动的速度设定
        public static double set_movement_speed_position_4600=15;
        public static double set_movement_speed_posture_4600=0.5;
        public static double set_movement_speed_position_6650S=20;
        public static double set_movement_speed_posture_6650S=0.5;

        public static double Y_speed_4600 = 50;
        public static double Z_speed_4600 = 50;
        public static double Y_speed_6650S = 0.5;
        public static double Z_speed_6650S = 0.5;
        public static double L_speed = 30;

        //4600机器人运动范围
        public static int robot_4600range_x_max=0;
        public static int robot_4600range_y_max = 1080;
        public static int robot_4600range_z_max = 1900;
        public static int robot_4600range_roll_max = 15;
        public static int robot_4600range_pitch_max = 15;
        public static int robot_4600range_yaw_max = 15;

        public static int robot_4600range_x_min = 0;
        public static int robot_4600range_y_min = -1080;
        public static int robot_4600range_z_min = 1070;
        public static int robot_4600range_roll_min = -15;
        public static int robot_4600range_pitch_min = -15;
        public static int robot_4600range_yaw_min = -15;

        //6650S机器人运动范围
        public static int robot_6650range_x_max = 3480;
        public static int robot_6650range_y_max = 351;
        public static int robot_6650range_z_max = 1201;
        public static int robot_6650range_roll_max = 0;
        public static int robot_6650range_pitch_max = 10;
        public static int robot_6650range_yaw_max = 15;
        public static int robot_6650range_L_max = 1000;

        public static int robot_6650range_x_min = 1480;
        public static int robot_6650range_y_min = -351;
        public static int robot_6650range_z_min = -161;
        public static int robot_6650range_roll_min = 0;
        public static int robot_6650range_pitch_min = -10;
        public static int robot_6650range_yaw_min = -15;
        public static int robot_6650range_L_min = -1000;

        //运动空间中心
        public static double robot_set_x_4600=2120;
        public static double robot_set_y_4600=0;
        public static double robot_set_z_4600=520;
        public static double robot_set_x_6650=2480;
        public static double robot_set_y_6650=0;
        public static double robot_set_z_6650=-240;


        //由trackBar控制时 发送的运动量
        public static double robot_send_x;
        public static double robot_send_y;
        public static double robot_send_z;
        public static double robot_send_roll;
        public static double robot_send_pitch;
        public static double robot_send_yaw;

        //向工作位置或停止位置运动时 发送的运动量
        public static double robot_send_x1;             
        public static double robot_send_y1;
        public static double robot_send_z1;
        public static double robot_send_roll1;
        public static double robot_send_pitch1;
        public static double robot_send_yaw1;

        //工作位置或停止位置预设的位置数据
        public static double robot_set_x;
        public static double robot_set_y;
        public static double robot_set_z;
        public static double robot_set_roll;
        public static double robot_set_pitch;
        public static double robot_set_yaw;
        public static int Port_Number;
        public static TextBox MyTextBox;
        public static TextBox MyTextBox2;
        public static Button Mybutton1;
        public static Button Mybutton3;
        public static Button Mybutton4;
        public static Button Mybutton5;
        public static TrackBar Mytrackbar1;
        public static TrackBar Mytrackbar2;
        public static TrackBar Mytrackbar3;
        public static TrackBar Mytrackbar4;
        public static TrackBar Mytrackbar5;
        public static TrackBar Mytrackbar6;
        public static TrackBar Mytrackbar7;
        public static CheckBox MycheckBox1;
        public static CheckBox MycheckBox2;
        public static ToolStripMenuItem My设置ToolStripMenuItem;
        public static double Y_NOW;
        public static double Z_NOW;
        public static double PITCH_NOW;
        public static double YAW_NOW;
        public static double X_NOW;
        public static double ROLL_NOW;
        public static double L=0;

        //固定位置速度变量
        public static double movement_speed_position;
        public static double movement_speed_posture;
        public static bool st;

        public Form1()
        {

            joystick = new JoyKeys.Voluntary.Joystick(JoyKeys.Voluntary.JoystickAPI.JOYSTICKID1);
            joystick.Capture();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false; //关闭外部修改UI程序后的合法性检查 
            InitializeComponent();
            MyTextBox = this.textBox1; //使用外部变量指向UI工具
            MyTextBox2 = this.textBox2;
            Mybutton5 = this.button5;
            Mybutton4 = this.button4;
            Mybutton3 = this.button3;
            
            Mybutton1 = this.button1;
            Mytrackbar1= this.trackBar1;
            Mytrackbar2 = this.trackBar2;
            Mytrackbar3 = this.trackBar3;
            Mytrackbar4 = this.trackBar4;
            Mytrackbar5 = this.trackBar5;
            Mytrackbar6 = this.trackBar6;
            Mytrackbar7 = this.trackBar7;
            MycheckBox1 = this.checkBox1;
            MycheckBox2 = this.checkBox2;

            My设置ToolStripMenuItem = this.设置ToolStripMenuItem;
            button1.Enabled = false;
            
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            //trackBar1.Enabled = false;
            //trackBar2.Enabled = false;
            //trackBar3.Enabled = false;
            trackBar4.Enabled = false;
            trackBar5.Enabled = false;
            trackBar6.Enabled = false;
            trackBar7.Enabled = false;
            设置ToolStripMenuItem.Enabled = false;
            textBox1.Text = "请选择控制模型...".ToString();
            st = false;
        }
        public void button1_Click(object sender, EventArgs e)
        {
            s1 = new Sensor();
            s1.Start();
            button1.Enabled = false;
            checkBox1.Enabled= false;
            checkBox2.Enabled = false;
            textBox1.Text = "等待飞机连接...".ToString();
            // Console.ReadKey();
            timer1.Start();
            
        }
        public void button3_Click(object sender, EventArgs e)
        {
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar4.Value = 0;
            trackBar5.Value = 0;
            trackBar6.Value = 0;
            robot_send_x = trackBar1.Value;
            robot_send_y = trackBar2.Value;
            robot_send_z = trackBar3.Value;
            robot_send_roll = trackBar4.Value;
            robot_send_pitch = trackBar5.Value;
            robot_send_yaw = trackBar6.Value;
            Form1.get_initial_position = true;
            Form1.Set_position = false;
            textBox1.Text = "运动停止...".ToString();
            
            button4.Enabled = true;
            button5.Enabled = true;
            trackBar1.Enabled = true;
            trackBar2.Enabled = true;
            trackBar3.Enabled = true;
            trackBar4.Enabled = true;
            trackBar5.Enabled = true;
            trackBar6.Enabled = true;
            设置ToolStripMenuItem.Enabled = true;


        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            System.Environment.Exit(0);
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510)
            {
                if (trackBar1.Value >= robot_4600range_x_max) { trackBar1.Value = robot_4600range_x_max; }
                if (trackBar1.Value <= robot_4600range_x_min) { trackBar1.Value = robot_4600range_x_min; }

            }
            else
            {
                
                if (trackBar1.Value >= robot_6650range_x_max) { trackBar1.Value = robot_6650range_x_max; }
                if (trackBar1.Value <= robot_6650range_x_min) { trackBar1.Value = robot_6650range_x_min; }
                
            }
            robot_send_x = trackBar1.Value;

        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510)
            {
                if (trackBar2.Value >= robot_4600range_y_max) { trackBar2.Value = robot_4600range_y_max; }
                if (trackBar2.Value <= robot_4600range_y_min) { trackBar2.Value = robot_4600range_y_min; }

            }
            else
            { 
                if (trackBar2.Value >= robot_6650range_y_max) { trackBar2.Value = robot_6650range_y_max; }
                if (trackBar2.Value <= robot_6650range_y_min) { trackBar2.Value = robot_6650range_y_min; }
            }
            robot_send_y = trackBar2.Value;
        }
        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510)
            {
                if (trackBar3.Value >= robot_4600range_z_max) { trackBar3.Value = robot_4600range_z_max; }
                if (trackBar3.Value <= robot_4600range_z_min) { trackBar3.Value = robot_4600range_z_min; }

            }
            else
            {
                if (trackBar3.Value >= robot_6650range_z_max) { trackBar3.Value = robot_6650range_z_max; }
                if (trackBar3.Value <= robot_6650range_z_min) { trackBar3.Value = robot_6650range_z_min; }
            }
            robot_send_z = trackBar3.Value; 
        }
        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510)
            {
                if (trackBar4.Value >= robot_4600range_roll_max) { trackBar4.Value = robot_4600range_roll_max; }
                if (trackBar4.Value <= robot_4600range_roll_min) { trackBar4.Value = robot_4600range_roll_min; }
               
            }
            else
            {
                if (trackBar4.Value >= robot_6650range_roll_max) { trackBar4.Value = robot_6650range_roll_max; }
                if (trackBar4.Value <= robot_6650range_roll_min) { trackBar4.Value = robot_6650range_roll_min; }

            }
            robot_send_roll = trackBar4.Value;
        }
        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510)
            {
                if (trackBar5.Value >= robot_4600range_pitch_max) { trackBar5.Value = robot_4600range_pitch_max; }
                if (trackBar5.Value <= robot_4600range_pitch_min) { trackBar5.Value = robot_4600range_pitch_min; }

            }
            else
            {
                if (trackBar5.Value >= robot_6650range_pitch_max) { trackBar5.Value = robot_6650range_pitch_max; }
                if (trackBar5.Value <= robot_6650range_pitch_min) { trackBar5.Value = robot_6650range_pitch_min; }
            }
            robot_send_pitch = trackBar5.Value;
        }
        private void trackBar6_ValueChanged(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510)
            {
                if (trackBar6.Value >= robot_4600range_yaw_max) { trackBar6.Value = robot_4600range_yaw_max; }
                if (trackBar6.Value <= robot_4600range_yaw_min) { trackBar6.Value = robot_4600range_yaw_min; }

            }
            else
            {
                if (trackBar6.Value >= robot_6650range_yaw_max) { trackBar6.Value = robot_6650range_yaw_max; }
                if (trackBar6.Value <= robot_6650range_yaw_min) { trackBar6.Value = robot_6650range_yaw_min; }
            }
            robot_send_yaw = trackBar6.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Form1.Port_Number = 6510;
            button1.Enabled = true;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            设置ToolStripMenuItem.Enabled = true;
            trackBar7.Enabled = false;

            textBox1.Text = "选择受油机(IRB4600)...".ToString();

        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Form1.Port_Number = 6511;
            button1.Enabled = true;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            设置ToolStripMenuItem.Enabled = true;
            //trackBar1.Enabled = false;
            //trackBar2.Enabled = false;
            //trackBar3.Enabled = false;
            //trackBar4.Enabled = false;


            textBox1.Text = "选择加油机(IRB6650S)...".ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "正在运动至工作位置...".ToString();
            
            button4.Enabled = false;
            button5.Enabled = false;
            trackBar1.Enabled= false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;
            trackBar4.Enabled = false;
            trackBar5.Enabled = false;
            trackBar6.Enabled = false;
            trackBar7.Enabled = false;
            设置ToolStripMenuItem.Enabled = false;
            if (Form1.Port_Number == 6511)
            {
                movement_speed_position = set_movement_speed_position_6650S;
                movement_speed_posture = set_movement_speed_posture_6650S;
                Form1.robot_set_x = robot_set_x_6650;
                Form1.robot_set_y = robot_set_y_6650;
                Form1.robot_set_z = robot_set_z_6650;

                Form1.robot_set_roll = 0;
                Form1.robot_set_pitch = 30;
                Form1.robot_set_yaw = 0;
            }
            else 
            {
                movement_speed_position = set_movement_speed_position_4600;
                movement_speed_posture = set_movement_speed_posture_4600;
                Form1.robot_set_x = robot_set_x_4600;
                Form1.robot_set_y = robot_set_y_4600;
                Form1.robot_set_z = robot_set_z_4600;
                Form1.robot_set_roll = 0;
                Form1.robot_set_pitch = 0;
                Form1.robot_set_yaw = 0;
            }
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar4.Value = 0;
            trackBar5.Value = 0;
            trackBar6.Value = 0;
            trackBar7.Value = 0;
            robot_send_x = trackBar1.Value;
            robot_send_y = trackBar2.Value;
            robot_send_z = trackBar3.Value;
            robot_send_roll = trackBar4.Value;
            robot_send_pitch = trackBar5.Value;
            robot_send_yaw = trackBar6.Value;
            Set_position = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "正在运动至停机位置...".ToString();
            
            button4.Enabled = false;
            button5.Enabled = false;
            trackBar1.Enabled = false;
            trackBar2.Enabled = false;
            trackBar3.Enabled = false;
            trackBar4.Enabled = false;
            trackBar5.Enabled = false;
            trackBar6.Enabled = false;
            trackBar7.Enabled = false;
            设置ToolStripMenuItem.Enabled = false;
            if (Form1.Port_Number == 6511)
            {
                movement_speed_position = set_movement_speed_position_6650S;
                movement_speed_posture = set_movement_speed_posture_6650S;
                Form1.robot_set_x = 1956;
                Form1.robot_set_y = 0;
                Form1.robot_set_z = 1800;
                Form1.robot_set_roll = 0;
                Form1.robot_set_pitch = -10;
                Form1.robot_set_yaw = 0;

            }
            else
            {
                movement_speed_position = set_movement_speed_position_4600;
                movement_speed_posture = set_movement_speed_posture_4600;
                Form1.robot_set_x = 990;
                Form1.robot_set_y = 0;
                Form1.robot_set_z = 1410;
                Form1.robot_set_roll = 0;
                Form1.robot_set_pitch = 0;
                Form1.robot_set_yaw = 0;
                
            }
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 0;
            trackBar4.Value = 0;
            trackBar5.Value = 0;
            trackBar6.Value = 0;
            trackBar7.Value = 0;

            robot_send_x = trackBar1.Value;
            robot_send_y = trackBar2.Value;
            robot_send_z = trackBar3.Value;
            robot_send_roll = trackBar4.Value;
            robot_send_pitch = trackBar5.Value;
            robot_send_yaw = trackBar6.Value;
            Set_position = true;
        }



        private void 设置ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Form f = new Form3();
            f.Show();
        }

        private void 退出ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void trackBar7_ValueChanged(object sender, EventArgs e)
        {

            if (trackBar7.Value >= robot_6650range_L_max) { trackBar7.Value = robot_6650range_L_max; }
            if (trackBar7.Value <= robot_6650range_L_min) { trackBar7.Value = robot_6650range_L_min; }

            robot_send_x = trackBar7.Value * Math.Cos(robot_send_pitch) * Math.Cos(robot_send_yaw);
            robot_send_y = -trackBar7.Value * Math.Cos(robot_send_pitch) * Math.Sin(robot_send_yaw);
            robot_send_z = trackBar7.Value * Math.Sin(robot_send_pitch);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510 && st)
            {
                if (Form1.joystick.infoEx.dwYpos - 32760 < 0) //低头
                {
                    Form1.robot_send_pitch = -Form1.robot_4600range_pitch_max * (Form1.joystick.infoEx.dwYpos - 32767) / 32767;
                    
                    if (Z_NOW < robot_4600range_z_min * 1.1)
                    {
                        Form1.robot_send_z += (-0.1 * Form1.Z_speed_4600 * Form1.robot_send_pitch / Form1.robot_4600range_pitch_max) * (Z_NOW- robot_4600range_z_min) / (robot_4600range_z_min * 0.1);
                    }
                    else { Form1.robot_send_z += -0.1 * Form1.Z_speed_4600 * Form1.robot_send_pitch / Form1.robot_4600range_pitch_max; }
                }

                else if(Form1.joystick.infoEx.dwYpos - 32775 > 0) //仰头
                { 
                    Form1.robot_send_pitch = Form1.robot_4600range_pitch_min * (Form1.joystick.infoEx.dwYpos - 32767) / 32767;
                    //Console.WriteLine(robot_4600range_z_max - Z_NOW);
                    if (Z_NOW > robot_4600range_z_max * 0.9)
                    {
                         Form1.robot_send_z += (0.1 * Form1.Z_speed_4600 * Form1.robot_send_pitch / Form1.robot_4600range_pitch_min) * (robot_4600range_z_max - Z_NOW) / (robot_4600range_z_max * 0.1); 
                    }
                    else { Form1.robot_send_z += 0.1 * Form1.Z_speed_4600 * Form1.robot_send_pitch / Form1.robot_4600range_pitch_min; }

                }

                if (Form1.joystick.infoEx.dwXpos - 32760 < 0) //左偏
                {
                    Form1.robot_send_roll = -Form1.robot_4600range_roll_min * (Form1.joystick.infoEx.dwXpos - 32767) / 32767;
                    if (Y_NOW > robot_4600range_y_max * 0.9)
                    {
                        Form1.robot_send_y += (0.1 * Form1.Y_speed_4600 * Form1.robot_send_roll / Form1.robot_4600range_roll_min)*(robot_4600range_y_max- Y_NOW)/ (robot_4600range_y_max*0.1);
                    }
                    else { Form1.robot_send_y += 0.1 * Form1.Y_speed_4600 * Form1.robot_send_roll / Form1.robot_4600range_roll_min; }
                }
                else if (Form1.joystick.infoEx.dwXpos - 32775 > 0) //右偏
                {
                    Form1.robot_send_roll = Form1.robot_4600range_roll_max * (Form1.joystick.infoEx.dwXpos - 32767) / 32767;
                    if (Y_NOW < robot_4600range_y_min * 0.9)
                    {
                        Form1.robot_send_y += (-0.1 * Form1.Y_speed_4600 * Form1.robot_send_roll / Form1.robot_4600range_roll_max)*((Y_NOW- robot_4600range_y_min) /(-robot_4600range_y_min*0.1));
                    }
                    else { Form1.robot_send_y += -0.1 * Form1.Y_speed_4600 * Form1.robot_send_roll / Form1.robot_4600range_roll_max; }
                }
                

                //位置限制，超过最大位置的90%进行限位
            }
            else if (Form1.Port_Number == 6511 && st)
            {
                if (Form1.joystick.infoEx.dwYpos - 32760 < 0) //低头
                {
                    
                        Form1.robot_send_pitch += -0.1*Y_speed_6650S * (Form1.joystick.infoEx.dwYpos - 32767) / 32767;
                    if (Form1.robot_send_pitch > robot_6650range_pitch_max) Form1.robot_send_pitch = robot_6650range_pitch_max;

                }

                else if (Form1.joystick.infoEx.dwYpos - 32775 > 0) //仰头
                {
                    Form1.robot_send_pitch += -0.1 * Y_speed_6650S * (Form1.joystick.infoEx.dwYpos - 32767) / 32767;
                    if (Form1.robot_send_pitch < robot_6650range_pitch_min) Form1.robot_send_pitch = robot_6650range_pitch_min;
                }
                if (Form1.joystick.infoEx.dwXpos - 32760 < 0) //左偏
                {
                    Form1.robot_send_yaw += -0.1 * Z_speed_6650S * (Form1.joystick.infoEx.dwXpos - 32767) / 32767;
                    if (Form1.robot_send_yaw > robot_6650range_yaw_max) Form1.robot_send_yaw = robot_6650range_yaw_max;
                }
                else if (Form1.joystick.infoEx.dwXpos - 32775 > 0) //右偏
                {
                    Form1.robot_send_yaw += -0.1 * Z_speed_6650S * (Form1.joystick.infoEx.dwXpos - 32767) / 32767;
                    if (Form1.robot_send_yaw < robot_6650range_yaw_min) Form1.robot_send_yaw = robot_6650range_yaw_min;
                }
                if ((joystick.PreviousButtons & JoyKeys.Voluntary.JoystickButtons.B6) == JoyKeys.Voluntary.JoystickButtons.B6)
                {
                    //伸长
                    L += 0.1* L_speed;
                    if (L > robot_6650range_L_max) { L = robot_6650range_L_max; }

                }
                if ((joystick.PreviousButtons & JoyKeys.Voluntary.JoystickButtons.B5) == JoyKeys.Voluntary.JoystickButtons.B5)
                {
                    //缩短
                    L -= 3;
                    if (L < robot_6650range_L_min) { L = robot_6650range_L_min; }

                }
                robot_send_x  = L * Math.Cos(PITCH_NOW * Math.PI / 180) * Math.Cos(YAW_NOW * Math.PI / 180);
                robot_send_y  = L * Math.Sin(YAW_NOW * Math.PI / 180) * Math.Cos(PITCH_NOW * Math.PI / 180);
                robot_send_z  = -L * Math.Sin(PITCH_NOW * Math.PI / 180);
            }
            
        }
    }


     public class Sensor
    {

        public static int _ipPortNumber = Form1.Port_Number;
        public static double robot_initial_x;
        public static double robot_initial_y;
        public static double robot_initial_z;
        public static double robot_initial_roll;
        public static double robot_initial_pitch;
        public static double robot_initial_yaw;
        public EgmRobot robot;




        public bool st=false;
        public static Thread _sensorThread = null;
        private UdpClient _udpServer = null;
        public static bool _exitThread = false;
        private uint _seqNumber = 0;

        public void SensorThread()
        {
            // create an udp client and listen on any address and the port _ipPortNumber
            _udpServer = new UdpClient(_ipPortNumber);
            var remoteEP = new IPEndPoint(IPAddress.Any, _ipPortNumber);

           
            while (_exitThread == false)
            {
                // get the message from robot
                var data = _udpServer.Receive(ref remoteEP);  //接收机器人的位置信息
                if (data != null)
                {
                    
                    if (st == false)
                    {

                        st = true;
                        Form1.MyTextBox.Text = "飞机已连接...".ToString();
                        
                        Form1.Mybutton3.Enabled = true;
                        Form1.Mybutton4.Enabled = true;
                        Form1.Mybutton5.Enabled = true;
                        if (Form1.Port_Number == 6510)
                        {
                            Form1.Mytrackbar1.Enabled = true;
                            Form1.Mytrackbar2.Enabled = true;
                            Form1.Mytrackbar3.Enabled = true;
                        }
                        else 
                        {
                            Form1.Mytrackbar7.Enabled = true;
                        }
                        
                        Form1.Mytrackbar4.Enabled = true;
                        Form1.Mytrackbar5.Enabled = true;
                        Form1.Mytrackbar6.Enabled = true;
                    }
                        
                    // de-serialize inbound message from robot
                    robot = EgmRobot.CreateBuilder().MergeFrom(data).Build(); //位置信息反序列化

                    // display inbound message
                    DisplayInboundMessage(robot); //显示当前位置信息并且第一次收到位置信息时，设置为初始点。后续控制都是在初始点上加偏移量

                    // create a new outbound sensor message
                    EgmSensor.Builder sensor = EgmSensor.CreateBuilder(); //创建给机器人的发送信息
                    CreateSensorMessage(sensor);// 设置机器人下一步的运动信息
                    Form1.st = true;
                    using (MemoryStream memoryStream = new MemoryStream()) //序列化后发送给机器人
                    {
                        EgmSensor sensorMessage = sensor.Build();
                        sensorMessage.WriteTo(memoryStream);
                       // Console.WriteLine(sensorMessage.ToString());
                        //send the udp message to the robot
                        int bytesSent = _udpServer.Send(memoryStream.ToArray(),
                                                       (int)memoryStream.Length, remoteEP);
                        if (bytesSent < 0)
                        {
                            Console.WriteLine("Error send to robot");
                            Form1.MyTextBox.Text = "飞机断开连接...".ToString();
                        }
                    }


                }
                
                else 
                {
                    if (st)
                    {
                        st = false;
                        Form1.MyTextBox.Text = "飞机断开连接...".ToString();
                    }


                }
                
            }
            
        }

        // Display message from robot
        void DisplayInboundMessage(EgmRobot robot)
        {
            if (robot.HasHeader && robot.Header.HasSeqno && robot.Header.HasTm)
            {
                //Console.WriteLine("Seq={0} tm={1}\npos=\n{2}\nEuler=\n{3}",
                //    robot.Header.Seqno.ToString(), robot.Header.Tm.ToString(), robot.FeedBack.Cartesian.Pos, robot.FeedBack.Cartesian.Euler);
                string result = string.Format("X={0},Y={1},Z={2}\r\nRx={3},Ry={4},Rz={5}\r\nL={6}", Math.Round(robot.FeedBack.Cartesian.Pos.X,2), Math.Round(robot.FeedBack.Cartesian.Pos.Y,2), Math.Round(robot.FeedBack.Cartesian.Pos.Z,2), Math.Round(robot.FeedBack.Cartesian.Euler.X,2), Math.Round(robot.FeedBack.Cartesian.Euler.Y,2), Math.Round(robot.FeedBack.Cartesian.Euler.Z,2),Form1.L);
                Form1.MyTextBox2.Text = result.ToString();
                Form1.Z_NOW = robot.FeedBack.Cartesian.Pos.Z;
                Form1.Y_NOW = robot.FeedBack.Cartesian.Pos.Y;
                Form1.X_NOW = robot.FeedBack.Cartesian.Pos.X;
                Form1.YAW_NOW = robot.FeedBack.Cartesian.Euler.Z;
                Form1.PITCH_NOW = robot.FeedBack.Cartesian.Euler.Y;
                Form1.ROLL_NOW = robot.FeedBack.Cartesian.Euler.X;
                if (Form1.Set_position)
                {

                    robot_initial_yaw = robot.FeedBack.Cartesian.Euler.Z;
                    robot_initial_pitch = robot.FeedBack.Cartesian.Euler.Y;
                    robot_initial_roll = robot.FeedBack.Cartesian.Euler.X;
                    robot_initial_x = robot.FeedBack.Cartesian.Pos.X;
                    robot_initial_y = robot.FeedBack.Cartesian.Pos.Y;
                    robot_initial_z = robot.FeedBack.Cartesian.Pos.Z;

                    if (System.Math.Abs(robot.FeedBack.Cartesian.Euler.Z - Form1.robot_set_yaw) > Form1.movement_speed_posture) { Form1.robot_send_yaw1 = -Form1.movement_speed_posture * System.Math.Abs(robot.FeedBack.Cartesian.Euler.Z - Form1.robot_set_yaw) / (robot.FeedBack.Cartesian.Euler.Z - Form1.robot_set_yaw); }
                    else { Form1.robot_send_yaw1 = Form1.robot_set_yaw-robot.FeedBack.Cartesian.Euler.Z  ; }
                    if (System.Math.Abs(robot.FeedBack.Cartesian.Euler.Y - Form1.robot_set_pitch) > Form1.movement_speed_posture) { Form1.robot_send_pitch1 = -Form1.movement_speed_posture * System.Math.Abs(robot.FeedBack.Cartesian.Euler.Y - Form1.robot_set_pitch) / (robot.FeedBack.Cartesian.Euler.Y - Form1.robot_set_pitch); }
                    else { Form1.robot_send_pitch1 = Form1.robot_set_pitch- robot.FeedBack.Cartesian.Euler.Y; }
                    if (System.Math.Abs(robot.FeedBack.Cartesian.Euler.X - Form1.robot_set_roll) > Form1.movement_speed_posture) { Form1.robot_send_roll1 = -Form1.movement_speed_posture * System.Math.Abs(robot.FeedBack.Cartesian.Euler.X - Form1.robot_set_roll) / (robot.FeedBack.Cartesian.Euler.X - Form1.robot_set_roll); }
                    else { Form1.robot_send_roll1 = Form1.robot_set_roll- robot.FeedBack.Cartesian.Euler.X; }
                    if (System.Math.Abs(robot.FeedBack.Cartesian.Pos.X - Form1.robot_set_x) > Form1.movement_speed_position) { Form1.robot_send_x1 = (-System.Math.Abs(robot.FeedBack.Cartesian.Pos.X - Form1.robot_set_x) / (robot.FeedBack.Cartesian.Pos.X - Form1.robot_set_x)) * Form1.movement_speed_position; }
                    else { Form1.robot_send_x1 = Form1.robot_set_x- robot.FeedBack.Cartesian.Pos.X; }
                    if (System.Math.Abs(robot.FeedBack.Cartesian.Pos.Y - Form1.robot_set_y) > Form1.movement_speed_position) { Form1.robot_send_y1 = (-System.Math.Abs(robot.FeedBack.Cartesian.Pos.Y - Form1.robot_set_y) / (robot.FeedBack.Cartesian.Pos.Y - Form1.robot_set_y)) * Form1.movement_speed_position; }
                    else { Form1.robot_send_y1 = Form1.robot_set_y- robot.FeedBack.Cartesian.Pos.Y; }
                    if (System.Math.Abs(robot.FeedBack.Cartesian.Pos.Z - Form1.robot_set_z) > Form1.movement_speed_position) { Form1.robot_send_z1 = (-System.Math.Abs(robot.FeedBack.Cartesian.Pos.Z - Form1.robot_set_z) / (robot.FeedBack.Cartesian.Pos.Z - Form1.robot_set_z)) * Form1.movement_speed_position; }
                    else { Form1.robot_send_z1 = Form1.robot_set_z- robot.FeedBack.Cartesian.Pos.Z; }
                    if (System.Math.Abs(Form1.robot_send_x1)<=0.1 &&  System.Math.Abs(Form1.robot_send_y1)<=0.1 && System.Math.Abs(Form1.robot_send_z1)<=0.1 && System.Math.Abs(Form1.robot_send_yaw1) <=0.05 && System.Math.Abs(Form1.robot_send_pitch1) <=0.05 && System.Math.Abs(Form1.robot_send_roll1)  <= 0.05)
                    {
                        Form1.Set_position = false;
                        robot_initial_yaw = robot.FeedBack.Cartesian.Euler.Z;
                        robot_initial_pitch = robot.FeedBack.Cartesian.Euler.Y;
                        robot_initial_roll = robot.FeedBack.Cartesian.Euler.X;
                        robot_initial_x = robot.FeedBack.Cartesian.Pos.X;
                        robot_initial_y = robot.FeedBack.Cartesian.Pos.Y;
                        robot_initial_z = robot.FeedBack.Cartesian.Pos.Z;
                        Form1.MyTextBox.Text = "运动到位...".ToString();
                        Form1.Mybutton4.Enabled = true;
                        Form1.Mybutton5.Enabled = true;
                        Form1.Mytrackbar1.Enabled = true;
                        Form1.Mytrackbar2.Enabled = true;
                        Form1.Mytrackbar3.Enabled = true;
                        Form1.Mytrackbar4.Enabled = true;
                        Form1.Mytrackbar5.Enabled = true;
                        Form1.Mytrackbar6.Enabled = true;
                        Form1.Mytrackbar7.Enabled = true;
                        Form1.My设置ToolStripMenuItem.Enabled = true;
                    }
                    

                }

                if (Form1.get_initial_position)
                {
                    Form1.get_initial_position = false;
                    robot_initial_yaw = robot.FeedBack.Cartesian.Euler.Z;
                    robot_initial_pitch = robot.FeedBack.Cartesian.Euler.Y;
                    robot_initial_roll = robot.FeedBack.Cartesian.Euler.X;
                    robot_initial_x = robot.FeedBack.Cartesian.Pos.X;
                    robot_initial_y = robot.FeedBack.Cartesian.Pos.Y;
                    robot_initial_z = robot.FeedBack.Cartesian.Pos.Z;
                    


                }

            }
            else
            {
                Console.WriteLine("No header in robot message");
                if (st)
                {
                    st = false;
                    Form1.MyTextBox.Text = "飞机断开连接...".ToString();
                }
            }
        }

        //////////////////////////////////////////////////////////////////////////
        // Create a sensor message to send to the robot
        void CreateSensorMessage(EgmSensor.Builder sensor)
        {
            // create a header
            EgmHeader.Builder hdr = new EgmHeader.Builder();
            hdr.SetSeqno(_seqNumber++)
                .SetTm((uint)DateTime.Now.Ticks)
                .SetMtype(EgmHeader.Types.MessageType.MSGTYPE_CORRECTION);

            sensor.SetHeader(hdr);

            // create some sensor data
            EgmPlanned.Builder planned = new EgmPlanned.Builder();
            EgmPose.Builder pos = new EgmPose.Builder();
            EgmQuaternion.Builder pq = new EgmQuaternion.Builder();
            EgmCartesian.Builder pc = new EgmCartesian.Builder();
            EgmEuler.Builder po = new EgmEuler.Builder();

            if (Form1.Set_position)
            {
                pc.SetX(robot_initial_x + Form1.robot_send_x1).SetY(robot_initial_y + Form1.robot_send_y1).SetZ(robot_initial_z + Form1.robot_send_z1);
                po.SetX(robot_initial_roll+Form1.robot_send_roll1).SetY(robot_initial_pitch + Form1.robot_send_pitch1).SetZ(robot_initial_yaw + Form1.robot_send_yaw1);
            }
            else {
                pc.SetX(robot_initial_x + Form1.robot_send_x).SetY(robot_initial_y + Form1.robot_send_y).SetZ(robot_initial_z + Form1.robot_send_z);
                po.SetX(robot_initial_roll + Form1.robot_send_roll).SetY(robot_initial_pitch+Form1.robot_send_pitch).SetZ(robot_initial_yaw + Form1.robot_send_yaw);
            }
                

            pos.SetPos(pc).SetEuler(po);
         
            planned.SetCartesian(pos);  // bind pos object to planned
            sensor.SetPlanned(planned); // bind planned to sensor object

            return;
        }

        // Start a thread to listen on inbound messages
        public void Start()
        {
            _sensorThread = new Thread(new ThreadStart(SensorThread));
            _sensorThread.Start();
            
        }


        // Stop and exit thread
        public void Stop()
        {


            _exitThread = true;
            _sensorThread.Abort();

        }
    }

}
