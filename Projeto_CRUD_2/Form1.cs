using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_CRUD_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTeste_Click(object sender, EventArgs e)
        {
            SQLiteConnection connect = new SQLiteConnection("Data Source = C:\\bancos\\Banco_CSharp.db; Version=3;");
            connect.Open();//abre a conexão com o banco de dados
            MessageBox.Show("Conexao OK");
            
           
            SQLiteCommand command = connect.CreateCommand();//cria o comando para criar o comando select
            //criar o comando para inserir os dados no banco de dados
            command.CommandText = "INSERT INTO FILMES(NOME, AUTOR, DIRETOR, DATA_LANCAMENTO, GENERO) VALUES (@nome, @autor, @diretor, @data, @genero)";
            command.Parameters.AddWithValue("@nome", "Matrix");//essa chamada de comando ela chama a coluna e troca os parametros escritos aqui
            command.Parameters.AddWithValue("@autor", "ZYX");
            command.Parameters.AddWithValue("@diretor", "ZYC");
            command.Parameters.AddWithValue("@data", "2000-01-11");
            command.Parameters.AddWithValue("@genero", "Ficção");
            command.Prepare();
            command.ExecuteNonQuery();
            command.CommandText = "select *from filmes";
            SQLiteDataReader result = command.ExecuteReader();
            MessageBox.Show("itens localizados:" + result.GetValues().Count);
            while(result.Read())
            {
                int id = result.GetInt32(0);
                string nome = result.GetString(1);
                string autor = result.GetString(2);
                string diretor = result.GetString(3);
                string data = result.GetDateTime(4).ToString();
                string genero = result.GetString(5);
                MessageBox.Show(id.ToString() + " - " + nome + " - " + autor + " - " + diretor + " - " + data + " - " + genero);
            }
            
        }
    }
}
