
using CorePush.Apple;
using CorePush.Google;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RockyConnectBackend.Data;
using RockyConnectBackend.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static RockyConnectBackend.Model.GoogleNotification;
public interface INotificationService
{
    Task<ResponseM> SendNotification(NotificationModel notificationModel);
}

public class NotificationService : INotificationService
{
    private readonly FcmNotificationSetting _fcmNotificationSetting;
    public NotificationService(IOptions<FcmNotificationSetting> settings)
    {
        _fcmNotificationSetting = settings.Value;
    }

    public async Task<ResponseM> SendNotification(NotificationModel notificationModel)
    {
        ResponseM response = new ResponseM();
        try
        {
              /* FCM Sender (Android Device) */
                FcmSettings settings = new FcmSettings()
                {
                    SenderId = _fcmNotificationSetting.SenderId,
                    ServerKey = _fcmNotificationSetting.ServerKey
                };
                HttpClient httpClient = new HttpClient();

                string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                string deviceToken = notificationModel.DeviceId;

                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                httpClient.DefaultRequestHeaders.Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                DataPayload dataPayload = new DataPayload();
                dataPayload.Title = notificationModel.Title;
                dataPayload.Body = notificationModel.Body;

                GoogleNotification notification = new GoogleNotification();
                notification.Data = dataPayload;
                notification.Notification = dataPayload;

                var fcm = new FcmSender(settings, httpClient);
                var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                if (fcmSendResponse.IsSuccess())
                {
                    response.IsSuccess = true;
                    response.Message = "Notification sent successfully";
                    return response;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = fcmSendResponse.Results[0].Error;
                    return response;
                }
         
            return response;
        }
        catch (Exception ex)
        {
            response.IsSuccess = false;
            response.Message = "Something went wrong";
            return response;
        }
    }


        internal static Response GetNotification(string? email)
        {

            var status = new Response();
           List <Notification> result = NotificationData.SelectNotificationData(email);
            if (result.Count >=1)
            {
                status.statusCode = "00";
                status.status = "Successfull";
                status.data = result;
            }
            else
            {

                status.statusCode = "01";
                status.status = "No Notification is tied to this account";
            }
            return status;
        }

        //public static async Task<ResponseM> SendNotification(NotificationModel notificationModel, FcmNotificationSetting setting)
        //{
        //    ResponseM response = new ResponseM();
        //    try
        //    {
        //        /* FCM Sender (Android Device) */
        //        FcmSettings settings = new FcmSettings()
        //        {
        //            SenderId = setting.SenderId,
        //             ServerKey = setting.ServerKey
        //          };  
        //            HttpClient httpClient = new HttpClient();

        //            string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
        //            string deviceToken = notificationModel.DeviceId;

        //            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
        //            httpClient.DefaultRequestHeaders.Accept
        //                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            DataPayload dataPayload = new DataPayload();
        //            dataPayload.Title = notificationModel.Title;
        //            dataPayload.Body = notificationModel.Body;

        //            GoogleNotification notification = new GoogleNotification();
        //            notification.Data = dataPayload;
        //            notification.Notification = dataPayload;

        //            var fcm = new FcmSender(settings, httpClient);
        //            var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

        //            if (fcmSendResponse.IsSuccess())
        //            {
        //                response.IsSuccess = true;
        //                response.Message = "Notification sent successfully";
        //                return response;
        //            }
        //            else
        //            {
        //                response.IsSuccess = false;
        //                response.Message = fcmSendResponse.Results[0].Error;
        //                return response;
        //            }
      
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = "Something went wrong";
        //        return response;
        //    }
        //}
    }
