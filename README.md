# 💳 SmartPay: ASP.NET Core Web API

> A secure backend API for the **SmartPay** mobile wallet, built using **ASP.NET Core 9.0**. This API provides the necessary services for handling user authentication, transaction management, Safe Mode activation, and more.

---

## 📌 Project Overview

The **SmartPay Web API** serves as the backend for the **SmartPay Mobile App** and **Web App**. It handles user authentication, wallet management, transactions, voice recognition triggers for Safe Mode, and silent alerts. The API is designed with security in mind, utilizing industry-standard practices for handling sensitive data.

---

## 🚨 Key Features

- 🔐 **User Authentication**: Secure JWT authentication for users.
- 💰 **Wallet Management**: Users can view their wallet balance, transaction history, and perform transfers.
- 🛑 **Safe Mode Activation**: Trigger Safe Mode via voice recognition or panic phrase.
- 📩 **Silent Alerts**: Sends silent alerts to predefined contacts when Safe Mode is triggered.
- 🔐 **Secure PIN System**: Dual PIN authentication — one for login, one for Safe Mode exit.
- 📝 **Transaction History**: View past transactions, with support for filtering and sorting.
- 🛠 **Admin Panel**: Admin can monitor users and transactions in real-time.

---

## 🧠 Tech Stack

- **Backend**: ASP.NET Core 9.0 (C#)
- **Authentication**: JWT Tokens, Secure PIN System
- **Database**: SQL Server / Azure SQL
- **Voice Recognition**: Integration with the mobile app using Vosk API (for Safe Mode trigger)


---
### API Test: **Postman**

For testing all the API endpoints, **Postman** was used to ensure proper functionality and smooth interactions with the SmartPay API. Postman was chosen as the tool for the following reasons:

- **Easy to Use**: With a simple user interface for sending requests, viewing responses, and managing collections of API calls.
- **Environment Management**: Supports managing variables (like JWT tokens) in environments, making repeated testing seamless.
- **Collection Sharing**: Allows exporting the API collection for sharing or reusing requests.

The **SmartPay API Postman Collection** has been pre-configured with all the essential endpoints for testing the API. You can import this collection into your Postman and start testing right away.

---


## 🚀 Project Status

- ✅ **Authentication endpoints** (login, JWT token management) complete
- ✅ **Wallet endpoints** (balance, transaction) working
- 🔜 **Voice recognition integration** for Safe Mode (via mobile app)
- 🔜 **Database schema** updates (transactions, Safe Mode flags)
- 🔜 **Admin panel** for monitoring and reporting
- 🔜 **Demo video** & **live deployment** coming soon
