// Import the functions you need from the SDKs you need
// Firebase initialization
import { initializeApp } from "https://www.gstatic.com/firebasejs/10.11.0/firebase-app.js";
import { getMessaging, getToken } from "https://www.gstatic.com/firebasejs/10.11.0/firebase-messaging.js";

// Your web app's Firebase configuration
const firebaseConfig = {
    apiKey: "AIzaSyAVGw6mVN3AY5dkqxEZWYjzurL4TmZEc-o",
    authDomain: "push-notification-servic-2c622.firebaseapp.com",
    projectId: "push-notification-servic-2c622",
    storageBucket: "push-notification-servic-2c622.appspot.com",
    messagingSenderId: "857237846287",
    appId: "1:857237846287:web:29a3ed35f6a0a728a5e67a"
};

// Function to initialize Firebase messaging
function initializeFirebaseMessaging() {
    // Initialize Firebase
    const app = initializeApp(firebaseConfig);

    // Get the serviceWorker URL
    const serviceWorker = $('#script-worker').attr('script-worker');

    // Check if permission for notifications is granted
    if (Notification.permission === 'granted') {
        // Permission granted, register service worker and get token
        navigator.serviceWorker.register(serviceWorker).then(registration => {
            // Get the messaging instance
            const messaging = getMessaging(app);

            // Retrieve the FCM token
            getToken(messaging, {
                serviceWorkerRegistration: registration,
                vapidKey: "BFpU-59e_pdpCnJkKM7WJHNL4BSpoFGPPnENaomXVaKllwNSfhbnek91w-Dao7xe9QBpuFypq3_xGhLuV7QbQuQ"
            }).then(currentToken => {
                if (currentToken) {
                    $('#deviceToken').val(currentToken); // Set value to deviceToken input
                    hideSpinner();
                }
            }).catch(err => {
                console.log('An error occurred while retrieving token:', err);
                hideSpinner();
            });
        }).catch(error => {
            hideSpinner();
            console.error('Error registering service worker:', error);
        });
    }
}

// Export the function to be accessible from other modules if needed
export { initializeFirebaseMessaging };
