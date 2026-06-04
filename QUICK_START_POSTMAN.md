# 🚀 Quick Start: Testing OData in Postman

## Method 1: Import the Collection (Easiest!)

### Step 1: Import Collection
1. Open **Postman**
2. Click **Import** button (top left)
3. Click **Upload Files**
4. Select `todolist-dotnet_OData_Postman_Collection.json`
5. Click **Import**

✅ Done! All 20+ test requests are ready to use!

---

## Method 2: Manual Setup

If you prefer to create requests manually, follow the **POSTMAN_GUIDE.md** file.

---

## 🏃 Start Testing (4 Steps)

### Step 1: Run Your Main Application
```
Press F5 in Visual Studio
```
Main app runs at: **http://localhost:5106**

### Step 2: Run Chatbot Microservice (for chatbot tests)
```bash
dotnet run --project services/chatbot-service/ChatbotService.csproj
```
Chatbot microservice runs at: **http://localhost:5000**

### Step 3: Configure Postman for HTTPS (One-time setup)
**If you get SSL errors:**
1. Postman → **Settings** (⚙️ icon)
2. **General** tab
3. Turn **OFF**: "SSL certificate verification"

### Step 4: Test Your First Request
1. Open the imported collection
2. Expand **"1. Basic Queries"**
3. Click **"Get All Todos"**
4. Click **Send** button

**Expected Result:** You should see a JSON response with todos! 🎉

---

## 📍 Your OData Endpoints

| Endpoint | URL |
|----------|-----|
| **OData Base** | `http://localhost:5106/odata/Todos` |
| **Metadata** | `http://localhost:5106/odata/$metadata` |

**Note:** All URLs are hardcoded - no variables needed!

---

## 🤖 Chatbot Endpoint (New)

| Endpoint | URL |
|----------|-----|
| **Chatbot Message** | `http://localhost:5000/api/chatbot/message` |

**Method:** `POST`  
**Header:** `Content-Type: application/json`

**Body Example:**
```json
{
  "message": "Give me 3 productivity tips.",
  "sessionId": "quick-start-user",
  "systemPrompt": "You are a helpful assistant."
}
```

**Tip:** The Foundry API key is server-side configuration. You only call your local chatbot microservice endpoint from Postman.

**Important:** Run `services/chatbot-service` before testing chatbot requests.

---

## 🧪 Recommended Testing Order

### 1. Start Simple ✅
- **Get All Todos** - Verify endpoint works
- **Get All Todos with Count** - See total count
- **Get Todo by ID** - Test single item retrieval

### 2. Try Filtering 🔍
- **Filter - Completed Todos** - Only completed items
- **Filter - Incomplete Todos** - Only pending items
- **Filter - Title Contains** - Search functionality

### 3. Sort & Select 🎯
- **Sort - Newest First** - Order by date descending
- **Select Specific Fields** - Return only needed fields

### 4. Pagination 📄
- **Top 5 Todos** - Limit results
- **Skip 5, Take 5** - Paginate through data

### 5. Create & Modify ✏️
- **Create New Todo (POST)** - Add a new item
- **Partial Update (PATCH)** - Update one field
- **Update Todo (PUT)** - Full update
- **Delete Todo** - Remove an item

### 6. Complex Queries 🚀
- **Filter + Sort + Select** - Combine multiple operations
- **Filter + Sort + Page + Count** - All features together

### 7. Chatbot Test 🤖
- **POST /api/chatbot/message** - Send a chatbot prompt
- Try with and without `systemPrompt`
- Try an empty `message` to verify validation behavior

---

## 💡 Quick Tips

### If Your App Runs on a Different Port
If your app runs on a different port, you'll need to manually edit each request URL in the collection since we're using full URLs instead of variables.

Alternatively, you can:
1. Find and replace `5106` with your port number in the collection JSON file before importing
2. Or set up collection variables after import if you prefer

### Save Your Responses
After a successful test:
- Click **"Save Response"**
- Click **"Save as Example"**
- Helps document expected behavior

### Use Tests Tab for Automation
Add this to the **Tests** tab of any request:
```javascript
pm.test("Status code is 200", function () {
	pm.response.to.have.status(200);
});

pm.test("Response is JSON", function () {
	pm.response.to.be.json;
});
```

---

## 🆘 Troubleshooting

### ❌ "Could not get any response"
**Solution:** Make sure your app is running (F5 in Visual Studio)

### ❌ "404 Not Found"
**Solution:** 
- Check URL is exactly: `/odata/Todos` (capital T)
- Test metadata first: `http://localhost:5106/odata/$metadata`
- Verify app is running

### ❌ "Connection refused"
**Solution:** 
- Check Visual Studio console for actual port
- Ensure no firewall blocking localhost

---

## 📚 More Resources

- **POSTMAN_GUIDE.md** - Complete detailed guide
- **ODATA_USAGE.md** - OData query syntax reference
- **ODataTests.http** - VS Code REST Client format

---

## 🎯 Success Checklist

- [ ] Imported Postman collection
- [ ] Started application (F5)
- [ ] Sent first GET request successfully
- [ ] Tried filtering todos
- [ ] Created a new todo (POST)
- [ ] Updated a todo (PATCH)
- [ ] Tested complex queries

---

## 🎓 Next Steps

1. ✅ **Test all basic queries** (GET, GET by ID)
2. ✅ **Experiment with filters** (completed, search)
3. ✅ **Try CRUD operations** (POST, PUT, PATCH, DELETE)
4. ✅ **Combine query options** (filter + sort + page)
5. ✅ **Save examples** of successful responses
6. ✅ **Add automated tests** in Tests tab

---

**Ready? Press F5 in Visual Studio and start testing! 🚀**
