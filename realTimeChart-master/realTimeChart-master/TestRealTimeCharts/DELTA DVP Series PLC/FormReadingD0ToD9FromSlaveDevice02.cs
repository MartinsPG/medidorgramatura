﻿using IndustrialNetworks.ModbusCore.ASCII;
using IndustrialNetworks.ModbusCore.Comm;
using IndustrialNetworks.ModbusCore.DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IndustrialNetworks.DVPSeries
{
    public partial class FormReadingD0ToD9FromSlaveDevice02 : Form
    {
        private IModbusMaster objIModbusMaster = null;
        private byte slaveAddress = 1;
        private uint startAddress = 4096; // start address : D0.
        private ushort numberOfPoints = 10; // Reads 10 registers.
        public FormReadingD0ToD9FromSlaveDevice02()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void FormReadingD0ToD9FromSlaveDevice02_Load(object sender, EventArgs e)
        {
            try
            {
                objIModbusMaster = new ModbusASCIIMaster("COM9", 9600, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
                objIModbusMaster.Connection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReadTimer_Click(object sender, EventArgs e)
        {
            try
            {
                ModbusScan.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModbusScan_Tick(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = objIModbusMaster.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                txtResult.Text = string.Empty; // clear text.
                if(bytes != null)
                {
                    ushort[] result = Word.ToArray(bytes);
                    foreach (ushort item in result)
                    {
                        txtResult.Text += string.Format("[{0}], ", item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
