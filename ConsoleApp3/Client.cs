using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Client
    {
        private String PrimeiroNome;
        private String UltimoNome;
        private String ID;
        private String Telefone;
        private String Telemovel;
        private bool IsValid;
        public Client(string ClientID,string Telefone,string Telem)
        {
            //Apanha espaços letras barras etc..
            string pattern = @"\s|[a-zA-Z]|\(|\)|\/|\-";
            string numFixos = @"^[2]";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            Regex rgxFixos = new Regex(numFixos);
            //se ficarem dois numeros colados fica apenas com o primeiro
            if (rgx.Replace(Telem, replacement).Length > 9) {
                this.Telemovel = rgx.Replace(Telem, replacement).Substring(0, 9);
                if (rgxFixos.IsMatch(this.Telemovel))
                    this.Telemovel = "";
            }
            else
            {
                this.Telemovel = rgx.Replace(Telem, replacement);
                if (rgxFixos.IsMatch(this.Telemovel))
                    this.Telemovel = "";
            }

            if (rgx.Replace(Telefone, replacement).Length > 9)
            {
                this.Telefone = rgx.Replace(Telefone, replacement).Substring(0, 9);
                if (rgxFixos.IsMatch(this.Telefone))
                    this.Telefone = "";
            }
            else
            {
                this.Telefone = rgx.Replace(Telefone, replacement);
                if (rgxFixos.IsMatch(this.Telefone))
                    this.Telefone = "";
            }
            
            //= Telefone.Replace("\\s+|\\w+|\\-","");
            //=Telem.Replace("\\s+|\\w+|\\-","");

            this.ID = ClientID;
             
        }
        public String GetTelefone()
        {
            return this.Telefone;
        }
        public String GetTelemovel()
        {
            return this.Telemovel;
        }
        public String GetID()
        {
            return this.ID;
        }
        public String GetPrimNome()
        {
            return this.PrimeiroNome;
        }
        public String GetLastName()
        {
            return this.UltimoNome;
        }
    }
}
