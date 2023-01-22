## API.

### Storing a figure.

##### Request: ```POST``` ```/figures```

Example:

```
curl --location --request POST 'https://localhost:5001/figures' \
--header 'Content-Type: application/json' \
--data-raw '{
    "type": "circle",
    "radius": 3.0
}'
```

```
curl --location --request POST 'https://localhost:5001/figures' \
--header 'Content-Type: application/json' \
--data-raw '{
    "type": "triangle",
    "a": 3.0,
    "b": 4.0,
    "c": 5.0
}'
```

##### Response

HTTP Code: ```201```

```
{
    "type": "triangle",
    "a": 3.0,
    "b": 4.0,
    "c": 5.0
}
```

#### If any of provided arguments was not valid:

```
curl --location --request POST 'https://localhost:5001/figures' \
--header 'Content-Type: application/json' \
--data-raw '{
    "type": "triangle",
    "a": -3.0,
    "b": 4.0,
    "c": 5.0
}'
```

##### Response

HTTP Code: ```400```

```
{
    "title": "Invalid argument supplied",
    "detail": "Length of a side of the triangle should be a positive number (Parameter 'a')"
}
```

### Retrieving a figure

##### Request: ```GET``` ```/figures/{id}```

Example:

```
curl --location --request GET 'https://localhost:5001/figures/1'
```

##### Response

HTTP Code: ```200```

```
{
    "type": "circle",
    "radius": 8.0
}
```

#### If there is no such a figure.

```curl --location --request GET 'https://localhost:5001/figures/5```

##### Response

HTTP Code: ```404```

```
{
    "title": "Specified entity does not exist",
    "detail": "There is no figure associated with id 5"
}
```

### Area calculation.

##### Request: ```GET``` ```/figures/{id}/area```

Example:

```
curl --location --request GET 'https://localhost:5001/figures/1/area'
```

##### Response

HTTP Code: ```200```

```
{
    "area": 6.0
}
```

#### If there is no such a figure.

```curl --location --request GET 'https://localhost:5001/figures/5/area```

##### Response

HTTP Code: ```404```

```
{
    "title": "Specified entity does not exist",
    "detail": "There is no figure associated with id 5"
}
```
