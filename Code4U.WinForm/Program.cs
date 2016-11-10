using Autofac;
using Autofac.Features.Variance;
using AutoMapper;
using Code4U.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Code4U.WinForm
{
    static class Program
    {
        public const string GENERAL_CATEGORY = "Geral";
        public const string BEHAVIOR_CATEGORY = "Behavior";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var builder = new ContainerBuilder();

            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(typeof(RunTemplate).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterInstance(Console.Out).As<TextWriter>();

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();

                return t => c.Resolve(t);
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();

                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterType<FrmPrincipal>().AsSelf();
            builder.RegisterType<FrmDatabaseSchemaSettings>().AsSelf();

            AutoMapperConfiguration.SetUp();

            Application.Run(builder.Build().Resolve<FrmPrincipal>());
        }
    }
}
