# RokuECP
.NET Library for communicating with Roku devices over the external control protocol (ECP). Read more at https://developer.roku.com/docs/developer-program/dev-tools/external-control-api.md.

An example of usage:
```csharp
IPAddress playerIpAddress = IPAddress.Parse("192.168.1.69");
RokuPlayer player = new(playerIpAddress);

Console.WriteLine("This device's serial number is {0}.", player.SerialNumber);
// -> This device's serial number is YL003F587777.
```

TODO:
  - Keydown/keyup commands
  - Look into uploading developer channels
  - Add built-in "secret screen" shortcuts (single call rather than SendKeypresses(...))
  - Telnet control support

Known issues:
  - Some secret screen shortcuts cannot be reached using ECP. Needs further research, but Telnet control may be able to do so.
