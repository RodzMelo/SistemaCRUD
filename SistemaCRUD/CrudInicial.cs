using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaCRUD
{
    public partial class CrudInicial : Form
    {
        DataTable dt = new DataTable();
        public CrudInicial()
        {
            InitializeComponent();
            ConfigurarTabela();
        }
        
        private void ConfigurarTabela()
        {
            dt.Columns.Add("Id", typeof(Int32));
            dt.Columns["Id"].AutoIncrement = true;
            dt.Columns["Id"].AutoIncrementSeed = 1;

            dt.Columns.Add("Nome", typeof(String));
            dt.Columns.Add("Email", typeof(String));
            dt.Columns.Add("Livro", typeof(String));
            dt.Columns.Add("Retirada", typeof(String));
            dt.Columns.Add("Devolução", typeof(String));

            dataGridView1.DataSource = dt;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarForm())
            {
                AtualizarLinha(Convert.ToInt32("0" + txtID.Text));
                LimparForm();
                txtEmail.Focus();
            }
        }

        private bool ValidarForm()
        {
            var resultado = true;

            if (txtEmail.Text == "")
            {
                MessageBox.Show("Email é obrigatório", "Validar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                resultado = false;
            }
            else if (txtNome.Text == "")
            {
                MessageBox.Show("Nome é obrigatório", "Validar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                resultado = false;
            }
            else if (txtLivro.Text == "")
            {
                MessageBox.Show("Nome do livro é obrigatório", "Validar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLivro.Focus();
                resultado = false;
            }
            else if (txtDataRetirada.Text == "")
            {
                MessageBox.Show("Data de retirada é obrigatório", "Validar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataRetirada.Focus();
                resultado = false;
            }
            else if (txtDataEntrega.Text == "")
            {
                MessageBox.Show("Data de devolução é obrigatório", "Validar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDataEntrega.Focus();
                resultado = false;
            }
            return resultado;
        }

        private void AtualizarLinha(int id)
        {
            if (id == 0)
            {
                dt.Rows.Add(null, txtNome.Text, txtEmail.Text,
                    txtLivro.Text, txtDataRetirada.Text, txtDataEntrega.Text);
            }
            else
            {
                DataRow[] row = dt.Select("id = " + id);
                if (row.Length > 0)
                {
                    row[0]["email"] = txtEmail.Text;
                    row[0]["nome"] = txtNome.Text;
                    row[0]["Livro"] = txtLivro.Text;
                    row[0]["Retirada"] = txtDataRetirada.Text;
                    row[0]["Devolução"] = txtDataEntrega.Text;
                }
            }
        }

        private void LimparForm()
        {
            txtID.Text = "";
            txtEmail.Clear();
            txtNome.Clear();
            txtLivro.Clear();
            txtDataRetirada.Clear();
            txtDataEntrega.Clear();
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            GetEmail(txtEmail.Text);
        }

        private void GetEmail(string email)
        {
            DataRow[] row = dt.Select("email = '" + email + "'");
            if (row.Length > 0)
            {
                txtNome.Text = row[0]["nome"].ToString();
                txtID.Text = row[0]["id"].ToString();
                txtEmail.Text = row[0]["email"].ToString();
                txtLivro.Text = row[0]["Livro"].ToString();
                txtDataRetirada.Text = row[0]["Retirada"].ToString();
                txtDataEntrega.Text = row[0]["Devolução"].ToString();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Deseja excluir o registro?", "Excluir",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ExcluirLinha(Convert.ToInt32("0" + txtID.Text));
                    LimparForm();
                    txtEmail.Focus();
                }
            }
        }

        private void ExcluirLinha(int id)
        {
            DataRow[] row = dt.Select("id = " + id);
            if (row.Length > 0)
                row[0].Delete();
        }

        private void btnClassificar_Click(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = "nome ASC";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtDataEntrega_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void lblDataEntrega_Click(object sender, EventArgs e)
        {

        }
    }
}
