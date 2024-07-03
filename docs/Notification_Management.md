## Bildirim Y�netimi

### Belirli Bir Kullan�c�ya Ait T�m Bildirimleri Al

**Endpoint:** `GET /api/Notifications`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir kullan�c�ya ait t�m bildirimleri al�r.

**Yan�t:**

- **200 OK:** Bildirimler ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
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

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirimi al�r.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yan�t:**

- **200 OK:** Bildirim detaylar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Bildirim bulunamad�.

**�rnek Yan�t:**
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

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Yeni bir bildirim ekler.

**Parametreler:**
- **notificationDto (NotificationCreateDto):** Bildirim detaylar�

**Yan�t:**

- **201 Created:** Bildirim ba�ar�yla eklendi.
- **400 Bad Request:** Bildirim detaylar� yanl��sa.

**�rnek Yan�t:**
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

### Belirli Bir ID'ye Sahip Bildirimi G�ncelle

**Endpoint:** `PUT /api/Notifications/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirimi g�nceller.

**Parametreler:**
- **id (int):** Bildirim ID'si
- **notificationDto (NotificationUpdateDto):** G�ncellenmi� bildirim detaylar�

**Yan�t:**

- **204 No Content:** Bildirim ba�ar�yla g�ncellendi.
- **400 Bad Request:** Bildirim ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Bildirim bulunamad�.

### Belirli Bir ID'ye Sahip Bildirimi Siler

**Endpoint:** `DELETE /api/Notifications/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirimi siler.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yan�t:**

- **204 No Content:** Bildirim ba�ar�yla silindi.
- **404 Not Found:** Bildirim bulunamad�.