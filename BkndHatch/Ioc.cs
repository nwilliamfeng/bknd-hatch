using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Autofac;
using System.IO;

namespace BkndHatch
{
    public static class Ioc
    {
        private static IContainer container;

        static Ioc()
        {
            if (container == null)
                Resolve();
        }

        private static void Resolve()
        {
            
            var assemblys = Directory.GetFiles(Directory.GetCurrentDirectory())
                .Select(x=>new FileInfo(x))
                .Where(x => x.Name.Contains(typeof(Ioc).Namespace) && x.Extension== ".dll" )
                .Select(x => Assembly.LoadFrom(x.FullName))
                .ToArray();
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
            container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
