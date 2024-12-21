namespace ERP.Services.Interfaces
{
	public interface IPushNotificationService
	{
		/// <summary>
		/// Sends a push notification to the specified device identified by its token.
		/// </summary>
		/// <param name="deviceToken">The token of the device to which the notification will be sent.</param>
		/// <param name="title">The title of the notification.</param>
		/// <param name="body">The body content of the notification.</param>
		/// <returns>A task representing the asynchronous operation of sending the notification.</returns>
		Task SendNotification(string deviceToken, string title, string body, string url);

        /// <summary>
        /// Sends a notification to all devices associated with the specified employee ID.
        /// </summary>
        /// <param name="empId">The ID of the employee.</param>
        /// <param name="title">The title of the notification.</param>
        /// <param name="body">The body of the notification.</param>
        /// <param name="url">The URL to be included in the notification.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendNotificationToAllDevices(int empId, string title, string body, string icon, string url, int? NotificationFrom=0);

    }
}
