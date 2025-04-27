using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
        public string VendorName
        {
            get => _VendorName;
        }
        public string UDN
        {             
            get => _Udn;
        }
        #endregion

        #region "Private Properties"
        private readonly RokuPlayerHelper _Helper = new();

        // Connection details
        private readonly IPAddress _IpAddress;
        private readonly ushort _Port = 8060;
        private readonly string _BaseUrl;

        // Device configuration
        private string _Udn;
        private string _VendorName;
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
        public List<string> TypingKeypresses(string stringToType) // Hello -> { Lit_H, Lit_e, Lit_l, Lit_l, Lit_o }
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
                            _serialNumber = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "device-id":
                            _deviceId = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "advertising-id":
                            _advertisingId = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "vendor-name":
                            _VendorName = kvpPlayerInfo.Value;
                            break;
                        case "model-name":
                            _modelName = kvpPlayerInfo.Value;
                            break;
                        case "model-number":
                            _modelNumber = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "model-region":
                            _modelRegion = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "is-tv":
                            _isTv = kvpPlayerInfo.Value == "true";
                            break;
                        case "is-stick":
                            _isStick = kvpPlayerInfo.Value == "true";
                            break;
                        case "mobile-has-live-tv":
                            _mobileHasLiveTv = kvpPlayerInfo.Value == "true";
                            break;
                        case "ui-resolution":
                            _uiResolution = kvpPlayerInfo.Value;
                            break;
                        case "supports-ethernet":
                            _supportsEthernet = kvpPlayerInfo.Value == "true";
                            break;
                        case "wifi-mac":
                            _wifiMac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "wifi-driver":
                            _wifiDriver = kvpPlayerInfo.Value;
                            break;
                        case "has-wifi-5G-support":
                            _hasWifi5GSupport = kvpPlayerInfo.Value == "true";
                            break;
                        case "bluetooth-mac":
                            _bluetoothMac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "wifi2-mac":
                            _wifi2Mac = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "network-type":
                            _networkType = kvpPlayerInfo.Value.ToLower();
                            break;
                        case "network-name":
                            _networkName = kvpPlayerInfo.Value;
                            break;
                        case "friendly-device-name":
                            _friendlyDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "friendly-model-name":
                            _friendlyModelName = kvpPlayerInfo.Value;
                            break;
                        case "default-device-name":
                            _defaultDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "user-device-name":
                            _userDeviceName = kvpPlayerInfo.Value;
                            break;
                        case "user-device-location":
                            _userDeviceLocation = kvpPlayerInfo.Value;
                            break;
                        case "build-number":
                            _buildNumber = kvpPlayerInfo.Value;
                            break;
                        case "software-version":
                            _softwareVersion = kvpPlayerInfo.Value;
                            break;
                        case "software-build":
                            _softwareBuild = kvpPlayerInfo.Value;
                            break;
                        case "lightning-base-build-number":
                            // _lightningBaseBuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "ui-build-number":
                            _uiBuildNumber = kvpPlayerInfo.Value;
                            break;
                        case "ui-software-version":
                            _uiSoftwareVersion = kvpPlayerInfo.Value;
                            break;
                        case "ui-software-build":
                            _uiSoftwareBuild = kvpPlayerInfo.Value;
                            break;
                        case "secure-device":
                            _secureDevice = kvpPlayerInfo.Value == "true";
                            break;
                        case "ecp-setting-mode":
                            _ecpSettingMode = kvpPlayerInfo.Value;
                            break;
                        case "language":
                            _language = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "country":
                            _country = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "locale":
                            _locale = kvpPlayerInfo.Value.ToUpper();
                            break;
                        case "time-zone-auto":
                            _timeZoneAuto = kvpPlayerInfo.Value == "true";
                            break;
                        case "time-zone":
                            _timeZone = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-name":
                            _timeZoneName = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-tz":
                            _timeZoneTz = kvpPlayerInfo.Value;
                            break;
                        case "time-zone-offset":
                            _timeZoneOffset = Convert.ToInt32(kvpPlayerInfo.Value);
                            break;
                        case "clock-format":
                            _clockFormat = kvpPlayerInfo.Value;
                            break;
                        case "uptime":
                            _uptime = Convert.ToInt32(kvpPlayerInfo.Value);
                            break;
                        case "power-mode":
                            _powerMode = kvpPlayerInfo.Value;
                            break;
                        case "supports-suspend":
                            _supportsSuspend = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-find-remote":
                            _supportsFindRemote = kvpPlayerInfo.Value == "true";
                            break;
                        case "find-remote-is-possible":
                            _findRemoteIsPossible = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-audio-guide":
                            _supportsAudioGuide = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-rva":
                            _supportsRva = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-hands-free-voice-remote":
                            _hasHandsFreeVoiceRemote = kvpPlayerInfo.Value == "true";
                            break;
                        case "developer-enabled":
                            _developerEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "keyed-developer-id":
                            // _keyedDeveloperId = kvpPlayerInfo.Value;
                            break;
                        case "device-automation-bridge-enabled":
                            _deviceAutomationBridgeEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "search-enabled":
                            _searchEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "search-channels-enabled":
                            _searchChannelsEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "voice-search-enabled":
                            _voiceSearchEnabled = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-private-listening":
                            _supportsPrivateListening = kvpPlayerInfo.Value == "true";
                            break;
                        case "headphones-connected":
                            _headphonesConnected = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-audio-settings":
                            _supportsAudioSettings = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-ecs-textedit":
                            _supportsECSTextEdit = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-ecs-microphone":
                            _supportsECSMicrophone = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-wake-on-wlan":
                            _supportsWakeOnWLAN = kvpPlayerInfo.Value == "true";
                            break;
                        case "supports-airplay":
                            _supportsAirplay = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-play-on-roku":
                            _hasPlayOnRoku = kvpPlayerInfo.Value == "true";
                            break;
                        case "has-mobile-screensaver":
                            _hasMobileScreenSaver = kvpPlayerInfo.Value == "true";
                            break;
                        case "support-url":
                            _supportUrl = kvpPlayerInfo.Value;
                            break;
                        case "grandcentral-version":
                            _grandCentralVersion = kvpPlayerInfo.Value;
                            break;
                        case "supports-trc":
                            _supportsTrc = kvpPlayerInfo.Value == "true";
                            break;
                        case "trc-version":
                            _trcVersion = kvpPlayerInfo.Value;
                            break;
                        case "trc-channel-version":
                            _trcChannelVersion = kvpPlayerInfo.Value;
                            break;
                        case "av-sync-calibration-enabled":
                            _avSyncCalibrationEnabled = kvpPlayerInfo.Value;
                            break;
                        case "brightscript-debugger-version":
                            _brightScriptDebuggerVersion = kvpPlayerInfo.Value;
                            break;
                        default:
                            _unhandledProperties.Add(kvpPlayerInfo.Key, kvpPlayerInfo.Value);
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
        }
        RokuPlayer(IPAddress playerIp, ushort playerPort)
        {
            _IpAddress = playerIp;
            _Port = playerPort;
            _BaseUrl = string.Format("http://{0}:{1}/", playerIp.ToString(), _Port);
        }
        #endregion
    }
}
