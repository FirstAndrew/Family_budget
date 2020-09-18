using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace family_budget
{
    public partial class Form1 : Form
    {
        public List<One_person> persons = new List<One_person>();
        public List<One_spend> spends = new List<One_spend>();
        public List<One_spend_categories> categories = new List<One_spend_categories>();
        public int new_id = 0;
        int active_in = -1;

        public Form1()
        {
            One_person first = new One_person();
            One_spend_categories first_categories = new One_spend_categories();

            first_categories.Name = "General";
            first_categories.Notion = "Default categories spends";
            first.Name = "Owner";
            first.Notion = "Default Owner";

            persons.Add(first);
            categories.Add(first_categories);

            InitializeComponent();
            comboBox1.Items.Add(first.Name);
            comboBox2.Items.Add(first_categories.Name);
        }

        public void Add_spends() 
        {    
            ListViewItem item = new ListViewItem(spends[spends.Count - 1].ID.ToString());
            item.SubItems.Add(spends[spends.Count-1].Person);
            item.SubItems.Add(spends[spends.Count-1].Name);
            item.SubItems.Add(spends[spends.Count-1].Date.ToString());
            item.SubItems.Add(spends[spends.Count-1].Categories);            
            item.SubItems.Add(spends[spends.Count-1].Price.ToString());            
            listView1.Items.Add(item);            
        }

        public void Add_spends(One_spend spend)
        {
            ListViewItem item = new ListViewItem(spend.ID.ToString());
            item.SubItems.Add(spend.Person);
            item.SubItems.Add(spend.Name);
            item.SubItems.Add(spend.Date.ToString());
            item.SubItems.Add(spend.Categories);
            item.SubItems.Add(spend.Price.ToString());
            listView1.Items.Add(item);
        }
        public void refresh() 
        {
            listView1.Items.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            for (int i = 0; i < spends.Count; i++)
            {
                Add_spends(spends[i]);
            }
            for (int i = 0; i < categories.Count; i++)
            {
                comboBox2.Items.Add(categories[i].Name);
            }
            for (int i = 0; i < persons.Count; i++)
            {
                comboBox1.Items.Add(persons[i].Name);
            }
        }

        public int[] Sort_by_id(int id)
        {
            int[] spposbyid = new int[spends.Count];//ключ формирования порядка
            int j = 0;//идти с начала 
            int e = spends.Count - 1;//идти с конца

            for (int i = 0; i < spends.Count; i++)
            {
                if (id != spends[i].ID)
                {
                    spposbyid[e] = spends[i].ID;
                    e--;
                    goto exit2;
                }
                spposbyid[j] = spends[i].ID;
                j++;

            exit2:;
            }
            return spposbyid;
        }


        public int[] Sort_by_date(string name)
        {
            int[] spposbydate = new int[spends.Count];
            int j = 0;
            int e = spends.Count - 1;

            string selectdate = dateTimePicker1.Value.ToString();
            char[] arrseldat = selectdate.ToCharArray();

            for (int i = 0; i < spends.Count; i++)
            {                
                string finddate = spends[i].Date.ToString();//8 знаков                
                char[] arrfindat = finddate.ToCharArray();

                for (int v = 0; v < 10; v++)
                {
                    if (arrseldat[v] != arrfindat[v])
                    {
                        spposbydate[e] = spends[i].ID;
                        e--;
                        goto exit2;
                    }
                }

                spposbydate[j] = spends[i].ID;
                j++;
            exit2:;
            }
            return spposbydate;
        }

        public int[] Sort_by_categories(string name)
        {
            int[] spposbycatego = new int[spends.Count];
            int j = 0;
            int e = spends.Count - 1;
            for (int i = 0; i < spends.Count; i++)
            {
                if (spends[i].Categories == name)
                {
                    spposbycatego[j] = spends[i].ID;
                    j++;
                }
                else
                {
                    spposbycatego[e] = spends[i].ID;
                    e--;
                }
            }
            return spposbycatego;
        }



        public int[] Sort_by_person(string name) 
        {
            int[] spposbyname = new int[spends.Count];
            int j = 0;
            int e = spends.Count-1;
            for (int i = 0; i < spends.Count; i++)
            {
                if (spends[i].Person == name)
                {
                    spposbyname[j] = spends[i].ID;
                    j++;
                }
                else 
                {
                    spposbyname[e] = spends[i].ID;
                    e--;
                }
            }
            return spposbyname;
        }
        static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }
        public int[] Sort_by_ID()
        {
            int[] spposbyid = new int[spends.Count];
            short flag = 0;
            int i = 0;

            for (int j = 0; j < spends.Count; j++)
            {
                spposbyid[j] = spends[j].ID;
            }

            while (flag != 2)
            {
                if (i == spends.Count - 1)
                {
                    flag = 2;
                    continue;
                }
                if (spposbyid[i] > spposbyid[i + 1])
                {
                    Swap(ref spposbyid[i], ref spposbyid[i + 1]);
                    i = 0;
                }
                i++;
                
            }
            return spposbyid;
        }

        public void Add_person(One_person pers)
        {
            comboBox1.Items.Add(pers.Name);
            persons.Add(pers);
        }

        public void Add_category(One_spend_categories categ)
        {
            comboBox2.Items.Add(categ.Name);
            categories.Add(categ);
        }

        public void Dell_category(int index)
        {
            comboBox2.Items.RemoveAt(index);            
        }
        public void Dell_person(int index)
        {
            comboBox1.Items.RemoveAt(index);
        }


        private void родственникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Persons person = new Persons(this);
            person.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[]rightpos = Sort_by_person(comboBox1.Text);
            listView1.Items.Clear();
            for (int i = 0; i < spends.Count; i++)
            {
                for (int j = 0; j < spends.Count; j++)
                {
                    if (spends[j].ID == rightpos[i])
                    {
                        Add_spends(spends[j]);
                    }
                }                
            }            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Spend spend = new Spend(this);
            spend.Show(this);  //открыть окно с полями, передать в него объект этой формы
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection sic = listView1.SelectedIndices; //колекция выбранных индесков

            foreach (int item in sic)
                active_in = item; //определяет активный индекс клиента, важный элемент управления             

            if (sic.Count == 0)
                textBox2.Text = "";
            else
            {
                One_spend to_change = spends[active_in];
                Change_spend spend = new Change_spend(this, to_change, active_in);
                spend.Show(this);  //открыть окно с полями, передать в него объект этой формы
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            ListView.SelectedListViewItemCollection ic = listView1.SelectedItems;//колекция выбранных элементов
            ListView.SelectedIndexCollection sic = listView1.SelectedIndices; //колекция выбранных индесков

            foreach (int item in sic)            
                active_in = item; //определяет активный индекс клиента, важный элемент управления             

            if (sic.Count == 0)
                textBox2.Text = "";
            else
            {
                textBox2.Text = spends[active_in].Notion;
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            ListView.SelectedIndexCollection sic = listView1.SelectedIndices;
        }

        private void новыйРасходToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Spend spend = new Spend(this);
            spend.Show(this);  //открыть окно с полями, передать в него объект этой формы

        }

        private void Люди(object sender, EventArgs e)
        {

        }

        private void убратьРодственникаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Persons person = new Persons(this);
            person.Show();
        }

        private void новаяКатегорияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spend_categories category = new Spend_categories(this);
            category.Show();
        }

        private void убратьКатегориюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Spend_categories category = new Spend_categories(this);
            category.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int[] rightpos = Sort_by_categories(comboBox2.Text);
            listView1.Items.Clear();
            for (int i = 0; i < spends.Count; i++)
            {
                for (int j = 0; j < spends.Count; j++)
                {
                    if (spends[j].ID == rightpos[i])
                    {
                        Add_spends(spends[j]);
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (spends.Count>=active_in-1)
            {
                spends[active_in].Notion = textBox2.Text;
            }
        }

        private void убратьРасходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.CheckedIndexCollection chekindex = listView1.CheckedIndices;
            if (chekindex.Count < 1)
            {
                MessageBox.Show(
                          "1. В поле \"ID\" Отметьте галочкой ненавистные платежи\n\n2. Нажмите кнопку \"Убрать расход\"",
                          "Для удаления",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button1,
                           MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                int i = 0;
                foreach (int item in chekindex)
                {
                    spends.RemoveAt(item - i);
                    listView1.Items.RemoveAt(item - i);
                    i++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] rightpos = Sort_by_ID();
            listView1.Items.Clear();
            for (int i = 0; i < spends.Count; i++)
            {
                for (int j = 0; j < spends.Count; j++)
                {
                    if (spends[j].ID == rightpos[i])
                    {
                        Add_spends(spends[j]);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection sic = listView1.SelectedIndices; //колекция выбранных индесков

            foreach (int item in sic)
                active_in = item; //определяет активный индекс клиента, важный элемент управления             

            if (sic.Count == 0)
                textBox2.Text = "";
            else
            {
                One_spend to_change = spends[active_in];
                Change_spend spend = new Change_spend(this, to_change, active_in);
                spend.Show(this);  //открыть окно с полями, передать в него объект этой формы
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = "0";
            if (spends.Count>0)
            {
                int monthrice = 0;
                for (int i = 0; i < spends.Count; i++)
                {
                    string selectdate = dateTimePicker1.Value.ToString();
                    string finddate = spends[i].Date.ToString();//8 знаков
                    char[] arrseldat = selectdate.ToCharArray();
                    char[] arrfindat = finddate.ToCharArray();
                    for (int j = 2; j < 10; j++)
                    {
                        if (arrseldat[j] != arrfindat[j])
                        {
                            goto exit2;
                        }
                    }
                    monthrice += spends[i].Price;//общие траты выбранного месяца
                    label2.Text = monthrice.ToString();
                    exit2:;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] rightpos = Sort_by_date(comboBox2.Text);
            listView1.Items.Clear();
            for (int i = 0; i < spends.Count; i++)
            {
                for (int j = 0; j < spends.Count; j++)
                {
                    if (spends[j].ID == rightpos[i])
                    {
                        Add_spends(spends[j]);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (spends.Count>1)
            {
                MatchCollection m = Regex.Matches(textBox1.Text, "[0-9]+", RegexOptions.IgnoreCase);
                string p = "";
                foreach (Match match in m)
                {
                    p += match;
                }
                if (p != "")
                {
                    int[] rightpos = Sort_by_id(int.Parse(p));
                    listView1.Items.Clear();
                    for (int i = 0; i < spends.Count; i++)
                    {
                        for (int j = 0; j < spends.Count; j++)
                        {
                            if (spends[j].ID == rightpos[i])
                            {
                                Add_spends(spends[j]);
                            }
                        }
                    }
                }
            }
        }//

        private void коррекцияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection sic = listView1.SelectedIndices; //колекция выбранных индесков

            foreach (int item in sic)
                active_in = item; //определяет активный индекс клиента, важный элемент управления             

            if (sic.Count == 0)
                textBox2.Text = "";
            else
            {
                One_spend to_change = spends[active_in];
                Change_spend spend = new Change_spend(this, to_change, active_in);
                spend.Show(this);  //открыть окно с полями, передать в него объект этой формы
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // класс для записи XML
            XmlWriter writer;

            // настройки записи
            XmlWriterSettings settings = new XmlWriterSettings();

            // перенос на новые строки
            settings.Indent = true;

            // символы перехода на следующую строку
            settings.NewLineChars = "\r\n";
            settings.ConformanceLevel = ConformanceLevel.Auto;

            // кодировка
            settings.Encoding = Encoding.ASCII;

            // переход на новую строку для атрибутов
            settings.NewLineOnAttributes = false;

            writer = XmlWriter.Create("Our_family_budget" + ".xml", settings);

            writer.WriteStartDocument();

            writer.WriteStartElement("Collection_of_" + spends.Count.ToString() + "_spends");
            {
                for (int i = 0; i < spends.Count; i++)
                {
                    if (spends[i].Name == "" || spends[i].Name == " ")
                        spends[i].Name = "no name";
                    if (spends[i].Categories == "" || spends[i].Categories == " ")
                        spends[i].Categories = "no Categories";
                    if (spends[i].Person == "" || spends[i].Person == " ")
                        spends[i].Person = "no Person";
                    if (spends[i].Notion == "" || spends[i].Notion == " ")
                        spends[i].Notion = "nothing";


                    writer.WriteStartElement("Spend"); 
                    
                    writer.WriteElementString("ID", spends[i].ID.ToString());                    
                    writer.WriteElementString("name", spends[i].Name);                   
                    writer.WriteElementString("categories", spends[i].Categories);                                      
                    writer.WriteElementString("person", spends[i].Person);                    
                    writer.WriteElementString("date", spends[i].Date.ToString());                    
                    writer.WriteElementString("notion", spends[i].Notion);  
                    writer.WriteElementString("price", spends[i].Price.ToString());

                    writer.WriteEndElement();
                }
            }

            writer.WriteStartElement("Collection_of_" + persons.Count.ToString() + "_persons");
            {
                for (int i = 0; i < persons.Count; i++)
                {
                    writer.WriteStartElement("_person");                    
                    writer.WriteElementString("name", persons[i].Name.ToString());
                    writer.WriteElementString("notion", persons[i].ToString());                    
                    writer.WriteEndElement();
                }
            }

            writer.WriteStartElement("Collection_of_" + categories.Count.ToString() + "_categories");
            {
                for (int i = 0; i < categories.Count; i++)
                {
                    writer.WriteStartElement("_category");                    
                    writer.WriteElementString("name", categories[i].Name.ToString());
                    writer.WriteElementString("notion", categories[i].Notion.ToString());                    
                    writer.WriteEndElement();
                }
            }

            writer.WriteStartElement("_end");
            writer.WriteElementString("_end", "1488_8841");            
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteEndElement();

            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();

            MessageBox.Show(
              "готово",
              "сохранение завершего",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information,
               MessageBoxDefaultButton.Button1);
               //MessageBoxOptions.DefaultDesktopOnly);
            
        }//сохранение 

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            string path = "";

            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.FileName;
            }
            else
            {
                return;
            }

            XmlReader reader;

            categories.Clear();
            persons.Clear();
            spends.Clear();

            // открытие существующего файла
            reader = XmlReader.Create(path);
            {
                One_person pers = new One_person();
                One_spend spend = new One_spend();
                One_spend_categories categ = new One_spend_categories();
                short pass = 0;
                int i=0;

                while (reader.Read())
                {
                    pers = new One_person();
                    spend = new One_spend();
                    categ = new One_spend_categories();


                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Spend")
                    {
                        while (reader.Read()||pass == 0)
                        {
                            if (pass>0)
                            {
                                break;
                            }

                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "_person")
                            { 
                                pass = 1;
                                break;
                            }

                            if (i > 6)
                            {
                                i = 0;
                                Add_spends(spend);
                                spends.Add(spend);
                                
                                spend = new One_spend();                                
                            }

                            if (reader.NodeType == XmlNodeType.Text)
                            {
                                if (reader.Value != ""&& reader.Value != " ")
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            spend.ID = int.Parse(reader.Value);
                                            i++;
                                            break;
                                        case 1:
                                            spend.Name = reader.Value;
                                            i++;
                                            break;
                                        case 2:
                                            spend.Categories = reader.Value;
                                            i++;
                                            break;
                                        case 3:
                                            spend.Person = reader.Value;
                                            i++;
                                            break;
                                        case 4:
                                            DateTime dateTimeStart = DateTime.Parse(reader.Value);
                                            spend.Date = dateTimeStart;
                                            i++;
                                            break;
                                        case 5:
                                            spend.Notion = reader.Value;
                                            i++;
                                            break;
                                        case 6:
                                            spend.Price = int.Parse(reader.Value);
                                            i++;
                                            break;
                                    }

                                }
                            }
                        }
                    }//добавление спендов


                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "_person")// не читает первую
                    {
                        int count = 0;
                        while (reader.Read() || pass == 1)
                        {
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "_category")
                            {
                                pass = 2;
                                break;
                            }
                            if (i > 1)
                            {

                                i = 0;
                                Add_person(pers);
                                pers = new One_person();

                            }
                            if (reader.NodeType == XmlNodeType.Text)
                            {
                                if (reader.Value != "" && reader.Value != " ")
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            pers.Name = reader.Value;
                                            i++;
                                            break;
                                        case 1:
                                            pers.Notion = reader.Value;
                                            i++;
                                            count++;
                                            label5.Text = count.ToString();
                                            break;
                                    }
                                }
                            }
                        };
                    }//добавление персон

                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "_category")
                    {

                        while (reader.Read() || pass == 2)
                        {
                            if (reader.Value == "1488_8841")
                            {
                                pass = 3;
                                break;
                            }

                            if (i > 1)
                            {
                                i = 0;
                                Add_category(categ);
                                categ = new One_spend_categories();

                            }
                            if (reader.NodeType == XmlNodeType.Text)
                            {
                                if (reader.Value != "" && reader.Value != " ")
                                {
                                    switch (i)
                                    {
                                        case 0:
                                            categ.Name = reader.Value;
                                            i++;
                                            break;
                                        case 1:
                                            categ.Notion = reader.Value;
                                            i++;
                                            break;
                                    }
                                }
                            }
                        }
                    }//добавление категорий 
                }
            }
            new_id = spends.Count();
            reader.Close();
           // search_categ();
            refresh();
        }

        private void графикToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics graphics = new Graphics(this);
            graphics.Show();  //открыть окно с полями, передать в него объект этой формы
        }

        public void search_categ() 
        {
            bool flag = false;
            foreach (var item in spends)
            {
                flag = false;
                for (int i = 0; i < categories.Count; i++)
                {
                    if (item.Categories == categories[i].Name)
                    {
                        flag = true;
                    }
                }
                if (flag == false)
                {
                    One_spend_categories newspc = new One_spend_categories();
                    newspc.Name = item.Categories;
                    Add_category(newspc); 
                }
            } 
        }
    }
}
