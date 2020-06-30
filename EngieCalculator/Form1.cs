using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace EngieCalculator
{
    public partial class Form1 : Form
    {

        List<Details> Listdetails = new List<Details>();
        //List of nothing
        List<Details> Listdetails1 = new List<Details>();
        string path = @".\MyText.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Bconvertir_Click(object sender, EventArgs e)
        {
            try
            {
                Lkwh.Text = (float.Parse(TmCube.Text) * 11.2).ToString("n2");
            } catch
            {
                MessageBox.Show("Inserez des chiffres s'il vous plait", "Error Engie Calculator");
            }
        }

        private void Bdifference_Click(object sender, EventArgs e)
        {
            try
            {
                Ldifference.Text = (float.Parse(TnouveauM3.Text) - float.Parse(TancienM3.Text)).ToString("n2");
                Lfacture.Text = (float.Parse(Ldifference.Text) *11.2* float.Parse(TprixKWH.Text)).ToString("n2");
            }
            catch
            {

                MessageBox.Show("Inserez des chiffres s'il vous plait", "Error Engie Calculator");
            }
        }


        public void show()
        {
            try
            {

                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    File.CreateText(path);

                }
                else //if (Listdetails.Count > 0)
                {
                    Listdetails.Clear();
                    CurrencyManager cm = (CurrencyManager)this.dataGridView1.BindingContext[Listdetails];
                    if (cm != null)
                    {
                        cm.Refresh();
                    }
                    // Open the file to read from.
                    using (StreamReader sr = File.OpenText(path))
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Details detail = new Details();
                            detail.date = s.Split(',')[0];
                            detail.mCube = s.Split(',')[1];
                            Listdetails.Add(detail);

                        }
                        cm = (CurrencyManager)this.dataGridView1.BindingContext[Listdetails];
                        if (cm != null)
                        {
                            cm.Refresh();
                        }
                        dataGridView1.DataSource = Listdetails;
                        sr.Close();
                    }
                }
            }catch (Exception e) {
            MessageBox.Show(e.ToString(), "Error Engie Calculator");
            }

        }
            


        public void insert()
        {
            
                try
                {
                    if (!File.Exists(path))
                    {
                        // Create a file to write to.
                        File.CreateText(path);
                    }
                    else
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            Details detail = new Details();
                            detail.date = Tdate.Text;
                            detail.mCube = TnouveauM3.Text;
                            Listdetails.Add(detail);
                            
                        foreach (var d in Listdetails)
                            {
                                sw.WriteLine(d.date + ',' + d.mCube);
                            }
                            sw.Close();
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error Engie Calculator");
                }
            
        }

        public void delete()
        {
           

                try
                {

                //removing
                    Listdetails.RemoveAll(detail => detail.date.ToString().Equals(Tdate.Text));
                    //inserting

                    if (!File.Exists(path))
                    {
                        File.CreateText(path);
                    }
                    else
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            foreach (var d in Listdetails)
                            {
                                sw.WriteLine(d.date + ',' + d.mCube);
                            }
                            sw.Close();
                        }

                    }
                    //showing
                    CurrencyManager cm = (CurrencyManager)this.dataGridView1.BindingContext[Listdetails];
                    if (cm != null)
                    {
                        cm.Refresh();
                    }
                    dataGridView1.DataSource = Listdetails;
            }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(), "Error Engie Calculator");
                }
           
        }


        

        private void button1_Click(object sender, EventArgs e)
        {
          
                    show();
        }

    private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(path))
            {
                var myFile = File.Create(path);
                myFile.Close();
            }
            Tdate.Text =  DateTime.Now.Date.ToString("dd/MM/yyyy").Replace('-', '/');
        }
        

        private void Binserer_Click(object sender, EventArgs e)
        {
            if (float.TryParse(TnouveauM3.Text, out _) == true)
            {
                DateTime dateValue;
                string date = Tdate.Text;
                date.Replace('/', '-');
                    bool isdateValue = DateTime.TryParseExact(date, "dd/mm/yyyy",
                                          new CultureInfo("fr-FR"),DateTimeStyles.None,
                                out dateValue);
                    if (isdateValue)
                    {
                        insert();
                        CurrencyManager cm = (CurrencyManager)this.dataGridView1.BindingContext[Listdetails];
                        if (cm != null)
                        {
                            cm.Refresh();
                        }
                    dataGridView1.DataSource = Listdetails;

                }
                    else
                    {
                        MessageBox.Show("Inserez une bonne date ", "Error Engie Calculator");
                    }
                

                
                
            }
            else
            {
                MessageBox.Show("Inserez des chiffres s'il vous plait", "Error Engie Calculator");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
                DateTime dateValue;
                string date = Tdate.Text;
                date.Replace('/', '-');
                bool isdateValue = DateTime.TryParseExact(date, "dd/mm/yyyy",
                                      new CultureInfo("fr-FR"), DateTimeStyles.None,
                            out dateValue);
                if (isdateValue)
                {
                    delete();

                }
                else
                {
                    MessageBox.Show("Inserez une bonne date ", "Error Engie Calculator");
                }
                
            }
            
        }

       
    
}
