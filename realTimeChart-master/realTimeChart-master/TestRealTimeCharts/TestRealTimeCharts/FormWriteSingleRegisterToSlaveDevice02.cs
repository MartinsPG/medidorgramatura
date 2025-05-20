using IndustrialNetworks.ModbusCore.ASCII;
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

namespace TestRealTimeCharts
{
    public partial class FormWriteSingleRegisterToSlaveDevice02 : Form
    {
        private byte slaveAddress = 1;
        private uint startAddress = 4200;
        private uint startAddressw = 4400;
        private byte[] values = null;
        private ushort numberOfPoints = 10; // Reads 10 registers.

        private IModbusMaster objIModbusMaster = null;

        public uint StartAddressw { get => startAddressw; set => startAddressw = value; }

        public FormWriteSingleRegisterToSlaveDevice02()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void FormWriteSingleRegisterToSlaveDevice02_Load(object sender, EventArgs e)
        {
            try
            {
                
                objIModbusMaster = new ModbusASCIIMaster("COM6", 9600, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
                objIModbusMaster.Connection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                StartAddressw = (uint)txtAddress.Value;
                values = Int.ToByteArray((short)txtValue.Value);
                objIModbusMaster.WriteSingleRegister(slaveAddress, StartAddressw, values);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormWriteSingleRegisterToSlaveDevice02_FormClosed(object sender, FormClosedEventArgs e)
        {
            objIModbusMaster.Disconnection();
        }

        private void btnReadTimer_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] bytes = objIModbusMaster.ReadHoldingRegisters(slaveAddress, startAddress, numberOfPoints);
                txtResult.Text = string.Empty; // clear text.
                if (bytes != null)
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
    }
}
