# PetMate 🐾  
Subscription-Based Pet Rental Platform

PetMate is a full-stack web application that connects pet owners with people who want to experience pet companionship without long-term commitment. Users can browse available pets, subscribe to rent them for specified periods, and communicate with owners through real-time chat.

Built with Blazor WebAssembly, ASP.NET Core, and AWS services for a scalable, modern pet-sharing experience.

---

## 📱 Features

- Browse and search available pets for rent
- Subscription-based rental system with Stripe integration
- Real-time chat between renters and pet owners
- Secure user authentication and profiles
- Pet listing management (create, edit, delete)
- Photo uploads for pet profiles
- Calendar-based availability scheduling
- Booking management and status tracking
- Review and rating system

---

## 🛠️ Tech Stack

- **Frontend:** Blazor WebAssembly
- **Backend:** ASP.NET Core 
- **Database:** SQL Server (Identity), AWS DynamoDB (Pet data)
- **Storage:** AWS S3 (Pet photos)
- **Payments:** Stripe API
- **Real-time Communication:** SignalR
- **Authentication:** ASP.NET Core Identity + IdentityServer
- **Architecture:** Client-Server-Shared (Blazor Hosted)

---

## 🚀 Installation & Setup

### Prerequisites
- .NET SDK (6.0 or higher)
- SQL Server Management Studio (SSMS)
- SQL Server LocalDB (comes with Visual Studio or can be installed separately)
- AWS Account with IAM access
- Stripe Account

### 1. Clone the repository
```bash
git clone https://github.com/yourusername/petmate.git
cd petmate
```

### 2. Set up SQL Server

#### Install SQL Server Management Studio (SSMS)
1. Download [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
2. Install SSMS following the setup wizard
3. Verify SQL Server LocalDB is installed:
```bash
sqllocaldb info
```
4. If LocalDB is not installed, download it from [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

#### Configure Database Connection
The app uses LocalDB for user authentication and identity management. No manual database creation is needed - Entity Framework will create it automatically on first run.

### 3. Configure AWS Services

#### Step 1: Create IAM User
1. Go to [AWS IAM Console](https://console.aws.amazon.com/iam/)
2. Create a new user with **Programmatic access**
3. Attach the following policies:
   - `AmazonDynamoDBFullAccess`
   - `AmazonS3FullAccess`
4. Save your **Access Key ID** and **Secret Access Key**

#### Step 2: Configure AWS Credentials
```bash
aws configure
# Enter your Access Key ID
# Enter your Secret Access Key
# Enter your preferred region (e.g., us-east-1)
# Enter output format: json
```

#### Step 3: Create DynamoDB Table
1. Go to [DynamoDB Console](https://console.aws.amazon.com/dynamodb/)
2. Click **Create table**
3. Configure the table:
   - **Table name:** `pets`
   - **Partition key:** `id` (String)
   - **Sort key:** `username` (String)
   - **Table settings:** Use default settings or On-demand capacity
4. Click **Create table**

#### Step 4: Create S3 Bucket
1. Go to [S3 Console](https://s3.console.aws.amazon.com/)
2. Click **Create bucket**
3. Configure the bucket:
   - **Bucket name:** `petmate-photos` (or your preferred name)
   - **Region:** Match your DynamoDB region
   - **Block Public Access:** Uncheck (for public pet photos) or configure with CloudFront
   - **Bucket Versioning:** Optional
4. Click **Create bucket**
5. Configure CORS policy:
   - Go to bucket **Permissions** → **CORS**
   - Add this configuration:
```json
[
    {
        "AllowedHeaders": ["*"],
        "AllowedMethods": ["GET", "PUT", "POST", "DELETE"],
        "AllowedOrigins": ["*"],
        "ExposeHeaders": []
    }
]
```

### 4. Configure Application Settings

Create or update `appsettings.json` in the Server project:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=IdentityUsers;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "AWS": {
    "Region": "us-east-1",
    "Profile": "default"
  },
  "Stripe": {
    "SecretKey": "sk_test_YOUR_STRIPE_SECRET_KEY",
    "PublishableKey": "pk_test_YOUR_STRIPE_PUBLISHABLE_KEY"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

**Important Notes:**
- `TrustServerCertificate=True` is required for LocalDB SSL connections
- The database `IdentityUsers` will be created automatically on first run
- No need to manually create the SQL database

**Or use User Secrets (recommended for development):**
```bash
cd PetMateCoreHosted/Server
dotnet user-secrets init
dotnet user-secrets set "Stripe:SecretKey" "sk_test_YOUR_KEY"
dotnet user-secrets set "Stripe:PublishableKey" "pk_test_YOUR_KEY"
```

### 5. Set up Stripe
1. Create a [Stripe account](https://stripe.com)
2. Get your API keys from the [Stripe Dashboard](https://dashboard.stripe.com/apikeys)
3. Set up products and prices for pet subscriptions
4. Add keys to your configuration

### 6. Initialize Database
```bash
cd PetMateCoreHosted/Server
dotnet ef database update
```

### 7. Run the application
```bash
cd PetMateCoreHosted/Server
dotnet run
```

The app will be available at `https://localhost:5001` (or the port shown in console)

---

## 🎥 Video Walkthrough

[![Watch the demo](https://img.youtube.com/vi/PwjsjRZ-m0g/0.jpg)](https://www.youtube.com/watch?v=PwjsjRZ-m0g)

---

## 📂 Project Structure

```
PetMateCoreHosted/
├── Client/          # Blazor WebAssembly frontend
├── Server/          # ASP.NET Core backend
│   ├── Controllers/ # API endpoints
│   ├── Data/        # EF Core DbContext
│   ├── Hubs/        # SignalR chat hub
│   ├── Models/      # Data models
│   └── Program.cs   # App configuration
└── Shared/          # Shared models & contracts
```

---

## 🔑 Key Features Explained

### Pet Rental Flow
1. Pet owner creates a listing with photos, description, and pricing
2. Price is synced with Stripe as a subscription product
3. Renter browses pets and subscribes via Stripe Checkout
4. Booking status updates from `Created` → `Payment` → `Reserved` → `Fulfilled`
5. Real-time chat enables coordination between owner and renter

### Payment Integration
- Stripe handles all payment processing
- Subscription-based model (recurring payments)
- Each pet has a unique `price_id` linked to Stripe
- Automatic payment status tracking

### Real-time Features
- SignalR powers the chat system
- Instant notifications for booking updates
- Live availability calendar updates

---

## 🧪 Related Projects

**Spiral — AI-Powered Bullet Journaling**  
Swift/SwiftUI + Firebase  
AI-generated journal entries, task organization, dynamic collages  
Integrated with Hugging Face, Unsplash, GIPHY APIs

**Uvavine — Full Stack Social Media Platform**  
MERN Stack, WebSockets, OAuth 2.0  
Optimized for 1,000+ daily interactions  
Advanced animation & DOM performance techniques

---

## 👤 Created & Designed By

**Alan Grissette**  
Creator, Designer & Developer of PetMate  
Philadelphia, PA  
Boston University — B.A. in Computer Science (May 2024)

GitHub: https://github.com/algrissette  
Portfolio: https://alangrissette.com  
LinkedIn: https://linkedin.com/in/alangrissette  
Email: alangrissette02@gmail.com

---

## 📄 License

This project is licensed under the License — feel free to fork, explore, and build on top of it.

---

## 🤝 Contributing

Contributions are welcome! Feel free to:
- Report bugs
- Suggest new features
- Submit pull requests

---

If you like this project, consider giving it a ⭐ on GitHub.
