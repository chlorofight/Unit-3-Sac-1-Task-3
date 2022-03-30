
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Unit_3_Sac_1_Task_3
{
    public partial class Form1 : Form
    {
        //Unit 3 Sac 1 Task 3 , Leonardo Bini, 24/03/2022
        string filter;
        bool ASC = true;
        List<Sale> sales = new List<Sale>();
        BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
            bs.DataSource = sales;
            dataGridView1.DataSource = bs;
        }

        //Function: Open a file browser
        //Input: NA
        //Output: Function main, message with error, message if no file selected
        private void openFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //only run function if file selected
                    main(ofd.FileName);
                }
                //if the file browser is closed
                else if (ofd.ShowDialog() == DialogResult.Cancel)
                {
                    //make sure that no file is selected
                    MessageBox.Show("No file selected");
                }
            }
            //if the file is open return error
            catch(System.IO.IOException)
            {
                MessageBox.Show("File Cannot Be Open", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Not valid", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Function: read and add the lines in the file to objects
        //Input: string, temp
        //Output:string[], List<Sale>, returns the list of the file that was opened
        private void main(string temp) 
        {
            List<string> lines = new List<string>();
            lines = File.ReadAllLines(temp).ToList();
            foreach (string line in lines)
            {
                List<string> item = line.Split(',').ToList();
                Sale s = new Sale();
                s.Textbook = item[0];
                s.Subject = item[1];
                s.Seller = item[2];
                s.Purchaser = item[3];
                try
                {
                    s.PurchasePrice = float.Parse(item[4]);
                    s.SalePrice = item[5];
                    s.Rating = item[6];
                }
                catch
                {
                    Console.WriteLine("Parse Fail");       
                }
                sales.Add(s);

            }
            //add to the binding source outside of the fucntion as to not overload the program
            bs.DataSource = sales;
            bs.ResetBindings(false);
        }



        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFile();
        }


        //Function: sort the list that is given
        //Input: List<sales> tempList, a list under a class
        //OutPut: The sorted version of the inputed list
        private void selectionSort(List<Sale> tempList)
        {
            int min;
            string temp;
            for (int i = 0; i < tempList.Count; i++)
            {
                min = i;
                for (int j = i + 1; j < tempList.Count; j++)
                {
                    if (int.TryParse(tempList[j].Rating, out int rating))
                    {
                        if (int.TryParse(tempList[min].Rating, out int ratingMin))
                        {
                            if (rating < ratingMin)
                            {
                                min = j;
                            }
                        }
                        else
                        {
                            min = j;
                        }
                    }
                }
                temp = tempList[min].Rating;
                tempList[min].Rating = tempList[i].Rating;
                tempList[i].Rating = temp;
            }

            //if the user wants to sort asc then reverse the list
            if (!ASC)
            {
                tempList.Reverse();
            }
        }

        //Check what the user wants to search by
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter = cmbFilter.Text;
            if (filter == "Rating")
            { 
                selectionSort(sales); 
            }
            dataGridView1.DataSource = bs;
            bs.ResetBindings(false);
        }

        //check to see if ASC or DESC for rating
        private void cmbAsc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbAsc.Text == "ASC")
            {
                //can refactor code here to make it a var in the selection sort function if needed
                ASC = true;
                selectionSort(sales);
            }
            if(cmbAsc.Text == "DESC")
            {
                ASC = false;
                selectionSort(sales);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            List<Sale> s = Search(txtSearch.Text, filter);
            bs.DataSource = s;
            dataGridView1.DataSource = s;
            bs.ResetBindings(false);
        }

        //Function: remove parts of a list to sort it
        //Input: string target, string filter, Use to choose what is getting selected
        //Output: returns new list.
        private List<Sale> Search(string target, string filter)
        {
            List<Sale> results = new List<Sale>();
            foreach (Sale s in sales)
            {
                if (filter == "Rating")
                {
                    if (s.Rating.ToLower() == target.ToLower()) results.Add(s);
                }
                if (filter == "Textbook")
                {
                    if (s.Textbook.ToLower().Contains(target.ToLower())) 
                    {
                        results.Add(s); 
                    }
                }

                if (filter == "Subject")
                {
                    if (s.Subject.ToLower().Contains(target.ToLower())) 
                    { 
                        results.Add(s);
                    }
                }

            }
            return results;
        }
    }
}
