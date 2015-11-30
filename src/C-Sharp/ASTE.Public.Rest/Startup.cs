using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ASTE.Public.Rest.Startup))]

namespace ASTE.Public.Rest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
