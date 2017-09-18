using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;

using System.Windows.Forms;

namespace PM2016
{
    public partial class AffinityForm : Form
    {

        public IntPtr AffinityNum
        {
            get;
            set;
        }

        public AffinityForm(IntPtr AffNum)
        {
            this.AffinityNum = AffNum;
            InitializeComponent();
        }

        private void AffinityForm_Load(object sender, EventArgs e)
        {
            this.checkedListBox1.Items.Add("<모든 프로세서>");
            this.checkedListBox1.Items.Add("CPU 0");
            this.checkedListBox1.Items.Add("CPU 1");
            this.checkedListBox1.Items.Add("CPU 2");
            this.checkedListBox1.Items.Add("CPU 3");

            int count = 0;

            if ( (int)AffinityNum % 2 == 1)
            {
                this.checkedListBox1.SetItemChecked(1, true);
                count++;
            }
            if ( (int)AffinityNum % 4 == 2 || (int)AffinityNum % 4 == 3 ) 
            {
                this.checkedListBox1.SetItemChecked(2, true);
                count++;
            }
            if ( (int)AffinityNum % 8 == 4 || (int)AffinityNum % 8 == 5 || (int)AffinityNum % 8 == 6 || (int)AffinityNum % 8 == 7)
            { 
                this.checkedListBox1.SetItemChecked(3, true);
                count++;
            }
            if ((int)AffinityNum >= 8)
            {
                this.checkedListBox1.SetItemChecked(4, true);
                count++;
            }
            if(count == 4)
            {
                this.checkedListBox1.SetItemChecked(0, true);
            }

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = checkedListBox1.SelectedIndex;
            if(index == 0)
            {
                if(checkedListBox1.GetItemChecked(0))
                {
                    this.checkedListBox1.SetItemChecked(1, true);
                    this.checkedListBox1.SetItemChecked(2, true);
                    this.checkedListBox1.SetItemChecked(3, true);
                    this.checkedListBox1.SetItemChecked(4, true);
                }
                else
                {
                    this.checkedListBox1.SetItemChecked(1, false);
                    this.checkedListBox1.SetItemChecked(2, false);
                    this.checkedListBox1.SetItemChecked(3, false);
                    this.checkedListBox1.SetItemChecked(4, false);
                }
            }

            if(checkedListBox1.GetItemChecked(1) && checkedListBox1.GetItemChecked(2) && checkedListBox1.GetItemChecked(3) && checkedListBox1.GetItemChecked(4))
            {
                this.checkedListBox1.SetItemChecked(0, true);
            }
            else
            {
                this.checkedListBox1.SetItemChecked(0, false);
            }
            if (checkedListBox1.GetItemChecked(1) || checkedListBox1.GetItemChecked(2) || checkedListBox1.GetItemChecked(3) || checkedListBox1.GetItemChecked(4))
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tempAff = 0;
            AffinityNum = (IntPtr)0;
            if (checkedListBox1.GetItemChecked(0))
            {
                AffinityNum = (IntPtr)15;
            }
            else
            {
                if (checkedListBox1.GetItemChecked(1))
                {
                    tempAff = tempAff + 1;
                }
                if (checkedListBox1.GetItemChecked(2))
                {
                    tempAff = tempAff + 2;
                }
                if (checkedListBox1.GetItemChecked(3))
                {
                    tempAff = tempAff + 4;
                }
                if (checkedListBox1.GetItemChecked(4))
                {
                    tempAff = tempAff + 8;
                }
                AffinityNum = (IntPtr)tempAff;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
