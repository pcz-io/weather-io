# Change Password

**URL** : `/api/delete-favourite-location`

**Mathod** : `POST`

**Authorize** : Yes

**Input Data** :

```json
{
    "Id": "[number]",
    "Name": "[string?]",
    "Latitude": "[double?]",
    "Longitude": "[double?]"
}
```

**Example** :

```json
{
    "Id": 2
}
```

## Success

**URL** : `/api/delete-favourite-location`

**Condition** : Valid data

**Input Data** :

```json
{
    "Id": 2
}
```

**Code** : `200 OK`

## Error

**URL** : `/api/delete-favourite-location`

**Condition** : Incorrect id

**Input Data** :

```json
{
    "Id": 43506
}
```

**Code** : `400 Bad request`
