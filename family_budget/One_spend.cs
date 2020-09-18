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
   public class One_spend
    {
        int id = -0;
        string person = "no_person";
        string name = "no_name";
        DateTime date ;
        string categories = "no_categories";
        string notion = "no_notion";
        int price = 0;

        public int ID
        {
            set
            {
                id = value;
            }
            get
            {
                return id;
            }
        }
        public int Price
        {
            set
            {
                price = value;
            }
            get
            {
                return price;
            }
        }

        public string Person
        {
            set
            {
                person = value;
            }
            get
            {
                return person;
            }
        }

        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }

        public DateTime Date
        {
            set
            {
                date = value;
            }
            get
            {
                return date;
            }
        }

        public string Categories
        {
            set
            {
                categories = value;
            }
            get
            {
                return categories;
            }
        }

        public string Notion
        {
            set
            {
                notion = value;
            }
            get
            {
                return notion;
            }
        }
    }
}