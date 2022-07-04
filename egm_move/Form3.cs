using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace egm_move
{
    public partial class Form3 : Form
    {


        public Form3()
        {
            
            InitializeComponent();

            if (Form1.Port_Number == 6511)
            {
                X.Text = Convert.ToString(Form1.robot_set_x_6650);
                Y.Text = Convert.ToString(Form1.robot_set_y_6650); ;
                Z.Text = Convert.ToString(Form1.robot_set_z_6650); ;

                position_rate.Text = Convert.ToString(Form1.Z_speed_6650S);
                posture_rate.Text = Convert.ToString(Form1.Y_speed_6650S);
                L.Text = Convert.ToString(Form1.L_speed);

                X_MAX.Text = Convert.ToString(Form1.robot_6650range_x_max);
                X_MIN.Text = Convert.ToString(Form1.robot_6650range_x_min);
                Y_MAX.Text = Convert.ToString(Form1.robot_6650range_y_max);
                Y_MIN.Text = Convert.ToString(Form1.robot_6650range_y_min);
                Z_MAX.Text = Convert.ToString(Form1.robot_6650range_z_max);
                Z_MIN.Text = Convert.ToString(Form1.robot_6650range_z_min);

                roll_MAX.Text = Convert.ToString(Form1.robot_6650range_roll_max);
                roll_MIN.Text = Convert.ToString(Form1.robot_6650range_roll_min);
                pitch_MAX.Text = Convert.ToString(Form1.robot_6650range_pitch_max);
                pitch_MIN.Text = Convert.ToString(Form1.robot_6650range_pitch_min);
                yaw_MAX.Text = Convert.ToString(Form1.robot_6650range_yaw_max);
                yaw_MIN.Text = Convert.ToString(Form1.robot_6650range_yaw_min);
            }
            else 
            {
                X.Text = Convert.ToString(Form1.robot_set_x_4600);
                Y.Text = Convert.ToString(Form1.robot_set_y_4600); ;
                Z.Text = Convert.ToString(Form1.robot_set_z_4600); ;

                position_rate.Text = Convert.ToString(Form1.Z_speed_4600);
                posture_rate.Text = Convert.ToString(Form1.Y_speed_4600);

                X_MAX.Text = Convert.ToString(Form1.robot_4600range_x_max);
                X_MIN.Text = Convert.ToString(Form1.robot_4600range_x_min);
                Y_MAX.Text = Convert.ToString(Form1.robot_4600range_y_max);
                Y_MIN.Text = Convert.ToString(Form1.robot_4600range_y_min);
                Z_MAX.Text = Convert.ToString(Form1.robot_4600range_z_max);
                Z_MIN.Text = Convert.ToString(Form1.robot_4600range_z_min);

                roll_MAX.Text = Convert.ToString(Form1.robot_4600range_roll_max);
                roll_MIN.Text = Convert.ToString(Form1.robot_4600range_roll_min);
                pitch_MAX.Text = Convert.ToString(Form1.robot_4600range_pitch_max);
                pitch_MIN.Text = Convert.ToString(Form1.robot_4600range_pitch_min);
                yaw_MAX.Text = Convert.ToString(Form1.robot_4600range_yaw_max);
                yaw_MIN.Text = Convert.ToString(Form1.robot_4600range_yaw_min);
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.Port_Number == 6510)
            {
                Form1.Y_speed_4600= Convert.ToDouble(position_rate.Text);
                Form1.Z_speed_4600 = Convert.ToDouble(posture_rate.Text);

                Form1.robot_set_x_4600 = Convert.ToInt32(X.Text);
                Form1.robot_set_y_4600 = Convert.ToInt32(Y.Text);
                Form1.robot_set_z_4600 = Convert.ToInt32(Z.Text);

                Form1.robot_4600range_x_max = Convert.ToInt32(X_MAX.Text); 
                Form1.robot_4600range_y_max = Convert.ToInt32(Y_MAX.Text);
                Form1.robot_4600range_z_max = Convert.ToInt32(Z_MAX.Text);
                Form1.robot_4600range_roll_max = Convert.ToInt32(roll_MAX.Text);
                Form1.robot_4600range_pitch_max = Convert.ToInt32(pitch_MAX.Text);
                Form1.robot_4600range_yaw_max = Convert.ToInt32(yaw_MAX.Text);

                Form1.robot_4600range_x_min = Convert.ToInt32(X_MIN.Text);
                Form1.robot_4600range_y_min = Convert.ToInt32(Y_MIN.Text);
                Form1.robot_4600range_z_min = Convert.ToInt32(Z_MIN.Text);
                Form1.robot_4600range_roll_min = Convert.ToInt32(roll_MIN.Text);
                Form1.robot_4600range_pitch_min = Convert.ToInt32(pitch_MIN.Text);
                Form1.robot_4600range_yaw_min = Convert.ToInt32(yaw_MIN.Text);

            }
            else
            {
                Form1.Y_speed_6650S = Convert.ToDouble(position_rate.Text);
                Form1.Z_speed_6650S = Convert.ToDouble(posture_rate.Text);
                Form1.L_speed = Convert.ToDouble(L.Text);

                Form1.robot_set_x_6650 = Convert.ToInt32(X.Text);
                Form1.robot_set_y_6650 = Convert.ToInt32(Y.Text);
                Form1.robot_set_z_6650 = Convert.ToInt32(Z.Text);

                Form1.robot_6650range_x_max = Convert.ToInt32(X_MAX.Text);
                Form1.robot_6650range_y_max = Convert.ToInt32(Y_MAX.Text);
                Form1.robot_6650range_z_max = Convert.ToInt32(Z_MAX.Text);
                Form1.robot_6650range_roll_max = Convert.ToInt32(roll_MAX.Text);
                Form1.robot_6650range_pitch_max = Convert.ToInt32(pitch_MAX.Text);
                Form1.robot_6650range_yaw_max = Convert.ToInt32(yaw_MAX.Text);

                Form1.robot_6650range_x_min = Convert.ToInt32(X_MIN.Text);
                Form1.robot_6650range_y_min = Convert.ToInt32(Y_MIN.Text);
                Form1.robot_6650range_z_min = Convert.ToInt32(Z_MIN.Text);
                Form1.robot_6650range_roll_min = Convert.ToInt32(roll_MIN.Text);
                Form1.robot_6650range_pitch_min = Convert.ToInt32(pitch_MIN.Text);
                Form1.robot_6650range_yaw_min = Convert.ToInt32(yaw_MIN.Text);

            }
            


                Form1.MyTextBox.Text = "参数设置完成...".ToString();
                this.Close();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.MyTextBox.Text = "取消参数设置...".ToString();
            this.Close();
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Form1.MyTextBox.Text = "取消参数设置...".ToString();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
