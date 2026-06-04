# Postman Testing Guide for OData Endpoints

## 🚀 Getting Started

### Step 1: Start Your Applications
1. Press **F5** in Visual Studio to run your main application
2. Main app runs at: `http://localhost:5106`
3. Start chatbot microservice in a separate process:
   - `dotnet run --project services/chatbot-service/ChatbotService.csproj`
4. Chatbot microservice runs at: `http://localhost:5000`
5. Keep the required application(s) running while testing

### Step 2: Open Postman
Download Postman from https://www.postman.com/downloads/ if you don't have it.

---

## 📋 Setting Up Postman Collection

### Create a New Collection
1. Click **Collections** in the left sidebar
2. Click **+** (New Collection)
3. Name it: `todolist-dotnet OData Tests`

**Note:** All URLs in this guide use the full URL `http://localhost:5106/odata/Todos` directly - no variables needed!

---

## 🤖 Chatbot Microservice Testing (New)

You can test the new chatbot endpoint from Postman just like any other API.

### Chat Endpoint

**Name:** Chatbot - Send Message

**Method:** POST

**URL:** `http://localhost:5000/api/chatbot/message`

**Headers:**
```
Content-Type: application/json
```

**Body (raw JSON):**
```json
{
  "message": "Hello! Can you help me plan my tasks for today?",
  "sessionId": "user-123",
  "systemPrompt": "You are a concise and helpful assistant."
}
```

**Expected Response:**
```json
{
  "reply": "...assistant response...",
  "sessionId": "user-123",
  "model": "...",
  "finishReason": "stop"
}
```

### Common Chatbot Test Cases

1. **Basic prompt**
   - Send only `message`
2. **With session id**
   - Include `sessionId` for client-side conversation tracking
3. **With system prompt**
   - Include `systemPrompt` to customize assistant behavior
4. **Validation check**
   - Send empty `message` and verify you get `400 Bad Request`

### Important Note

The Azure AI Foundry API key is configured on the server (`appsettings` / user-secrets / env vars), so Postman calls your local chatbot microservice API only. You do **not** need to send the Foundry API key from Postman to `/api/chatbot/message`.

Make sure `services/chatbot-service` is running before sending this request.

---

## 🧪 Test Requests to Add

### 1️⃣ GET ALL TODOS

**Name:** Get All Todos

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos`

**Expected Response:**
```json
{
  "@odata.context": "https://localhost:7134/odata/$metadata#Todos",
  "value": [
	{
	  "Id": 1,
	  "Title": "Create sample application",
	  "Description": "Build a web API with ASP.NET Core",
	  "IsCompleted": true,
	  "CreatedDate": "2024-01-15T10:00:00Z",
	  "CompletedDate": "2024-01-16T14:30:00Z"
	}
  ]
}
```

---

### 2️⃣ GET ALL TODOS WITH COUNT

**Name:** Get All Todos with Count

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$count=true`

**Expected Response:**
```json
{
  "@odata.context": "https://localhost:7134/odata/$metadata#Todos",
  "@odata.count": 5,
  "value": [...]
}
```

---

### 3️⃣ GET SINGLE TODO BY ID

**Name:** Get Todo by ID

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos(1)`

**Note:** OData uses parentheses for keys, not slashes like REST

---

### 4️⃣ FILTER: GET COMPLETED TODOS

**Name:** Filter - Completed Todos

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$filter=IsCompleted eq true`

---

### 5️⃣ FILTER: GET INCOMPLETE TODOS

**Name:** Filter - Incomplete Todos

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$filter=IsCompleted eq false`

---

### 6️⃣ FILTER: SEARCH BY TITLE

**Name:** Filter - Title Contains

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$filter=contains(Title, 'application')`

**Try these variations:**
- `contains(Title, 'meeting')`
- `startswith(Title, 'Create')`
- `endswith(Title, 'app')`

---

### 7️⃣ SORT BY CREATION DATE

**Name:** Sort - Newest First

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$orderby=CreatedDate desc`

**Variations:**
- Oldest first: `?$orderby=CreatedDate asc`
- By title: `?$orderby=Title`

---

### 8️⃣ SELECT SPECIFIC FIELDS

**Name:** Select - Specific Fields

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$select=Id,Title,IsCompleted`

**Why use this?** Reduces payload size by only returning needed fields

---

### 9️⃣ PAGINATION

**Name:** Pagination - Skip & Top

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$skip=0&$top=5`

**Try:**
- First 5: `?$top=5`
- Next 5: `?$skip=5&$top=5`
- Page 3 (5 per page): `?$skip=10&$top=5`

---

### 🔟 COMPLEX QUERY

**Name:** Complex Query - Filter + Sort + Select

**Method:** GET

**URL:** `http://localhost:5106/odata/Todos?$filter=IsCompleted eq false&$orderby=CreatedDate desc&$select=Id,Title,CreatedDate`

**This combines:**
- Filter incomplete todos
- Sort by newest first
- Return only Id, Title, CreatedDate

---

### 1️⃣1️⃣ CREATE NEW TODO (POST)

**Name:** Create New Todo

**Method:** POST

**URL:** `http://localhost:5106/odata/Todos`

**Headers:**
```
Content-Type: application/json
```

