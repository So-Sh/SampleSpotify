using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleSpotify.Services;
using SampleSpotify.Utilities.Http;

namespace SampleSpotify.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container,
        Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
             Component
                 .For<IHttpClient>()
                 .ImplementedBy<HttpClient>()
                 .LifestyleTransient());

            container.Register(
                Component
                .For<ISpotifyService>()
                .ImplementedBy<SpotifyService>()
                .LifestyleTransient());
        }
    }
}
