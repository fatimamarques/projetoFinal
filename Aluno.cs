using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoFinal
{
    class Aluno
    {
        private int id;
        private string nome;
        private string email;
        private string telefone;
        private string cep;
        private string endereco;
        private string numero;
        private string bairro;
        private string cidade;
        private string uf;

        //Construtor sem parâmetros
        public Aluno()
        {
        }

        //Construtor com parâmetros
        public Aluno(int id, string nome, string email, string telefone, string cep, string endereco, string numero, string bairro, string cidade, string uf)
        {
            this.id = id;
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.cep = cep;
            this.endereco = endereco;
            this.numero = numero;
            this.bairro = bairro;
            this.cidade = cidade;
            this.uf = uf;
        }

        public int Id { get => id; set => id = value; }

        public string Nome { get => nome; set => nome = value; }

        public string Email { get => email; set => email = value; }

        public string Telefone { get => telefone; set => telefone = value; }

        public string Cep { get => cep; set => cep = value; }

        public string Endereco { get => endereco; set => endereco = value; }

        public string Numero { get => numero; set => numero = value; }

        public string Bairro { get => bairro; set => bairro = value; }

        public string Cidade { get => cidade; set => cidade = value; }

        public string Uf { get => uf; set => uf = value; }

        public override string ToString()
        {
            return (String.Format("Id: {0} - Nome: {1} - E-mail: {2} - Telefone: {3} - Cep: {4} - Endereco: {5} - Numero: {6} - Bairro: {7} - Cidade: {8} - Uf: {9}", id, nome, email, telefone, cep, endereco, numero, bairro, cidade, uf));
        }
    }
}

