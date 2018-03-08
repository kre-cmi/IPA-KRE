﻿using Topshelf;

namespace CMI.Host.ExampleServiceA
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ExampleServiceA>(s =>
                {
                    s.ConstructUsing(name => new ExampleServiceA());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("The example service is used to mock a running service in the live environement.");
                x.SetDisplayName("CMI Viaduc Example Service");
                x.SetServiceName("CMIExampleService");
            });
        }
    }
}