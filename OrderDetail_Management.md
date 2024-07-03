## Sipariþ Detaylarý Yönetimi

### Tüm Sipariþ Detaylarýný Al

**Endpoint:** `GET /api/OrderDetails`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Tüm sipariþ detaylarýný alýr.

**Yanýt:**

- **200 OK:** Sipariþ detaylarýnýn listesi baþarýyla döndürüldü.

**Örnek Yanýt:**
```json
[
  {
    "OrderDetailId": 1,
    "OrderId": 1,
    "BookId": 1,
    "Quantity": 2,
    "Price": 29.99
  },
  {
    "OrderDetailId": 2,
    "OrderId": 1,
    "BookId": 2,
    "Quantity": 1,
    "Price": 19.99
  }
]
```

### Belirli Bir ID'ye Sahip Sipariþ Detayýný Al

**Endpoint:** `GET /api/OrderDetails/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþ detayýný alýr.

**Parametreler:**
- **id (int):** Sipariþ detayý ID'si

**Yanýt:**

- **200 OK:** Sipariþ detayý baþarýyla döndürüldü.
- **404 Not Found:** Sipariþ detayý bulunamadý.

**Örnek Yanýt:**
```json
{
  "OrderDetailId": 1,
  "OrderId": 1,
  "BookId": 1,
  "Quantity": 2,
  "Price": 29.99
}
```

### Yeni Bir Sipariþ Detayý Ekle

**Endpoint:** `POST /api/OrderDetails`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Yeni bir sipariþ detayý ekler.

**Parametreler:**
- **orderDetailDto (OrderDetailCreateDto):** Sipariþ detayý detaylarý

**Yanýt:**

- **201 Created:** Sipariþ detayý baþarýyla eklendi.
- **400 Bad Request:** Sipariþ detayý detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "OrderDetailId": 1,
  "OrderId": 1,
  "BookId": 1,
  "Quantity": 2,
  "Price": 29.99
}
```

### Belirli Bir ID'ye Sahip Sipariþ Detayýný Güncelle

**Endpoint:** `PUT /api/OrderDetails/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþ detayýný günceller.

**Parametreler:**
- **id (int):** Sipariþ detayý ID'si
- **orderDetailDto (OrderDetailUpdateDto):** Güncellenmiþ sipariþ detayý detaylarý

**Yanýt:**

- **204 No Content:** Sipariþ detayý baþarýyla güncellendi.
- **400 Bad Request:** Sipariþ detayý ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Sipariþ detayý bulunamadý.

### Belirli Bir ID'ye Sahip Sipariþ Detayýný Siler

**Endpoint:** `DELETE /api/OrderDetails/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþ detayýný siler.

**Parametreler:**
- **id (int):** Sipariþ detayý ID'si

**Yanýt:**

- **204 No Content:** Sipariþ detayý baþarýyla silindi.
- **404 Not Found:** Sipariþ detayý bulunamadý.