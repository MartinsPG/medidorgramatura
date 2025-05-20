using IndustrialNetworks.ModbusCore.ASCII;
using IndustrialNetworks.ModbusCore.Comm;
using IndustrialNetworks.ModbusCore.DataTypes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
namespace TestRealTimeCharts
{
    /*public static class Dadosdeusuario 
    {
        private static double v_densidade = 0;
    }*/
    

    public partial class FormMedicao : Form
    {

//        private static double v_densidade = 0;
        private const string srvProtheus = "Server=192.168.1.80;Database=METABASE;User Id=sa;Password =;";
        private const string srvLocal = "Server=localhost;Database=MEDICOES;User Id=sa;Password =;";
        private Thread cpuThread;
        int nPassos;
        private float nPointOld = 1;
        private float nPointMed = 0;
        private double grM2Med = 0;
        private double grM2Tot = 0;
        private float[] amedicoes = new float[67];
        private float[] amedicoesMedia = new float[67];
        private float[] amediagrm2 = new float[67];
        private double[,] aMedias = new double[5, 67];
        private float[] agabarito = new float[1];
        private float nPoint = 0;
        private byte[] values = null;
        private string cLeitura;
        private string cSqlInsert;
        private string cPortName = "COM6";
        private string cSentido = "E";
        private IModbusMaster objIModbusMaster = null;
        private byte slaveAddress = 1;
        private uint startAddressr = 4096; // start address : D0.
        private ushort numberOfPoints = 2; // Reads 10 registers.

        public FormMedicao()
        {
            InitializeComponent();
        }

