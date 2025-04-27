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
        /// <summary>
        /// Unique device number (UDID)
        /// </summary>
        public string UDN
        {             
            get => _Udn;
        }
        /// <summary>
        /// Device serial number
        /// </summary>
        public string SerialNumber
        {
            get => _SerialNumber;
        }
        /// <summary>
        /// Device ID
        /// </summary>
        public string DeviceId
        { 
            get => _DeviceId; 
        }
        /// <summary>
        /// Device advertising ID
        /// </summary>
        public string AdvertisingId
        {
            get => _AdvertisingId;
        }
        public string VendorName
        {
            get => _VendorName;
        }
        public string ModelName
        {
            get => _ModelName;
        }
        public string ModelNumber
        {
            get => _ModelNumber;
        }
        public string ModelRegion
        {
            get => _ModelRegion;
        }
        public bool? IsTv
        {
            get => _IsTv;
        }
        public bool? IsStick
        {
            get => _IsStick;
        }
        public bool? MobileHasLiveTv
        {
            get => _MobileHasLiveTv;
        }
        public string UIResolution
        {
            get => _UiResolution;
        }
        public bool? SupportsEthernet
        {
            get => _SupportsEthernet;
        }
        public string WifiMac
        {
            get => _WifiMac;
        }
        public string WifiDriver
        {
            get => _WifiDriver;
        }
        public bool? HasWifi5GSupport
        {
            get => _HasWifi5GSupport;
        }
        public string BluetoothMac
        {
            get => _BluetoothMac;
        }
        public string Wifi2Mac
        {
            get => _Wifi2Mac;
        }
        public string NetworkType
        {
            get => _NetworkType;
        }
        public string NetworkName
        {
            get => _NetworkName;
        }
        public string FriendlyDeviceName
        {
            get => _FriendlyDeviceName;
        }
        public string FriendlyModelName
        {
            get => _FriendlyModelName;
        }
        public string DefaultDeviceName
        {
            get => _DefaultDeviceName;
        }
        public string UserDeviceName
        {
            get => _UserDeviceName;
        }
        public string UserDeviceLocation
        {
            get => _UserDeviceLocation;
        }
        public string BuildNumber
        {
            get => _BuildNumber;
        }
        public string SoftwareVersion
        {
            get => _SoftwareVersion;
        }
        public string SoftwareBuild
        {
            get => _SoftwareBuild;
        }
        public string UIBuildNumber
        {
            get => _UiBuildNumber;
        }
        public string UISoftwareVersion
        {
            get => _UiSoftwareVersion;
        }
        public string UISoftwareBuild
        {
            get => _UiSoftwareBuild;
        }
        public bool? SecureDevice
        {
            get => _SecureDevice;
        }
        public string ECPSettingMode
        {
            get => _EcpSettingMode;
        }
        public string Language
        {
            get => _Language;
        }
        public string Country
        {
            get => _Country;
        }
        public string Locale
        {
            get => _Locale;
        }
        public bool? TimeZoneAuto
        {
            get => _TimeZoneAuto;
        }
        public string TimeZone
        {
            get => _TimeZone;
        }
        public string TimeZoneName
        {
            get => _TimeZoneName;
        }
        public string TimeZoneTz
        {
            get => _TimeZoneTz;
        }
        public int TimeZoneOffset
        {
            get => _TimeZoneOffset;
        }
        public string ClockFormat
        {
            get => _ClockFormat;
        }
        public int? Uptime
        {
            get => _Uptime;
        }
        public string PowerMode
        {
            get => _PowerMode;
        }
        public bool? SupportsSuspend
        {
            get => _SupportsSuspend;
        }
        public bool? SupportsFindRemote
        {
            get => _SupportsFindRemote;
        }
        public bool? FindRemoteIsPossible
        {
            get => _FindRemoteIsPossible;
        }
        public bool? SupportsAudioGuide
        {
            get => _SupportsAudioGuide;
        }
        public bool? SupportsRva
        {
            get => _SupportsRva;
        }
        public bool? HasHandsFreeVoiceRemote
        {
            get => _HasHandsFreeVoiceRemote;
        }
        public bool? DeveloperMode
        {
            get => _DeveloperMode;
        }
        public bool? DeviceAutomationBridgeEnabled
        {
            get => _DeviceAutomationBridgeEnabled;
        }
        public bool? SearchEnabled
        {
            get => _SearchEnabled;
        }
        public bool? SearchChannelsEnabled
        {
            get => _SearchChannelsEnabled;
        }
        public bool? VoiceSearchEnabled
        {
            get => _VoiceSearchEnabled;
        }
        public bool? SupportsPrivateListening
        {
            get => _SupportsPrivateListening;
        }
        public bool? HeadphonesConnected
        {
            get => _HeadphonesConnected;
        }
        public bool? SupportsAudioSettings
        {
            get => _SupportsAudioSettings;
        }
        public bool? SupportsECSTextEdit
        {
            get => _SupportsECSTextEdit;
        }
        public bool? SupportsECSMicrophone
        {
            get => _SupportsECSMicrophone;
        }
        public bool? SupportsWakeOnWLAN
        {
            get => _SupportsWakeOnWlan;
        }
        public bool? SupportsAirplay
        {
            get => _SupportsAirplay;
        }
        public bool? HasPlayOnRoku
        {
            get => _HasPlayOnRoku;
        }
        public bool? HasMobileScreenSaver
        {
            get => _HasMobileScreenSaver;
        }
        public string SupportUrl
        {
            get => _SupportUrl;
        }
        public string GrandCentralVersion
        {
            get => _GrandCentralVersion;
        }
        public bool? SupportsTrc
        {
            get => _SupportsTrc;
        }
        public string TrcVersion
        {
            get => _TrcVersion;
        }
        public string TrcChannelVersion
        {
            get => _TrcChannelVersion;
        }
        public string AVSyncCalibrationEnabled
        {
            get => _AvSyncCalibrationEnabled;
        }
        public string BrightScriptDebuggerVersion
        {
            get => _BrightScriptDebuggerVersion;
        }
        #endregion

        #region "Private Properties"
        private readonly RokuPlayerHelper _Helper = new();

        // Connection details
        private readonly IPAddress _IpAddress;
        private readonly ushort _Port = 8060;
        private readonly string _BaseUrl;

        // Device details
        private string _Udn = string.Empty;
        private string _SerialNumber = string.Empty;
        private string _DeviceId = string.Empty;
        private string _AdvertisingId = string.Empty;
        private string _VendorName = string.Empty;
        private string _ModelName = string.Empty;
        private string _ModelNumber = string.Empty;
        private string _ModelRegion = string.Empty;
        private bool? _IsTv;
        private bool? _IsStick;
        private bool? _MobileHasLiveTv;
        private string _UiResolution = string.Empty;
        private bool? _SupportsEthernet;
        private string _WifiMac = string.Empty;
        private string _WifiDriver = string.Empty;
        private bool? _HasWifi5GSupport;
        private string _BluetoothMac = string.Empty;
        private string _Wifi2Mac = string.Empty;
        private string _NetworkType = string.Empty;
        private string _NetworkName = string.Empty;
        private string _FriendlyDeviceName = string.Empty;
        private string _FriendlyModelName = string.Empty;
        private string _DefaultDeviceName = string.Empty;
        private string _UserDeviceName = string.Empty;
        private string _UserDeviceLocation = string.Empty;
        private string _BuildNumber = string.Empty;
        private string _SoftwareVersion = string.Empty;
        private string _SoftwareBuild = string.Empty;
        private string _UiBuildNumber = string.Empty;
        private string _UiSoftwareVersion = string.Empty;
        private string _UiSoftwareBuild = string.Empty;
        private bool? _SecureDevice;
        private string _EcpSettingMode = string.Empty;
        private string _Language = string.Empty;
        private string _Country = string.Empty;
        private string _Locale = string.Empty;
        private bool? _TimeZoneAuto;
        private string _TimeZone = string.Empty;
        private string _TimeZoneName = string.Empty;
        private string _TimeZoneTz = string.Empty;
        private int _TimeZoneOffset;
        private string _ClockFormat = string.Empty;
        private int? _Uptime;
        private string _PowerMode = string.Empty;
        private bool? _SupportsSuspend;
        private bool? _SupportsFindRemote;
        private bool? _FindRemoteIsPossible;
        private bool? _SupportsAudioGuide;
        private bool? _SupportsRva;
        private bool? _HasHandsFreeVoiceRemote;
        private bool? _DeveloperMode;
        private bool? _DeviceAutomationBridgeEnabled;
        private bool? _SearchEnabled;
        private bool? _SearchChannelsEnabled;
        private bool? _VoiceSearchEnabled;
        private bool? _SupportsPrivateListening;
        private bool? _HeadphonesConnected;
        private bool? _SupportsAudioSettings;
        private bool? _SupportsECSTextEdit;
        private bool? _SupportsECSMicrophone;
        private bool? _SupportsWakeOnWlan;
        private bool? _SupportsAirplay;
        private bool? _HasPlayOnRoku;
        private bool? _HasMobileScreenSaver;
        private string _SupportUrl = string.Empty;
        private string _GrandCentralVersion = string.Empty;
        private bool? _SupportsTrc;
        private string _TrcVersion = string.Empty;
        private string _TrcChannelVersion = string.Empty;
        private string _AvSyncCalibrationEnabled = string.Empty;
        private string _BrightScriptDebuggerVersion = string.Empty;
        #endregion

        #region "Public Methods"
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
                            _Udn = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "serial-number":
                            _SerialNumber = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "device-id":
                            _DeviceId = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "advertising-id":
                            _AdvertisingId = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "vendor-name":
                            _VendorName = kvpPlayerInfo.Value;
                            break;
                        case "model-name":
                            _ModelName = kvpPlayerInfo.Value;
                            break;
                        case "model-number":
                            _ModelNumber = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "model-region":
                            _ModelRegion = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "is-tv":
                            _IsTv = kvpPlayerInfo.Value == "true";
                            break;
                        case "is-stick":
                            _IsStick = kvpPlayerInfo.Value == "true";
                            break;
                        case "mobile-has-live-tv":
                            _MobileHasLiveTv = kvpPlayerInfo.Value == "true";
                            break;
                        case "ui-resolution":
                            _UiResolution = kvpPlayerInfo.Value;
                            break;
                        case "supports-ethernet":
                            _SupportsEthernet = kvpPlayerInfo.Value == "true";
                            break;
                        case "wifi-mac":
                            _WifiMac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "wifi-driver":
                            _WifiDriver = kvpPlayerInfo.Value;
                            break;
                        case "has-wifi-5G-support":
                            _HasWifi5GSupport = kvpPlayerInfo.Value == "true";
                            break;
                        case "bluetooth-mac":
                            _BluetoothMac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "wifi2-mac":
                            _Wifi2Mac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "network-type":
                            _NetworkType = kvpPlayerInfo.Value.ToLower();
                            break;
                        case "network-name":
                            _NetworkName = kvpPlayerInfo.Value;
                            break;
                        case "friendly-device-name":
                            _FriendlyDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "friendly-model-name":
                            _FriendlyModelName = kvpPlayerInfo.Value;
                            break;
                        case "default-device-name":
                            _DefaultDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "user-device-name":
                            _UserDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "user-device-location":
                            _UserDeviceLocation = kvpPlayerInfo.Value;
                            break;
                        case "build-number":
                            _BuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "software-version":
                            _SoftwareVersion = kvpPlayerInfo.Value;
                            break;
                        case "software-build":
                            _SoftwareBuild = kvpPlayerInfo.Value;
                            break;
                        case "lightning-base-build-number":
                            // _lightningBaseBuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "ui-build-number":
                            _UiBuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "ui-software-version":
                            _UiSoftwareVersion = kvpPlayerInfo.Value;
                            break;
                        case "ui-software-build":
                            _UiSoftwareBuild = kvpPlayerInfo.Value;
                            break;
                        case "secure-device":
                            _SecureDevice = kvpPlayerInfo.Value == "true";
                            break;
                        case "ecp-setting-mode":
                            _EcpSettingMode = kvpPlayerInfo.Value;
                            break;
                        case "language":
                            _Language = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "country":
                            _Country = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "locale":
                            _Locale = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "time-zone-auto":
                            _TimeZoneAuto = kvpPlayerInfo.Value == "true";
                            break;
                        case "time-zone":
                            _TimeZone = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-name":
                            _TimeZoneName = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-tz":
                            _TimeZoneTz = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-offset":
                            _TimeZoneOffset = Convert.ToInt32(kvpPlayerInfo.Value);
                            break;
                        case "clock-format":
                            _ClockFormat = kvpPlayerInfo.Value;
                            break;
                        case "uptime":
                            _Uptime = Convert.ToInt32(kvpPlayerInfo.Value);
                            break;
                        case "power-mode":
                            _PowerMode = kvpPlayerInfo.Value;
                            break;
                        case "supports-suspend":
                            _SupportsSuspend = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-find-remote":
                            _SupportsFindRemote = kvpPlayerInfo.Value == "true";
                            break;
                        case "find-remote-is-possible":
                            _FindRemoteIsPossible = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-audio-guide":
                            _SupportsAudioGuide = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-rva":
                            _SupportsRva = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-hands-free-voice-remote":
                            _HasHandsFreeVoiceRemote = kvpPlayerInfo.Value == "true";
                            break;
                        case "developer-enabled":
                            _DeveloperMode = kvpPlayerInfo.Value == "true";
                            break;
                        case "keyed-developer-id":
                            // _keyedDeveloperId = kvpPlayerInfo.Value;
                            break;
                        case "device-automation-bridge-enabled":
                            _DeviceAutomationBridgeEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "search-enabled":
                            _SearchEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "search-channels-enabled":
                            _SearchChannelsEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "voice-search-enabled":
                            _VoiceSearchEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-private-listening":
                            _SupportsPrivateListening = kvpPlayerInfo.Value == "true";
                            break;
                        case "headphones-connected":
                            _HeadphonesConnected = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-audio-settings":
                            _SupportsAudioSettings = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-ecs-textedit":
                            _SupportsECSTextEdit = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-ecs-microphone":
                            _SupportsECSMicrophone = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-wake-on-wlan":
                            _SupportsWakeOnWlan = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-airplay":
                            _SupportsAirplay = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-play-on-roku":
                            _HasPlayOnRoku = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-mobile-screensaver":
                            _HasMobileScreenSaver = kvpPlayerInfo.Value == "true";
                            break;
                        case "support-url":
                            _SupportUrl = kvpPlayerInfo.Value;
                            break;
                        case "grandcentral-version":
                            _GrandCentralVersion = kvpPlayerInfo.Value;
                            break;
                        case "supports-trc":
                            _SupportsTrc = kvpPlayerInfo.Value == "true";
                            break;
                        case "trc-version":
                            _TrcVersion = kvpPlayerInfo.Value;
                            break;
                        case "trc-channel-version":
                            _TrcChannelVersion = kvpPlayerInfo.Value;
                            break;
                        case "av-sync-calibration-enabled":
                            _AvSyncCalibrationEnabled = kvpPlayerInfo.Value;
                            break;
                        case "brightscript-debugger-version":
                            _BrightScriptDebuggerVersion = kvpPlayerInfo.Value;
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
        RokuPlayer(IPAddress playerIp)
        {
            _IpAddress = playerIp;
            _BaseUrl = string.Format("http://{0}:{1}/", playerIp.ToString(), _Port);
            Dictionary<string, string> playerInfo = GetPlayerInfo();
            SetPlayerInfo(playerInfo);
        }
        RokuPlayer(IPAddress playerIp, ushort playerPort)
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
