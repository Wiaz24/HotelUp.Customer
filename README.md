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

## AMQP exchanges
This service creates the following exchanges:
- `HotelUp.Customer:ReservationCreatedEvent` - to notify about new reservations. The message provides the following example payload structure:
    ```json
    {
      "reservationDto":{
        "id":"3ef70b03-2fa4-4a94-b5d8-91c0d0077147",
        "status":"Valid",
        "startDate":"2025-01-01T00:00:00Z",
        "endDate":"2025-01-05T00:00:00Z",
        "rooms":[
          {
            "id":2,
            "capacity":2,
            "floor":0,
            "withSpecialNeeds":false,
            "type":"Basic",
            "imageUrl":"https://www.example.com/image.jpg"
          }
        ],
        "bill":{
          "accommodationPrice":"180,4 PLN",
          "additionalCosts":[
    
          ],
          "payments":[
    
          ]
        },
        "tenants":[
          {
            "firstName":"John",
            "lastName":"Doe",
            "phoneNumber":"123456789",
            "email":"john.doe@email.com",
            "pesel":"12345678901",
            "documentType":"Passport",
            "status":"Pending"
          }
        ]
      }
    }
    ```

- `HotelUp.Customer:ReservationCanceledEvent` - to notify about canceled reservations. The message provides the following example payload structure:
    ```json
    {
      "reservationId":"3ef70b03-2fa4-4a94-b5d8-91c0d0077147"
    }
    ```