namespace TestRealTimeCharts
{
    partial class FrmFichas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GridFichas = new System.Windows.Forms.DataGridView();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.btnIncluir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numDensidade = new System.Windows.Forms.NumericUpDown();
            this.numCoeficientegm3 = new System.Windows.Forms.NumericUpDown();
            this.numEspessura = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numGramaturagm2 = new System.Windows.Forms.NumericUpDown();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnLiberar = new System.Windows.Forms.Button();
            this.chkReadOnly = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GridFichas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDensidade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoeficientegm3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEspessura)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGramaturagm2)).BeginInit();
            this.SuspendLayout();
            // 
            // GridFichas
            // 
            this.GridFichas.AllowUserToAddRows = false;
            this.GridFichas.AllowUserToDeleteRows = false;
            this.GridFichas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.GridFichas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridFichas.Location = new System.Drawing.Point(12, 68);
            this.GridFichas.Name = "GridFichas";
            this.GridFichas.ReadOnly = true;
            this.GridFichas.Size = new System.Drawing.Size(769, 539);
            this.GridFichas.TabIndex = 1;
            this.GridFichas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridFichas_CellContentClick);
            this.GridFichas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridFichas_CellEndEdit);
            this.GridFichas.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridFichas_RowLeave);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Location = new System.Drawing.Point(12, 12);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(260, 50);
            this.btnSalvar.TabIndex = 2;
            this.btnSalvar.Text = "&Selecionar Produto - F5";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // txtProduto
            // 
            this.txtProduto.Location = new System.Drawing.Point(887, 147);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.Size = new System.Drawing.Size(157, 20);
            this.txtProduto.TabIndex = 2;
            // 
            // btnIncluir
            // 
            this.btnIncluir.Location = new System.Drawing.Point(1018, 175);
            this.btnIncluir.Name = "btnIncluir";
            this.btnIncluir.Size = new System.Drawing.Size(118, 90);
            this.btnIncluir.TabIndex = 7;
            this.btnIncluir.Text = "&Incluir";
            this.btnIncluir.UseVisualStyleBackColor = true;
            this.btnIncluir.Click += new System.EventHandler(this.btnIncluir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(787, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Produto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(787, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Densidade g/m3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(787, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Coeficiente g/m3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(787, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Espessura mm:";
            // 
            // numDensidade
            // 
            this.numDensidade.DecimalPlaces = 3;
            this.numDensidade.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numDensidade.Location = new System.Drawing.Point(887, 173);
            this.numDensidade.Name = "numDensidade";
            this.numDensidade.Size = new System.Drawing.Size(100, 20);
            this.numDensidade.TabIndex = 3;
            this.numDensidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numDensidade.ValueChanged += new System.EventHandler(this.txtDensidade_ValueChanged);
            // 
            // numCoeficientegm3
            // 
            this.numCoeficientegm3.DecimalPlaces = 6;
            this.numCoeficientegm3.Location = new System.Drawing.Point(887, 199);
            this.numCoeficientegm3.Name = "numCoeficientegm3";
            this.numCoeficientegm3.Size = new System.Drawing.Size(100, 20);
            this.numCoeficientegm3.TabIndex = 4;
            this.numCoeficientegm3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCoeficientegm3.Value = new decimal(new int[] {
            3124691,
            0,
            0,
            327680});
            // 
            // numEspessura
            // 
            this.numEspessura.DecimalPlaces = 3;
            this.numEspessura.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numEspessura.Location = new System.Drawing.Point(887, 224);
            this.numEspessura.Maximum = new decimal(new int[] {
            70,
            0,
            0,
            131072});
            this.numEspessura.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            131072});
            this.numEspessura.Name = "numEspessura";
            this.numEspessura.Size = new System.Drawing.Size(100, 20);
            this.numEspessura.TabIndex = 5;
            this.numEspessura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numEspessura.Value = new decimal(new int[] {
            8,
            0,
            0,
            131072});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(787, 252);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Gramatura g/m2";
            // 
            // numGramaturagm2
            // 
            this.numGramaturagm2.Location = new System.Drawing.Point(887, 250);
            this.numGramaturagm2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numGramaturagm2.Name = "numGramaturagm2";
            this.numGramaturagm2.Size = new System.Drawing.Size(100, 20);
            this.numGramaturagm2.TabIndex = 6;
            this.numGramaturagm2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(787, 584);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(131, 23);
            this.btnExcluir.TabIndex = 17;
            this.btnExcluir.Text = "E&xcluir Selecionado";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnLiberar
            // 
            this.btnLiberar.Location = new System.Drawing.Point(614, 12);
            this.btnLiberar.Name = "btnLiberar";
            this.btnLiberar.Size = new System.Drawing.Size(167, 49);
            this.btnLiberar.TabIndex = 18;
            this.btnLiberar.Text = "Liberar";
            this.btnLiberar.UseVisualStyleBackColor = true;
            this.btnLiberar.Click += new System.EventHandler(this.btnLiberar_Click);
            // 
            // chkReadOnly
            // 
            this.chkReadOnly.AutoSize = true;
            this.chkReadOnly.Enabled = false;
            this.chkReadOnly.Location = new System.Drawing.Point(333, 32);
            this.chkReadOnly.Name = "chkReadOnly";
            this.chkReadOnly.Size = new System.Drawing.Size(136, 17);
            this.chkReadOnly.TabIndex = 19;
            this.chkReadOnly.Text = "Bloqueado para edição";
            this.chkReadOnly.UseVisualStyleBackColor = true;
            // 
            // FrmFichas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 617);
            this.Controls.Add(this.chkReadOnly);
            this.Controls.Add(this.btnLiberar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.numGramaturagm2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numEspessura);
            this.Controls.Add(this.numCoeficientegm3);
            this.Controls.Add(this.numDensidade);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnIncluir);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.GridFichas);
            this.KeyPreview = true;
            this.Name = "FrmFichas";
            this.Text = "FrmFichas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmFichas_Load);
            this.ClientSizeChanged += new System.EventHandler(this.FrmFichas_ClientSizeChanged);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmFichas_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.GridFichas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDensidade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCoeficientegm3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEspessura)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGramaturagm2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView GridFichas;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.Button btnIncluir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numDensidade;
        private System.Windows.Forms.NumericUpDown numCoeficientegm3;
        private System.Windows.Forms.NumericUpDown numEspessura;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numGramaturagm2;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnLiberar;
        private System.Windows.Forms.CheckBox chkReadOnly;
    }
}