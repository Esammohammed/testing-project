using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tqa
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char carrent_state1, current_state2;
            List<char> current_state = new List<char>();
          
            if (textBox1.Text == "")
                current_state.Add('n');
            else
                current_state.Add('y');
            if (textBox2.Text == "")
                current_state.Add('n');
            else
                current_state.Add('y');
            
            //searh in rules to find output
            int i;
            bool th=true;
            for ( i = 1; i < Form1.rules[1].Count; i++)
            {
                 th = true;
                for (int g = 1; g < Form1.rules.Count - Form1.Act_num; g++)
                {
                    if (Form1.rules[g][i][0] == '%')
                        continue;
                    if (Form1.rules[g][i][0]!= current_state[g-1])
                    {
                        th = false;
                    }
                }
                if (th)
                { 
                    break; 
                }
            }
            if (th)
                if (Form1.actions[i-1] == "t")
                    MessageBox.Show("welcome ");
            else
                {
                    MessageBox.Show("invalid information");
                }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