**Body (raw JSON):**
```json
{
  "Title": "Test OData in Postman",
  "Description": "Learning how to use OData endpoints",
  "IsCompleted": false
}
```

**Expected Response:** Status 201 Created with the created object

---

### 1️⃣2️⃣ UPDATE TODO - FULL UPDATE (PUT)

**Name:** Update Todo (PUT)

**Method:** PUT

**URL:** `http://localhost:5106/odata/Todos(1)`

**Headers:**
```
Content-Type: application/json
```

**Body (raw JSON):**
```json
{
  "Id": 1,
  "Title": "Updated Title from Postman",
  "Description": "This todo was updated via PUT request",
  "IsCompleted": true,
  "CreatedDate": "2024-01-01T00:00:00Z",
  "CompletedDate": "2024-01-15T10:30:00Z"
}
```

**Note:** PUT requires the full object

---

### 1️⃣3️⃣ UPDATE TODO - PARTIAL UPDATE (PATCH)

**Name:** Update Todo (PATCH)

**Method:** PATCH

**URL:** `http://localhost:5106/odata/Todos(2)`

**Headers:**
```
Content-Type: application/json
```

**Body (raw JSON):**
```json
{
  "IsCompleted": true
}
```

**Why PATCH?** Only update specific fields without sending the entire object

---

### 1️⃣4️⃣ DELETE TODO

**Name:** Delete Todo

**Method:** DELETE

**URL:** `http://localhost:5106/odata/Todos(3)`

**Expected Response:** Status 204 No Content

---

### 1️⃣5️⃣ GET ODATA METADATA

**Name:** Get OData Metadata

**Method:** GET

**URL:** `http://localhost:5106/odata/$metadata`

**What is this?** Shows the complete data model structure in EDMX format

---

## 🎯 Advanced OData Query Examples

### Filter by Date Range
```
http://localhost:5106/odata/Todos?$filter=CreatedDate ge 2024-01-01T00:00:00Z and CreatedDate le 2024-12-31T23:59:59Z
```

### Multiple Sort Orders
```
http://localhost:5106/odata/Todos?$orderby=IsCompleted desc,CreatedDate desc
```

### Combine Everything
```
http://localhost:5106/odata/Todos?$filter=IsCompleted eq false&$orderby=CreatedDate desc&$select=Id,Title&$top=10&$count=true
```

---

## 🔧 Troubleshooting

### ❌ Connection Refused
- Make sure your app is running (F5 in Visual Studio)
- Check the console output for the actual port number
- Verify the URL in Postman matches your app's URL

### ❌ 404 Not Found
- Check spelling: `/odata/Todos` (capital T)
- Ensure app is running
- Try the metadata endpoint first: `http://localhost:5106/odata/$metadata`

---

## 📊 Testing Workflow

### Recommended Testing Order:

1. **Start Simple:**
   - Get All Todos
   - Get Single Todo

2. **Test Filtering:**
   - Filter by IsCompleted
   - Search by Title

3. **Test Sorting & Selection:**
   - Order by Date
   - Select specific fields

4. **Test Pagination:**
   - Skip and Top

5. **Test CRUD Operations:**
   - Create new todo (POST)
   - Update todo (PUT/PATCH)
   - Delete todo (DELETE)

6. **Test Complex Queries:**
   - Combine filter + sort + select

---

## 💡 Pro Tips

### Save Responses as Examples
After getting a successful response:
1. Click **Save Response**
2. Click **Save as Example**
3. This helps document expected responses

### Use Tests Tab
Add automatic validation in the **Tests** tab:

```javascript
// Test for successful GET
pm.test("Status code is 200", function () {
	pm.response.to.have.status(200);
});

pm.test("Response has todos", function () {
	var jsonData = pm.response.json();
	pm.expect(jsonData.value).to.be.an('array');
});
```

### Use Pre-request Scripts
Set dynamic data before requests:

```javascript
// Set current timestamp
pm.environment.set("timestamp", new Date().toISOString());
```

### Export Your Collection
1. Right-click collection → **Export**
2. Save as JSON
3. Share with team members

---

## 📝 Quick Reference Card

| Feature | OData Syntax | Example |
|---------|--------------|---------|
| Filter | `?$filter=` | `?$filter=IsCompleted eq true` |
| Sort | `?$orderby=` | `?$orderby=CreatedDate desc` |
| Select fields | `?$select=` | `?$select=Id,Title` |
| Pagination | `?$top=&$skip=` | `?$top=10&$skip=20` |
| Count | `?$count=true` | `?$count=true` |
| Get by ID | `(id)` | `/odata/Todos(1)` |

### Filter Operators
- **Equals:** `eq` → `IsCompleted eq true`
- **Not equals:** `ne` → `IsCompleted ne false`
- **Greater than:** `gt` → `Id gt 5`
- **Less than:** `lt` → `Id lt 10`
- **And:** `and` → `IsCompleted eq true and Id gt 5`
- **Or:** `or` → `Title eq 'Test' or IsCompleted eq true`
- **Contains:** `contains(field, 'text')`
- **Starts with:** `startswith(field, 'text')`

---

## 🎓 Next Steps

1. **Create the collection** with all these requests
2. **Test each endpoint** one by one
3. **Experiment** with different query combinations
4. **Save examples** of successful responses
5. **Share your collection** with your team

Happy testing! 🚀
