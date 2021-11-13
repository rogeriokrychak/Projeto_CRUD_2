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
        SQLiteConnection connect = new SQLiteConnection("Data Source = C:\\bancos\\Banco_CSharp.db; Version=3;");

        public Form1()
        {
            InitializeComponent();
        }

        private void btnTeste_Click(object sender, EventArgs e)//inserção de dados de forma estática
        {
            
            connect.Open();//abre a conexão com o banco de dados
            MessageBox.Show("Conexao OK");
            /*SQLiteCommand command = connect.CreateCommand();//cria o comando para criar o comando select
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
           /* while(result.Read())
            {
                int id = result.GetInt32(0);
                string nome = result.GetString(1);
                string autor = result.GetString(2);
                string diretor = result.GetString(3);
                string data = result.GetDateTime(4).ToString();
                string genero = result.GetString(5);
                MessageBox.Show(id.ToString() + " - " + nome + " - " + autor + " - " + diretor + " - " + data + " - " + genero);
            }*/
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
            connect.Open();//abre a conexão com o banco de dados
            MessageBox.Show("Conexao OK");
            SQLiteCommand command = connect.CreateCommand();//cria o comando para criar o comando select
            //criar o comando para inserir os dados no banco de dados
            command.CommandText = "INSERT INTO FILMES(NOME, AUTOR, DIRETOR, DATA_LANCAMENTO, GENERO) VALUES (@nome, @autor, @diretor, @data, @genero)";
            command.Parameters.AddWithValue("@nome", campoNome.Text);//essa chamada de comando ela chama a coluna e troca os parametros escritos aqui
            command.Parameters.AddWithValue("@autor", campoAutor.Text);
            command.Parameters.AddWithValue("@diretor", campoDiretor.Text);
            command.Parameters.AddWithValue("@data", campoDataLancamento.Text);
            command.Parameters.AddWithValue("@genero", campoGenero.Text);
            command.Prepare();
            command.ExecuteNonQuery();
            connect.Close();
                MessageBox.Show("Cadastro inserido!!!");
            }catch (Exception)
            {
                MessageBox.Show("Falha ao Salvar!!!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect.Open();//abre a conexão com o banco de dados
            SQLiteCommand command = connect.CreateCommand();//cria o comando para criar o comando select
            command.CommandText = "select *from filmes where id = @id";//faz um select ou consulta pelo id
            command.Parameters.AddWithValue("@id", campoID.Text);
            SQLiteDataReader result = command.ExecuteReader();
           
            if (result.HasRows)
            {
                while (result.Read())
                {
                    campoNome.Text = result.GetString("nome");
                    campoAutor.Text = result.GetString("autor");
                    campoDiretor.Text = result.GetString("diretor");
                    campoDataLancamento.Text = result.GetString("data_lancamento");
                    campoGenero.Text = result.GetString("genero");
                }
            } else
            {
                MessageBox.Show("Id não está cadastrado!!!!");
            }
            connect.Close();//fecha a conexão com o banco de dados
        }

        private void button3_Click(object sender, EventArgs e)//comando para fazer o relatório
        {
            connect.Open();//abre a conexão com o banco de dados
            SQLiteCommand command = connect.CreateCommand();//cria o comando para manipular os dados
            command.CommandText = "select *from filmes where id like id order by id ";//faz um select
            //ou consulta e mostra um relatório dentro do gridview pelo id em ordem decrescente
            SQLiteDataReader result = command.ExecuteReader();
            dataGridView1.Rows.Clear();//limpa as linhas de consulta dentro do gridview
            while (result.Read())
            {
                dataGridView1.Rows.Add(new string[] {
                    result.GetInt32("id").ToString(),
                    result.GetString("nome"),
                    result.GetString("autor"),
                    result.GetString("diretor"),
                    result.GetString("data_lancamento"),
                    result.GetString("genero")

                });
            }
            connect.Close();//fecha a conexão com o banco de dados
        }

        private void button4_Click(object sender, EventArgs e)// este é o comando para alterar os dados
        {
            try
            {
                connect.Open();//abre a conexão com o banco de dados
                MessageBox.Show("Conexao OK");
                SQLiteCommand command = connect.CreateCommand();
                command.CommandText = "update filmes set nome=@nome,autor=@autor,diretor=@diretor,data_lancamento=@data,genero=@genero where id=@id";
                command.Parameters.AddWithValue("id", campoID.Text);
                command.Parameters.AddWithValue("@nome", campoNome.Text);
                command.Parameters.AddWithValue("@autor", campoAutor.Text);
                command.Parameters.AddWithValue("@diretor", campoDiretor.Text);
                command.Parameters.AddWithValue("@data", campoDataLancamento.Text);
                command.Parameters.AddWithValue("@genero", campoGenero.Text);
                command.Prepare();
                command.ExecuteNonQuery();
                
                MessageBox.Show("Cadastro alterado!!!");
            }
            catch (Exception)
            {
                MessageBox.Show("Falha ao Alterar!!!");
            }
            connect.Close();//fecha a conexão com o banco
        }
    }
}
