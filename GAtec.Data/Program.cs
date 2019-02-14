using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

using System.Configuration;

namespace GAtec.Data
{
    class Program
    {
        
        public static void AbrirConexao(IDbConnection connection)
        {
            connection.Open();
        }


        static void ExemploLeitura()
        {

            string connectionString = "Server=;Database=;User Id=;Password=;";
            

            SqlConnection conexao = new SqlConnection(connectionString);

            conexao.Open();

            Console.WriteLine("Conexao ok");

            SqlCommand comando = new SqlCommand("select * from ga_saf_plantio; select * from GA_SAF_DIVI4", conexao);

            SqlDataReader leitor = comando.ExecuteReader();

            do
            {
                Console.WriteLine("---------------------------------");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("---------------------------------");

                while (leitor.Read())
                {
                    for (int i = 0; i < leitor.FieldCount; i++)
                    {
                        Console.WriteLine(leitor.GetName(i) + ": " + leitor[i]);
                    }
                    //var data = Convert.ToDateTime(leitor["PLA_DATA"]);
                    //Console.WriteLine(data.ToShortDateString());
                    Console.WriteLine("---------------------------------");

                }
            } while (leitor.NextResult());

            conexao.Close();
        }

        static string connectionString = "Server=;Database=;User Id=;Password=;";
        

        public static void ExemploTransacoes()
        {

            // XConnection
            // XCommand
            // XDataReader
            // XTransaction

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                conexao.Open();

                Console.WriteLine("Conexao ok");

                using (SqlTransaction transacao = conexao.BeginTransaction())
                {

                    try
                    {
                        using (SqlCommand comando = new SqlCommand("INSERT INTO GA_EMPR(COD_EMPR, DSC_EMPR, ABV_EMPR) VALUES (@COD_EMPR, @DSC_EMPR, 'tst')", conexao, transacao))
                        {
                            comando.Parameters.Add("COD_EMPR", SqlDbType.Int).Value = 96;
                            comando.Parameters.Add("DSC_EMPR", SqlDbType.VarChar).Value = "GIBAO";

                            int linhas = comando.ExecuteNonQuery();

                            Console.WriteLine("Linhas afetadas: " + linhas);
                        }

                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }

                conexao.Close();
            }
        }

        static void Main(string[] args)
        {
            
            var projectName = ConfigurationManager.AppSettings[0];

            Console.WriteLine(projectName);

            var cs = ConfigurationManager.ConnectionStrings[0];

            Console.WriteLine(cs.ConnectionString);

            DataTable dt = System.Data.Common.DbProviderFactories.GetFactoryClasses();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine(row["Name"] + " - " + row["InvariantName"]);
            }



            var factory = System.Data.Common.DbProviderFactories.GetFactory("System.Data.SqlClient");

            var connection = factory.CreateConnection();
            connection.ConnectionString = connectionString;

            connection.Open();

            Console.WriteLine("Conexao realizada!");

            var transaction = connection.BeginTransaction();

            try
            {
                var command = connection.CreateCommand();

                command.Transaction = transaction;
                command.CommandText = "INSERT INTO GA_EMPR(COD_EMPR, DSC_EMPR, ABV_EMPR) VALUES (@COD_EMPR, @DSC_EMPR, 'tst')";

                var codEmprParameter = command.CreateParameter();
                codEmprParameter.ParameterName = "COD_EMPR";
                codEmprParameter.Value = 91;

                command.Parameters.Add(codEmprParameter);

                var dscParameter = command.CreateParameter();
                dscParameter.ParameterName = "DSC_EMPR";
                dscParameter.Value = "Empresa ABC";

                command.Parameters.Add(dscParameter);
                
                int linhas = command.ExecuteNonQuery();

                Console.WriteLine("Linhas afetadas: " + linhas);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                Console.WriteLine(ex.Message);
            }
            

            connection.Close();

            Console.ReadLine();
            

        }
    }
}
