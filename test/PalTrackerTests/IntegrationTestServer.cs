using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using PalTracker;

namespace PalTrackerTests
{
    public static class IntegrationTestServer
    {
        //public static TestServer Start() =>
            //new WebApplicationFactory<Program>().Server;

            public static TestServer Start()
            {
                if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("WELCOME_MESSAGE")))
                {
                    System.Environment.SetEnvironmentVariable("WELCOME_MESSAGE", "Default message Test");
                }

                if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("PORT")))
                {
                    System.Environment.SetEnvironmentVariable("PORT", "port-123");
                }

                if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("MEMORY_LIMIT")))
                {
                    System.Environment.SetEnvironmentVariable("MEMORY_LIMIT", "500GB");
                }

                if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("CF_INSTANCE_INDEX")))
                {
                    System.Environment.SetEnvironmentVariable("CF_INSTANCE_INDEX", "1");
                }

                if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("CF_INSTANCE_ADDR")))
                {
                    System.Environment.SetEnvironmentVariable("CF_INSTANCE_ADDR", "add1234");
                }

                return new WebApplicationFactory<Program>().Server;
            }
    }
}