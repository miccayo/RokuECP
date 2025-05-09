using RokuECP;
using System.Net;

namespace ECP_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            RokuPlayer myPlayer = new(IPAddress.Parse("192.168.1.69"));
            myPlayer.SendKeypress(RokuPlayer.KeypressType.Home);
            Console.WriteLine("This device's serial number is {0}.", myPlayer.SerialNumber);

            foreach (RokuApp app in myPlayer.InstalledApps)
            {
                Console.WriteLine("App ID: {0}, Type: {1}, Version: {2}, Name: {3}", app.AppId, app.AppType, app.AppVersion, app.AppName);
            }
        }
    }
}
