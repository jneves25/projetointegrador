using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Projeto_integrador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private
            MySqlConnectionStringBuilder conexaoBanco()
        {
            MySqlConnectionStringBuilder conexaoBD = new MySqlConnectionStringBuilder();
            conexaoBD.Server = "localhost";
            conexaoBD.Database = "locadora";
            conexaoBD.UserID = "root";
            conexaoBD.Password = "";
            return conexaoBD;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            atualizarGrid();
        }

        private void buttonSalvar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString()); try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "INSERT INTO filme (nomeFilme,generoFilme,anoFilme) " +
                    "VALUES('" + textBoxNome.Text + "', '" + textBoxGenero.Text + "', " + Convert.ToInt16(textBoxAno.Text) + ")";
                comandoMySql.ExecuteNonQuery();

                realizaConexacoBD.Close();
                MessageBox.Show("Inserido!");
                limparCampos();
                atualizarGrid();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void buttonLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void limparCampos()
        {
            textBoxID.Clear();
            textBoxAno.Clear();
            textBoxGenero.Clear();
            textBoxNome.Clear();
        }

        private void buttonAtualizar_Click(object sender, EventArgs e)
        {
            {
                MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
                MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
                try
                {
                    realizaConexacoBD.Open(); 

                    MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand(); 
                    comandoMySql.CommandText = "UPDATE filme SET nomeFilme = '" + textBoxNome.Text + "', " +
                        "generoFilme = '" + textBoxGenero.Text + "', " +
                        "anoFilme = '" + textBoxAno.Text + "' WHERE idFilme = " + textBoxID.Text + "";
                    comandoMySql.ExecuteNonQuery();

                    realizaConexacoBD.Close();
                    MessageBox.Show("Atualizado!");
                    atualizarGrid();
                    limparCampos();
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.CurrentRow.Selected = true;
                
                textBoxNome.Text = dataGridView1.Rows[e.RowIndex].Cells["ColumnNome"].FormattedValue.ToString();
                textBoxGenero.Text = dataGridView1.Rows[e.RowIndex].Cells["ColumnGenero"].FormattedValue.ToString();
                textBoxAno.Text = dataGridView1.Rows[e.RowIndex].Cells["ColumnAno"].FormattedValue.ToString();
                textBoxID.Text = dataGridView1.Rows[e.RowIndex].Cells["ColumnID"].FormattedValue.ToString();
            }
        }

            private void atualizarGrid()

        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open();

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand();
                comandoMySql.CommandText = "SELECT * FROM filme WHERE inativoFilme = 0";
                MySqlDataReader reader = comandoMySql.ExecuteReader();

                dataGridView1.Rows.Clear();

                while (reader.Read())
                {
                    DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                    row.Cells[0].Value = reader.GetInt32(0);
                    row.Cells[1].Value = reader.GetString(1);
                    row.Cells[3].Value = reader.GetString(2);
                    row.Cells[2].Value = reader.GetString(3);
                    dataGridView1.Rows.Add(row);
                }

                realizaConexacoBD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! ");
                Console.WriteLine(ex.Message);
            }
        }

        private void buttonDeletar_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder conexaoBD = conexaoBanco();
            MySqlConnection realizaConexacoBD = new MySqlConnection(conexaoBD.ToString());
            try
            {
                realizaConexacoBD.Open(); 

                MySqlCommand comandoMySql = realizaConexacoBD.CreateCommand(); 
                
                comandoMySql.CommandText = "UPDATE filme SET inativoFilme = 1 WHERE idFilme = " + textBoxID.Text + "";
                comandoMySql.ExecuteNonQuery();

                realizaConexacoBD.Close(); 
                MessageBox.Show("Deletado!"); 
                atualizarGrid();
                limparCampos();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
    }
}