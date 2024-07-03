## Bildirim Yönetimi

### Belirli Bir Kullanýcýya Ait Tüm Bildirimleri Al

**Endpoint:** `GET /api/Notifications`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir kullanýcýya ait tüm bildirimleri alýr.

**Yanýt:**

- **200 OK:** Bildirimler baþarýyla döndürüldü.

**Örnek Yanýt:**
```json
[
  {
    "NotificationId": 1,
    "UserId": "user1",
    "Title": "New Book Added",
    "Message": "A new book has been added to your library.",
    "Date": "2023-07-01T12:00:00Z",
    "NotificationType": "Email"
  },
  {
    "NotificationId": 2,
    "UserId": "user1",
    "Title": "Order Shipped",
    "Message": "Your order has been shipped.",
    "Date": "2023-07-02T15:30:00Z",
    "NotificationType": "SMS"
  }
]
```

### Belirli Bir ID'ye Sahip Bildirimi Al

**Endpoint:** `GET /api/Notifications/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirimi alýr.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yanýt:**

- **200 OK:** Bildirim detaylarý baþarýyla döndürüldü.
- **404 Not Found:** Bildirim bulunamadý.

**Örnek Yanýt:**
```json
{
  "NotificationId": 1,
  "UserId": "user1",
  "Title": "New Book Added",
  "Message": "A new book has been added to your library.",
  "Date": "2023-07-01T12:00:00Z",
  "NotificationType": "Email"
}
```

### Yeni Bir Bildirim Ekle

**Endpoint:** `POST /api/Notifications`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Yeni bir bildirim ekler.

**Parametreler:**
- **notificationDto (NotificationCreateDto):** Bildirim detaylarý

**Yanýt:**

- **201 Created:** Bildirim baþarýyla eklendi.
- **400 Bad Request:** Bildirim detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "NotificationId": 3,
  "UserId": "user2",
  "Title": "Book Returned",
  "Message": "Your book has been returned.",
  "Date": "2023-07-03T10:15:00Z",
  "NotificationType": "PushNotification"
}
```

### Belirli Bir ID'ye Sahip Bildirimi Güncelle

**Endpoint:** `PUT /api/Notifications/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirimi günceller.

**Parametreler:**
- **id (int):** Bildirim ID'si
- **notificationDto (NotificationUpdateDto):** Güncellenmiþ bildirim detaylarý

**Yanýt:**

- **204 No Content:** Bildirim baþarýyla güncellendi.
- **400 Bad Request:** Bildirim ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Bildirim bulunamadý.

### Belirli Bir ID'ye Sahip Bildirimi Siler

**Endpoint:** `DELETE /api/Notifications/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirimi siler.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yanýt:**

- **204 No Content:** Bildirim baþarýyla silindi.
- **404 Not Found:** Bildirim bulunamadý.