using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoWebApi.Entidades;
using ToDoWebApi.Infra;

namespace ToDoWebApi.DAO
{
    public class TarefaDAO
    {
        private ISession session;


        public TarefaDAO(ISession session)
        {
            this.session = session;
        }

        public void Adicionar(Tarefa tarefa)
        {
            ITransaction transacao = session.BeginTransaction();
            session.Save(tarefa);
            transacao.Commit();
        }

        public bool Remover(int id)
        {
            ITransaction transacao = session.BeginTransaction();
            Tarefa tarefa = session.Get<Tarefa>(id);

            if (tarefa != null)
            {
                session.Delete(tarefa);
                transacao.Commit();
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool Atualizar(Tarefa tarefaEnviada)
        {
            Tarefa tarefa = session.Get<Tarefa>(tarefaEnviada.Id);
            if (tarefa != null)
            {
                ITransaction transacao = session.BeginTransaction();
                tarefa = tarefaEnviada;
                session.Merge(tarefa);
                transacao.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tarefa BuscarPorId(int id)
        {
            return session.Get<Tarefa>(id);
        }

        public IList<Tarefa> Listar()
        {
             
            String hqp = "from Tarefa";
            IQuery query = session.CreateQuery(hqp);
            IList<Tarefa> tarefas = query.List<Tarefa>();


            return tarefas;
        }



    }
}