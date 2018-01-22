using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoWebApi.Infra;

namespace ToDoWebApi.Models
{
    public class Tarefa
    {


        public  int Id { get; set; }
        public  string Descricao { get; set; }
        public  bool Concluido { get; set; }

        public Tarefa()
        {

        }

        public Tarefa(int id, string descricao, bool concluido)
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Concluido = concluido;
        }

    }


    
}