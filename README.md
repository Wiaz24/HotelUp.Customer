# HotelUp - Customer service
![Application tests](https://github.com/Wiaz24/HotelUp.Customer/actions/workflows/tests.yml/badge.svg)
![Github issues](https://img.shields.io/github/issues/Wiaz24/HotelUp.Customer)
[![Docker Image Size](https://badgen.net/docker/size/wiaz/hotelup.customer?icon=docker&label=image%20size)](https://hub.docker.com/r/wiaz/hotelup.customer/)

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

## AMQP exchanges
This service creates the following exchanges:
- `HotelUp.Customer:ReservationCreatedEvent` - to notify about new reservations. The message provides the following example payload structure:
    ```json
    {
       "reservationId":"00ce21d3-1b14-4d95-9a68-aa3a883e6e09",
       "startDate":"2025-11-01T00:00:00Z",
       "endDate":"2025-11-05T00:00:00Z",
       "rooms":[
          {
             "id":1,
             "capacity":2,
             "floor":0,
             "withSpecialNeeds":false,
             "type":"Basic",
             "imageUrl":"https://s3.us-east-1.amazonaws.com/hotelup.customer.storage/rooms/1/room1.jpg"
          }
       ]
    }
    ```

- `HotelUp.Customer:ReservationCanceledEvent` - to notify about canceled reservations. The message provides the following example payload structure:
    ```json
    {
      "reservationId":"3ef70b03-2fa4-4a94-b5d8-91c0d0077147"
    }
    ```
  
- `HotelUp.Customer:RoomCreatedEvent` - to notify about new rooms. The message provides the following example payload structure:
    ```json
    {
      "id":1,
      "capacity":2,
      "floor":0,
      "withSpecialNeeds":false,
      "type":"Basic",
      "imageUrl":"https://s3.us-east-1.amazonaws.com/hotelup.customer.storage/rooms/1/room1.jpg"
    }
    ```