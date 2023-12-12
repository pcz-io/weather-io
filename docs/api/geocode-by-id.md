# Geocode by Id

**URL** : `/api/geocode-by-id?id=[int]`

**Mathod** : `GET`

**Authorize** : No

**Input Data** : No

## Success

**URL** : `/api/geocode-by-id?id=3100946`

**Code** : `200 OK`

**Example** :

```json
{
    "id": 3100946,
    "cityName": "Częstochowa",
    "longitude": 19.12409,
    "latitude": 50.79646
}
```

## Error

**URL** : `/api/geocode-by-id?id=999999999`

**Condition** : Incorrect city id

**Code** : `404 Not found`

**Content** :

```json
{
    "message": "Nie znaleziono miasta o podanym id: 999999999"
}
```
