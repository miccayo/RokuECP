using RokuECP;
using System.Net;
using System.Reflection;

namespace ECP_Example
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            RokuPlayer myPlayer = new(IPAddress.Parse("192.168.1.69"));
            Console.WriteLine("This device's serial number is {0}.", myPlayer.SerialNumber);
        }
    }
}
