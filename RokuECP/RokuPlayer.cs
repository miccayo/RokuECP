using System.Net;
using System.Xml.Linq;

namespace RokuECP
{
    public class RokuPlayer
    {
        #region "Enums"
        public enum KeypressType : byte
        {
            // All devices should support these keypresses (0-16)
            Home = 0,
            Rewind = 1,
            Forward = 2,
            PlayPause = 3,
            Select = 4,
            Left = 5,
            Right = 6,
            Down = 7,
            Up = 8,
            Back = 9,
            InstantReplay = 10,
            Info = 11,
            Backspace = 12,
            Search = 13,
            Enter = 14,
            // Certain devices (TVs, mainly) support these keypresses (16-32)
            VolumeDown = 16,
            VolumeUp = 17,
            VolumeMute = 18,
            PowerOff = 19,
            ChannelUp = 20,
            ChannelDown = 21,
            InputTuner = 22,
            InputHDMI1 = 23,
            InputHDMI2 = 24,
            InputHDMI3 = 25,
            InputHDMI4 = 26,
            InputAV1 = 27,
            FindRemote = 28
        }
        #endregion

        #region "Public Properties"
        public string UDN { get; private set; } = string.Empty;
        public string SerialNumber { get; private set; } = string.Empty;
        public string DeviceId { get; private set; } = string.Empty;
        public string AdvertisingId { get; private set; } = string.Empty;
        public string VendorName { get; private set; } = string.Empty;
        public string ModelName { get; private set; } = string.Empty;
        public string ModelNumber { get; private set; } = string.Empty;
        public string ModelRegion { get; private set; } = string.Empty;
        public bool? IsTv { get; private set; } = null;
        public bool? IsStick { get; private set; } = null;
        public bool? MobileHasLiveTv { get; private set; } = null;
        public string UIResolution { get; private set; } = string.Empty;
        public bool? SupportsEthernet { get; private set; } = null;
        public string WifiMac { get; private set; } = string.Empty;
        public string WifiDriver { get; private set; } = string.Empty;
        public bool? HasWifi5GSupport { get; private set; } = null;
        public string BluetoothMac { get; private set; } = string.Empty;
        public string Wifi2Mac { get; private set; } = string.Empty;
        public string NetworkType { get; private set; } = string.Empty;
        public string NetworkName { get; private set; } = string.Empty;
        public string FriendlyDeviceName { get; private set; } = string.Empty;
        public string FriendlyModelName { get; private set; } = string.Empty;
        public string DefaultDeviceName { get; private set; } = string.Empty;
        public string UserDeviceName { get; private set; } = string.Empty;
        public string UserDeviceLocation { get; private set; } = string.Empty;
        public string BuildNumber { get; private set; } = string.Empty;
        public string SoftwareVersion { get; private set; } = string.Empty;
        public string SoftwareBuild { get; private set; } = string.Empty;
        public string UIBuildNumber { get; private set; } = string.Empty;
        public string UISoftwareVersion { get; private set; } = string.Empty;
        public string UISoftwareBuild { get; private set; } = string.Empty;
        public bool? SecureDevice { get; private set; } = null;
        public string ECPSettingMode { get; private set; } = string.Empty;
        public string Language { get; private set; } = string.Empty;
        public string Country { get; private set; } = string.Empty;
        public string Locale { get; private set; } = string.Empty;
        public bool? TimeZoneAuto { get; private set; } = null;
        public string TimeZone { get; private set; } = string.Empty;
        public string TimeZoneName { get; private set; } = string.Empty;
        public string TimeZoneTz { get; private set; } = string.Empty;
        public int? TimeZoneOffset { get; private set; } = null;
        public string ClockFormat { get; private set; } = string.Empty;
        public int? Uptime { get; private set; } = null;
        public string PowerMode { get; private set; } = string.Empty;
        public bool? SupportsSuspend { get; private set; } = null;
        public bool? SupportsFindRemote { get; private set; } = null;
        public bool? FindRemoteIsPossible { get; private set; } = null;
        public bool? SupportsAudioGuide { get; private set; } = null;
        public bool? SupportsRVA { get; private set; } = null;
        public bool? HasHandsFreeVoiceRemote { get; private set; } = null;
        public bool? DeveloperMode { get; private set; } = null;
        public bool? DeviceAutomationBridgeEnabled { get; private set; } = null;
        public bool? SearchEnabled { get; private set; } = null;
        public bool? SearchChannelsEnabled { get; private set; } = null;
        public bool? VoiceSearchEnabled { get; private set; } = null;
        public bool? SupportsPrivateListening { get; private set; } = null;
        public bool? HeadphonesConnected { get; private set; } = null;
        public bool? SupportsAudioSettings { get; private set; } = null;
        public bool? SupportsECSTextEdit { get; private set; } = null;
        public bool? SupportsECSMicrophone { get; private set; } = null;
        public bool? SupportsWakeOnWLAN { get; private set; } = null;
        public bool? SupportsAirplay { get; private set; } = null;
        public bool? HasPlayOnRoku { get; private set; } = null;
        public bool? HasMobileScreenSaver { get; private set; } = null;
        public string SupportUrl { get; private set; } = string.Empty;
        public string GrandCentralVersion { get; private set; } = string.Empty;
        public bool? SupportsTrc { get; private set; } = null;
        public string TrcVersion { get; private set; } = string.Empty;
        public string TrcChannelVersion { get; private set; } = string.Empty;
        public string AVSyncCalibrationEnabled { get; private set; } = string.Empty;
        public string BrightScriptDebuggerVersion { get; private set; } = string.Empty;
        #endregion