        private void GetData()
        {
            while (true)
            {
                if (cpuChart.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateChart(); });
                }
                Thread.Sleep(50);
            }
        }
        private void UpdateChart()
        {
            try
            {
                byte[] bytesr = objIModbusMaster.ReadHoldingRegisters(slaveAddress, startAddressr, numberOfPoints);
                txtRead.Text = string.Empty; // clear text.
                txtHora.Text = DateTime.Now.ToString();
                cLeitura = string.Empty;
                if (bytesr != null)
                {
                    ushort[] resultr = Word.ToArray(bytesr);
                    foreach (ushort itemr in resultr)
                    {
                        txtRead.Text += string.Format("{0},", itemr);
                        cLeitura += string.Format("{0},", itemr);
                    }
                }
                nPassos = amedicoes.Length;// 670;// Convert.ToInt32(cLeitura.ToString());

                int nindexPoint = cLeitura.IndexOf(',');
                int nIndexValue = cLeitura.IndexOf(',', nindexPoint + 1);
                float nPointProv = float.Parse(cLeitura.Substring(0, nindexPoint));
                float nValue = float.Parse(cLeitura.Substring(nindexPoint + 1, nIndexValue - nindexPoint - 1));
                //float nPoint = float.Parse(txtPosManual.Text);
                //float nValue = float.Parse(txtEspManual.Text);
                if (nPointProv == 1 && cSentido=="E"){
                    cSentido = "D";
                    nPoint = 1;
                }else if(nPointProv==67 && cSentido=="D"){
                    cSentido = "E";
                    nPoint = 67;
                }
                if (cSentido == "D")
                {
                    if (nPointProv > nPoint)
                    {
                        nPoint++;
                    }
                }
                if (cSentido == "E")
                {
                    if (nPointProv < nPoint)
                    {
                        nPoint--;
                    }
                }
                lblSentido.Text = cSentido;
                if (nValue > 0 &&nPoint>0 && nPoint < 68)
                {
                    double vl = 601;//Exposição inicial (I)
                    double vlo = 601-nValue;//exposição medida (Io)
                    double lol = vlo / vl;//lo/l
                    double vloglo = Math.Log(lol, Math.Exp(1));//log(lo/l)
                    double vCoeficientegm3 = Program.v_coeficientegm3;//coeficiente g/m3
                    double vDensidadegm3 = Convert.ToDouble(numDensidadeNom.Value);//densidade g/m3
                    double vCoeficiented = vDensidadegm3 * vCoeficientegm3;//coeficiente d coef*dens
                    double espReal = vloglo / (-vCoeficiented) * 10;//espessura ú
                    double grM2 = vDensidadegm3 * espReal*1000;
                    //double mV = nValue;
                    //double mVgr = float.Parse(txtFator.Text);
                    //double grM2 = Math.Round(mV * mVgr, 3);
                    double vDensNom = Convert.ToDouble(numDensidadeNom.Value);
                    double vEspNom = Convert.ToDouble(numEspessura.Value);
                    double vGrNom = Convert.ToDouble(Program.v_gramaturagm2); //Math.Round(vDensNom * vEspNom * 1000, 3);
                    txtGrReal.Text = Convert.ToString(grM2);
                    txtGrNominal.Text = Convert.ToString(vGrNom);
                    //numGrNominal.Value = vGrNom;
                    //double espReal = grM2 / vDensNom / 1000;
                    Double nMedicao = Convert.ToDouble(espReal);
                    trackPoint.Value = Convert.ToInt16(nPoint);
                    txtPoint.Text = Convert.ToString(nPoint);
                    txtEspReal.Text = Convert.ToString(espReal);
                    if (nPoint > 0 && nPoint <= nPassos)
                    {
                        if (nPoint != Convert.ToDouble(nPointOld))
                        {
                            Int32 nPoint32 = Convert.ToInt32(nPoint);
                            if (nPoint32 <= 67)
                            {
                                aMedias[4, nPoint32 - 1] = aMedias[3, nPoint32 - 1];
                                aMedias[3, nPoint32 - 1] = aMedias[2, nPoint32 - 1];
                                aMedias[2, nPoint32 - 1] = aMedias[1, nPoint32 - 1];
                                aMedias[1, nPoint32 - 1] = aMedias[0, nPoint32 - 1];
                                aMedias[0, nPoint32 - 1] = grM2Med;
                                nPointMed = 1;
                                nPointOld = nPoint;
                                grM2Tot = grM2;
                                grM2Med = grM2Tot / nPointMed;
                            }
                        }
                        else
                        {
                            grM2Tot += grM2;
                            nPointMed++;
                            grM2Med = grM2Tot / nPointMed;
                        }

                        amedicoes[Convert.ToInt32(nPoint - 1)] = float.Parse(Convert.ToString(grM2Med));
                        amediagrm2[Convert.ToInt32(nPoint - 1)] = float.Parse(Convert.ToString(grM2Med));
                        cpuChart.Series["Series1"].Points.Clear();
                        cpuChart.Series["Series2"].Points.Clear();
                        cpuChart.Series["LI"].Points.Clear();
                        cpuChart.Series["LS"].Points.Clear();
                        cpuChart.Series["LI-"].Points.Clear();
                        cpuChart.Series["LS-"].Points.Clear();
                        chartMedia.Series["Series1"].Points.Clear();
                        chartMedia.Series["Series2"].Points.Clear();
                        chartMedia.Series["LI"].Points.Clear();
                        chartMedia.Series["LS"].Points.Clear();
                        chartMedia.Series["LI-"].Points.Clear();
                        chartMedia.Series["LS-"].Points.Clear();
                        for (int i = 1; i <= nPassos; ++i)
                        {
                            double nMedia = 0;
                            nMedia += aMedias[0, Convert.ToInt32(i) - 1];
                            nMedia += aMedias[1, Convert.ToInt32(i) - 1];
                            nMedia += aMedias[2, Convert.ToInt32(i) - 1];
                            nMedia += aMedias[3, Convert.ToInt32(i) - 1];
                            nMedia += aMedias[4, Convert.ToInt32(i) - 1];
                            nMedia = nMedia / 5;
                            chartMedia.Series["Series1"].Points.AddY(nMedia);
                            if (i == nPoint)
                            {
                                cpuChart.Series["Series1"].Points.AddY(grM2Med);
                            }
                            else
                            {
                                cpuChart.Series["Series1"].Points.AddY(amedicoes[Convert.ToInt32(i - 1)]);
//                                chartMedia.Series["Series1"].Points.AddY(amedicoes[Convert.ToInt32(i - 1)]);
                            }
                            cpuChart.Series["Series2"].Points.AddY(vGrNom);
                            cpuChart.Series["LI"].Points.AddY(vGrNom + 5);
                            cpuChart.Series["LS"].Points.AddY(vGrNom - 5);
                            cpuChart.Series["LI-"].Points.AddY(vGrNom + 20);
                            cpuChart.Series["LS-"].Points.AddY(vGrNom - 20);
                            chartMedia.Series["Series2"].Points.AddY(vGrNom);
                            chartMedia.Series["LI"].Points.AddY(vGrNom + 5);
                            chartMedia.Series["LS"].Points.AddY(vGrNom - 5);
                            chartMedia.Series["LI-"].Points.AddY(vGrNom + 20);
                            chartMedia.Series["LS-"].Points.AddY(vGrNom - 20);
                        }
                        cpuChart.DataBind();
                        chartMedia.DataBind();
                        /*
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = srvLocal;
                        conn.Open();

                        cSqlInsert = "INSERT INTO MEDICOES (SEQ,DTREAD,POINT,MEASURE,GRAMATURA,OP,PRODUTO) ";
                        cSqlInsert += "VALUES ('1',getdate()," + nPoint + "," + espReal.ToString().Replace(',', '.') + ","+grM2.ToString().Replace(',','.') + "," + txtOP.Text + ",'" + txtCodigo.Text + "')";
                        SqlCommand command = new SqlCommand(cSqlInsert, conn);
                        command.ExecuteNonQuery();
                        */
                        double nTotal = 0;
                        for (int i = 1; i <= nPassos; ++i)
                        {                          nTotal += amediagrm2[i-1];
                        }
                        numGrMedia.Value = Convert.ToDecimal(nTotal) / nPassos;
                    }
                    //int nindexValue = cLeitura.IndexOf(',', nindexPoint + 1);
                    //string vRead = cLeitura.Substring(0, nindexPoint);
                    //float nValue = float.Parse(vRead);
                    //txtReal.Text = Convert.ToString(nValue);
                    //amedicoes[Convert.ToInt32(nPoint), 1] = nPoint;
                    //amedicoes[Convert.ToInt32(nPoint)] = nValue;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cpuThread.Abort();
                objIModbusMaster.Disconnection();

                button1.Enabled = true;
                button2.Enabled = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            nPassos = amedicoes.Length;// 670;// Convert.ToInt32(cLeitura.ToString());
            cpuChart.Series["Series1"].Points.Clear();
            cpuChart.Series["Series2"].Points.Clear();
            cpuChart.Series["LI"].Points.Clear();
            cpuChart.Series["LS"].Points.Clear();
            cpuChart.Series["LI-"].Points.Clear();
            cpuChart.Series["LS-"].Points.Clear();
            chartMedia.Series["Series1"].Points.Clear();
            chartMedia.Series["Series2"].Points.Clear();
            chartMedia.Series["LI"].Points.Clear();
            chartMedia.Series["LS"].Points.Clear();
            chartMedia.Series["LI-"].Points.Clear();
            chartMedia.Series["LS-"].Points.Clear();
            for (int i = 1; i <= nPassos; ++i)
            {
                cpuChart.Series["Series1"].Points.AddY(0.1);
                cpuChart.Series["Series2"].Points.AddY(Convert.ToDouble(txtNominal.Text));
                cpuChart.Series["LI"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                cpuChart.Series["LS"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
                cpuChart.Series["LI-"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                cpuChart.Series["LS-"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
                chartMedia.Series["Series1"].Points.AddY(0.1);
                chartMedia.Series["Series2"].Points.AddY(Convert.ToDouble(txtNominal.Text));
                chartMedia.Series["LI"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                chartMedia.Series["LS"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
                chartMedia.Series["LI-"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                chartMedia.Series["LS-"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
            }
            cpuThread = new Thread(new ThreadStart(this.GetData));
            cpuThread.IsBackground = true;
            cpuThread.Start();
            button1.Enabled = false;
            button2.Enabled = true;

            objIModbusMaster = new ModbusASCIIMaster(cPortName, 9600, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
            objIModbusMaster.Connection();
            ModbusScan.Start();

            /*            byte[] bytes1 = objIModbusMaster.ReadHoldingRegisters(slaveAddress, 4302, 1); //d200=4296
                        if (bytes1 != null)
                        {
                            ushort[] result1 = Word.ToArray(bytes1);
                            foreach (ushort item1 in result1)
                            {
                                txtMaxDest.Text += string.Format("{0}", item1);
                            }
                        }
                        byte[] bytes2 = objIModbusMaster.ReadHoldingRegisters(slaveAddress, 4303, 1); //d200=4296
                        if (bytes2 != null)
                        {
                            ushort[] result2 = Word.ToArray(bytes2);
                            foreach (ushort item2 in result2)
                            {
                                txtMinDest.Text += string.Format("{0}", item2);
                            }
                        }
                        */

        }


        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            numDensidadeNom.Focus();
            timer1.Start();
            FrmFichas frm2 = new FrmFichas();
            frm2.Show();
            frm2.TopMost = true;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Parar();
            //serialPort1.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cpuChart_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show(
      "Close the window?",
      "Are you sure?",
      MessageBoxButtons.YesNo);
            if (window == DialogResult.Yes)
            {
                try
                {
                    cpuThread.Abort();
                }
                catch
                {

                }
                //                Thread.Sleep(5);
            }
            e.Cancel = (window == DialogResult.No);
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void btnFichas_Click(object sender, EventArgs e)
        {
            FrmFichas fichas = new FrmFichas();
            fichas.Show();

        }

        private void timer1_Tick_2(object sender, EventArgs e)
        {

        }
        private void gravamedicoes()
        {
            string format = "yyyy-MM-ddTHH:mm:ss";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = srvProtheus;
            conn.Open();
            if (cLeitura != "")
            {
                DateTime dMedicao = System.DateTime.Now;

                for (int lin = 1; lin <= amedicoes.Length; ++lin)
                {
                    try
                    {
                        //if (amedicoes[lin-1] > 0)
                        {
                            String cMedicao = amedicoes[lin - 1].ToString().Replace(',', '.');
                            cSqlInsert = "INSERT INTO MEDICOES (SEQ,DTREAD,POINT,MEASURE) VALUES ('1',convert(datetime,'" + dMedicao.ToString(format) + "'),'" + lin + "'," + cMedicao + ")";
                            SqlCommand command = new SqlCommand(cSqlInsert, conn);
                            command.ExecuteNonQuery();
                        }

                    }
                    catch
                    { }
                }
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtNominal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtDensidade_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            FormWriteSingleRegisterToSlaveDevice02 frm = new FormWriteSingleRegisterToSlaveDevice02();
            frm.Show();
            /*try
            {
                values = null;
                startAddress = 4302;// (uint)txtAddress.Value;
                values = Int.ToByteArray((short)txtMaxDest.Value);
                objIModbusMaster.WriteSingleRegister(slaveAddress, startAddress, values);
                values = null;
                startAddress = 4303;// (uint)txtAddress.Value;
                values = Int.ToByteArray((short)txtMinDest.Value);
                objIModbusMaster.WriteSingleRegister(slaveAddress, startAddress, values);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            FormReadingD0ToD9FromSlaveDevice02 frm = new FormReadingD0ToD9FromSlaveDevice02();
            frm.Show();

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtMinDest_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numDensidadeNom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void FormMedicao_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (numDensidadeNom.Value < Convert.ToDecimal("3"))
                {
                    numDensidadeNom.Value = numDensidadeNom.Value + Convert.ToDecimal("0,005");
                }
            }
            if (e.KeyCode == Keys.F6)
            {
                if (numDensidadeNom.Value > Convert.ToDecimal("1,00"))
                {
                    numDensidadeNom.Value = numDensidadeNom.Value - Convert.ToDecimal("0,005");
                }
            }
            if (e.KeyCode == Keys.F7)
            {
                if (numEspessura.Value < Convert.ToDecimal("1"))
                {
                    numEspessura.Value = numEspessura.Value + Convert.ToDecimal("0,01");
                }
            }
            if (e.KeyCode == Keys.F8)
            {
                if (numEspessura.Value > Convert.ToDecimal("0,01"))
                {
                    numEspessura.Value = numEspessura.Value - Convert.ToDecimal("0,01");
                }
            }
            if (e.KeyCode == Keys.F2)
            {
                if (button1.Enabled)
                {
                    Iniciar();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                if (button2.Enabled)
                {
                    Parar();
                }
            }
            if (e.KeyCode == Keys.F12)
            {
                if (button2.Enabled)
                {
                    Parar();
                }
                FrmFichas fichas = new FrmFichas();
                fichas.Show();
            }


        }
        private void Iniciar()
        {
            if (false)//txtOP.Text == "")
            {
                MessageBox.Show("Informe o numero da OP para seguir com a medição!!!");
                txtOP.Focus();
            }
            else
            {
                nPassos = amedicoes.Length;// 670;// Convert.ToInt32(cLeitura.ToString());
                cpuChart.Series["Series1"].Points.Clear();
                cpuChart.Series["Series2"].Points.Clear();
                cpuChart.Series["LI"].Points.Clear();
                cpuChart.Series["LS"].Points.Clear();
                cpuChart.Series["LI-"].Points.Clear();
                cpuChart.Series["LS-"].Points.Clear();
                chartMedia.Series["Series1"].Points.Clear();
                chartMedia.Series["Series2"].Points.Clear();
                chartMedia.Series["LI"].Points.Clear();
                chartMedia.Series["LS"].Points.Clear();
                chartMedia.Series["LI-"].Points.Clear();
                chartMedia.Series["LS-"].Points.Clear();
                for (int i = 1; i <= nPassos; ++i)
                {
                    cpuChart.Series["Series1"].Points.AddY(0.1);
                    cpuChart.Series["Series2"].Points.AddY(Convert.ToDouble(txtNominal.Text));
                    cpuChart.Series["LI"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                    cpuChart.Series["LS"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
                    cpuChart.Series["LI-"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                    cpuChart.Series["LS-"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
                    chartMedia.Series["Series1"].Points.AddY(0.1);
                    chartMedia.Series["Series2"].Points.AddY(Convert.ToDouble(txtNominal.Text));
                    chartMedia.Series["LI"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                    chartMedia.Series["LS"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
                    chartMedia.Series["LI-"].Points.AddY(Convert.ToDouble(txtNominal.Text) + 0.01);
                    chartMedia.Series["LS-"].Points.AddY(Convert.ToDouble(txtNominal.Text) - 0.01);
                }
                cpuThread = new Thread(new ThreadStart(this.GetData));
                cpuThread.IsBackground = true;
                cpuThread.Start();
                button1.Enabled = false;
                button2.Enabled = true;

                objIModbusMaster = new ModbusASCIIMaster(cPortName, 9600, 7, System.IO.Ports.StopBits.One, System.IO.Ports.Parity.Even);
                objIModbusMaster.Connection();
                ModbusScan.Start();

            }


        }
        private void Parar()
        {
            cpuThread.Abort();
            objIModbusMaster.Disconnection();
            button1.Enabled = true;
            button2.Enabled = false;

        }

        private void timer1_Tick_3(object sender, EventArgs e)
        {
            numDensidadeNom.Value = Convert.ToDecimal(Program.v_densidadegm3);
            numGrNominal.Value= Convert.ToDecimal(Program.v_gramaturagm2);
            numEspessura.Value = Convert.ToDecimal(Program.v_espessuramm);
            txtProduto.Text = Program.v_produto;
        }

        private void txtOP_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void txtOP_Leave(object sender, EventArgs e)
        {
            if (txtOP.Text != "")
            {
               /* SqlConnection conn = new SqlConnection();
                conn.ConnectionString = srvProtheus;
                conn.Open();
                DataTable dt = new DataTable();

                String cSQL = "SELECT C2_NUM,C2_ITEM,C2_SEQUEN,C2_PRODUTO,C2_QUANT from VINIPLAST.dbo.SC2010 SC2 WHERE SC2.D_E_L_E_T_<>'*' AND C2_NUM+C2_ITEM+C2_SEQUEN='" + txtOP.Text + "'";



                SqlDataAdapter da = new SqlDataAdapter(cSQL, conn);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("OP não localizada!!! Informe uma op correta.");
                    txtOP.Focus();
                }
                else
                {
                    txtCodigo.Text = dt.Rows[0]["C2_PRODUTO"].ToString();
                }
                */
            }
        }

        private void txtGrReal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtGrNominal_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void chartMedia_Click(object sender, EventArgs e)
        {

        }
    }
}