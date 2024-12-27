# HotelUp - Customer service
![dockerhub_badge](https://github.com/Wiaz24/HotelUp.Customer/actions/workflows/dockerhub.yml/badge.svg)
![tests_badge](https://github.com/Wiaz24/HotelUp.Customer/actions/workflows/tests.yml/badge.svg)

This service should expose endpoints on port `5000` starting with:
```http
/api/customer/
```

## Healthchecks
Health status of the service should be available at:
```http
/api/customer/_health
```
and should return 200 OK if the service is running, otherwise 503 Service Unavailable.
