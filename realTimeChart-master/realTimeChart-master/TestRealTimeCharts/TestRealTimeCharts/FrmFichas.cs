using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace TestRealTimeCharts
{
    public partial class FrmFichas : Form
    {
        public FrmFichas()
        {
            InitializeComponent();
        }

        private void FrmFichas_Load(object sender, EventArgs e)
        {
            Program.v_readonly = true;
            chkReadOnly.Checked = true;
            carregar();
        }


        private void btnCarregar_Click(object sender, EventArgs e)
        {

        }

        private void btnFecharFichas_Click(object sender, EventArgs e)
        {

        }

        private void GridFichas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GridFichas_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void carregar()
        {
            DataTable dt = new DataTable();

            String insSQL = "select * from fichas";
            String strPath = Application.StartupPath + "\\db_medidor.s3db";
            String strConn = @"Data Source=" + strPath;

            SQLiteConnection conn = new SQLiteConnection(strConn);

            SQLiteDataAdapter da = new SQLiteDataAdapter(insSQL, strConn);
            da.Fill(dt);
            GridFichas.DataSource = dt.DefaultView;
            GridFichas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            

        }
        private void SalvaDados()
        {
            String strPath = Application.StartupPath + "\\db_medidor.s3db";
            String strConn = @"Data Source=" + strPath;
            String cSQL = "";
            DataTable dta = new DataTable();
            SQLiteConnection connSelect = new SQLiteConnection(strConn);

            for (int i = 0; i < GridFichas.RowCount-1; i++)
            {
                cSQL = "select * from fichas where id_produto=" + GridFichas[0, i].Value;
                SQLiteCommand command = new SQLiteCommand(cSQL, connSelect);
                command.Connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                Boolean bReader = reader.Read();
                command.Connection.Close();
                if (bReader)
                {
                    SQLiteConnection connUpdate = new SQLiteConnection(strConn);
                    string cUpdate = "";
                    cUpdate += "update fichas ";
                    cUpdate += "set produto=@produto";
                    cUpdate += ",densidadegm3=@densidadegm3";
                    cUpdate += ",coeficientegm3=@coeficientegm3";
                    cUpdate += ",espessuramm=@espessuramm";
                    cUpdate += ",gramaturagm2=@gramaturagm2";
                    cUpdate += " where id_produto=@id_produto";
                    SQLiteCommand cmd = new SQLiteCommand(cUpdate, connUpdate);
                    cmd.Connection.Open();
                    for (int j = 0; j <= GridFichas.ColumnCount - 1; j++)
                    {
                        cmd.Parameters.AddWithValue("@" + GridFichas.Columns[j].Name, GridFichas[j, i].Value);
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                else
                {
                    SQLiteConnection connInsert = new SQLiteConnection(strConn);
                    SQLiteCommand cmd = new SQLiteCommand("insert into fichas(id_produto,produto,densidadegm3,coeficientegm3,espessuramm,gramaturagm2) values(@id_produto,@produto,@densidadegm3,@coeficientegm3,@espessuramm,@gramaturagm2)", connInsert);
                    cmd.Connection.Open();
                    for (int j = 0; j <= GridFichas.ColumnCount - 1; j++)
                    {
                        cmd.Parameters.AddWithValue("@" + GridFichas.Columns[j].Name, GridFichas[j, i].Value);
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
        }


        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Selecionar();

        }

        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            String insSQL = "select * from fichas";
            String strPath = Application.StartupPath + "\\db_medidor.s3db";
            String strConn = @"Data Source=" + strPath;

            SQLiteConnection conn = new SQLiteConnection(strConn);

            SQLiteDataAdapter da = new SQLiteDataAdapter(insSQL, strConn);
            da.Fill(dt);
            GridFichas.DataSource = dt.DefaultView;

        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (chkReadOnly.Checked == false)
            {
                String strPath = Application.StartupPath + "\\db_medidor.s3db";
                String strConn = @"Data Source=" + strPath;
                SQLiteConnection connInsert = new SQLiteConnection(strConn);
                SQLiteCommand cmd = new SQLiteCommand("insert into fichas(id_produto,produto,densidadegm3,coeficientegm3,espessuramm,gramaturagm2) " +
                    " values((select max(id_produto)+1 from fichas),@produto,@densidadegm3,@coeficientegm3,@espessuramm,@gramaturagm2)", connInsert);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@produto", txtProduto.Text);
                cmd.Parameters.AddWithValue("@densidadegm3", numDensidade.Value);
                cmd.Parameters.AddWithValue("@coeficientegm3", numCoeficientegm3.Value);
                cmd.Parameters.AddWithValue("@espessuramm", numEspessura.Value);
                cmd.Parameters.AddWithValue("@gramaturagm2", numGramaturagm2.Value);

                /*            for (int j = 0; j <= GridFichas.ColumnCount - 1; j++)
                                                    {
                                                        cmd.Parameters.AddWithValue("@" + GridFichas.Columns[j].Name, GridFichas[j, i].Value);
                                                    }*/
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                carregar();
            }
        }

        private void txtDensidade_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (GridFichas.RowCount > 0)
            {
                String strPath = Application.StartupPath + "\\db_medidor.s3db";
                String strConn = @"Data Source=" + strPath;
                SQLiteConnection connInsert = new SQLiteConnection(strConn);
                SQLiteCommand cmd = new SQLiteCommand("delete from fichas where id_produto=@id_produto", connInsert);
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@id_produto", GridFichas.CurrentRow.Cells[0].Value.ToString());

                /*            for (int j = 0; j <= GridFichas.ColumnCount - 1; j++)
                                                    {
                                                        cmd.Parameters.AddWithValue("@" + GridFichas.Columns[j].Name, GridFichas[j, i].Value);
                                                    }*/
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                carregar();

            }
            else
            {
                MessageBox.Show("nao há dados para excluir");
            }

        }

        private void GridFichas_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            String strPath = Application.StartupPath + "\\db_medidor.s3db";
            String strConn = @"Data Source=" + strPath;
            SQLiteConnection connInsert = new SQLiteConnection(strConn);
            SQLiteCommand cmd = new SQLiteCommand("update fichas set produto=@produto,densidadegm3=@densidadegm3,coeficientegm3=@coeficientegm3,espessuramm=@espessuramm,gramaturagm2=@gramaturagm2 where id_produto=@id_produto", connInsert);
            cmd.Connection.Open();
//            cmd.Parameters.AddWithValue("@id_produto", GridFichas.CurrentRow.Cells[0].Value.ToString());

            for (int j = 0; j <= GridFichas.ColumnCount - 1; j++)
            {
                cmd.Parameters.AddWithValue("@" + GridFichas.Columns[j].Name, GridFichas.CurrentRow.Cells[j].Value);
            }
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            carregar();
            //MessageBox.Show(GridFichas.CurrentRow.Cells[0].Value.ToString());
        }

        private void Selecionar()
        {
            int linha = GridFichas.CurrentRow.Index;
            int cIdProduto = Convert.ToInt16(GridFichas[0, linha].Value.ToString());
            string cProduto = GridFichas[1, linha].Value.ToString();
            string cDensidadegm3 = GridFichas[2, linha].Value.ToString();
            string cCoeficientegm3 = GridFichas[3, linha].Value.ToString();
            string cEspessuramm = GridFichas[4, linha].Value.ToString();
            string cGramaturagm2 = GridFichas[5, linha].Value.ToString();

            TestRealTimeCharts.Program.v_id_produto = cIdProduto;
            TestRealTimeCharts.Program.v_produto = Convert.ToString(cProduto);
            TestRealTimeCharts.Program.v_densidadegm3 = Convert.ToDouble(cDensidadegm3);
            TestRealTimeCharts.Program.v_coeficientegm3 = Convert.ToDouble(cCoeficientegm3);
            TestRealTimeCharts.Program.v_espessuramm = Convert.ToDouble(cEspessuramm);
            TestRealTimeCharts.Program.v_gramaturagm2 = Convert.ToDouble(cGramaturagm2);

            this.Close();

        }
        private void FrmFichas_Layout(object sender, LayoutEventArgs e)
        {

        }

        private void FrmFichas_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void FrmFichas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Selecionar();
            }

        }

        private void btnLiberar_Click(object sender, EventArgs e)
        {
            FrmSenha frmSenha = new FrmSenha();
            if (frmSenha.ShowDialog(this) == DialogResult.OK)
            {
                GridFichas.ReadOnly = Program.v_readonly;
                chkReadOnly.Checked = Program.v_readonly;
            }

        }
    }
}
