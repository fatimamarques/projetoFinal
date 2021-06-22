using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoFinal
{
    public partial class FCliente : Form
    {
        public FCliente()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GlobalDB.conectar();
            txtId.Text = proximoId();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

          
                if (validarCampos())
                {
                    GlobalDB.cadastrar(txtNome.Text, txtEmail.Text, mskTelefone.Text, mskCep.Text, txtEndereco.Text, txtNumero.Text, txtBairro.Text, Convert.ToString(cboCidade.SelectedItem), Convert.ToString(cboUf.SelectedItem));
                    limparCampos();
                }
        }

        private string proximoId()
        {
            int proximoId = Int32.Parse(GlobalDB.ultimoId()) + 1;
            return proximoId.ToString();
        }

        private void limparCampos()
        {
            txtId.Text = proximoId();
            txtNome.Text = "";
            txtEmail.Text = "";
            mskTelefone.Text = "";
            mskCep.Text = "";
            txtEndereco.Text = "";
            txtNumero.Text = "";
            txtBairro.Text = "";
            cboCidade.SelectedIndex = -1;
            cboUf.SelectedIndex = -1;
            txtNome.Focus();
            btnIncluir.Enabled = true;
        }
        private bool validarCampos()
        {
            string mensagem = "Validação dos Campos Obrigatórios\n\n";
            bool status = true;

            if (txtNome.Text.Length < 2)
            {
                mensagem += "\n Campo nome vazio";
                txtNome.Focus();
                status = false;
            }
            else
            {
                if (txtEmail.Text.Length == 0 || !txtEmail.Text.Contains("@"))
                {
                    mensagem += "\n Campo e-mail inválido";
                    txtEmail.Focus();
                    status = false;
                }
            }

            if (status == false)
                MessageBox.Show(mensagem, "Dados Incorretos", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            return status;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnIncluir.Enabled = false;
            List<Aluno> listaAlunos = GlobalDB.consultar(txtNome.Text);
            int contador = 0;
            if (listaAlunos.Count() != 0)
            {
                do
                {
                    txtId.Text = listaAlunos[contador].Id.ToString();
                    txtNome.Text = listaAlunos[contador].Nome.ToString();
                    mskTelefone.Text = listaAlunos[contador].Telefone.ToString();
                    txtEmail.Text = listaAlunos[contador].Email.ToString();
                    mskCep.Text = listaAlunos[contador].Cep.ToString();
                    txtEndereco.Text = listaAlunos[contador].Endereco.ToString();
                    txtNumero.Text = listaAlunos[contador].Numero.ToString();
                    txtBairro.Text = listaAlunos[contador].Bairro.ToString();
                    cboCidade.SelectedItem = listaAlunos[contador].Cidade.ToString();
                    cboUf.SelectedItem = listaAlunos[contador].Uf.ToString();

                    if (listaAlunos.Count() > 0)
                        if (MessageBox.Show("Gostaria de ver o próximo registro?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                            break;

                    contador++;

                } while (contador < listaAlunos.Count());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Aluno p = new Aluno(Int32.Parse(txtId.Text), txtNome.Text, txtEmail.Text, mskTelefone.Text, mskCep.Text, txtEndereco.Text, txtNumero.Text, txtBairro.Text, Convert.ToString(cboCidade.SelectedItem), Convert.ToString(cboUf.SelectedItem));
            GlobalDB.alterar(p);
            limparCampos();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo excluir este registro?", "Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                GlobalDB.excluir(txtId.Text);

            limparCampos();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            string listagem = GlobalDB.listar();
            MessageBox.Show(listagem);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
        
}
