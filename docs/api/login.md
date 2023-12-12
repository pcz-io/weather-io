# Login

**URL** : `/api/login/`

**Method** : `POST`

**Authorize** : No

**Input Data** :

```json
{
    "Email": "[string]",
    "Password": "[string]"
}
```

**Example** :

```json
{
    "Email": "test@example.com",
    "Password": "zaq1@WSX"
}
```

## Success

**Code** : `200 OK`

**Content** :

```json
{
    "Token": "[token]"
}
```

**Example** :

```json
{
    "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoic3RyaW5nIiwianRpIjoiNmY0ODQzYWYtNDIwMy00NWQ3LTk0MjgtNDE0YWUzNmY0MjZiIiwiZXhwIjoxNjk3NDc0NTY0LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjYxOTU1IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo0MjAwIn0.-wV4hvEX5gltdZC3MOniLCp3BRU4Y8SWLhPF0sHmSTA"
}
```

## Error

**Condition** : Username or password are incorrect

**Code** : `401 UNAUTHORIZED`

**Content** : `Brak`
