# PetMate

The project is ready to run out of the box, once Amazon Web Services (AWS) credentials are configured. 

This application was built using .NET 6.0, installation can be found at https://dotnet.microsoft.com/en-us/download/dotnet/6.0

In order to configure your AWS Credentials:

For Mac users:

1. Run this command in the terminal: **curl "https://awscli.amazonaws.com/AWSCLIV2.pkg" -o "AWSCLIV2.pkg" sudo installer -pkg AWSCLIV2.pkg -target /**
2. Run **aws --version** to confirm installation. 
3. Run **aws configure** to enter credentials for AWS Access Key ID, AWS Secret Access Key, Default Region Name, leave default blank
   Be sure to use the AWS credentials that I have supplied previously via email, and _us-east-1_ for the Default Region Name.  

For Windows users:
1. Run this command in the terminal: **msiexec.exe /i https://awscli.amazonaws.com/AWSCLIV2.msi**
2. Run **aws --version** to confirm installation.
3. Run **aws configure** and enter credentials for AWS Access Key ID, AWS Secret Access Key, Default Region Name, leave default blank
   Be sure to use the AWS credentials that I have supplied previously via email, and _us-east-1_ for the Default Region Name.  
4. Can test if AWS credentials configured using a simple command such **aws s3 ls** which lists our S3 buckets (including petmate392)

Technologies Used:

1. ASP.NET Core Hosted application -> Includes Server and Client side code, Server contains models and controllers written in C# for supporting API endpoints, Client uses razor pages for user facing side of application, this is where requests to backend controllers are made
2. AWS DynamoDB for storing pets, messages, addresses, and reviews
3. AWS RDS for hosting a SQL server for authentication
4. AWS S3 for storing images of pets
5. Signal R for real time chat between users
6. Stripe Payment API for transactions between pet owners and customers
7. Google Maps API for displaying pets within a certain area

   
