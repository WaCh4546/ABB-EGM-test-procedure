using System;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using abb.egm;

namespace egm_move1
{
    public partial class Form1 : Form
    {        
        public Form1()
        { 
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false; 
            InitializeComponent();
        }
        //变量定义

        public static EGMMotion IRBrobot ;
        public static RobotTarget stop_position_4600 = new RobotTarget(990,0,1410);
        public static RobotTarget stop_position_6650S = new RobotTarget(1956,0,1800,0,-10);
        public static RobotTarget range_4600 = new RobotTarget(0, 1000,  1000,  15,  15,  15);
        public static RobotTarget range_6650S = new RobotTarget(910, 490, 653, 0,  10,  15, 2000);
        public static RobotTarget work_center_4600 = new RobotTarget(2180,0,500);
        public static RobotTarget work_center_6650S = new RobotTarget(2480, 0, -200,0,30,0);


        private void 选中IRB4600(object sender, EventArgs e)
        {
            IRBrobot = new EGMMotion(6510, stop_position_4600, work_center_4600, range_4600, textBox2, textBox1);
            textBox2.Text = DateTime.Now.Ticks.ToString(); 
        }

        private void 选中IRB6650S(object sender, EventArgs e)
        {
            IRBrobot = new EGMMotion(6511, stop_position_6650S, work_center_6650S, range_6650S, textBox2, textBox1);
        }

        public void 启动(object sender, EventArgs e)
        {
            IRBrobot.Start();
        }

        private void 刹车(object sender, EventArgs e)
        {
            XBar.Value = 0;
            YBar.Value = 0;
            ZBar.Value = 0;
            YawBar.Value = 0;
            PitchBar.Value = 0;
            RollBar.Value = 0;
            LBar.Value = 0;
            IRBrobot.Brake();

        }

        private void 关闭窗口(object sender, FormClosedEventArgs e)
        {
            if (IRBrobot != null) { IRBrobot.Stop(); }
            System.Environment.Exit(0);
        }

        private void 停机位置(object sender, EventArgs e)
        {
            IRBrobot.GoToStopPosition();
        }

        private void 工作位置(object sender, EventArgs e)
        {
            IRBrobot.GoToWorkPosition();
        }

        private void X轴运动(object sender, EventArgs e)
        {
            if (IRBrobot.robotchoice) IRBrobot.Sport_a.X = XBar.Value;
        }

        private void Y轴运动(object sender, EventArgs e)
        {
            if (IRBrobot.robotchoice) IRBrobot.Sport_a.Y = YBar.Value;
        }

        private void Z轴运动(object sender, EventArgs e)
        {
            if (IRBrobot.robotchoice) IRBrobot.Sport_a.Z = ZBar.Value;
        }

        private void 滚转运动(object sender, EventArgs e)
        {
            if(!IRBrobot.robotchoice) IRBrobot.Sport_x.ROLL = RollBar.Value;
            else IRBrobot.Sport_a.ROLL = RollBar.Value;
        }

        private void 俯仰运动(object sender, EventArgs e)
        {
            if (!IRBrobot.robotchoice) IRBrobot.Sport_x.PITCH = PitchBar.Value;
            else IRBrobot.Sport_a.PITCH = PitchBar.Value;
        }

        private void 偏航运动(object sender, EventArgs e)
        {
            if (!IRBrobot.robotchoice) IRBrobot.Sport_x.YAW = YawBar.Value;
            else IRBrobot.Sport_a.YAW = YawBar.Value;
        }

        private void 加油杆长变化(object sender, EventArgs e)
        {
            if (!IRBrobot.robotchoice) IRBrobot.Sport_x.L = LBar.Value;
        }

        private void 定期刷新状态(object sender, EventArgs e)
        {

        }

    }


    /// </EGM运动>
    public class EGMMotion
    {
        public EGMMotion(int ipPortNumber, RobotTarget stopposition, RobotTarget workposition, RobotTarget workrange,
            TextBox datatext = null, TextBox prompttext = null)
        {
            _ipPortNumber = ipPortNumber;
            Stopposition = stopposition;
            Workposition = workposition;
            Sport_xmax = workrange;
            DataText = datatext;
            PromptMessage = prompttext;
            Init(_ipPortNumber, T);
        }

