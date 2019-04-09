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
    public partial class Form2 : Form
    {
        public Form1 f1;
        public static string Str = @"Data Source=.; Database = mydatabase; Integrated Security=true";
        public int choice;
        public int InOutStatus;
        public Form2()
        {
            InitializeComponent();
            choice = 0;
            InOutStatus = 0;
            f1 = Program.f1;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            choice = 3;
            Inquire();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            choice = 1;
            Inquire();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            choice = 2;
            Inquire();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            choice = 0;
            Inquire();
        }

        //查询库存
        private void Inquire()
        {
            string sql = "kucun_proc";
            SqlParameter[] p=
            {
                new SqlParameter("@choice", SqlDbType.Int){Value=choice},
                new SqlParameter("@cangku",SqlDbType.Char,5){Value=textBox1.Text},
                new SqlParameter("@huowu",SqlDbType.Char,5){Value=textBox2.Text}
            };
            MySqlFunc(p, sql);
        }

        //通用方法
        private void MySqlFunc(SqlParameter[] p,string sql)
        {
            SqlConnection thisConnect = new SqlConnection(Str);
            thisConnect.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, thisConnect);
            sda.SelectCommand.CommandType = CommandType.StoredProcedure;           
            sda.SelectCommand.Parameters.AddRange(p);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            this.dataGridView1.DataSource = dt;
            thisConnect.Close();
        }
        //查询货物
        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "huowu_proc";
            SqlParameter[] p =
            {
                new SqlParameter("@status", SqlDbType.Int){Value=1},
                new SqlParameter("@huowu",SqlDbType.Char,5){Value=textBox4.Text}
            };
            MySqlFunc(p, sql);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "huowu_proc";
            SqlParameter[] p =
            {
                new SqlParameter("@status", SqlDbType.Int){Value=0},
                new SqlParameter("@huowu",SqlDbType.Char,5){Value=textBox4.Text}
            };
            MySqlFunc(p, sql);
        }

        //查询所有出入库信息--按钮ALL
        private void button7_Click(object sender, EventArgs e)
        {
            choice = 0;
            this.comboBox1.Text = "";
        }

        //入库单号查询
        private void button9_Click(object sender, EventArgs e)
        {
            if (this.comboBox2.Text.Equals("入库")) InOutStatus = 1;
            else InOutStatus = 0;
            choice=1;
            this.comboBox1.Text = "";
            comBoxShow();
        }

        //将结果放入comboBox供选择
        private void comBoxShow()
        {
            string sql = "selec_proc";
            SqlConnection thisConnect = new SqlConnection(Str);  
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@in", InOutStatus);
            s.Parameters.AddWithValue("@status", choice);
            thisConnect.Open();
            SqlDataReader sr = s.ExecuteReader();
            this.comboBox1.Items.Clear();
            while (sr.Read())
            {
                this.comboBox1.Items.Add(sr[0].ToString());
            }
            thisConnect.Close();
        }

        //最终查询出入库按钮1
        private void button12_Click(object sender, EventArgs e)
        {
            if (this.comboBox2.Text.Equals("入库")) InOutStatus = 1;
            else InOutStatus = 0;
            string sql = "InOutStatus_proc";
            SqlParameter[] p =
            {
                new SqlParameter("@in",SqlDbType.Int){Value=InOutStatus},
                new SqlParameter("@status",SqlDbType.Int){Value=choice},
                new SqlParameter("@para",SqlDbType.VarChar,50){Value=comboBox1.Text}
            };
            MySqlFunc(p, sql);
        }
        //仓库查询
        private void button10_Click(object sender, EventArgs e)
        {
            if (this.comboBox2.Text.Equals("入库")) InOutStatus = 1;
            else InOutStatus = 0;
            choice=2;
            this.comboBox1.Text = "";
            comBoxShow();
        }

        //按时间
        private void button11_Click(object sender, EventArgs e)
        {
            if (this.comboBox2.Text.Equals("入库")) InOutStatus = 1;
            else InOutStatus = 0;
            choice = 3;
            this.comboBox1.Text = "";
            comBoxShow();
        }

        //全部借还记录
        private void button8_Click(object sender, EventArgs e)
        {
            choice = 0;
            this.comboBox3.Text = "";
        }

        //查询借还记录的最终按钮
        private void button16_Click(object sender, EventArgs e)
        {
            string sql = "selectJieH_proc";
            SqlParameter[] p =
            {
               new SqlParameter("@jie",SqlDbType.Int){Value=choice},
                new SqlParameter("@para",SqlDbType.VarChar,10){Value=this.comboBox3.Text}
            };
            MySqlFunc(p, sql);
        }

        //借条号查询
        private void button17_Click(object sender, EventArgs e)
        {
            choice = 2;
            this.comboBox3.Text = "";
        }
        //按仓库查借还记录
        private void button13_Click(object sender, EventArgs e)
        {
            choice = 1;
            this.comboBox3.Text = "";
        }
        //借出时间查找借还记录
        private void button14_Click(object sender, EventArgs e)
        {
            choice = 3;
            this.comboBox3.Text = "";
            string sql = "select distinct CONVERT(varchar(4),Year(借出日期))+'年'+convert(varchar(2),Month(借出日期))+'月'+ convert(varchar(2),Day(借出日期) )+'日 ' from 借还		";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);     
            thisConnect.Open();
            SqlDataReader sr = s.ExecuteReader();
            this.comboBox3.Items.Clear();
            while (sr.Read())
            {
                this.comboBox3.Items.Add(sr[0].ToString());
            }
            thisConnect.Close();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            choice = 4;
            this.comboBox3.Text = "";
        }
        
        //入库提交按钮
        private void button22_Click(object sender, EventArgs e)
        {
            string sql = "in_proc";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@cangku", this.textBox3.Text);
            s.Parameters.AddWithValue("@huowu", this.textBox6.Text);
            s.Parameters.AddWithValue("@num", this.comboBox5.Text);
            s.Parameters.AddWithValue("@admin", f1.textBox1.Text);
            SqlParameter par = s.Parameters.Add("@status", SqlDbType.Int);　　//定义输出参数
            par.Direction = ParameterDirection.Output;　　//参数类型为Output
            thisConnect.Open();
            s.ExecuteNonQuery();
            if ((int)par.Value == 0) MessageBox.Show("数量超过默认设置，请先修改设置再入库！");
            if ((int)par.Value == 1) MessageBox.Show("入库失败！入库量过大！");
            if ((int)par.Value == 2) MessageBox.Show("入库成功！");
            if ((int)par.Value == 3) MessageBox.Show("无此仓库！");
            if ((int)par.Value == 4) MessageBox.Show("无此编号的货物！");
            thisConnect.Close();            
        }

        //出库提交按钮
        private void button18_Click(object sender, EventArgs e)
        {
            string sql = "out_proc";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@cangku", this.textBox8.Text);   
            s.Parameters.AddWithValue("@huowu", this.textBox10.Text);
            s.Parameters.AddWithValue("@num", this.textBox11.Text);
            s.Parameters.AddWithValue("@kehu", this.textBox12.Text);
            s.Parameters.AddWithValue("@admin", f1.textBox1.Text);
            SqlParameter par = s.Parameters.Add("@status", SqlDbType.Int);　　//定义输出参数
            par.Direction = ParameterDirection.Output;　　//参数类型为Output
            thisConnect.Open();
            s.ExecuteNonQuery();
            if ((int)par.Value == 1) MessageBox.Show("货物不在所选仓库！");
            if ((int)par.Value == 2) MessageBox.Show("库存不足！");
            if ((int)par.Value == 3) MessageBox.Show("出库成功！");
            if ((int)par.Value == 4) MessageBox.Show("无此仓库！");
            thisConnect.Close();            
        }

        //借出
        private void button19_Click(object sender, EventArgs e)
        {
            string sql = "jiechu_proc";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@cangku", this.textBox14.Text);
            s.Parameters.AddWithValue("@huowu", this.textBox15.Text);
            s.Parameters.AddWithValue("@num", this.textBox16.Text);
            s.Parameters.AddWithValue("@person", this.textBox17.Text);
            s.Parameters.AddWithValue("@admin", f1.textBox1.Text);
            SqlParameter par = s.Parameters.Add("@status1", SqlDbType.Int);
            par.Direction = ParameterDirection.Output;
            SqlParameter par1 = s.Parameters.Add("@No", SqlDbType.VarChar,10);　　//定义输出参数
            par1.Direction = ParameterDirection.Output;　　//参数类型为Output
            thisConnect.Open();
            s.ExecuteNonQuery();
            if ((int)par.Value == 1) MessageBox.Show("货物不在所选仓库！");
            if ((int)par.Value == 2) MessageBox.Show("库存不足！");
            if ((int)par.Value == 3)
            {
                MessageBox.Show("借出成功！");
                this.label2.Text = par1.Value.ToString();
            }
            if ((int)par.Value == 4) MessageBox.Show("无此仓库！");
            thisConnect.Close();            
        }

        //重置
        private void button23_Click(object sender, EventArgs e)
        {
            this.label2.Text = "                  ";
            this.textBox14.Text = this.textBox15.Text = this.textBox16.Text = this.textBox17.Text = "";
        }

        //查询
        private void button21_Click(object sender, EventArgs e)
        {
            choice = 4;
            string sql = "selectJieH_proc";
            SqlParameter[] p =
            {
               new SqlParameter("@jie",SqlDbType.Int){Value=choice},
                new SqlParameter("@para",SqlDbType.VarChar,10){Value=this.comboBox4.Text}
            };
            MySqlFunc(p, sql);
        }

        //确认归还
        private void button20_Click(object sender, EventArgs e)
        {
            string sql = "guihuan_proc";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@No", this.comboBox4.Text);
            s.Parameters.AddWithValue("@admin", f1.textBox1.Text);//测试
            SqlParameter par = s.Parameters.Add("@status", SqlDbType.Int);
            par.Direction = ParameterDirection.Output;
            thisConnect.Open();
            s.ExecuteNonQuery();
            if ((int)par.Value == 1) MessageBox.Show("容量不足，归还失败！");
            if ((int)par.Value == 3) MessageBox.Show("此借条无法归还！");
            if ((int)par.Value == 2)
            {
                MessageBox.Show("归还成功！");
                this.comboBox4.Text = "";
            }
        }
        //确认货物管理的按钮
        private void button24_Click(object sender, EventArgs e)
        {
            if (this.comboBox7.Text.Equals("添加")) choice = 2;
            else if (this.comboBox7.Text.Equals("删除")) choice = 1;
            string sql = "SelecHuowu_proc";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@choice", choice);
            s.Parameters.AddWithValue("@huowu", this.textBox5.Text);
            s.Parameters.AddWithValue("@name", this.textBox7.Text);
            s.Parameters.AddWithValue("@type", textBox9.Text);
            s.Parameters.AddWithValue("@gongying", comboBox6.Text);
            SqlParameter par = s.Parameters.Add("@status", SqlDbType.Int);
            par.Direction = ParameterDirection.Output;
            thisConnect.Open();
            s.ExecuteNonQuery();
            if ((int)par.Value == 0) MessageBox.Show("货物已存在！");
            if ((int)par.Value == 1) MessageBox.Show("添加成功！");
            if ((int)par.Value == 2) MessageBox.Show("删除成功！");
            if ((int)par.Value == 3) MessageBox.Show("请选择操作！");              
        }
        private void tabControl3_Selecting(object sender, TabControlCancelEventArgs e)
        {
            this.panel1.Hide();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            this.panel1.Show();
            choice = 1;
            this.textBox13.Text = this.textBox18.Text = this.textBox19.Text = "";
        }

        //确认
        private void button27_Click(object sender, EventArgs e)
        {
            string sql = "admin_proc";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@choice", choice);
            s.Parameters.AddWithValue("@num", this.textBox13.Text);
            s.Parameters.AddWithValue("@name", this.textBox18.Text);
            s.Parameters.AddWithValue("@pws", this.textBox19.Text);
            SqlParameter par = s.Parameters.Add("@status", SqlDbType.Int);
            par.Direction = ParameterDirection.Output;
            thisConnect.Open();
            s.ExecuteNonQuery();
            if ((int)par.Value == 0) MessageBox.Show("添加成功！");
            if ((int)par.Value == 1) MessageBox.Show("修改成功！");
            if ((int)par.Value == 2) MessageBox.Show("删除成功！");
            if ((int)par.Value == 3) MessageBox.Show("操作无效！");
            if ((int)par.Value == 4) MessageBox.Show("默认管理员禁止删除！");  
            this.panel1.Hide();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            this.panel1.Show();
            choice = 2;
            this.textBox13.Text = this.textBox18.Text = this.textBox19.Text = "";
        }

        private void button30_Click(object sender, EventArgs e)
        {
            this.panel1.Show();
            choice = 3;
            this.textBox13.Text = this.textBox18.Text = this.textBox19.Text = "";
        }

        //查询
        private void button26_Click(object sender, EventArgs e)
        {
            string sql = "select * from 管理员";
            SqlConnection thisConnect = new SqlConnection(Str);
            thisConnect.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, thisConnect);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            this.dataGridView1.DataSource = dt;
            thisConnect.Close();
        }

        //查看设置
        private void button31_Click(object sender, EventArgs e)
        {
            string sql = "select * from 库存设置";
            SqlConnection thisConnect = new SqlConnection(Str);
            thisConnect.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, thisConnect);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            this.dataGridView1.DataSource = dt;
            thisConnect.Close();
        }

        //修改库存设置
        private void button32_Click(object sender, EventArgs e)
        {
            this.panel2.Show();
            string cangku = "select distinct 仓库编号 from 库存设置";
            string huowu = "select distinct 货物编号 from 库存设置";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s1 = new SqlCommand(cangku, thisConnect);    
            thisConnect.Open();
            SqlDataReader sr1 = s1.ExecuteReader();
            this.comboBox8.Items.Clear();
            this.comboBox9.Items.Clear();
            while (sr1.Read())
            {
                this.comboBox8.Items.Add(sr1[0].ToString());
            }
            thisConnect.Close();
            SqlCommand s2 = new SqlCommand(huowu, thisConnect);
            thisConnect.Open();
            SqlDataReader sr2 = s2.ExecuteReader();
            while (sr2.Read())
            {
                this.comboBox9.Items.Add(sr2[0].ToString());
            }
            thisConnect.Close();
        }

        private void tabControl3_Selected(object sender, TabControlEventArgs e)
        {
            this.panel2.Hide();
        }

        //重置
        private void button25_Click(object sender, EventArgs e)
        {
            this.comboBox7.Text = this.textBox5.Text = this.textBox7.Text = this.textBox9.Text = "";
        }

        //修改设置确认按钮
        private void button33_Click(object sender, EventArgs e)
        {
            string sql = "alterkucun_proc";
            SqlConnection thisConnect = new SqlConnection(Str);
            SqlCommand s = new SqlCommand(sql, thisConnect);
            s.CommandType = CommandType.StoredProcedure;
            s.Parameters.AddWithValue("@cangku", this.comboBox8.Text);
            s.Parameters.AddWithValue("@huowu", this.comboBox9.Text);
            s.Parameters.AddWithValue("@min", this.comboBox10.Text);
            s.Parameters.AddWithValue("@max", this.comboBox11.Text);
            SqlParameter par = s.Parameters.Add("@status", SqlDbType.Int);
            par.Direction = ParameterDirection.Output;
            thisConnect.Open();
            s.ExecuteNonQuery();
            if ((int)par.Value == 0) MessageBox.Show("修改成功！");
            if ((int)par.Value == 1) MessageBox.Show("请选择仓库和货物！");      
            this.panel1.Hide();
        }
        //注销
        private void button35_Click(object sender, EventArgs e)
        {
            f1.Show();
            this.Close();
        }

        //退出
        private void button34_Click(object sender, EventArgs e)
        {
            this.Close();
            f1.Close();
        }

        //仓库信息
        private void button36_Click(object sender, EventArgs e)
        {
            string sql = "select * from 仓库";
            SqlConnection thisConnect = new SqlConnection(Str);
            thisConnect.Open();
            SqlDataAdapter sda = new SqlDataAdapter(sql, thisConnect);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            this.dataGridView1.DataSource = dt;
            thisConnect.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            f1.Close();
        }








        











       
    }
}
