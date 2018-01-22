using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ToDoWebApi.DAO;
using ToDoWebApi.Entidades;
using ToDoWebApi.Infra;

namespace ToDoWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ToDoController : ApiController
    {
        public HttpResponseMessage Get()
        {
            ISession session = NHibernateHelper.AbreSession();
            TarefaDAO tarefaDAO = new TarefaDAO(session);
            IList<Tarefa> tarefas = tarefaDAO.Listar();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, tarefas);

            return response;
        }

        public HttpResponseMessage Get([FromUri] int id)
        {

            ISession session = NHibernateHelper.AbreSession();
            TarefaDAO tarefaDAO = new TarefaDAO(session);

            Tarefa tarefa = tarefaDAO.BuscarPorId(id);

            session.Close();

            if(tarefa == null)
            {
                string mensagem = string.Format("Tarefa {0} não foi encontrada", id);
                HttpError error = new HttpError(mensagem);
                return Request.CreateResponse(HttpStatusCode.NotFound, error);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, tarefa);
            }
            

        }
        

        public HttpResponseMessage Post ([FromBody] Tarefa tarefa)
        {

            if (tarefa == null) {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
            }

            ISession session = NHibernateHelper.AbreSession();
            TarefaDAO tarefaDAO = new TarefaDAO(session);
            tarefaDAO.Adicionar(tarefa);
            session.Close();
            
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            string location = Url.Link("DefaultApi", new { controller = "todo", id = tarefa.Id });
            response.Headers.Location = new Uri(location);

            return response;
        }

        public HttpResponseMessage Delete( [FromUri] int id)
        {
            ISession session = NHibernateHelper.AbreSession();
            TarefaDAO tarefaDAO = new TarefaDAO(session);
            bool resultado = tarefaDAO.Remover(id);
            session.Close();

            if (resultado == false)
            {
                string mensagem = string.Format("Tarefa {0} não foi encontrada", id);
                HttpError error = new HttpError(mensagem);
                return Request.CreateResponse(HttpStatusCode.NotFound, error);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }


        }

        public HttpResponseMessage Put([FromBody] Tarefa tarefa)
        {

            if (tarefa == null)
            {
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed);
            }


            ISession session = NHibernateHelper.AbreSession();
            TarefaDAO tarefaDAO = new TarefaDAO(session);
            bool resultado = tarefaDAO.Atualizar(tarefa);
            session.Close();

            if (resultado == false)
            {
                string mensagem = string.Format("Tarefa não foi encontrada");
                HttpError error = new HttpError(mensagem);
                return Request.CreateResponse(HttpStatusCode.NotFound, error);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        



    }
}
