﻿using Newtonsoft.Json;

namespace OmniLinkBridge.MQTT.HomeAssistant
{
    public class Alarm : Device
    {
        public Alarm(DeviceRegistry deviceRegistry) : base(deviceRegistry)
        {

        }

        public string command_topic { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string command_template { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string code { get; set; }
    }
}
