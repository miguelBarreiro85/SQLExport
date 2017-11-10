using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Client> listClient = new List<Client>();
            List<Client> listNome = new List<Client>();
            string connectionString =
                @"Data Source=MIGUELBARREIRO\SQLEXPRESS;Initial Catalog=SIG40_EMP_200;"
                + "Integrated Security=true";
            string queryString = "SELECT Ct$,Telef$,Telem$,Grupo$ from dbo.CtaAng";
            string queryNomes = "Select Ct$,Nome$ from dbo.CtaMst";

            using (SqlConnection connection =
            new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                //SqlCommand commandGetName = new SqlCommand(queryNomes);
                SqlCommand commandGetPhone = new SqlCommand(queryString, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = commandGetPhone.ExecuteReader();

                    while (reader.Read())
                    {
                        Client novoCliente = new Client(reader[0].ToString(),reader[1].ToString(),
                            reader[2].ToString());
                        Regex rgx = new Regex(@"^[9]");
                        if(rgx.IsMatch(novoCliente.GetTelefone())||rgx.IsMatch(novoCliente.GetTelemovel()))
                            listClient.Add(novoCliente);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //listaClientes(listClient);
            for (int i = 0; i <= listClient.Count;i+=450)
            {
                Console.WriteLine((listClient.Count() / 450).ToString());
                Console.WriteLine(i);
                using (var file = File.CreateText(@"D:\numeros" + i + ".txt"))
                {
                    for(int j=i;j<=(i+450);j++)
                    {
                        if (listClient[j].GetTelefone() != "")
                        {
                            file.WriteLine(listClient[j].GetTelefone() + ",");
                            Console.WriteLine(listClient[j].GetTelefone() + "\n");
                        }
                        if (listClient[j].GetTelemovel() != "")
                        {
                            file.WriteLine(listClient[j].GetTelemovel() + ",");
                            Console.WriteLine(listClient[j].GetTelemovel() + "\n");
                        }
                    }
                }
            }
        }

        private static void ListaClientes(List<Client> listClient)
        {
            foreach (Client cli in listClient)
            {
                Console.WriteLine("ID: " + cli.GetID() + "\tTelef: " + cli.GetTelefone() +
                    "\tTelemovel: " + cli.GetTelemovel());
            }
        }
    }
}
