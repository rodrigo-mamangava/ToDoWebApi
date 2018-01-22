using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoWebApi.Controllers;
using ToDoWebApi.Entidades;

namespace ToDoWebApi.Tests
{
    [TestClass]
    public class TestToDoController
    {

        public int idTarefaTeste = 122;

        [TestMethod]
        [Priority(0)]
        public void TestPostNovaTarefa()
        {
            // Arrange
            ToDoController controller = new ToDoController();

            controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/todo")
            };
            controller.Configuration = new HttpConfiguration();
            controller.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "todo" } });

            // Act
            Tarefa novaTarefa = new Tarefa() {  Descricao = "Nova tarefa de teste 01", Concluido = false };
            var response = controller.Post(novaTarefa);

            var uriResposta = response.Headers.Location.AbsoluteUri;
            var codigoResposta = response.StatusCode.ToString();

            var idNovaTarefa = uriResposta.ToString();
            idNovaTarefa = idNovaTarefa.Remove(0, 26);
            this.idTarefaTeste = Int32.Parse(idNovaTarefa); 

            // Assert            
            Assert.AreEqual("Created", codigoResposta);
            Assert.AreEqual("http://localhost/api/todo/"+idNovaTarefa , uriResposta);

        }

        [TestMethod]
        public void TestGetSemParematro()
        {
            // Arrange
            var controller = new ToDoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get();
            var codigoResposta = response.StatusCode.ToString();

            // Assert
            Assert.AreEqual("OK", codigoResposta);
        }

        [TestMethod]
        public void TestGetPassandoIdValida()
        {
            // Arrange
            var idTarefaTeste = this.idTarefaTeste;
            var controller = new ToDoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            // Act
            var response = controller.Get(idTarefaTeste);
            var codigoResposta = response.StatusCode.ToString();

            // Assert
            Assert.AreEqual("OK", codigoResposta);
        }

        [TestMethod]
        public void TestGetPassandoIdInvalida()
        {
            // Arrange
            int idTarefa = -1;
            var controller = new ToDoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get(idTarefa);
            var codigoResposta = response.StatusCode.ToString();

            // Assert
            Assert.AreEqual("NotFound", codigoResposta);
        }

        [TestMethod]
        public void TestPutValdio()
        {
            // Arrange
            var idTarefaTeste = this.idTarefaTeste;
            ToDoController controller = new ToDoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            Tarefa novaTarefa = new Tarefa() {Id = idTarefaTeste, Descricao = "Nova tarefa de teste 01", Concluido = true };
            var response = controller.Put(novaTarefa);

            var codigoResposta = response.StatusCode.ToString();


            // Assert            
            Assert.AreEqual("OK", codigoResposta);

        }

        [TestMethod]
        public void TestPutInvaldio()
        {
            // Arrange
            ToDoController controller = new ToDoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            Tarefa novaTarefa = new Tarefa() { Id = -1, Descricao = "Nova tarefa de teste 01", Concluido = true };
            var response = controller.Put(novaTarefa);

            var codigoResposta = response.StatusCode.ToString();


            // Assert            
            Assert.AreEqual("NotFound", codigoResposta);

        }

        [TestMethod]
        public void TestDeleteIdValida()
        {
            // Arrange
            int idTarefa = this.idTarefaTeste;
            var controller = new ToDoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Delete(idTarefa);
            var codigoResposta = response.StatusCode.ToString();

            // Assert
            Assert.AreEqual("OK", codigoResposta);
        }

        [TestMethod]
        public void TestDeleteIdInvalida()
        {
            // Arrange
            int idTarefa = -1;
            var controller = new ToDoController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Delete(idTarefa);
            var codigoResposta = response.StatusCode.ToString();

            // Assert
            Assert.AreEqual("NotFound", codigoResposta);
        }

    }
}
