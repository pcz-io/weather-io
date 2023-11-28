# Change Password

**URL** : `/api/get-favourite-locations`

**Mathod** : `GET`

**Authorize** : Yes

**Input Data** : No

## Success

**URL** : `/api/get-favourite-locations`

**Condition** : Valid data

**Example result** :

```json
[
    {
        "id": 9,
        "name": "Warszawa",
        "latitude": 52.22977,
        "longitude": 21.01178
    },
    {
        "id": 10,
        "name": "Częstochowa",
        "latitude": 50.49646,
        "longitude": 19.62409
    }
]
```

**Code** : `200 OK`

## Error

**URL** : `/api/get-favourite-locations`

**Condition** : Probably some error with token

**Code** : `400 Bad request`