        /// <summary>
        /// 变量定义
        /// </summary>
        private Thread TimerThread = null;
        private Thread WorkThread = null;
        private Thread ReceiveThread = null;
        private UdpClient _udpServer = null;
        private static int _ipPortNumber;
        private uint _seqNumber = 0;
        private IPEndPoint remoteEP;
        public int T=30;
        public System.Timers.Timer aTimer;
        public bool robotchoice;
        private bool connection = false;
        private bool brake = false;
        public double speed_position=25;
        public double speed_posture=2;
        public double l_max_ratio = 0.8;
        private static TextBox DataText, PromptMessage;
        private RobotTarget Standard = new RobotTarget();
        public RobotTarget Sport_a = new RobotTarget();
        public RobotTarget Sport_v = new RobotTarget();
        public RobotTarget Sport_vReal = new RobotTarget();
        private RobotTarget Sport_vl = new RobotTarget(0,200,200,0.5,0.5,0.5);  //临时
        private RobotTarget Sport_vmax = new RobotTarget(0,300,300,1,1,1);//临时
        public RobotTarget Sport_x = new RobotTarget();
        private RobotTarget Sport_xl = new RobotTarget();
        private RobotTarget Sport_xmax = new RobotTarget();
        public RobotTarget Receive = new RobotTarget();
        private RobotTarget Stopposition = new RobotTarget();
        private RobotTarget Workposition = new RobotTarget();


        /// </summary>
        /// 初始化
        /// <param name="_ipPortNumber"></通信端口号>
        /// <param name="CommuSpeed"></通信速度T>
        /// <param name="speed_position"></XYZ方向在停机工作时单步移动步长>
        /// <param name="speed_posture"></姿态角在停机工作时单步移动步长>
        public void Init(int _ipPortNumber,int T)
        {
            _udpServer = new UdpClient(_ipPortNumber);
            remoteEP = new IPEndPoint(IPAddress.Any, _ipPortNumber);
            aTimer = new System.Timers.Timer{Interval = T}; 
            if (_ipPortNumber == 6510) { robotchoice = true; }
            else { robotchoice = false; }
            //临时
            Sport_xl.PITCH = Sport_xmax.PITCH*0.8; Sport_xl.YAW = Sport_xmax.YAW * 0.8; Sport_xl.ROLL = Sport_xmax.ROLL * 0.8;
            Sport_xl.X = Sport_xmax.X * 0.5; Sport_xl.Y = Sport_xmax.Y * 0.5; Sport_xl.Z = Sport_xmax.Z * 0.5;
        }

