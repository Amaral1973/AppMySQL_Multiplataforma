using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySqlConnector;
using AppMySQL.Models;

namespace AppMySQL.Controller
{
    public class MySQLCon
    {
        static string conn = @"server=sql.freedb.tech;port=3306;database=freedb_testemulti;user id=freedb_abc321973;password=t8PtvCFeR3s?69r;charset=utf8";

        public static List<Pessoa> ListarPessoas()
        {
            List<Pessoa> listapessoas = new List<Pessoa>();
            string sql = "SELECT * FROM pessoa";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pessoa pessoa = new Pessoa()
                            {
                                id = reader.GetInt32(0),
                                nome = reader.GetString(1),
                                idade = reader.GetString(2),
                                celular = reader.GetString(3)
                            };
                            listapessoas.Add(pessoa);
                        }
                    }
                }
                con.Close();
                return listapessoas;
            }
        }

        public static void InserirPessoa(string nome, string idade, string celular)
        {
            string sql = "INSERT INTO pessoa(nome, idade, celular) VALUES (@nome, @idade, @celular)";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                    cmd.Parameters.Add("@idade", MySqlDbType.VarChar).Value = idade;
                    cmd.Parameters.Add("@celular", MySqlDbType.VarChar).Value = celular;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public static void AtualizarPessoa(Pessoa pessoa)
        {
            string sql = "UPDATE pessoa SET nome=@nome, idade=@idade, celular=@celular WHERE id=@id";
            try
            {
                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = pessoa.nome;
                        cmd.Parameters.Add("@idade", MySqlDbType.VarChar).Value = pessoa.idade;
                        cmd.Parameters.Add("@celular", MySqlDbType.VarChar).Value = pessoa.celular;
                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = pessoa.id;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ExcluirPessoa(Pessoa pessoa)
        {
            string sql = "DELETE FROM pessoa WHERE id=@id";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = pessoa.id;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}