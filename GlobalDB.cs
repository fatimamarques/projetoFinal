using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;

namespace ProjetoFinal
{
    class GlobalDB
    {
        public static MySqlConnection conexao;

        public static MySqlCommand comando;

        public static MySqlDataReader resultado;

     
        public static void conectar()
        {
            /*b = new MySqlConnectionStringBuilder();
            b.Server = "localhost";
            b.Port = 3306;
            //b.Database = "testdb";
            b.UserID = "root";
            b.Password = "root";
            b.CharacterSet = "utf8";
            var connstr = b.ToString();
            conexao = new MySqlConnection(connstr);
            */
            //Estabelece os parâmetros para conexão com o BD
            conexao = new MySqlConnection("server=localhost;uid=root;pwd=admin;");

            conexao.Open();

            comando = new MySqlCommand("CREATE DATABASE IF NOT EXISTS dbaluno; use dbaluno;", conexao);

            comando.ExecuteNonQuery();

            comando = new MySqlCommand("CREATE TABLE IF NOT EXISTS alunos " +
                                        "(id integer auto_increment primary key, " +
                                        "nome       varchar(50), " +
                                        "email      varchar(40), " +
                                        "telefone   varchar(20), " +
                                        "cep        varchar(10), " +
                                        "endereco   varchar(50), " +
                                        "numero     varchar(5), "+
                                        "bairro     varchar(20), " +
                                        "cidade     varchar(20), " +
                                        "uf         varchar(2)); ", conexao);

            comando.ExecuteNonQuery();
            conexao.Close();

        }

        public static void cadastrar(string nome, string email, string telefone, string cep, string endereco, string numero, string bairro, string cidade, string uf)
        {
            try
            {
                conexao.Open();

                comando = new MySqlCommand("INSERT INTO alunos (nome, email, telefone, cep, endereco, numero, bairro, cidade, uf) values ('" +
                                        nome + "','" + email + "','" + telefone + "','" + cep + "','" + endereco + "','" + numero + "','" + bairro + "','" + cidade + "','" + uf + "');", conexao);

                comando.ExecuteNonQuery();
                MessageBox.Show("Cadastro realizado com sucesso!", "Cadastro OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha no Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexao.Close();
            }
        }

        public static string ultimoId()
        {
            string id = "0";

            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT MAX(id) AS maior FROM alunos;", conexao);

                resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {
                    resultado.Read();
                    id = resultado["maior"].ToString();

                    id = id != "" ? id : "0";

                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na recuperação do último ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }
            return id;
        }

        public static List<Aluno> consultar(string nome)
        {
            Aluno p = new Aluno();
            List<Aluno> listaAlunos = new List<Aluno>();

            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM alunos where nome like upper('%" + nome + "%');", conexao);

                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        resultado.Read();
                        p.Id = Convert.ToInt32(resultado["id"].ToString());
                        p.Nome = resultado["nome"].ToString();
                        p.Email = resultado["email"].ToString();
                        p.Telefone = resultado["telefone"].ToString();
                        p.Cep = resultado["cep"].ToString();
                        p.Endereco = resultado["endereco"].ToString();
                        p.Numero = resultado["numero"].ToString();
                        p.Bairro = resultado["bairro"].ToString();
                        p.Cidade = resultado["cidade"].ToString();
                        p.Uf = resultado["uf"].ToString();
                        listaAlunos.Add(p);
                    }
                }
                else
                {
                    MessageBox.Show("Nome não encontrado!", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na consulta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }

            return listaAlunos;
        }

        public static void alterar(Aluno p)
        {
            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM alunos where id=" + p.Id + ";", conexao);

                resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {
                    resultado.Read();
                    string instrucao = "UPDATE alunos set ";
                    instrucao += "nome = '" + p.Nome + "',";
                    instrucao += "email = '" + p.Email + "',";
                    instrucao += "telefone = '" + p.Telefone + "',";
                    instrucao += "endereco = '" + p.Endereco + "',";
                    instrucao += "numero = '" + p.Numero + "',";
                    instrucao += "bairro = '" + p.Bairro + "',";
                    instrucao += "cidade = '" + p.Cidade + "',";
                    instrucao += "uf = '" + p.Uf + "' WHERE id=" + p.Id + ";";

                    if (!resultado.IsClosed)
                        resultado.Close();

                    comando = new MySqlCommand(instrucao, conexao);

                    comando.ExecuteNonQuery();

                    MessageBox.Show("Alteração realizada com sucesso!", "Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Registro não encontrado", "Falha na Alteração", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na alteração", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }
        }

        public static void excluir(string id)
        {
            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM alunos where id=" + id + ";", conexao);

                MySqlDataReader resultado = comando.ExecuteReader();

                if (resultado.HasRows)
                {
                    resultado.Read();
                    string instrucao = "DELETE FROM alunos WHERE id=" + id + ";";

                    if (!resultado.IsClosed)
                        resultado.Close();

                    comando = new MySqlCommand(instrucao, conexao);

                    comando.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Registro não encontrado!", "Falha na exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na exclusão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }
        }

        public static string listar()
        {
            string listagem = "Lista vazia!";
            try
            {
                conexao.Open();

                comando = new MySqlCommand("SELECT * FROM alunos ORDER BY nome;", conexao);

                MySqlDataReader resultado = comando.ExecuteReader();

                Aluno p = new Aluno();

                if (resultado.HasRows)
                {
                    listagem = "Listagem de Cadastros\n\n";
                    while (resultado.Read())
                    {
                        p.Id = Convert.ToInt32(resultado["id"].ToString());
                        p.Nome = resultado["nome"].ToString();
                        p.Email = resultado["email"].ToString();
                        p.Telefone = resultado["telefone"].ToString();
                        p.Cep = resultado["cep"].ToString();
                        p.Endereco = resultado["endereco"].ToString();
                        p.Numero = resultado["numero"].ToString();
                        p.Bairro = resultado["bairro"].ToString();
                        p.Cidade = resultado["cidade"].ToString();
                        p.Uf = resultado["uf"].ToString();

                        listagem += p.ToString() + '\n';
                    }
                }
            }
            catch (Exception erro)
            {
                MessageBox.Show("ERRO ==> " + erro.Message, "Falha na listagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!resultado.IsClosed)
                    resultado.Close();

                conexao.Close();
            }

            return listagem;

        }
    }
}