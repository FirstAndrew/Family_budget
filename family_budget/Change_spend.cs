using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace family_budget
{
    public partial class Change_spend : Form
    {
        Form1 F1;
        One_spend spend;
        int pos;
        public Change_spend(Form1 F, One_spend spend,int pos)
        {
            F1 = F;
            this.spend = spend;
            this.pos = pos;
            InitializeComponent();

            foreach (var item in F1.persons)
            {
                comboBox1.Items.Add(item.Name);
            }

            foreach (var item in F1.categories)
            {
                comboBox2.Items.Add(item.Name);
            }

            textBox1.Text = spend.ID.ToString();
            textBox2.Text = spend.Notion;
            textBox3.Text = spend.Price.ToString();
            textBox4.Text = spend.Name;
            comboBox1.Text = spend.Person;
            comboBox2.Text = spend.Categories;
            dateTimePicker1.Value = spend.Date;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            One_spend new_spend = new One_spend();

            new_spend.ID = spend.ID;
            new_spend.Notion = textBox2.Text;
            new_spend.Price = int.Parse(textBox3.Text);
            new_spend.Name = textBox4.Text;
            new_spend.Person = comboBox1.Text;
            new_spend.Categories = comboBox2.Text;
            new_spend.Date = dateTimePicker1.Value;

            F1.spends.RemoveAt(pos);
            F1.spends.Insert(pos, new_spend);
            F1.refresh();
        }

    }
}
