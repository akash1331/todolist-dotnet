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
cd SampleWebApplication1
```

### Run the Application

#### Option 1: Visual Studio
1. Open `SampleWebApplication1.sln`
2. Press `F5` to run
3. Navigate to `http://localhost:5106`

#### Option 2: Command Line
```bash
dotnet restore
dotnet run
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

```
SampleWebApplication1/
├── Controllers/
│   └── TodoController.cs      # API endpoints
├── Data/
│   └── TodoContext.cs          # EF Core DbContext
├── Models/
│   └── Todo.cs                 # Todo entity model
├── wwwroot/
│   └── index.html              # Frontend UI
├── Program.cs                  # App configuration
├── appsettings.json            # App settings
├── Dockerfile                  # Docker configuration
└── README.md                   # This file
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

## 📄 License

This project is open source and available under the [MIT License](LICENSE).

## 👨‍💻 Author
Akash
Created using ASP.NET Core 10

---

**Enjoy organizing your tasks!** 🎉
