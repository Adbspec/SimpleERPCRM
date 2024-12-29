
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ERP.Services.Interfaces
{
	[Authorize]
	public class NotificationHub : Hub
    {
		

		private readonly Dictionary<string, string> _subscriptions = new Dictionary<string, string>();


		public async Task SendNotification(string deviceToken, string title, string body)
		{
			//try
			//{
			//	var message = new Message()
			//	{
			//		Token = deviceToken,
			//		Notification = new Notification
			//		{
			//			Title = title,
			//			Body = body
			//		}
			//	};

			//	// Send the notification
			//	await _messaging.SendAsync(message);

			//	// You can also perform additional logging or processing here
			//	Console.WriteLine($"Notification sent successfully to device: {deviceToken}");
			//}
			//catch (Exception ex)
			//{
			//	// Handle any errors
			//	Console.WriteLine($"Failed to send notification: {ex.Message}");
			//}
		}

		public async Task SubscribeToPushNotifications(string subscription)
        {
        }

    



    }
}