        public void GoToStopPosition()
        {
            aTimer.Enabled = false;
            if (TimerThread != null && TimerThread.ThreadState == ThreadState.Running) {  TimerThread.Abort();  }
            if (ReceiveThread != null && ReceiveThread.ThreadState == ThreadState.Running) { ReceiveThread.Abort(); }
            if (WorkThread != null && WorkThread.ThreadState == ThreadState.Running) { WorkThread.Abort(); }
            WorkThread = new Thread(new ThreadStart(GoToStopPositionThread));
            WorkThread.Start();
            
            
        }
        public void GoToWorkPosition()
        {
            aTimer.Enabled = false;
            if (TimerThread != null && TimerThread.ThreadState == ThreadState.Running) {  TimerThread.Abort();  }
            if (ReceiveThread != null && ReceiveThread.ThreadState == ThreadState.Running) { ReceiveThread.Abort(); }
            if (WorkThread != null && WorkThread.ThreadState == ThreadState.Running) { WorkThread.Abort(); }
            WorkThread = new Thread(new ThreadStart(GoToWorkPositionThread));
            WorkThread.Start();
            
            
        }
        private void GoToStopPositionThread()
        {
            brake = false;
            if (robotchoice) //4600
            {
                if (BackStop(Stopposition)) DisplayPrompt("受油机返回停机位置...");
                else if(connection)DisplayPrompt("受油机刹车...");
            }
            else //6650S
            {
                GoWork(Workposition);
                if (BackStop(Stopposition)) DisplayPrompt("加油机返回停机位置...");
                else if (connection) DisplayPrompt("加油机刹车...");
            }
            Start();
            WorkThread.Abort();
        }
        private void GoToWorkPositionThread()
        {
            brake = false;
            if (robotchoice) //4600
            {
                if (GoWork(Workposition))DisplayPrompt("受油机前往工作位置...");
                else DisplayPrompt("受油机刹车..."); 
            }
            else //6650S
            {
                if (GoWork(Workposition)) DisplayPrompt("加油机前往工作位置...");
                else DisplayPrompt("加油机刹车..."); 
            }
            Start();
            WorkThread.Abort();
            
        }
        private bool BackStop(RobotTarget stop)
        {
            RobotTarget send1 = new RobotTarget();
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                if (System.Math.Abs(Receive.PITCH - stop.PITCH) >= speed_posture)
                { send1.PITCH = Sigmoid(stop.PITCH, Receive.PITCH) * speed_posture; }
                else if (System.Math.Abs(Receive.PITCH - stop.PITCH) >= 0.1)
                { send1.PITCH = stop.PITCH - Receive.PITCH; }
                else { send1.PITCH = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.PITCH = stop.PITCH;
                if (System.Math.Abs(Receive.ROLL - stop.ROLL) >= speed_posture)
                { send1.ROLL = Sigmoid(stop.ROLL, Receive.ROLL) * speed_posture; }
                else if (System.Math.Abs(Receive.ROLL - stop.ROLL) >= 0.1)
                { send1.ROLL = stop.ROLL - Receive.ROLL; }
                else { send1.ROLL = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.PITCH = stop.PITCH;
                Receive.ROLL = stop.ROLL;
                if (System.Math.Abs(Receive.YAW - stop.YAW) >= speed_posture)
                { send1.YAW = Sigmoid(stop.YAW, Receive.YAW) * speed_posture; }
                else if (System.Math.Abs(Receive.YAW - stop.YAW) >= 0.1)
                { send1.YAW = stop.YAW - Receive.YAW; }
                else { send1.YAW = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.PITCH = stop.PITCH;
                Receive.ROLL = stop.ROLL;
                Receive.YAW = stop.YAW;
                if (System.Math.Abs(Receive.Y - stop.Y) >= speed_position)
                { send1.Y = Sigmoid(stop.Y, Receive.Y) * speed_position; }
                else if (System.Math.Abs(Receive.Y - stop.Y) >= 0.05)
                { send1.Y = stop.Y - Receive.Y; }
                else { send1.Y = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.PITCH = stop.PITCH;
                Receive.ROLL = stop.ROLL;
                Receive.YAW = stop.YAW;
                Receive.Y = stop.Y;
                if (System.Math.Abs(Receive.Z - stop.Z) >= speed_position)
                { send1.Z = Sigmoid(stop.Z, Receive.Z) * speed_position; }
                else if (System.Math.Abs(Receive.Z - stop.Z) >= 0.05)
                { send1.Z = stop.Z - Receive.Z; }
                else { send1.Z = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.PITCH = stop.PITCH;
                Receive.ROLL = stop.ROLL;
                Receive.YAW = stop.YAW;
                Receive.Y = stop.Y;
                Receive.Z = stop.Z;
                if (System.Math.Abs(Receive.X - stop.X) >= speed_position)
                { send1.X = Sigmoid(stop.X, Receive.X) * speed_position; }
                else if (System.Math.Abs(Receive.X - stop.X) >= 0.05)
                { send1.X = stop.X - Receive.X; }
                else { send1.X = 0; send1.BOOL0 = true; break; }
                SendSetPosition(Receive, send1);
            }
            
            return send1.BOOL0;
        }
        private bool GoWork(RobotTarget work)
        {
            RobotTarget send1 = new RobotTarget();
            while (!brake && connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                if (System.Math.Abs(Receive.Y - work.Y) >= speed_position)
                { send1.Y = Sigmoid(work.Y, Receive.Y) * speed_position; }
                else if (System.Math.Abs(Receive.Y - work.Y) >= 0.05)
                { send1.Y = work.Y - Receive.Y; }
                else { send1.Y = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake && connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.Y = work.Y;
                if (System.Math.Abs(Receive.X - work.X) >= speed_position)
                { send1.X = Sigmoid(work.X, Receive.X) * speed_position; }
                else if (System.Math.Abs(Receive.X - work.X) >= 0.05)
                { send1.X = work.X - Receive.X; }
                else { send1.X = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake && connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.Y = work.Y;
                Receive.X = work.X;
                if (System.Math.Abs(Receive.Z - work.Z) >= speed_position)
                { send1.Z = Sigmoid(work.Z, Receive.Z) * speed_position; }
                else if (System.Math.Abs(Receive.Z - work.Z) >= 0.05)
                { send1.Z = work.Z - Receive.Z; }
                else { send1.Z = 0;  break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive.Y = work.Y;
                Receive.X = work.X;
                Receive.Z = work.Z;
                Receive = DisplayRobotFeedbackPosition();
                if (System.Math.Abs(Receive.PITCH - work.PITCH) >= speed_posture)
                { send1.PITCH = Sigmoid(work.PITCH, Receive.PITCH) * speed_posture; }
                else if (System.Math.Abs(Receive.PITCH - work.PITCH) >= 0.1)
                { send1.PITCH = work.PITCH - Receive.PITCH ; }
                else { send1.PITCH = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.Y = work.Y;
                Receive.X = work.X;
                Receive.Z = work.Z;
                Receive.PITCH = work.PITCH;
                if (System.Math.Abs(Receive.ROLL - work.ROLL) >= speed_posture)
                { send1.ROLL = Sigmoid(work.ROLL, Receive.ROLL) * speed_posture; }
                else if (System.Math.Abs(Receive.ROLL - work.ROLL) >= 0.1)
                { send1.ROLL = work.ROLL - Receive.ROLL;  }
                else  { send1.ROLL = 0; break; }
                SendSetPosition(Receive, send1);
            }
            while (!brake&&connection)
            {
                Receive = DisplayRobotFeedbackPosition();
                Receive.Y = work.Y;
                Receive.X = work.X;
                Receive.Z = work.Z;
                Receive.PITCH = work.PITCH;
                Receive.ROLL = work.ROLL;
                if (System.Math.Abs(Receive.YAW - work.YAW) >= speed_posture)
                { send1.YAW = Sigmoid(work.YAW, Receive.YAW) * speed_posture; }
                else if (System.Math.Abs(Receive.YAW - work.YAW) >= 0.1)
                { send1.YAW = work.YAW - Receive.YAW; }
                else{ send1.YAW = 0; send1.BOOL0 = true; break; }
                SendSetPosition(Receive, send1);
            }
             
            
            return send1.BOOL0;
        }

        /// </主线程函数，启动定时器>
        private void SensorThread()
        {
            while (!WorkPositionInspection()) DisplayPrompt("飞机已连接,等待前往工作位置...");          
            DisplayPrompt("飞机正在工作...");
            Sport_x.Clear();
            Sport_v.Clear();
            Standard = DisplayRobotFeedbackPosition();
            aTimer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEvent);          
            aTimer.Enabled = true;            
        }

        
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {

            
            if(!brake&& connection&& robotchoice) IntegralA2X();
            if (!brake && connection && !robotchoice)
            {
                Sport_x.X = Sport_x.L * Math.Cos(Sport_x.PITCH) * Math.Cos(Sport_x.ROLL);
                Sport_x.Y = -Sport_x.L * Math.Cos(Sport_x.PITCH) * Math.Sin(Sport_x.ROLL);
                Sport_x.Z = Sport_x.L * Math.Sin(Sport_x.PITCH);
            }
                SendSetPosition(Standard, Sport_x);


        }
        private void ReceiveData()
        {while(true) DisplayRobotFeedbackPosition(); }


        public bool WorkPositionInspection()
        {
            RobotTarget zero = new RobotTarget();
            Receive = DisplayRobotFeedbackPosition();
            if (System.Math.Abs(Receive.PITCH - Workposition.PITCH) < 1 && System.Math.Abs(Receive.YAW - Workposition.YAW) < 1 &&
                System.Math.Abs(Receive.ROLL - Workposition.ROLL) < 1 && System.Math.Abs(Receive.X - Workposition.X) < 5 &&
                System.Math.Abs(Receive.Y - Workposition.Y) < 5 && System.Math.Abs(Receive.Z - Workposition.Z) < 5) Workposition.BOOL0 = true;
            else Workposition.BOOL0 = false;
            SendSetPosition(Receive, zero);
            return Workposition.BOOL0;
        }
        
        
        public RobotTarget DisplayRobotFeedbackPosition()
        {           
            var data = _udpServer.Receive(ref remoteEP);  //接收机器人的位置信息
            EgmRobot robot = EgmRobot.CreateBuilder().MergeFrom(data).Build(); //位置信息反序列化
            RobotTarget receive = new RobotTarget();           
            if (data != null)
            {
                if (robot.RapidExecState.State == EgmRapidCtrlExecState.Types.RapidCtrlExecStateType.RAPID_RUNNING)
                {
                    
                    receive.X = robot.FeedBack.Cartesian.Pos.X;
                    receive.Y = robot.FeedBack.Cartesian.Pos.Y;
                    receive.Z = robot.FeedBack.Cartesian.Pos.Z;
                    receive.ROLL = robot.FeedBack.Cartesian.Euler.X;
                    receive.PITCH = robot.FeedBack.Cartesian.Euler.Y;
                    receive.YAW = robot.FeedBack.Cartesian.Euler.Z;
                    string result = string.Format("X={0}\r\nY={1}\r\nZ={2}\r\nRx={3}\r\nRy={4}\r\nRz={5}\r\nX_v={6}\r\nY_v={7}\r\nZ_v={8}\r\nRx_v={9}\r\nRy_v={10}\r\nRz_v={11}\r\n",
                        Math.Round(receive.X, 2), Math.Round(receive.Y, 2), Math.Round(receive.Z, 2),
                        Math.Round(receive.ROLL, 2), Math.Round(receive.PITCH, 2), Math.Round(receive.YAW, 2),
                        Math.Round(Sport_vReal.X, 2), Math.Round(Sport_vReal.Y, 2), Math.Round(Sport_vReal.Z, 2),
                         Math.Round(Sport_vReal.ROLL, 2), Math.Round(Sport_vReal.PITCH, 2), Math.Round(Sport_vReal.YAW, 2));
                    DisplayData(result);
                    receive.BOOL0 = true;
                    connection = true;
                }
                else 
                {
                    connection = false;
                    receive.BOOL0 = false;
                }
            }
              return receive;
        }
        
        
        public bool SendSetPosition( RobotTarget standard, RobotTarget send)
        {
            EgmSensor.Builder sensor = EgmSensor.CreateBuilder(); //创建给机器人的发送信息
            // create a header
            EgmHeader.Builder hdr = new EgmHeader.Builder();
            hdr.SetSeqno(_seqNumber++)
                .SetTm((uint)DateTime.Now.Ticks)
                .SetMtype(EgmHeader.Types.MessageType.MSGTYPE_CORRECTION);
            sensor.SetHeader(hdr);        
            // create some sensor data
            EgmPlanned.Builder planned = new EgmPlanned.Builder();
            EgmPose.Builder pos = new EgmPose.Builder();
            EgmCartesian.Builder pc = new EgmCartesian.Builder();
            EgmEuler.Builder po = new EgmEuler.Builder();
            pc.SetX(standard.X+ send.X).SetY(standard.Y + send.Y).SetZ(standard.Z + send.Z);
            po.SetX(standard.ROLL + send.ROLL).SetY(standard.PITCH + send.PITCH).SetZ(standard.YAW + send.YAW);

           //Console.WriteLine("{0};{1};{2}\n{3};{4};{5}\n", standard.X,standard.Y, standard.Z, send.X, send.Y, send.Z);
            pos.SetPos(pc).SetEuler(po);       
            planned.SetCartesian(pos);  // bind pos object to planned
            sensor.SetPlanned(planned); // bind planned to sensor object
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
                    send.BOOL0 = false;
                }
                else { send.BOOL0 = true; }
            }
            return send.BOOL0;
        }

        public void Brake()
        {
            brake = true;
            //if (WorkThread != null && WorkThread.ThreadState == ThreadState.Running) { brake=true; }
            //if (TimerThread != null && TimerThread.ThreadState == ThreadState.Running) { brake = true; Sport_v.Clear(); Sport_a.Clear(); }
        }

        private void DisplayData(string str)
        {
            if (DataText != null) { DataText.Text = str.ToString(); }
            else { Console.WriteLine(str); }
        }
        private void DisplayPrompt(string str)
        {
            if (PromptMessage != null) { PromptMessage.Text = str.ToString(); }
            else { Console.WriteLine(str); }
        }

        
        private int Sigmoid(double a, double b)
        {
            return (a > b) ? 1 : -1;
        }

        
        public void Start()
        {
            if (TimerThread == null || (WorkThread != null && WorkThread.ThreadState == ThreadState.Running))
            {
                TimerThread = new Thread(new ThreadStart(SensorThread));
                TimerThread.Start();
                ReceiveThread=new Thread(new ThreadStart(ReceiveData));
                ReceiveThread.Start();
            }
            else { 
                Sport_a.Clear();
                Sport_v.Clear();
                brake = false;
            }
        }
        
        public void Stop()
        {            
            if (TimerThread!=null && TimerThread.ThreadState == ThreadState.Running) { TimerThread.Abort();}
            if (WorkThread!= null && WorkThread.ThreadState == ThreadState.Running) { WorkThread.Abort();}
            Sport_x.Clear();
            Sport_v.Clear();
        }

        private void IntegralA2X()
        {
            SportStatus Vlimit = new SportStatus(Sport_v, Sport_vl);
            SportStatus Xlimit = new SportStatus(Sport_x, Sport_xl);
           //Console.WriteLine("{0}\n{1}\n", Vlimit.value_y, Xlimit.value_y);
            Sport_v.PITCH = A2V(Vlimit.value_pitch,  Sport_a.PITCH, Sport_v.PITCH, Sport_x.PITCH,
                Sport_vl.PITCH, Sport_vmax.PITCH);
            RealVX PITCH = V2X(Xlimit.value_pitch, Sport_a.PITCH, Sport_v.PITCH, Sport_x.PITCH, Sport_xl.PITCH, Sport_xmax.PITCH);
            Sport_vReal.PITCH = PITCH.v;
            Sport_x.PITCH= PITCH.x;
            if(PITCH.bool0) Sport_v.PITCH = PITCH.v;

            Sport_v.YAW = A2V(Vlimit.value_yaw, Sport_a.YAW, Sport_v.YAW, Sport_x.YAW,Sport_vl.YAW, Sport_vmax.YAW);
            RealVX YAW = V2X(Xlimit.value_yaw, Sport_a.YAW, Sport_v.YAW, Sport_x.YAW, Sport_xl.YAW, Sport_xmax.YAW);
            Sport_vReal.YAW = YAW.v;
            Sport_x.YAW = YAW.x;
            if (YAW.bool0) Sport_v.YAW = YAW.v;

            Sport_v.ROLL = A2V(Vlimit.value_roll, Sport_a.ROLL, Sport_v.ROLL, Sport_x.ROLL, Sport_vl.ROLL, Sport_vmax.ROLL);
            RealVX ROLL = V2X(Xlimit.value_roll, Sport_a.ROLL, Sport_v.ROLL, Sport_x.ROLL, Sport_xl.ROLL, Sport_xmax.ROLL);
            Sport_vReal.ROLL = ROLL.v;
            Sport_x.ROLL = ROLL.x;
            if (ROLL.bool0) Sport_v.ROLL = ROLL.v;

            Sport_v.X = A2V(Vlimit.value_x, Sport_a.X, Sport_v.X, Sport_x.X, Sport_vl.X, Sport_vmax.X);
            RealVX X = V2X(Xlimit.value_x, Sport_a.X, Sport_v.X, Sport_x.X, Sport_xl.X, Sport_xmax.X);
            Sport_vReal.X = X.v;
            Sport_x.X = X.x;
            if (X.bool0) Sport_v.X = X.v;

            Sport_v.Y = A2V(Vlimit.value_y, Sport_a.Y, Sport_v.Y, Sport_x.Y, Sport_vl.Y, Sport_vmax.Y);
            RealVX Y = V2X(Xlimit.value_y, Sport_a.Y, Sport_v.Y, Sport_x.Y, Sport_xl.Y, Sport_xmax.Y);
            Sport_vReal.Y = Y.v;
            Sport_x.Y = Y.x;
            if (Y.bool0) Sport_v.Y = Y.v;

            Sport_v.Z = A2V(Vlimit.value_z, Sport_a.Z, Sport_v.Z, Sport_x.Z, Sport_vl.Z, Sport_vmax.Z);
            RealVX Z = V2X(Xlimit.value_z, Sport_a.Z, Sport_v.Z, Sport_x.Z, Sport_xl.Z, Sport_xmax.Z);
            Sport_vReal.Z = Z.v;
            Sport_x.Z = Z.x;
            if (Z.bool0) Sport_v.Z = Z.v;
            Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n", Xlimit.value_z, Sport_a.Z, Sport_v.Z, Sport_vReal.Z, Sport_x.Z);
        }
        private double A2V(int limitv,  double _a, double _v, double _x, double _vl, double _vmax)
        {
            _v += _a * T * 0.001;
            double K = _vmax - _vl;
            double _T = 2 / (K);
            
            if (limitv > 0 && _v > 0 )
            {
                    _v = K * (2.0 / (1.0 + System.Math.Exp(-_T * (System.Math.Abs(_v) - _vl))) - 1.0) + _vl;
            }
            else if (limitv < 0 && _v < 0 )
            {     
                    _v = -K * (2.0 / (1.0 + System.Math.Exp(-_T * (System.Math.Abs(_v) - _vl))) - 1.0) - _vl;
            }

            return _v;
        }
        private RealVX V2X(int limitx, double _a, double _v, double _x, double _xl, double _xmax)
        {
            RealVX vx = new RealVX();
            vx.x = _v * T * 0.001+_x;
            double K = _xmax - _xl;
            double _T = 2 / (K);
            vx.v = (vx.x - _x) / (T * 0.001);
            if (limitx > 0 && _v > 0 )
            {
                
                
                vx.x = K * (2.0 / (1.0 + System.Math.Exp(-_T * (System.Math.Abs(vx.x) - _xl))) - 1.0) + _xl;
                vx.v = (vx.x - _x) / (T * 0.001);

                if (_a<0 && vx.v<0)
                {
                    vx.bool0 = true;
                }


            }
            else if (limitx < 0 && _v < 0)
            {
                vx.x = -K * (2.0 / (1.0 + System.Math.Exp(-_T * (System.Math.Abs(vx.x) - _xl))) - 1.0) - _xl;
                vx.v = (vx.x - _x) / (T * 0.001);
                if (_a > 0 && vx.v>0)
                {
                    vx.bool0 = true;
                }

            }

            return vx;
        }

    }
    // 机器人位置点
    public class RobotTarget
    {
        public double X, Y, Z, ROLL, YAW, PITCH,L;
        public bool BOOL0=false;

        public RobotTarget(double x =0, double y =0, double z =0, double roll =0, double pitch =0, double yaw =0, double l =0)
        {
            X = x;Y = y;Z = z;ROLL = roll;PITCH = pitch;YAW = yaw; L = l;
        }
        public void Clear()
        { X = 0; Y = 0; Z = 0; ROLL = 0; PITCH = 0; YAW = 0; L = 0; }
    }
    public class SportStatus
    {

        public int value_pitch, value_roll, value_yaw, value_x, value_y, value_z;
        public SportStatus(RobotTarget x, RobotTarget xl )
        {
            if (x.PITCH > xl.PITCH) value_pitch = 1;
            else if (x.PITCH < -xl.PITCH) value_pitch = -1;
            else value_pitch = 0;

            if (x.YAW > xl.YAW) value_yaw = 1;
            else if (x.YAW < -xl.YAW) value_yaw = -1;
            else value_yaw = 0;

            if (x.ROLL > xl.ROLL) value_roll = 1;
            else if (x.ROLL < -xl.ROLL) value_roll = -1;
            else value_roll = 0;

            if (x.X > xl.X) value_x = 1;
            else if (x.X < -xl.X) value_x = -1;
            else value_x = 0;

            if (x.Y > xl.Y) value_y = 1;
            else if (x.Y < -xl.Y) value_y = -1;
            else value_y = 0;

            if (x.Z > xl.Z) value_z = 1;
            else if (x.Z < -xl.Z) value_z = -1;
            else value_z = 0;

        }
    }
    public class RealVX
    {
        public double v, x;
        public bool bool0=false;
    }





}
// 判断停机点
//if (System.Math.Abs(Receive.PITCH - Stopposition.PITCH) < 1 && System.Math.Abs(Receive.YAW - Stopposition.YAW) < 1 &&
//    System.Math.Abs(Receive.ROLL - Stopposition.ROLL) < 1 && System.Math.Abs(Receive.X - Stopposition.X) < 5 &&
//    System.Math.Abs(Receive.Y - Stopposition.Y) < 5 && System.Math.Abs(Receive.Z - Stopposition.Z) < 5) Stopposition.BOOL0 = true;
//else Stopposition.BOOL0 = false;