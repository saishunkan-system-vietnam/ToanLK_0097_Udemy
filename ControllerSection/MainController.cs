using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Serialization;

namespace ControllerSection
{
    public static class MainController
    {
        public static void Program(WebApplication app) {
            // map all attribute ([route],...)
            app.MapControllers();
        }
    }
}
