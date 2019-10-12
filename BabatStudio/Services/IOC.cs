﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace BabatStudio.Services
{
    public class IoC
    {
        private static IoC _reference = new IoC();
        public static IoC Reference { get { return _reference; } }

        private IContainer container;
        private ContainerBuilder builder;

        private IoC()
        {
            builder = new ContainerBuilder();
        }

        public IoC Register<TImplementation, TInterface>()
        {
            builder.RegisterType<TImplementation>().As<TInterface>();
            return Reference;
        }

        public IoC Register<TImplementation>()
        {
            builder.RegisterType<TImplementation>();
            return Reference;
        }

        public void Build()
        {
            container = builder.Build();
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }
    }
}
