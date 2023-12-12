# Get Forecast

Gets forecast for given location.
Default value for timezone is "Europe/Warsaw"
Default value for model is "dwdicon"

**URL** : `/api/get-forecast?latitude=[double]&longitude=[double]&timezone=[timezone string?]&model=[dwdicon|ecmwf ?]`

**Mathod** : `GET`

**Authorize** : No

**Input Data** : No

## Success

**URL** : `/api/get-forecast?latitude=19.5&longitude=50.1&model=dwdicon`

**Code** : `200 OK`

**Example** :

```json
{
    "current": {
        "time": "2023-10-24T17:30:00",
        "wmoCode": 3,
        "wmoString": "Pochmurno",
        "temperature": 11.4,
        "apparentTemperature": 7.9,
        "precipitation": 0,
        "humidity": 81,
        "pressure": 1002.9,
        "windSpeed": 20.5,
        "windDirection": 267,
        "windGusts": 37.8
    },
    "forecast": [
        {
            "time": "2023-10-24T00:00:00",
            "wmoCode": 2,
            "wmoString": "Częściowe zachmurzenie",
            "temperature": 8.1,
            "apparentTemperature": 2.8,
            "precipitation": 0,
            "humidity": 81,
            "pressure": 1009.8,
            "windSpeed": 28.2,
            "windDirection": 186,
            "windGusts": 45.4
        },
        {
            "time": "2023-10-24T01:00:00",
            "wmoCode": 3,
            "wmoString": "Pochmurno",
            "temperature": 8,
            "apparentTemperature": 2.8,
            "precipitation": 0,
            "humidity": 85,
            "pressure": 1008.9,
            "windSpeed": 28.4,
            "windDirection": 189,
            "windGusts": 45.4
        },
        ...
    ]
}
```

## Error

**Condition** : Longitude or latitude outside its bounds

**Code** : `404 Not found`

**Content** :

```json
{
    "message": "Nie znaleziono prognozy dla podanej lokalizacji"
}
```
