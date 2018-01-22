using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using NHibernate.Tool.hbm2ddl;
using NHibernate;

namespace ToDoWebApi.Infra
{
    public class NHibernateHelper
    {

        private static ISessionFactory fabrica = CriaSessionFactory();

        private static ISessionFactory CriaSessionFactory()
        {
            Configuration cfg = RecuperaConfiguracao();
            return cfg.BuildSessionFactory();
        }

        public static Configuration RecuperaConfiguracao()
        {
            Configuration cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(Assembly.GetExecutingAssembly());

            return cfg;
        }

        public static void GeraSchema()
        {
            Configuration cfg = RecuperaConfiguracao();
            new SchemaExport(cfg).Create(true, true);
        }

        public static ISession AbreSession()
        {
            return fabrica.OpenSession();
        }

    }
}