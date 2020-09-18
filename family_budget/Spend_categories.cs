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
    public partial class Spend_categories : Form
    {
        Form1 F1;
        int last_selected_index = 0;
        public Spend_categories(Form1 F)
        {
            InitializeComponent();
            F1 = F;
            foreach (var item in F1.categories)
            {
                comboBox1.Items.Add(item.Name);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = F1.categories[comboBox1.SelectedIndex].Notion;
            textBox1.Text = "";
            last_selected_index = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                foreach (One_spend_categories item in F1.categories)
                {
                    if (item.Name == textBox1.Text)
                    {
                        MessageBox.Show(
                          "Такая категория уже существует",
                          "Обратите внимание",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button1);
                           
                        return;
                    }
                }

                One_spend_categories category = new One_spend_categories();
                category.Name = textBox1.Text;
                category.Notion = textBox2.Text;
                comboBox1.Items.Add(textBox1.Text);
                F1.Add_category(category);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 1)
            {
                F1.categories.RemoveAt(comboBox1.SelectedIndex);
                F1.Dell_category(comboBox1.SelectedIndex);
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                foreach (One_spend_categories item in F1.categories)
                {
                    //if (item.Name == comboBox1.Text)
                    //{
                    //    MessageBox.Show(
                    //      "Такая категория уже существует",
                    //      "Обратите внимание",
                    //       MessageBoxButtons.OK,
                    //       MessageBoxIcon.Information,
                    //       MessageBoxDefaultButton.Button1,
                    //      
                    //    return;
                    //}
                }

                string chcaregor = F1.categories[last_selected_index].Name;

                F1.categories.RemoveAt(last_selected_index);
                F1.Dell_category(last_selected_index);
                comboBox1.Items.RemoveAt(last_selected_index);

                One_spend_categories categ = new One_spend_categories();
                categ.Name = comboBox1.Text;
                categ.Notion = textBox2.Text;
                comboBox1.Items.Add(comboBox1.Text);
                F1.Add_category(categ);

                for (int i = 0; i < F1.spends.Count; i++)
                {
                    if (F1.spends[i].Categories == chcaregor)
                    {
                        F1.spends[i].Categories = comboBox1.Text;                       
                    }
                    
                }//заменить ответственных на новых
                F1.refresh();
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
