## Bildirim Ayarlarý Yönetimi

### Belirli Bir Kullanýcýya Ait Tüm Bildirim Ayarlarýný Al

**Endpoint:** `GET /api/NotificationSettings`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir kullanýcýya ait tüm bildirim ayarlarýný alýr.

**Yanýt:**

- **200 OK:** Bildirim ayarlarý baþarýyla döndürüldü.

**Örnek Yanýt:**
```json
[
  {
    "NotificationSettingsId": 1,
    "UserId": "user1",
    "EmailNotificationsEnabled": true,
    "SmsNotificationsEnabled": false,
    "PushNotificationsEnabled": true
  },
  {
    "NotificationSettingsId": 2,
    "UserId": "user1",
    "EmailNotificationsEnabled": true,
    "SmsNotificationsEnabled": true,
    "PushNotificationsEnabled": false
  }
]
```

### Belirli Bir ID'ye Sahip Bildirim Ayarlarýný Al

**Endpoint:** `GET /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirim ayarlarýný alýr.

**Parametreler:**
- **id (int):** Bildirim ayarlarý ID'si

**Yanýt:**

- **200 OK:** Bildirim ayarlarý baþarýyla döndürüldü.
- **404 Not Found:** Bildirim ayarlarý bulunamadý.

**Örnek Yanýt:**
```json
{
  "NotificationSettingsId": 1,
  "UserId": "user1",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": false,
  "PushNotificationsEnabled": true
}
```

### Yeni Bir Bildirim Ayarý Ekle

**Endpoint:** `POST /api/NotificationSettings`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Yeni bir bildirim ayarý ekler.

**Parametreler:**
- **notificationSettingsDto (NotificationSettingsCreateDto):** Bildirim ayarlarý detaylarý

**Yanýt:**

- **201 Created:** Bildirim ayarý baþarýyla eklendi.
- **400 Bad Request:** Bildirim ayarlarý detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "NotificationSettingsId": 3,
  "UserId": "user2",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": true,
  "PushNotificationsEnabled": false
}
```

### Belirli Bir ID'ye Sahip Bildirim Ayarlarýný Güncelle

**Endpoint:** `PUT /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirim ayarlarýný günceller.

**Parametreler:**
- **id (int):** Bildirim ayarlarý ID'si
- **notificationSettingsDto (NotificationSettingsUpdateDto):** Güncellenmiþ bildirim ayarlarý detaylarý

**Yanýt:**

- **204 No Content:** Bildirim ayarlarý baþarýyla güncellendi.
- **400 Bad Request:** Bildirim ayarlarý ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Bildirim ayarlarý bulunamadý.

### Belirli Bir ID'ye Sahip Bildirim Ayarlarýný Siler

**Endpoint:** `DELETE /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirim ayarlarýný siler.

**Parametreler:**
- **id (int):** Bildirim ayarlarý ID'si

**Yanýt:**

- **204 No Content:** Bildirim ayarlarý baþarýyla silindi.
- **404 Not Found:** Bildirim ayarlarý bulunamadý.