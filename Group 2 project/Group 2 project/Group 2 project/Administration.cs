﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Group_2_project
{
    public partial class Administration : Form
    {
        private MySqlConnection conn;
        public Administration()
        {
            
            InitializeComponent();
            FillEmployeeIds();
            FillDeptIds();
            ConnectToDatabase();
            dShow.DefaultCellStyle.ForeColor = Color.Black;
            dgDepartments.DefaultCellStyle.ForeColor = Color.Black;
     
        }
        public void ConnectToDatabase()
        {
             conn = new MySqlConnection("Persist Security Info=False;database=dbi434661;server=studmysql01.fhict.local;Connect Timeout=30;user id=dbi434661; pwd=daivbot");

        }

        void FillEmployeeIds()
        {
           
            string query = "select * from dbi434661.employee;";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    int eId = reader.GetInt32("FirstName");
                    this.cmEmployeeNames.Items.Add(eId);
                    this.cmeIds.Items.Add(eId);

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        void FillDeptIds()
        {
            {
               
                string query = "select * from dbi434661.departments;";
                MySqlCommand command = new MySqlCommand(query, conn);
                MySqlDataReader reader;

                try
                {
                    conn.Open();
                    reader = command.ExecuteReader();


                    while (reader.Read())
                    {
                        string dpname = reader.GetString("DeptName");
                        this.cmDeptIds.Items.Add(dpname);

                    }
                    conn.Close();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private string GeneratePassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

        private void CreateLoginDetails()
        {
           
            string query2 = $"INSERT INTO `login` (`empId`, `username`, `password`) VALUES ('{this.tbId.Text}', '{tbFn.Text}1', '{GeneratePassword()}')";

            MySqlCommand command2 = new MySqlCommand(query2, conn);

            conn.Open();
            MySqlDataReader reader;
            reader = command2.ExecuteReader();

            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
          
            string query = "insert into dbi434661.employee(ID,Firstname,LastName,Age,Gender,DepartmentName" +
                ",HireDate,EndDate,Salary,Adress,Role)values('" + this.tbId.Text + "','" + this.tbFn.Text + "','" + this.tbSn.Text + "','" + this.tbAge.Text + "','" + this.cmbGender.Text + "','" + this.cmDeptNames.Text + "','" + this.dateTimePicker1.Text + "','"+this.dateTimePicker2.Text+"','" + this.tbSal.Text + "','" + this.tbAdd.Text + "','" + this.cmbRole.Text + "');";
           
            MySqlCommand command = new MySqlCommand(query, conn);
           
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();
               
                
                MessageBox.Show("New Employee added");

                while (reader.Read())
                {

                }
                conn.Close();


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally{
                CreateLoginDetails();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
           
            string query = "update dbi434661.employee set ID='" + this.tbId.Text + "',FirstName='" + this.tbFn.Text + "',LastName='" + this.tbSn.Text + "',Age='" + this.tbAge.Text + "',Gender='" + this.cmbGender.Text+ "',DepartmentName='" + this.cmDeptNames.Text+ "',HireDate='" + this.dateTimePicker1.Text + "',EndDate='"+this.dateTimePicker2.Text+"',Salary='" + this.tbSal.Text + "',Adress='" + this.tbAdd.Text + "',Role='" + this.cmbRole.Text + "' where ID='" + this.tbId.Text + "' ;";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                MessageBox.Show("Employee Details Updated successfully!");

                while (reader.Read())
                {

                }

                conn.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
           
            string query = "delete from dbi434661.employee where ID='" + this.tbId.Text + "';";
            string query2 = "delete from dbi434661.departments where DeptName='" + this.cmDeptNames.Text + "';";
            string query3 = "delete from dbi434661.login where eID='" + this.cmDeptNames.Text + "';";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlCommand command2 = new MySqlCommand(query2, conn);
            MySqlCommand command3 = new MySqlCommand(query3, conn);

            try
            {
                conn.Open();


                

                command.CommandType = CommandType.Text;
                command.ExecuteScalar();



                command2.CommandType = CommandType.Text;
                command2.ExecuteScalar();

                command3.CommandType = CommandType.Text;
                command3.ExecuteScalar();

                MessageBox.Show("Deleted");
                conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



        }

        private void btnSignoutAdmin_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string constring = "Persist Security Info=False;database=dbi434661;server=studmysql01.fhict.local;Connect Timeout=30;user id=dbi434661; pwd=daivbot";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from dbi434661.employee;", conDataBase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDataBase;
                DataTable dbaTableset = new DataTable();
                sda.Fill(dbaTableset);
                BindingSource bSource = new BindingSource();

                bSource.DataSource = dbaTableset;

                dShow.DataSource = bSource;
                sda.Update(dbaTableset);


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnSignoUT_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                string constring = "Persist Security Info=False;database=dbi434661;server=studmysql01.fhict.local;Connect Timeout=30;user id=dbi434661; pwd=daivbot";
                MySqlConnection conDataBase = new MySqlConnection(constring);
                MySqlCommand cmdDataBase = new MySqlCommand("select * from dbi434661.departments;", conDataBase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDataBase;
                    DataTable dbaTableset = new DataTable();
                    sda.Fill(dbaTableset);
                    BindingSource bSource = new BindingSource();

                    bSource.DataSource = dbaTableset;

                    this.dgDepartments.DataSource = bSource;
                    sda.Update(dbaTableset);

                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void btnAddDept_Click(object sender, EventArgs e)
        {
           
            string query = "insert into dbi434661.departments(DeptID,DeptName)values('" + this.tbDeptId.Text + "','" + this.tbDeptName.Text + "');";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                MessageBox.Show("New Department added");

                while (reader.Read())
                {

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
            string query = "update dbi434661.departments set DeptName='" + this.tbDeptName.Text + "' where DeptID='" + this.tbDeptId + "' ;";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();
                MessageBox.Show("Department Details Updated successfully!");

                while (reader.Read())
                {

                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            string query = "delete from dbi434661.departments where DeptID='" + this.tbDeptId.Text + "';";
            //string query2 = "delete from dbi434661.departments where DeptID='" + this.tbDid.Text + "';";
            //string query3 = "delete from dbi434661.login where eID='" + this.tbDid.Text + "';";
            MySqlCommand command = new MySqlCommand(query, conn);
            //MySqlCommand command2 = new MySqlCommand(query2, conn);
            //MySqlCommand command3 = new MySqlCommand(query3, conn);

            try
            {
                conn.Open();


                // Object result = command.ExecuteScalar();
                //Object result2 = command2.ExecuteScalar();

                command.CommandType = CommandType.Text;
                command.ExecuteScalar();



                //command2.CommandType = CommandType.Text;
                //command2.ExecuteScalar();

                //command3.CommandType = CommandType.Text;
                //command3.ExecuteScalar();

                MessageBox.Show("Department Deleted");


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }




        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 form3 = new Form3();
            form3.Show();
        }

  
        private void cmeIds_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            string query = "select * from dbi434661.employee where FirstName='" + this.cmEmployeeNames.Text + "';";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();


               
                string ID = reader.GetInt32("ID").ToString();
                string FirstName = reader.GetString("FirstName");
                string LastName = reader.GetString("LastName");
                string Age = reader.GetInt32("Age").ToString();
                string Gender = reader.GetString("Gender");
                string DepartmentID = reader.GetInt32("DepartmentID").ToString();
                string HireDate = reader.GetString("HireDate");
                string Salary = reader.GetInt32("Salary").ToString();
                string Adress = reader.GetString("Adress");
                string Role = reader.GetString("Role");
                this.tbId.Text = ID;
                this.tbFn.Text = FirstName;
                this.tbSn.Text = LastName;
                this.tbAge.Text = Age;
                this.cmbGender.Text = Gender;
                this.tbDeptId.Text = DepartmentID;
                this.dateTimePicker1.Text = HireDate;
                this.tbSal.Text = Salary;
                this.tbAdd.Text = Adress;
                this.cmbRole.Text = Role;





               


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void cmDeptNames_SelectedIndexChanged(object sender, EventArgs e)
        

        {
            //MySqlConnection conn = new MySqlConnection("Persist Security Info=False;database=dbi434661;server=studmysql01.fhict.local;Connect Timeout=30;user id=dbi434661; pwd=daivbot");
            string query = "select * from dbi434661.departments;";
            MySqlCommand command = new MySqlCommand(query, conn);
            MySqlDataReader reader;

            try
            {
                conn.Open();
                reader = command.ExecuteReader();


                
                string dptmentId = reader.GetInt32("DeptID").ToString();
                string dptName = reader.GetString("DeptName");
                this.tbDeptId.Text = dptmentId;
                this.tbDeptName.Text = dptName;

               


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


    }
}   

