## Sipariþ Yönetimi

### Tüm Sipariþleri Al

**Endpoint:** `GET /api/Orders`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Tüm sipariþleri alýr.

**Yanýt:**

- **200 OK:** Sipariþlerin listesi baþarýyla döndürüldü.

**Örnek Yanýt:**
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

### Belirli Bir ID'ye Sahip Sipariþi Al

**Endpoint:** `GET /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþi alýr.

**Parametreler:**
- **id (int):** Sipariþ ID'si

**Yanýt:**

- **200 OK:** Sipariþ detaylarý baþarýyla döndürüldü.
- **404 Not Found:** Sipariþ bulunamadý.

**Örnek Yanýt:**
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

### Yeni Bir Sipariþ Ekle

**Endpoint:** `POST /api/Orders`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Yeni bir sipariþ ekler.

**Parametreler:**
- **orderDto (OrderCreateDto):** Sipariþ detaylarý

**Yanýt:**

- **201 Created:** Sipariþ baþarýyla eklendi.
- **400 Bad Request:** Sipariþ detaylarý yanlýþsa.
- **401 Unauthorized:** Kullanýcý kimliði bulunamadýysa.

**Örnek Yanýt:**
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

### Belirli Bir ID'ye Sahip Sipariþi Güncelle

**Endpoint:** `PUT /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþi günceller.

**Parametreler:**
- **id (int):** Sipariþ ID'si
- **orderDto (OrderUpdateDto):** Güncellenmiþ sipariþ detaylarý

**Yanýt:**

- **204 No Content:** Sipariþ baþarýyla güncellendi.
- **400 Bad Request:** Sipariþ ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Sipariþ bulunamadý.

### Belirli Bir ID'ye Sahip Sipariþi Siler

**Endpoint:** `DELETE /api/Orders/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþi siler.

**Parametreler:**
- **id (int):** Sipariþ ID'si

**Yanýt:**

- **204 No Content:** Sipariþ baþarýyla silindi.
- **404 Not Found:** Sipariþ bulunamadý.