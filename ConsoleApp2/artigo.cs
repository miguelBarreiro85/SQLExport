using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Artigo
    {
        private String referencia;
        private String descricao;
        private string quantidade;
        private string grupo;
        private string preco;
        public  Artigo(String referen, String Desc, String Qu, String gru, String prec)
        {
            this.referencia = referen;
            this.descricao = Desc;
            this.quantidade = Qu;
            this.grupo = gru;
            this.preco = prec;
        }
        public Artigo(String referen, String Desc, String gru)
        {
            this.referencia = referen;
            this.descricao = Desc;
            this.grupo = gru;
        }
        public Artigo(String referenc,String QEntrada,String QSaida,string anulo)
        {
            this.referencia = referenc;
            this.quantidade = (int.Parse(QEntrada)-int.Parse(QSaida)).ToString();
        }
        public String getGrupo()
        {
            return this.grupo;
        }
        public String getPreço()
        {
            return this.preco;
        }
        public String getRef()
        {
            return this.referencia;
        }
        public String getDescricao()
        {
            return this.descricao;
        }
        public String getQuantidade()
        {
            return this.quantidade;
        }

        public void setDescricao(String descri)
        {
            this.descricao = descri;
        }
        public void setGrupo(string grup)
        {
            this.grupo = grup;
        }
    }
}
