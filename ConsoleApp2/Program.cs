using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Artigo> listArtigos = new List<Artigo>();
            List<Artigo> listStocks = new List<Artigo>();
            string connectionString =
                @"Data Source=MIGUELBARREIRO\SQLEXPRESS;Initial Catalog=SIG40_EMP_200;"
                + "Integrated Security=true";
            string queryString = "SELECT Art$,Nome1$,Grupo$ from dbo.StcMst";
            string queryStocks = "Select Art$,QEnt,QSai from dbo.StcAcm WHERE Ano = 2017";

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                SqlCommand cmdGetQu = new SqlCommand(queryStocks, connection);
               // command.Parameters.AddWithValue("@pricePoint", paramValue);

                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        Artigo novoArtigo = new Artigo(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                        //Console.WriteLine("Codigo: " + reader[0].ToString() + "\tDescrição: " + reader[1].ToString(),Environment.NewLine);
                        listArtigos.Add(novoArtigo);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    
                    SqlDataReader reader = cmdGetQu.ExecuteReader();
                    Regex rgx = new Regex(@"^00$|^SACOS|^DESCO|^REPARA|^DIVERS|^INSTALA|^ISENT|^PORTE
                                |^FILTRO|^PUNHOAS|^CABOS|^COMISSO|^DESLOC|^11$|^PK-CCO100A");
                    while (reader.Read())
                    {
                        //Artigo novoArtigo = new Artigo(reader[0].ToString(), reader[1].ToString(), 0);
                        //Console.WriteLine("Codigo: " + reader[0].ToString() + "\tDescrição: " + reader[1].ToString(), Environment.NewLine);
                        //listArtigos.Add(novoArtigo);
                        
                        Artigo artigo = new Artigo(reader[0].ToString(),reader[1].ToString(),reader[2].ToString(),"null");
                        if (!rgx.IsMatch(artigo.getRef()) && int.Parse(artigo.getQuantidade()) > 0)
                            listStocks.Add(artigo); 
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                System.IO.StreamWriter fileWriter = new System.IO.StreamWriter(@"D:\referencias.txt");
                foreach (Artigo artStock in listStocks)
                {
                    foreach(Artigo artigo in listArtigos)
                    {
                        if(artigo.getRef() == artStock.getRef())
                        {
                            artStock.setDescricao(artigo.getDescricao());
                            artStock.setGrupo(artigo.getGrupo());
                        }
                    }
                    Console.WriteLine("Ref: " + artStock.getRef() + "\tDesc: " + artStock.getDescricao() + "\tQuantidade:  " +
                        artStock.getQuantidade()+"\tGrupo: "+artStock.getGrupo()+"\tPreço: " + artStock.getPreço(), Environment.NewLine);
                    fileWriter.WriteLine("Ref: " + artStock.getRef() + "\tDesc: " + artStock.getDescricao() + "\tQuantidade:  " +
                        artStock.getQuantidade() + "\tGrupo: " + artStock.getGrupo() + "\tPreço: " + artStock.getPreço());
                }

                
                Console.ReadLine();
                connection.Close();
            }
           



        }
    }
}
