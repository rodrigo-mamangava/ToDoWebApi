using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoWebApi.Entidades
{
    public class Tarefa
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual bool Concluido { get; set; }
    }
}