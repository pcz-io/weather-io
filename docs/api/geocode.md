# Geocode

**URL** : `/api/geocode?name=[string]`

**Mathod** : `GET`

**Authorize** : No

**Input Data** : No

## Success

**URL** : `/api/geocode?name=Cze`

**Code** : `200 OK`

**Example** :

```json
[
    {
        "id": 3645213,
        "cityName": "Coro",
        "longitude": -69.67344,
        "latitude": 11.4045
    },
    {
        "id": 1846266,
        "cityName": "Czedżu",
        "longitude": 126.52194,
        "latitude": 33.50972
    },
    ...
]
```

## Error

**URL** : `/api/geocode?name=x`

**Condition** : Incorrect city name

**Code** : `404 Not found`

**Content** :

```json
{
    "message": "Nie znaleziono miasta o podanej nazwie: x"
}
```
