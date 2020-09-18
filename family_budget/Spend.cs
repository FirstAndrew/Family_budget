using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace family_budget
{
    public partial class Spend : Form
    {
        Form1 F1;
        public Spend(Form1 F)
        {
            InitializeComponent();
            F1 = F;
            F1.new_id += 1;
            textBox2.Text = "spend number "+F1.new_id.ToString();
            foreach (var item in F1.categories)
            {
                comboBox1.Items.Add(item.Name);
            }
            foreach (var item in F1.persons)
            {
                comboBox2.Items.Add(item.Name);
            }
            comboBox1.Text = comboBox1.Items[0].ToString();
            comboBox2.Text = comboBox2.Items[0].ToString();
            textBox3.Text = "0";            
        }//тут вносим имеющихся владельцев и категории

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            MatchCollection m = Regex.Matches(textBox3.Text, "[0-9]+", RegexOptions.IgnoreCase);
            One_spend new_spend = new One_spend();
            string p = "";
            foreach (Match match in m)
            {
                p += match;
            }

            if (p=="")
               p = "0";
            new_spend.Price = int.Parse(p);
            new_spend.ID = F1.new_id;
            new_spend.Name = textBox2.Text;
            new_spend.Categories = comboBox1.Text;
            new_spend.Person = comboBox2.Text;
            new_spend.Notion = textBox1.Text;
            new_spend.Date = dateTimePicker1.Value;

            if (new_spend.Name == "" || new_spend.Name == " ")
                new_spend.Name = "No name";
            if (new_spend.Categories == "" || new_spend.Categories == " ")
                new_spend.Categories = "No name";
            if (new_spend.Person == "" || new_spend.Person == " ")
                new_spend.Person = "No name";


            F1.spends.Add(new_spend);
            F1.Add_spends();
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
