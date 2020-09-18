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
    public partial class Graphics : Form
    {
        Form1 F1;
        public Graphics(Form1 F)
        {
            F1 = F;
            InitializeComponent();
            foreach (One_spend_categories item in F1.categories)
            {
                int price = 0;
                for (int i = 0; i < F1.spends.Count; i++)
                {
                    if (F1.spends[i].Categories == item.Name)
                    {
                        price += F1.spends[i].Price;
                    }
                }
                chart2.Series[0].Points.AddXY(item.Name,price);                
            }

            foreach (var item in F1.persons)
            {
                comboBox1.Items.Add(item.Name);
            }

            foreach (var item in F1.categories)
            {
                comboBox2.Items.Add(item.Name);
            }

        }

        private void chart1_Click(object sender, EventArgs e)
        {
            //chart1.Series.Clear();
        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart3.Series.Clear();

            chart1.Series.Add("First");
            chart3.Series.Add("First");
            if (comboBox1.Text != "" && comboBox1.Text != " ")
            {
                if (comboBox2.Text != "" && comboBox2.Text != " ")
                {                    
                    chart1.Titles[0].Text = "Плательщик " + comboBox1.Text;
                    foreach (One_spend item in F1.spends)
                    {
                        if (item.Person == comboBox1.Text)
                        {
                            if (item.Categories == comboBox2.Text)
                            {
                                chart1.Series[0].Points.AddXY(item.Date.ToString(), item.Price);
                            }
                        }
                        
                    }

                    chart3.Titles[0].Text = "Плательщик " + comboBox1.Text+ " за текущие категории";
                    foreach (One_spend_categories item in F1.categories)
                    {
                        int money= 0;
                        foreach (One_spend sp in F1.spends)
                        {
                            if (sp.Person == comboBox1.Text)
                            {
                                if (item.Name == sp.Categories)
                                {
                                    money += sp.Price;
                                }                                
                            }
                        }
                        chart3.Series[0].Points.AddXY(item.Name, money);
                    }

                }
            }
        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
