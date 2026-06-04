# 📝 Todo List Application

A modern, full-stack Todo List web application built with ASP.NET Core 10 and vanilla JavaScript.

## 🚀 Features

- ✅ **Full CRUD Operations** - Create, Read, Update, and Delete todos
- 📊 **Live Statistics** - Real-time tracking of total, pending, and completed tasks
- 🔍 **Smart Filtering** - Filter todos by All, Pending, or Completed status
- 💾 **In-Memory Database** - Uses Entity Framework Core with InMemory provider
- 🎨 **Modern UI** - Beautiful, responsive design with gradient theme
- 📱 **Mobile Responsive** - Works seamlessly on desktop, tablet, and mobile
- ⏰ **Smart Timestamps** - Human-readable relative time (e.g., "5 mins ago")
- 🎭 **Smooth Animations** - Elegant slide-in effects and transitions

## 🛠️ Technologies

**Backend:**
- ASP.NET Core 10
- Entity Framework Core 10 (InMemory)
- C# 13

**Frontend:**
- HTML5
- CSS3 (Modern Flexbox/Grid)
- Vanilla JavaScript (ES6+)

**Tools:**
- Visual Studio 2026
- Docker Support

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio 2026 or VS Code
- Any modern web browser

## 🎯 Getting Started

### Clone the Repository

```bash
git clone <your-repo-url>
cd todolist-dotnet
```

### Run the Application

#### Option 1: Visual Studio
1. Open `todolist-dotnet.slnx`
2. Press `F5` to run
3. Navigate to `http://localhost:5106`

#### Option 2: Command Line
```bash
dotnet restore todolist-dotnet.slnx
dotnet run --project todolist-dotnet.csproj
```
Then open `http://localhost:5106` in your browser.

#### Option 3: Docker
```bash
docker build -t todolist-app .
docker run -p 5106:8080 todolist-app
```

## 🌐 API Endpoints

### Todo Operations

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todo` | Get all todos |
| GET | `/api/todo/{id}` | Get todo by ID |
| POST | `/api/todo` | Create new todo |
| PUT | `/api/todo/{id}` | Update todo |
| DELETE | `/api/todo/{id}` | Delete todo |
| GET | `/api/todo/completed` | Get completed todos |
| GET | `/api/todo/pending` | Get pending todos |

### Request/Response Examples

**Create Todo:**
```json
POST /api/todo
{
  "title": "Complete project",
  "description": "Finish the todo app"
}
```

**Update Todo:**
```json
PUT /api/todo/1
{
  "title": "Complete project",
  "description": "Finish the todo app",
  "isCompleted": true
}
```

## 📁 Project Structure

```text
todolist-dotnet/
├── Controllers/
│   └── TodoController.cs                 # Todo API endpoints
├── Data/
│   └── TodoContext.cs                    # EF Core DbContext
├── Models/
│   └── Todo.cs                           # Todo entity model
├── services/
│   └── chatbot-service/                  # Separate chatbot microservice
│       ├── Controllers/
│       │   └── ChatbotController.cs
│       ├── Models/
│       │   └── ChatModels.cs
│       ├── Services/
│       │   ├── IChatbotService.cs
│       │   └── AzureFoundryChatService.cs
│       ├── Program.cs
│       ├── appsettings.json
│       └── ChatbotService.csproj
├── wwwroot/
│   └── index.html                        # Frontend UI (wired to chatbot microservice)
├── Program.cs                            # Main app configuration
├── appsettings.json                      # Main app settings
├── Dockerfile                            # Docker configuration
└── README.md                             # This file
```

## 💡 Usage

### Adding a Todo
1. Enter a task title (required)
2. Optionally add a description
3. Click "➕ Add Todo"

### Managing Todos
- **Complete:** Click the checkbox next to a todo
- **Delete:** Click the 🗑️ Delete button
- **Filter:** Use the All/Pending/Completed buttons to filter your view

### Statistics
The dashboard shows:
- **Total** - All todos
- **Pending** - Incomplete tasks
- **Completed** - Finished tasks

## 🎨 UI Features

- **Purple Gradient Theme** - Modern, eye-catching design
- **Card-Based Layout** - Clean, organized interface
- **Hover Effects** - Interactive elements with smooth transitions
- **Empty State** - Friendly message when no todos exist
- **Error Handling** - User-friendly error messages

## 🔧 Configuration

The application uses an **In-Memory Database**, which means:
- ✅ No database setup required
- ✅ Perfect for development and testing
- ⚠️ Data is reset when the app restarts

To use a persistent database, modify `Program.cs` to use SQL Server, PostgreSQL, or another provider.

## 🐳 Docker Support

The project includes Docker support with a multi-stage Dockerfile optimized for production.

## 📝 Notes

- The app comes with 2 pre-seeded sample todos
- All dates/times are stored and displayed in UTC
- The InMemory database is perfect for demos and development

## 🤝 Contributing

Feel free to submit issues and enhancement requests!

## 🤖 Chatbot Microservice (Separate Service)

A separate chatbot microservice has been added under:

```text
services/chatbot-service
```

This service is independently deployable and exposes chatbot endpoints backed by Azure AI Foundry.

### Chatbot Microservice Endpoint

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/chatbot/message` | Send a message to the chatbot |

### Run Chatbot Microservice

From workspace root:

```bash
dotnet run --project services/chatbot-service/ChatbotService.csproj
```

Then call (example):

```text
http://localhost:5000/api/chatbot/message
```

> Note: port may vary by environment. Use launch output for the exact URL.

### Frontend Wiring (Main App -> Chatbot Microservice)

The main frontend (`wwwroot/index.html`) is wired to call the separate chatbot service at:

```text
http://localhost:5000/api/chatbot/message
```

To use chatbot from UI, run **both** apps:

```bash
dotnet run --project todolist-dotnet.csproj
```

```bash
dotnet run --project services/chatbot-service/ChatbotService.csproj
```

If your chatbot service runs on a different port, update `CHAT_SERVICE_BASE_URL` in `wwwroot/index.html`.

### Configure Azure AI Foundry

Set these values for `services/chatbot-service`:

- `AzureAIFoundry:ChatCompletionsUrl`
- `AzureAIFoundry:ApiKey`
- `AzureAIFoundry:Temperature`
- `AzureAIFoundry:MaxTokens`
- `Cors:AllowedOrigins` (ensure main app origin is included)

Prefer user-secrets or environment variables for API keys.

### Sample Request

```json
{
  "message": "Hello!",
  "sessionId": "sample-session",
  "systemPrompt": "You are a helpful assistant."
}
```

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 👨‍💻 Author
Akash
Created using ASP.NET Core 10

---

**Enjoy organizing your tasks!** 🎉
