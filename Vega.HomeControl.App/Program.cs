using Vega.HomeControl.Engine;

namespace Vega.HomeControl.App
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var vegaManager = new VegaHomeManager();
            await vegaManager.Start();
        }
    }
}