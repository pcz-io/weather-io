# Change Password

**URL** : `/api/change-password`

**Mathod** : `POST`

**Authorize** : Yes

**Input Data** :

```json
{
    "OldPassword": "[string]",
    "NewPassword": "[string]"
}
```

**Example** :

```json
{
    "OldPassword": "zaq1@WSX",
    "NewPassword": "ZAQ!2wsx"
}
```

## Success

**URL** : `/api/change-password`

**Condition** : Correct old password and valid new password

**Input Data** :

```json
{
    "OldPassword": "zaq1@WSX",
    "NewPassword": "ZAQ!2wsx"
}
```

**Code** : `200 OK`

## Error

**URL** : `/api/change-password`

**Condition** : Incorrect old password

**Input Data** :

```json
{
    "OldPassword": "ZAQ!2wsx",
    "NewPassword": "zaq1@WSX"
}
```

**Code** : `400 Bad request`

**Content** :

```json
[
    {
        "code": "PasswordMismatch",
        "description": "Incorrect password."
    }
]
```
