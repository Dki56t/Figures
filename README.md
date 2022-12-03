API.

To store a figure.
Request: ```POST``` ```/figure```

Example:

```
curl --location --request POST 'https://localhost:5001/figure' \
--header 'Content-Type: application/json' \
--data-raw '{
    "type": "circle",
    "radius": 8
}'
```

```
curl --location --request POST 'https://localhost:5001/figure' \
--header 'Content-Type: application/json' \
--data-raw '{
    "type": "triangle",
    "A": 3,
    "B": 4,
    "C": 5
}'
```

Response

HTTP Code: ```200```

```
{
    "id": 1
}
```

If any of provided arguments was not valid:

```
curl --location --request POST 'https://localhost:5001/figure' \
--header 'Content-Type: application/json' \
--data-raw '{
    "type": "triangle",
    "A": -3,
    "B": 4,
    "C": 5
}'
```

Response

HTTP Code: ```400```

```
{
    "title": "Invalid argument supplied",
    "detail": "Length of a side of the triangle should be a positive number (Parameter 'a')"
}
```

Area calculation.

Request: ```GET``` ```/figure/{id}```

Example:

```
curl --location --request GET 'https://localhost:5001/figure/3'
```

Response

HTTP Code: ```200```

```
{
    "area": 6.0
}
```

If there is no such a figure.

```curl --location --request GET 'https://localhost:5001/figure/5'```

Response

HTTP Code: ```404```

```
{
    "title": "Specified entity does not exist",
    "detail": "There is no figure associated with id 5"
}
```
