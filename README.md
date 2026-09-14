# Community Board - .NET + MongoDB 3-Tier Application

A simple browser-based 3-tier application built for DevOps practice on AWS EC2.

## What it does

Users can view, create, edit, delete, and resolve community posts. Data is stored in MongoDB.

## Architecture

```text
Browser
   |
   v
Presentation Layer
ASP.NET Core MVC + Razor Views
   |
   v
Application Layer
Controllers -> Services
   |
   v
Data Layer
Repository -> MongoDB
```

## Stack

- C# / .NET 8
- ASP.NET Core MVC
- Razor Views
- MongoDB
- MongoDB.Driver

## Run on AWS EC2

### 1. Prerequisites

```bash
dotnet --version
mongosh --version
```

Install MongoDB using the official instructions for your EC2 Linux distribution. Then:

```bash
sudo systemctl status mongod
sudo systemctl start mongod
sudo systemctl enable mongod
```

### 2. Get the source

```bash
git clone <YOUR_GITHUB_REPOSITORY_URL>
cd CommunityBoard
```

### 3. MongoDB connection

The application uses:

```text
mongodb://localhost:27017
```

and stores data in database `community_board`, collection `posts`.

For this single-EC2 learning architecture, MongoDB stays local and does not need public network access.

### 4. Restore, build and run

```bash
dotnet restore
dotnet build
dotnet run
```

The app is configured by default to listen on port 5000 when hosted with:

```bash
ASPNETCORE_URLS=http://0.0.0.0:5000 dotnet run
```

Then allow TCP 5000 in the EC2 Security Group and open:

```text
http://<EC2-PUBLIC-IP>:5000
```

### 5. Verify MongoDB

```bash
mongosh
```

```javascript
use community_board
show collections
db.posts.find().pretty()
```

Create a post through the browser and run the query again to verify the data path.

## DevOps practice

```text
GitHub
  -> EC2 Linux
  -> .NET runtime
  -> MongoDB service
  -> dotnet restore
  -> dotnet build
  -> dotnet run
  -> port 5000
  -> EC2 Security Group
  -> Browser
  -> MongoDB CLI verification
```

Useful checks:

```bash
sudo systemctl status mongod
ss -lntp | grep :5000
curl http://localhost:5000
dotnet --version
git status
```

## Troubleshooting

MongoDB unavailable:

```bash
sudo systemctl status mongod
sudo systemctl start mongod
```

Application not reachable externally:

```bash
curl http://localhost:5000
ss -lntp | grep :5000
```

If localhost works but the browser does not, check the EC2 Security Group and public IP.

## Git hygiene

Do not commit `bin/`, `obj/`, IDE files, or local secret/configuration files.

## Production awareness

This is intentionally a single-instance learning setup. A production architecture would normally separate the application tier from the database tier and add a load balancer/reverse proxy, managed MongoDB, TLS, secrets management, centralized logging, monitoring, backups, and an automated deployment process.
