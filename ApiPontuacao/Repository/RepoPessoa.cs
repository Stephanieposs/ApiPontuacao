using ApiPontuacao.Models;
using System.Data.SqlClient;

namespace ApiPontuacao.Repository
{
    public class RepoPessoa : IRepository<Pessoa, int>
    {
        private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Pessoa;Integrated Security=True";
        //private List<Pessoa> clienteList = new List<Pessoa>();

        public Pessoa Save(Pessoa entity)
        {
            string insertQuery = "insert into tb_Pessoa(Nome,Cpf,Pontos)values(@Nome,@Cpf,@Pontos);SELECT SCOPE_IDENTITY();";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(insertQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Nome", entity.Nome);
                    command.Parameters.AddWithValue("@Cpf", entity.Cpf);
                    command.Parameters.AddWithValue("@Pontos", 0);

                    connection.Open();
                    entity.Id = Convert.ToInt32(command.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return entity;
            }
        }

        public IEnumerable<Pessoa> GetAll()
        {
            List<Pessoa> clientes = new List<Pessoa>();
            string selectQuery = "SELECT Id, Nome, Cpf, Pontos FROM tb_Pessoa"; // Correção aqui
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Pessoa()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Cpf = reader.GetString(reader.GetOrdinal("Cpf")),
                                Pontos = reader.GetInt32(reader.GetOrdinal("Pontos"))
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return clientes;
        }

        public Pessoa Get(int id)
        {
            Pessoa cliente = null;
            string selectQuery = "SELECT Id, Nome, Cpf, Pontos FROM tb_Pessoa WHERE Id = @Id"; // Correção aqui
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(selectQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Pessoa
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                Cpf = reader.GetString(reader.GetOrdinal("Cpf")),
                                Pontos = reader.GetInt32(reader.GetOrdinal("Pontos"))
                            };
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro: " + ex.Message);
                }
            }
            return cliente;
        }

        public Pessoa Update(Pessoa entity)
        {
            string updateQuery = "UPDATE tb_Pessoa SET Nome = @Nome, Cpf = @Cpf WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Id", entity.Id);
                    command.Parameters.AddWithValue("@Nome", entity.Nome);
                    command.Parameters.AddWithValue("@Cpf", entity.Cpf);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


            }
            return entity;

        }

        public void Delete(int id)
        {
            string deleteQuery = "DELETE FROM tb_Pessoa WHERE Id = @Id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(deleteQuery, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Pessoa excluída com sucesso.");
                }
                catch (Exception ex)
                {
                    throw new Exception($"Could not delete {ex.Message}");
                }
            }
        }

        public void IncrementarPontos(int id, int pontos)
        {
            string updateQuery = "UPDATE tb_Pessoa SET Pontos = Pontos + @Pontos WHERE Id = @Id;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Pontos", pontos);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao incrementar pontos: {ex.Message}");
                }
            }
        }
    }
}