        #region "Private Properties"
        private readonly RokuPlayerHelper _Helper = new();

        // Connection details
        private readonly IPAddress _IpAddress;
        private readonly ushort _Port = 8060;
        private readonly string _BaseUrl;
        #endregion

        #region "Public Methods"
        public void RefreshPlayerInfo()
        {
            Dictionary<string, string> playerInfo = GetPlayerInfo();
            SetPlayerInfo(playerInfo);
        }
        public void SendKeypress(KeypressType keypress)
        {
            Uri keypressUrl = GetPlayerUri(string.Format("/keypress/{0}", RokuPlayerHelper.KeypressToString(keypress)));
            _Helper.PostContents(keypressUrl, string.Empty);
        }
        public void SendKeypress(string keypress)
        {
            Uri keypressUrl = GetPlayerUri(string.Format("/keypress/{0}", keypress));
            _Helper.PostContents(keypressUrl, string.Empty);
        }
        public void SendKeypressSequence(KeypressType[] keypresses, int intervalInMs = 300)
        {
            foreach (KeypressType keypress in keypresses)
            {
                Uri keypressUrl = GetPlayerUri(string.Format("/keypress/{0}", RokuPlayerHelper.KeypressToString(keypress)));
                Task delayTask = RokuPlayerHelper.DelayAsync(intervalInMs);
                delayTask.Wait();
                _Helper.PostContents(keypressUrl, string.Empty);
            }
        }
        public void SendKeypressSequence(string[] keypresses, int intervalInMs = 300)
        {
            foreach (string keypress in keypresses)
            {
                Uri keypressUrl = GetPlayerUri(string.Format("/keypress/{0}", keypress));
                Task delayTask = RokuPlayerHelper.DelayAsync(intervalInMs);
                delayTask.Wait();
                _Helper.PostContents(keypressUrl, string.Empty);
            }
        }
        public void TypeString(string stringToType)
        {
            List<string> keypresses = TypingKeypresses(stringToType);
            SendKeypressSequence(keypresses.ToArray());
        }
        public void ExitCurrentChannel()
        {
            Uri exitChannelUrl = GetPlayerUri("/exit-app");
            _Helper.PostContents(exitChannelUrl, string.Empty);
        }
        #endregion

