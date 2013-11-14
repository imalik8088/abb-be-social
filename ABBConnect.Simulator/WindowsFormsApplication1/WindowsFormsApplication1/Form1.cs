/*
 * Written by: Robert Gustavsson
 * Date: 11.11.2013
 * Project: ABBConnect
 * Revised:
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABBConnectSimulator.BLL;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        ISensorManager sensorManager;

        public Form1()
        {
            InitializeComponent();
            sensorManager = new SensorManager();
            FillSensorBox();
        }

        private void FillSensorBox()
        {
            lstbSensors.DisplayMember = "Name";
            lstbSensors.ValueMember = "ID";
            lstbSensors.DataSource = this.sensorManager.GetSensors();
        }


        private void lstbSensors_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = lstbSensors.SelectedIndex;

            //string sensorID = lstbSensors.SelectedValue.ToString();
           // int sID = Convert.ToInt32(sensorID);

            decimal[] vals = sensorManager.GetBounderyValues(selectedIndex);

            txtbLowerBoundery.Text = vals[0].ToString();
            txtbUpperBoundery.Text = vals[1].ToString();


        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstbSensors.SelectedIndex;
            decimal upperBoundery, lowerBoundery;

            if(!Decimal.TryParse(txtbUpperBoundery.Text, out upperBoundery))
            {
                MessageBox.Show("Invalid upper boundery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Decimal.TryParse(txtbLowerBoundery.Text, out lowerBoundery))
            {
                MessageBox.Show("Invalid upper boundery", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool result = sensorManager.ChangeBoundery(selectedIndex, upperBoundery, lowerBoundery);

            if (result)
            {
                MessageBox.Show("Boundery Changed", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillSensorBox();
            }
            else
                MessageBox.Show("Can not change the bounderies", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstbSensors.SelectedIndex;

            sensorManager.GenerateValues(selectedIndex, 100);

            MessageBox.Show("Values Added", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnPublish_Click(object sender, EventArgs e)
        {
            sensorManager.PublishSensorValues();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            int selectedIndex = lstbSensors.SelectedIndex;
            OpenFileDialog opnfdia = new OpenFileDialog();

            opnfdia.InitialDirectory = "c:\\";
            opnfdia.Filter = "txt files (*.txt)|*.txt";
            opnfdia.FilterIndex = 2;
            opnfdia.RestoreDirectory = true;

            if (opnfdia.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    sensorManager.LoadValues(selectedIndex, opnfdia.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }
    }
}
