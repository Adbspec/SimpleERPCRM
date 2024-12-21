self.addEventListener('notificationclick', function (event) {
    event.waitUntil(
        clients.openWindow(event.notification.data.url)
            .then(function (windowClient) {
                // Access extra data sent with the notification
                const notificationId = event.notification.data.notificationId;
                const notificationUrl = event.notification.data.notificationUrl;
                // Send acknowledgment to server that notification was received, including extra data if needed
                return fetch(notificationUrl, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ notificationId: notificationId })
                });
            })
    );
});

self.addEventListener('push', function (event) {
    const notif = event.data.json().notification;
    const notificationOptions = {
        body: notif.body,
        icon: notif.icon,
        data: {
            notificationId: notif.data.notificationId, // Add notificationId to data
            notificationUrl: notif.data.notificationUrl, // Add notificationUrl to data
            url: notif.click_action // Add notificationUrl to data
        }
    };
    event.waitUntil(
        self.registration.showNotification(notif.title, notificationOptions)
    );
});
