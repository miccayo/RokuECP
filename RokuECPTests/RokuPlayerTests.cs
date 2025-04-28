using System.Net;

namespace RokuECP.Tests
{
    [TestClass()]
    public class RokuPlayerTests
    {
        // To run tests successfully, we need an actual Roku device IP and an invalid (non-Roku) device
        private readonly IPAddress ValidIpAddress = IPAddress.Parse("192.168.1.69");
        private readonly ushort ValidPort = 8060;
        private readonly IPAddress InvalidIpAddress = IPAddress.Parse("10.0.0.1");
        private readonly ushort InvalidPort = 65535;

        [TestMethod]
        public void InitializePlayerWithValidIp()
        {
            RokuPlayer player = new(ValidIpAddress);
            Assert.IsTrue(player.UDN != string.Empty);
        }

        [TestMethod]
        public void InitializePlayerWithValidIpAndValidPort()
        {
            RokuPlayer player = new(ValidIpAddress);
            Assert.IsTrue(player.UDN != string.Empty);
        }

        [TestMethod]
        public void InitializePlayerWithInvalidIp()
        {
            Assert.ThrowsException<TaskCanceledException>(() =>
            {
                RokuPlayer player = new(InvalidIpAddress);
            });
        }

        [TestMethod]
        public void InitializePlayerWithValidIpAndInvalidPort()
        {
            Assert.ThrowsException<HttpRequestException>(() =>
            {
                RokuPlayer player = new(ValidIpAddress, InvalidPort);
            });
        }

        [TestMethod]
        public void InitializePlayerWithInvalidIpAndValidPort()
        {
            Assert.ThrowsException<TaskCanceledException>(() => { _ = new RokuPlayer(InvalidIpAddress, ValidPort); });
        }

        [TestMethod]
        public void InitializePlayerWithInvalidIpAndInvalidPort()
        {
            Assert.ThrowsException<TaskCanceledException>(() => { _ = new RokuPlayer(InvalidIpAddress, InvalidPort); });
        }
    }
}