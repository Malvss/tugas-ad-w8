using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tugas_AD_baru
{
    public partial class Form1 : Form
    {
       
 
public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connetionstring = null;
            MySqlConnection cnn; //connection ini untuk conect in visual studio dan mysql
            MySqlCommand cmm; // commend untuk query 
            MySqlDataAdapter adp; // untuk baca query nya 
            DataTable dt;
            connetionstring = "server=localhost;database=premier_league;uid=root;pwd=''";
            cnn = new MySqlConnection(connetionstring);
            cmm = new MySqlCommand("select m.manager_id as ID,m.manager_name as Name,concat(if (m.working=0,'Available',if(m.manager_id=t.assmanager_id,'Asisten Manager','Manager')),' ',if(m.working=1,t.team_name,'')) as Status from manager m left join team t on m.manager_id=t.assmanager_id or m.manager_id=t.manager_id where m.delete=0 order by ID asc", cnn);
            adp = new MySqlDataAdapter(cmm);
            dt = new DataTable();

            try

            {
                dt = new DataTable();
                connetionstring = "server=localhost;database=premier_league;uid=root;pwd=''";
                //cnn = new MySqlConnection(connetionstring);
                cmm = new MySqlCommand("select team_id,team_name from team t", cnn);
                adp = new MySqlDataAdapter(cmm);
                cnn.Open();
                adp.Fill(dt);
                toolStripComboBox1.ComboBox.DataSource = dt;
                toolStripComboBox1.ComboBox.DisplayMember = "team_name";
                toolStripComboBox1.ComboBox.ValueMember = "team_id";
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection !");
            }
            cnn.Close();
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {
            string connetionstring = null;
            MySqlConnection cnn; //connection ini untuk conect in visual studio dan mysql
            MySqlCommand cmm; // commend untuk query 
            MySqlDataAdapter adp; // untuk baca query nya 
            DataTable dt;
            try

            {

                dt = new DataTable();
                connetionstring = "server=localhost;database=premier_league;uid=root;pwd=''";
                cnn = new MySqlConnection(connetionstring);
                cmm = new MySqlCommand("select player_id,player_name from team t,player p where p.team_id=t.team_id and t.team_id=('" + toolStripComboBox1.ComboBox.SelectedValue + "')", cnn);
                adp = new MySqlDataAdapter(cmm);
                cnn.Open();
                adp.Fill(dt);
                toolStripComboBox2.ComboBox.DataSource = dt;
                toolStripComboBox2.ComboBox.DisplayMember = "player_name";
                toolStripComboBox2.ComboBox.ValueMember = "player_id";
                cnn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection !");
            }
        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {
            string connetionstring = null;
            MySqlConnection cnn; //connection ini untuk conect in visual studio dan mysql
            MySqlCommand cmm; // commend untuk query 
            MySqlDataAdapter adp; // untuk baca query nya 
            DataTable dt;
            try

            {

                dt = new DataTable();
                connetionstring = "server=localhost;database=premier_league;uid=root;pwd=''";
                cnn = new MySqlConnection(connetionstring);
                cmm = new MySqlCommand("select p.player_name,t.team_name,n.nation,p.playing_pos,sum(if (d.type='CY',1,0)) as 'cy',sum(if(d.type='CR',1,0)) as'cr',sum(if(d.type='GO',1,0)) as 'go', sum(if(d.type='PM',1,0)) as 'pm'from player p,team t,dmatch d,nationality n where p.team_id=t.team_id and d.player_id=p.player_id and p.nationality_id=n.nationality_id and d.team_id=t.team_id and p.player_id=('" + toolStripComboBox2.ComboBox.SelectedValue + "');", cnn);
                adp = new MySqlDataAdapter(cmm);
                cnn.Open();
                adp.Fill(dt);
                dataGridView1.DataSource = dt;
                cnn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection !");
            }
        }
    }
}
