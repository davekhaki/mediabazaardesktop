﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using MediaBazaar.Entities;
using MediaBazaar.Forms;
using MediaBazaar.Logic;
using MediaBazaarOO.Entities;
using MediaBazaarOO.Logic;

namespace MediaBazaarOO.Forms
{
    public partial class AdminForm : Form
    {
        private readonly PersonManager personManager = new PersonManager();
        private readonly DepartmentManager departmentManager = new DepartmentManager();
        private readonly StockManager stockManager = new StockManager();
        private string username;
        public AdminForm(string username)
        {
            InitializeComponent();

            employeesDataGrid.DataSource = personManager.GetAllEmployees();
            departmentsDataGrid.DataSource = departmentManager.GetAllDepartments();
            stockDataGrid.DataSource = stockManager.GetAllStock();
            this.username = username;

            Text = "Admin | " + username;
        }


        private void enableAllBtn_Click(object sender, EventArgs e)
        {
            idCheckBox.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox9.Checked = true;
            checkBox10.Checked = true;
            checkBox11.Checked = true;
            checkBox1.Checked = true;
        }

        private void disableAllBtn_Click(object sender, EventArgs e)
        {
            idCheckBox.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;
            checkBox7.Checked = false;
            checkBox8.Checked = false;
            checkBox9.Checked = false;
            checkBox10.Checked = false;
            checkBox11.Checked = false;
            checkBox1.Checked = false;
        }

        private void idCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["Id"].Visible = !employeesDataGrid.Columns["Id"].Visible;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["FirstName"].Visible = !employeesDataGrid.Columns["FirstName"].Visible;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["LastName"].Visible = !employeesDataGrid.Columns["LastName"].Visible;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["Age"].Visible = !employeesDataGrid.Columns["Age"].Visible;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["Gender"].Visible = !employeesDataGrid.Columns["Gender"].Visible;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["DepartmentName"].Visible = !employeesDataGrid.Columns["DepartmentName"].Visible;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["HireDate"].Visible = !employeesDataGrid.Columns["HireDate"].Visible;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["EndDate"].Visible = !employeesDataGrid.Columns["EndDate"].Visible;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["Salary"].Visible = !employeesDataGrid.Columns["Salary"].Visible;
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["Address"].Visible = !employeesDataGrid.Columns["Address"].Visible;
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["Role"].Visible = !employeesDataGrid.Columns["Role"].Visible;
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            departmentManager.AddNewDepartment(new Department(tbDptName.Text));
            MessageBox.Show("Department Added Successfully");
            departmentsDataGrid.DataSource = departmentManager.GetAllDepartments();
        }

        private void btnEditDepartment_Click(object sender, EventArgs e)
        {
            departmentManager.EditDepartment(new Department(Convert.ToInt32(tbDptId.Text), tbDptName.Text));
            MessageBox.Show("Department Changed Successfully");
            departmentsDataGrid.DataSource = departmentManager.GetAllDepartments();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(filePathTextBox.Text)) MessageBox.Show("Please choose a file path");
            if (string.IsNullOrEmpty(fileNameTextBox.Text)) MessageBox.Show("Please give the PDF a name");
            if (radioButton1.Checked)
            {
                PdfPTable t = new PdfPTable(stockDataGrid.Rows.Count);

                stockManager.ExportStockToPdf(t, filePathTextBox.Text + "/" + fileNameTextBox.Text + ".pdf", fileNameTextBox.Text, stockDataGrid);
            }
            else if (radioButton3.Checked)
            {
                PdfPTable t = new PdfPTable(departmentsDataGrid.Rows.Count);

                departmentManager.ExportDepartmentsToPdf(t, filePathTextBox.Text + "/" + fileNameTextBox.Text + ".pdf", fileNameTextBox.Text, departmentsDataGrid);
            }

            MessageBox.Show("PDF downloaded");
        }

        private void signOutBtn_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            employeesDataGrid.Columns["Preference"].Visible = !employeesDataGrid.Columns["Preference"].Visible;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            personManager.AddNewPerson(tbFirstName.Text, tbLastName.Text, Convert.ToDateTime(dobPicker.Text),
                cmbGender.Text, cmbDepartmentName.Text, dtpHireDate.Value, 
                Convert.ToInt32(tbSalary.Text), tbAddress.Text, cmbRole.Text, emailTextBox.Text);
            personManager.RefreshEmployees();
            employeesDataGrid.DataSource = personManager.GetAllEmployees();

            MessageBox.Show("Person Added");

            tbFirstName.Text = "";
            tbLastName.Text = "";
            dobPicker.Value = DateTime.Today;
            dtpHireDate.Value = DateTime.Today;
            tbSalary.Text = "";
            tbAddress.Text = "";
            
           

        }

        private void chooseLocationBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                filePathTextBox.Text = fbd.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new StatisticsForm(username);
            form.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var form = new EditEmployeeForm(username);
            form.Show();
            this.Hide();
        }
    }
}
