using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WareHouseManagSys
{
    public partial class Form1 : Form
    {
        public static string thisStr = @"Data Source=.; Database = mydatabase; Integrated Security=true";

        public Form1()
        {
            InitializeComponent();
        }
        
        //登陆
        private void button2_Click(object sender, EventArgs e)
        {           
            SqlConnection thisConnect = new SqlConnection(thisStr);
            SqlCommand cmd = new SqlCommand("login_proc", thisConnect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@pws", textBox2.Text);
            SqlParameter par = cmd.Parameters.Add("@status", SqlDbType.Int);　　//定义输出参数
            par.Direction = ParameterDirection.Output;　　//参数类型为Output
            thisConnect.Open();
            cmd.ExecuteNonQuery();
            switch ((int)par.Value)
            {
                case 0:
                    //MessageBox.Show("登陆成功！");
                    this.Hide();
                    Form2 f2 =new Form2();
                    f2.Show();
                    break;
                case 1:
                    MessageBox.Show("账号不存在！请重新输入");
                    break;
                case 2:
                    MessageBox.Show("密码错误！请重新输入");
                    break;
            }            
            thisConnect.Close();
            thisConnect.Dispose();
        }
    }
}
