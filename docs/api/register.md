# Register

**URL** : `/api/register/`

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
    "Status": "Success",
    "Message": "[string]"
}
```

**Example** :

```json
{
    "Status": "Success",
    "Message": "Użytkownik test@example.com utworzony!"
}
```

## Error

**Condition** : If user cannot be created (e.g. already exists)

**Code** : `500 INTERNAL SERVER ERROR`

**Content** :

```json
{
    "Status": "Error",
    "Message": "Użytkownik z podanym adresem e-mail już istnieje!"
}
```