        #region "Private Methods"
        private Uri GetPlayerUri(string urlPath)
        {
            return new Uri(string.Format("{0}{1}", _BaseUrl, urlPath));
        }
        public static List<string> TypingKeypresses(string stringToType) // Hello -> { Lit_H, Lit_e, Lit_l, Lit_l, Lit_o }
        {
            List<string> keypressSequence = [];
            foreach (char c in stringToType.ToCharArray())
            {
                keypressSequence.Add($"Lit_{c}");
            }
            return keypressSequence;
        }
        public Dictionary<string, string> GetPlayerInfo()
        {
            Dictionary<string, string> playerInfo = [];
            string deviceInfoString = _Helper.GetContents(GetPlayerUri("/query/device-info"));
            deviceInfoString = deviceInfoString.Trim();
            if (!string.IsNullOrEmpty(deviceInfoString))
            {
                XDocument deviceInfoDoc = RokuPlayerHelper.GetXDocument(deviceInfoString);
                foreach (XElement element in deviceInfoDoc.Descendants())
                {
                    if (element.Name.ToString() == "device-info") continue;
                    playerInfo.Add(element.Name.ToString(), element.Value);
                }
            }
            return playerInfo;
        }
        public void SetPlayerInfo(Dictionary<string, string> playerInfo)
        {
            if (playerInfo.Count == 0)
            {
                throw new Exception("RokuPlayer init: player info could not be fetched");
            }
            else
            {
                foreach (KeyValuePair<string, string> kvpPlayerInfo in playerInfo)
                {
                    switch (kvpPlayerInfo.Key.ToLower())
                    {
                        case "udn":
                            UDN = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "serial-number":
                            SerialNumber = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "device-id":
                            DeviceId = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "advertising-id":
                            AdvertisingId = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "vendor-name":
                            VendorName = kvpPlayerInfo.Value;
                            break;
                        case "model-name":
                            ModelName = kvpPlayerInfo.Value;
                            break;
                        case "model-number":
                            ModelNumber = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "model-region":
                            ModelRegion = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "is-tv":
                            IsTv = kvpPlayerInfo.Value == "true";
                            break;
                        case "is-stick":
                            IsStick = kvpPlayerInfo.Value == "true";
                            break;
                        case "mobile-has-live-tv":
                            MobileHasLiveTv = kvpPlayerInfo.Value == "true";
                            break;
                        case "ui-resolution":
                            UIResolution = kvpPlayerInfo.Value;
                            break;
                        case "supports-ethernet":
                            SupportsEthernet = kvpPlayerInfo.Value == "true";
                            break;
                        case "wifi-mac":
                            WifiMac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "wifi-driver":
                            WifiDriver = kvpPlayerInfo.Value;
                            break;
                        case "has-wifi-5G-support":
                            HasWifi5GSupport = kvpPlayerInfo.Value == "true";
                            break;
                        case "bluetooth-mac":
                            BluetoothMac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "wifi2-mac":
                            Wifi2Mac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "network-type":
                            NetworkType = kvpPlayerInfo.Value.ToLower();
                            break;
                        case "network-name":
                            NetworkName = kvpPlayerInfo.Value;
                            break;
                        case "friendly-device-name":
                            FriendlyDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "friendly-model-name":
                            FriendlyModelName = kvpPlayerInfo.Value;
                            break;
                        case "default-device-name":
                            DefaultDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "user-device-name":
                            UserDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "user-device-location":
                            UserDeviceLocation = kvpPlayerInfo.Value;
                            break;
                        case "build-number":
                            BuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "software-version":
                            SoftwareVersion = kvpPlayerInfo.Value;
                            break;
                        case "software-build":
                            SoftwareBuild = kvpPlayerInfo.Value;
                            break;
                        case "lightning-base-build-number":
                            // _lightningBaseBuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "ui-build-number":
                            UIBuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "ui-software-version":
                            UISoftwareVersion = kvpPlayerInfo.Value;
                            break;
                        case "ui-software-build":
                            UISoftwareBuild = kvpPlayerInfo.Value;
                            break;
                        case "secure-device":
                            SecureDevice = kvpPlayerInfo.Value == "true";
                            break;
                        case "ecp-setting-mode":
                            ECPSettingMode = kvpPlayerInfo.Value;
                            break;
                        case "language":
                            Language = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "country":
                            Country = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "locale":
                            Locale = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "time-zone-auto":
                            TimeZoneAuto = kvpPlayerInfo.Value == "true";
                            break;
                        case "time-zone":
                            TimeZone = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-name":
                            TimeZoneName = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-tz":
                            TimeZoneTz = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-offset":
                            TimeZoneOffset = Convert.ToInt32(kvpPlayerInfo.Value);
                            break;
                        case "clock-format":
                            ClockFormat = kvpPlayerInfo.Value;
                            break;
                        case "uptime":
                            Uptime = Convert.ToInt32(kvpPlayerInfo.Value);
                            break;
                        case "power-mode":
                            PowerMode = kvpPlayerInfo.Value;
                            break;
                        case "supports-suspend":
                            SupportsSuspend = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-find-remote":
                            SupportsFindRemote = kvpPlayerInfo.Value == "true";
                            break;
                        case "find-remote-is-possible":
                            FindRemoteIsPossible = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-audio-guide":
                            SupportsAudioGuide = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-rva":
                            SupportsRVA = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-hands-free-voice-remote":
                            HasHandsFreeVoiceRemote = kvpPlayerInfo.Value == "true";
                            break;
                        case "developer-enabled":
                            DeveloperMode = kvpPlayerInfo.Value == "true";
                            break;
                        case "keyed-developer-id":
                            // _keyedDeveloperId = kvpPlayerInfo.Value;
                            break;
                        case "device-automation-bridge-enabled":
                            DeviceAutomationBridgeEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "search-enabled":
                            SearchEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "search-channels-enabled":
                            SearchChannelsEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "voice-search-enabled":
                            VoiceSearchEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-private-listening":
                            SupportsPrivateListening = kvpPlayerInfo.Value == "true";
                            break;
                        case "headphones-connected":
                            HeadphonesConnected = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-audio-settings":
                            SupportsAudioSettings = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-ecs-textedit":
                            SupportsECSTextEdit = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-ecs-microphone":
                            SupportsECSMicrophone = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-wake-on-wlan":
                            SupportsWakeOnWLAN = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-airplay":
                            SupportsAirplay = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-play-on-roku":
                            HasPlayOnRoku = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-mobile-screensaver":
                            HasMobileScreenSaver = kvpPlayerInfo.Value == "true";
                            break;
                        case "support-url":
                            SupportUrl = kvpPlayerInfo.Value;
                            break;
                        case "grandcentral-version":
                            GrandCentralVersion = kvpPlayerInfo.Value;
                            break;
                        case "supports-trc":
                            SupportsTrc = kvpPlayerInfo.Value == "true";
                            break;
                        case "trc-version":
                            TrcVersion = kvpPlayerInfo.Value;
                            break;
                        case "trc-channel-version":
                            TrcChannelVersion = kvpPlayerInfo.Value;
                            break;
                        case "av-sync-calibration-enabled":
                            AVSyncCalibrationEnabled = kvpPlayerInfo.Value;
                            break;
                        case "brightscript-debugger-version":
                            BrightScriptDebuggerVersion = kvpPlayerInfo.Value;
                            break;
                        default:
                            // TODO: _unhandledProperties.Add(kvpPlayerInfo.Key, kvpPlayerInfo.Value);
                            break;
                    }
                }
            }
        }
        #endregion

        #region "Constructors"
        public RokuPlayer(IPAddress playerIp)
        {
            _IpAddress = playerIp;
            _BaseUrl = string.Format("http://{0}:{1}/", playerIp.ToString(), _Port);
            Dictionary<string, string> playerInfo = GetPlayerInfo();
            SetPlayerInfo(playerInfo);
        }
        public RokuPlayer(IPAddress playerIp, ushort playerPort)
        {
            _IpAddress = playerIp;
            _Port = playerPort;
            _BaseUrl = string.Format("http://{0}:{1}/", playerIp.ToString(), _Port);
            Dictionary<string, string> playerInfo = GetPlayerInfo();
            SetPlayerInfo(playerInfo);
        }
        #endregion
    }
}
