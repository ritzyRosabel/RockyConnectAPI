﻿using System;
using Newtonsoft.Json;

namespace RockyConnectBackend.Model
{
	public class Notification
	{
        public string NotificationID { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
    }

    public class FcmNotificationSetting
    {
        public string SenderId { get; set; }
        public string ServerKey { get; set; }
    }

    public class NotificationModel
    {
        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }
    }

    public class GoogleNotification
    {
        public class DataPayload
        {
            [JsonProperty("title")]
            public string Title { get; set; }
            [JsonProperty("body")]
            public string Body { get; set; }
        }

        [JsonProperty("priority")]
        public string Priority { get; set; } = "high";

        [JsonProperty("data")]
        public DataPayload Data { get; set; }

        [JsonProperty("notification")]
        public DataPayload Notification { get; set; }
    }
    public class ResponseM
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}

