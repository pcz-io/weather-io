# Change Password

**URL** : `/api/add-update-favourite-location`

**Mathod** : `POST`

**Authorize** : Yes

**Input Data** :

```json
{
    "Id": "[number]",
    "Name": "[string]",
    "Latitude": "[double]",
    "Longitude": "[double]"
}
```

**Example** :

```json
{
    "Id": 0,
    "Name": "Warszawa",
    "Latitude": 52.22977,
    "Longitude": 21.01178
}
```

## Success

**URL** : `/api/add-update-favourite-location`

**Condition** : Valid data

**Input Data** :

```json
{
    "Id": 0,
    "Name": "Warszawa",
    "Latitude": 52.22977,
    "Longitude": 21.01178
}
```

**Code** : `200 OK`

## Error

**URL** : `/api/add-update-favourite-location`

**Condition** : Incorrect id when updating

**Input Data** :

```json
{
    "Id": 43506,
    "Name": "Warszawa",
    "Latitude": 52.22977,
    "Longitude": 21.01178
}
```

**Code** : `400 Bad request`
