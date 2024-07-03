## Sipari� Y�netimi

### T�m Sipari�leri Al

**Endpoint:** `GET /api/Orders`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** T�m sipari�leri al�r.

**Yan�t:**

- **200 OK:** Sipari�lerin listesi ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
```json
[
  {
    "OrderId": 1,
    "OrderDate": "2023-06-30T12:00:00Z",
    "UserName": "JohnDoe",
    "OrderDetails": [
      {
        "OrderDetailId": 1,
        "BookId": 1,
        "BookTitle": "Book Title",
        "Quantity": 2,
        "Price": 29.99
      }
    ]
  },
  {
    "OrderId": 2,
    "OrderDate": "2023-06-30T13:00:00Z",
    "UserName": "JaneDoe",
    "OrderDetails": [
      {
        "OrderDetailId": 2,
        "BookId": 2,
        "BookTitle": "Another Book Title",
        "Quantity": 1,
        "Price": 19.99
      }
    ]
  }
]
```

### Belirli Bir ID'ye Sahip Sipari�i Al

**Endpoint:** `GET /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari�i al�r.

**Parametreler:**
- **id (int):** Sipari� ID'si

**Yan�t:**

- **200 OK:** Sipari� detaylar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Sipari� bulunamad�.

**�rnek Yan�t:**
```json
{
  "OrderId": 1,
  "OrderDate": "2023-06-30T12:00:00Z",
  "UserName": "JohnDoe",
  "OrderDetails": [
    {
      "OrderDetailId": 1,
      "BookId": 1,
      "BookTitle": "Book Title",
      "Quantity": 2,
      "Price": 29.99
    }
  ]
}
```

### Yeni Bir Sipari� Ekle

**Endpoint:** `POST /api/Orders`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Yeni bir sipari� ekler.

**Parametreler:**
- **orderDto (OrderCreateDto):** Sipari� detaylar�

**Yan�t:**

- **201 Created:** Sipari� ba�ar�yla eklendi.
- **400 Bad Request:** Sipari� detaylar� yanl��sa.
- **401 Unauthorized:** Kullan�c� kimli�i bulunamad�ysa.

**�rnek Yan�t:**
```json
{
  "OrderId": 1,
  "OrderDate": "2023-06-30T12:00:00Z",
  "UserName": "JohnDoe",
  "OrderDetails": [
    {
      "OrderDetailId": 1,
      "BookId": 1,
      "BookTitle": "Book Title",
      "Quantity": 2,
      "Price": 29.99
    }
  ]
}
```

### Belirli Bir ID'ye Sahip Sipari�i G�ncelle

**Endpoint:** `PUT /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari�i g�nceller.

**Parametreler:**
- **id (int):** Sipari� ID'si
- **orderDto (OrderUpdateDto):** G�ncellenmi� sipari� detaylar�

**Yan�t:**

- **204 No Content:** Sipari� ba�ar�yla g�ncellendi.
- **400 Bad Request:** Sipari� ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Sipari� bulunamad�.

### Belirli Bir ID'ye Sahip Sipari�i Siler

**Endpoint:** `DELETE /api/Orders/{id}`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari�i siler.

**Parametreler:**
- **id (int):** Sipari� ID'si

**Yan�t:**

- **204 No Content:** Sipari� ba�ar�yla silindi.
- **404 Not Found:** Sipari� bulunamad�.