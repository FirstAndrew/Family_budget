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
    public class One_spend_categories
    {
        string name = "no_name";
        string notion = "no_notion";

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