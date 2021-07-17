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
    public partial class Form1 : Form
    {
        List<List<string>> comp = new List<List<string>>();

        static List<string> GetCombinations(string[][] A, string[] B)
        {
            var C = new List<string>();
            var sb = new StringBuilder();

            for (int x = 0; x < B.Length; x++)
            {

                GetNextLevel(A, B, x, 0, sb, C);


            }

            return C;
        }
        static List<string> makedash(List<string> l)
        {

            return l;
        }
        public string s = "                               ";
        static void GetNextLevel(string[][] A, string[] B, int x, int y, StringBuilder sb, List<string> C)
        {
            if (y < A.Length)
            {
                if (B[x][y] == '1')
                {

                    foreach (string t in A[y])
                    {
                        sb.Append(t);
                     
                        GetNextLevel(A, B, x, y + 1, sb, C);

                        sb.Length = (sb.Length) - 1;
                    }
                }
                else
                {
                    GetNextLevel(A, B, x, y + 1, sb, C);


                }

            }
            else
            {

                C.Add(sb.ToString());
            }
        }
        public static List<string> actions = new List<string>();

        List<string> Comp_list = new List<string>();
        List<List<string>> conditions_and_value = new List<List<string>>();// may be map of list
        

        List<List<string>> value = new List<List<string>>();

        Dictionary<int, int> reduccol = new Dictionary<int, int>();

        public static List<List<string>> rules = new List<List<string>>();

        public static int Act_num = 0;
        public static int numberof_conditions = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void load(object sender, EventArgs e)
        {


        }

        private void r(object sender, EventArgs e)
        {
            conditions_and_value.Add(new List<string>());
            value.Add(new List<string>());
            conditions_and_value[numberof_conditions].Add(textBox2.Text);
            numberof_conditions++;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (int.Parse(textBox1.Text.ToString()) > conditions_and_value.Count)
            {
                MessageBox.Show("choose valid condition");
            }
            else
            {
                conditions_and_value[int.Parse(textBox1.Text.ToString()) - 1].Add(textBox3.Text);
                value[int.Parse(textBox1.Text.ToString()) - 1].Add(textBox3.Text);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {


        }

        private void button5_Click(object sender, EventArgs e)
        {



        }

        private void button6_Click(object sender, EventArgs e)
        {
            rules.Add(new List<string>());
            rules[rules.Count - 1].Add(textBox4.Text);
            Act_num++;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // for red
            for (int i = 0; i < numberof_conditions; i++)
            {
                for (int j = 0; j < numberof_conditions; j++)
                {
                    value[i].Add("%");
                }
            }
            int num_of_rules = 1;
            for (int i = 0; i < conditions_and_value.Count; i++)
            {
                num_of_rules *= conditions_and_value[i].Count - 1;
            }

            int Repeating_Factor = num_of_rules;
            int chosen_value = 1;
            rules.Add(new List<string>());
            rules[0].Add("");

           for (int i = 1; i <= conditions_and_value.Count; i++)
            {
                chosen_value = 1;
                rules.Add(new List<string>());
                rules[i].Add(conditions_and_value[i - 1][0]);

                Repeating_Factor /= (conditions_and_value[i - 1].Count - 1);
                for (int j = 1, test = 1; j <= num_of_rules; j++, test++)
                {
                    //check if test is bigger than Repeating_Factor so choose another value 
                    if (test > Repeating_Factor)
                    {
                        chosen_value++;
                        test = 1;
                    }
                    // if chosen index bigger than list boundary 
                    // chosen index must be first index

                    if (chosen_value > conditions_and_value[i - 1].Count - 1)
                    {
                        chosen_value = 1;
                    }
                    //rules 1base  conditions_and_value //0 
                    rules[i].Add(conditions_and_value[i - 1][chosen_value]);

                }

            } 
          /*  for (int i = 0; i < rules.Count; i++)
            {

                for (int j = 0; j < rules[i].Count; j++)
                {
                    Console.Write(rules[i][j] + " ");
                }
                Console.Write("\n");
            }*/
        }

        private void button5_Click_1(object sender, EventArgs e)
        {

            DataTable t_rules = new DataTable();
            DataTable actions = new DataTable();
            t_rules.Clear();
            t_rules.Columns.Add(" ");
            actions.Columns.Add(" ");

            for (int i = 1, indexafterreduc = 1; i < rules[1].Count; indexafterreduc++, i++)
            {
                int p = 0;
                if (reduccol.TryGetValue(i, out p))
                {
                    indexafterreduc--;
                    continue;
                }
                t_rules.Columns.Add("rule" + indexafterreduc);
                actions.Columns.Add("action" + indexafterreduc);
            }

            for (int i = 1; i <= numberof_conditions; i++)
            {
                DataRow row = t_rules.NewRow();
                for (int j = 0, indexafterreduc = 0; j < rules[1].Count; indexafterreduc++, j++)
                {
                    int p = 0;
                    if (reduccol.TryGetValue(j, out p))
                    {
                        indexafterreduc--;
                        continue;
                    }

                    row[indexafterreduc] = rules[i][j];

                }
                t_rules.Rows.Add(row);
            }
            for (int i = numberof_conditions +1; i < numberof_conditions + 1 + Act_num; i++)
            {
                DataRow roww = actions.NewRow();
                roww[0] = rules[i][0];
                for (int j = 1, indexafterreduc = 1; j < rules[1].Count; indexafterreduc++, j++)
                {
                    int p = 0;
                    if (reduccol.TryGetValue(j, out p))
                    {
                        indexafterreduc--;
                        continue;
                    }
                    if (j >= rules[i].Count)
                        roww[indexafterreduc] = " ";
                    else
                        roww[indexafterreduc] = rules[i][j];
                }
                actions.Rows.Add(roww);
            }
            dataGridView1.DataSource = t_rules;
            dataGridView2.DataSource = actions;
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            DataTable dtt = new DataTable();
            dtt = (DataTable)(dataGridView2.DataSource);
            int i = numberof_conditions + 1;
            foreach (DataRow row in dtt.Rows)
            {
                int j = 0;
                foreach (string item in row.ItemArray)
                {
                    if (j == 0)
                    {
                        j++;
                        continue;
                    }
                    rules[i].Add(item);
                    j++;
                }
                i++;
            }
            //save actions in string 

            int start_string = numberof_conditions + 1;
            for (int y = 1; y < rules[1].Count; y++)
            {
                actions.Add(rules[start_string][y]);
                for (int j = numberof_conditions + 2; j < numberof_conditions + 1 + Act_num; j++)
                {
                    actions[y - 1] += rules[j][y];
                }

            }

        }
        // reduction
        private void button4_Click_1(object sender, EventArgs e)
        {
            int N = value.Count;
            var A = new string[N][];
            string[,] h = new string[3, 0];

            for (int i = 0; i < value.Count; i++)
            {
                string[] ss = new string[value[i].Count];
                for (int j = 0; j < value[i].Count; j++)
                {
                   ss[j] = value[i][j];
                }
                A[i] = ss;
            }
            int max = (int)Math.Pow(2, N) - 1;
            string[] B = Enumerable.Range(1, max).
                         Select(i => Convert.ToString(i, 2).PadLeft(N, '0')).
                         OrderBy(s => s.ToCharArray().Count(c => c == '1')).
                         Select(s => new string(s.Reverse().ToArray())).
                         ToArray();


            List<string> C = GetCombinations(A, B);
            makedash(C);




            // extract valid compinations 
            for (int i = 0; i < C.Count; i++)
            {
                if (C[i].Length == numberof_conditions)
                {
                    if (!Comp_list.Contains(C[i]))
                       Comp_list.Add(C[i]);
                   
                }
            }

            Comp_list.Sort();
          
          
           // Comp_list.Reverse();
           // iterate on all compination
            for (int i = 0; i < Comp_list.Count - 1; i++)
            {
                List<int> l = new List<int>();
                bool equality_of_all_action = true;
                bool first_action_for_pattern = false;
                string first = "";
                //traverse rules column by column 
                for (int j = 1; j < rules[1].Count; j++)
                {
                    /*if column has been reduced dont check it */
                    int basiccolm;
                    if (reduccol.TryGetValue(j, out basiccolm))
                        continue;
                    bool equal = true;
                    // check on column
                    for (int g = 1; g < rules.Count - Act_num; g++)
                    {

                        if (Comp_list[i][g - 1] == '%')
                            continue;
                        if (Comp_list[i][g - 1] != rules[g][j][0])
                        {
                            equal = false;
                            break;
                        }

                    }
                    if (equal)
                    {
                        if (!first_action_for_pattern)
                        {
                            first = actions[j - 1];
                            first_action_for_pattern = true;
                        }
                        if (actions[j - 1] == first)
                        {
                            l.Add(j);
                        }
                        else
                        {
                            equality_of_all_action = false;
                            break;
                        }
                    }
                    if (!equality_of_all_action)
                        break;
                }
                if ((!equality_of_all_action) || l.Count < 2)
                    continue;

                // put reduction colom in map to skip them on display 
                for (int p = 1; p < l.Count; p++)
                {
                    reduccol.Add(l[p], 1);
                }
                // update basic colom 
                // make colom equal to pattern ex -n ;
                for (int gg = 1; gg < rules.Count - Act_num; gg++)
                {
                    int col = l[0];
                    rules[gg][col] = Comp_list[i][gg - 1].ToString();

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}

