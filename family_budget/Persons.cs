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
    public partial class Persons : Form
    {
        Form1 F1;
        int last_selected_index = 0;
        public Persons(Form1 F)
        {
            InitializeComponent();
            F1 = F;
            foreach (var item in F1.persons)
            {
                comboBox1.Items.Add(item.Name);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox2.Text = F1.persons[comboBox1.SelectedIndex].Notion;
            textBox1.Text = "";
            last_selected_index = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                foreach (One_person item in F1.persons)
                {
                    if (item.Name == textBox1.Text)
                    {
                        MessageBox.Show(
                          "Такое имя уже существует",
                          "Обратите внимание",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button1);
                        return;
                    }
                }

                One_person pers = new One_person();
                pers.Name = textBox1.Text;
                pers.Notion = textBox2.Text;
                comboBox1.Items.Add(textBox1.Text);
                F1.Add_person(pers);
            }            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count>1)
            {
                F1.persons.RemoveAt(comboBox1.SelectedIndex);
                F1.Dell_person(comboBox1.SelectedIndex);
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex); 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                //foreach (One_person item in F1.persons)
                //{
                //    if (item.Name == comboBox1.Text)
                //    {
                //        MessageBox.Show(
                //          "Такое имя уже существует",
                //          "Обратите внимание",
                //           MessageBoxButtons.OK,
                //           MessageBoxIcon.Information,
                //           MessageBoxDefaultButton.Button1,
                //           MessageBoxOptions.DefaultDesktopOnly);
                //        return;
                //    }
                //}

                string chname = F1.persons[last_selected_index].Name;

                

                One_person pers = new One_person();
                pers.Name = comboBox1.Text;
                pers.Notion = textBox2.Text;

                F1.persons.RemoveAt(last_selected_index);
                F1.Dell_person(last_selected_index);
                comboBox1.Items.RemoveAt(last_selected_index);
                comboBox1.Text = pers.Name;
                comboBox1.Items.Add(pers.Name);
                F1.Add_person(pers);

                for (int i = 0; i < F1.spends.Count; i++)
                {                    
                    if (F1.spends[i].Person == chname)
                    {
                        F1.spends[i].Person = comboBox1.Text;
                    }
                    
                }//заменить ответственных на новых
                F1.refresh();
            }
        }//изменить Имя

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Text = "";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label4.Text = "0.0";// всего 
            label9.Text = "0.0";// день
            label7.Text = "0.0";// месяц
            label5.Text = "0.0";// последний месяц

            if (last_selected_index > -1)
            {
                int summprice = 0;
                int dayprice = 0;
                int monthrice = 0;
                int lmonthrice = 0;                
                for (int i = 0; i < F1.spends.Count; i++)
                {
                    if (F1.spends[i].Person == comboBox1.Text)
                    {
                        summprice  += F1.spends[i].Price;//общие траты
                        label4.Text = summprice.ToString();
                    }
                }

                for (int i = 0; i < F1.spends.Count; i++)
                {
                    if (F1.spends[i].Person == comboBox1.Text)
                    {
                        string selectdate  = dateTimePicker1.Value.ToString();
                        string finddate = F1.spends[i].Date.ToString();//10 цифр
                        char[] arrseldat = selectdate.ToCharArray();
                        char[] arrfindat = finddate.ToCharArray();
                        for (int j = 0; j < 10; j++)
                        {
                            if (arrseldat[j] != arrfindat[j])
                            {
                                goto exit1;
                            }
                        }
                        dayprice += F1.spends[i].Price;//общие траты выбранного дня
                        label9.Text = dayprice.ToString();
                    exit1:;
                    }
                }
                


                for (int i = 0; i < F1.spends.Count; i++)
                {
                    if (F1.spends[i].Person == comboBox1.Text)
                    {
                        string selectdate = dateTimePicker1.Value.ToString();
                        string finddate = F1.spends[i].Date.ToString();//8 знаков
                        char[] arrseldat = selectdate.ToCharArray();
                        char[] arrfindat = finddate.ToCharArray();
                        for (int j = 2; j < 10; j++)
                        {
                            if (arrseldat[j] != arrfindat[j])
                            {
                                goto exit2;
                            }
                        }
                        monthrice += F1.spends[i].Price;//общие траты выбранного месяца
                        label7.Text = monthrice.ToString();
                    exit2:;
                    }
                }
                

                for (int i = 0; i < F1.spends.Count; i++)
                {
                    if (F1.spends[i].Person == comboBox1.Text)
                    {
                        string selectdate = DateTime.Now.ToString();
                        string finddate = F1.spends[i].Date.ToString();//8 знаков
                        char[] arrseldat = selectdate.ToCharArray();
                        char[] arrfindat = finddate.ToCharArray();
                        for (int j = 2; j < 10; j++)
                        {
                            if (arrseldat[j] != arrfindat[j])
                            {
                                goto exit3;
                            }
                        }
                        lmonthrice += F1.spends[i].Price;//общие траты последнего месяца
                        label5.Text = lmonthrice.ToString();
                        exit3:;
                    }
                }
              

                //label5.Text =;//прошлый месяц

            }
            
        }
    }
}
