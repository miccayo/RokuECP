using System.Xml.Linq;
using static RokuECP.RokuPlayer;

namespace RokuECP
{
    internal class RokuPlayerHelper
    {
        #region "Private Properties"
        private readonly HttpClient _httpClient = new();
        #endregion

        #region "Public Methods"
        /// <summary>
        /// Used to delay the execution of an operation for a specified number of milliseconds.
        /// </summary>
        /// <param name="milliseconds">Milliseconds</param>
        /// <returns>Task</returns>
        public static async Task DelayAsync(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

        public string GetContents(Uri requestUri)
        {
            HttpRequestMessage httpRequestMessage = new()
            {
                Method = HttpMethod.Get,
                RequestUri = requestUri
            };
            HttpResponseMessage httpResponseMessage = _httpClient.Send(httpRequestMessage);
            using StreamReader reader = new(httpResponseMessage.Content.ReadAsStream());
            return reader.ReadToEnd();
        }

        public string PostContents(Uri requestUri, string postData)
        {
            HttpRequestMessage httpRequestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = requestUri,
                Content = new StringContent(postData)
            };
            HttpResponseMessage httpResponseMessage = _httpClient.Send(httpRequestMessage);
            using StreamReader reader = new(httpResponseMessage.Content.ReadAsStream());
            return reader.ReadToEnd();
        }

        public static XDocument GetXDocument(string xmlString)
        {
            XDocument document = XDocument.Parse(xmlString);
            return document;
        }

        public static string KeypressToString(KeypressType keypress)
        {
            return keypress switch
            {
                KeypressType.Home => "Home",
                KeypressType.Rewind => "Rev",
                KeypressType.Forward => "Fwd",
                KeypressType.PlayPause => "Play",
                KeypressType.Select => "Select",
                KeypressType.Left => "Left",
                KeypressType.Right => "Right",
                KeypressType.Down => "Down",
                KeypressType.Up => "Up",
                KeypressType.Back => "Back",
                KeypressType.InstantReplay => "InstantReplay",
                KeypressType.Info => "Info",
                KeypressType.Backspace => "Backspace",
                KeypressType.Search => "Search",
                KeypressType.Enter => "Enter",
                KeypressType.VolumeDown => "VolumeDown",
                KeypressType.VolumeUp => "VolumeUp",
                KeypressType.VolumeMute => "VolumeMute",
                KeypressType.PowerOff => "PowerOff",
                KeypressType.ChannelUp => "ChannelUp",
                KeypressType.ChannelDown => "ChannelDown",
                KeypressType.InputTuner => "InputTuner",
                KeypressType.InputHDMI1 => "InputHDMI1",
                KeypressType.InputHDMI2 => "InputHDMI2",
                KeypressType.InputHDMI3 => "InputHDMI3",
                KeypressType.InputHDMI4 => "InputHDMI4",
                KeypressType.InputAV1 => "InputAV1",
                KeypressType.FindRemote => "FindRemote",
                _ => throw new Exception($"The keypress type {keypress} is not configured"),
            };
        }
        #endregion

        public RokuPlayerHelper()
        {
            _httpClient.Timeout = TimeSpan.FromMilliseconds(10000);
        }
    }
}
