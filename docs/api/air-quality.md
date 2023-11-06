# Air Quality

**URL** : `/api/air-quality?latitude=[double]&longitude=[double]&timezone=Europe%2FWarsaw`

**Mathod** : `GET`

**Authorize** : No

**Input Data** : No

## Success

**URL** : `/api/air-quality?latitude=50&longitude=20`

**Code** : `200 OK`

**Example** :

```json
{
    "airQualityIndex": 45,
    "airQualityString": "średnia"
}
```

## Error

**URL** : `/api/air-quality?latitude=350&longitude=20`

**Condition** : Incorrect latitude or longitude

**Code** : `404 Not found`

**Content** : None
