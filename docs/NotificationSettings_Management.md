## Bildirim Ayarlar� Y�netimi

### Belirli Bir Kullan�c�ya Ait T�m Bildirim Ayarlar�n� Al

**Endpoint:** `GET /api/NotificationSettings`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir kullan�c�ya ait t�m bildirim ayarlar�n� al�r.

**Yan�t:**

- **200 OK:** Bildirim ayarlar� ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
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

### Belirli Bir ID'ye Sahip Bildirim Ayarlar�n� Al

**Endpoint:** `GET /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirim ayarlar�n� al�r.

**Parametreler:**
- **id (int):** Bildirim ayarlar� ID'si

**Yan�t:**

- **200 OK:** Bildirim ayarlar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Bildirim ayarlar� bulunamad�.

**�rnek Yan�t:**
```json
{
  "NotificationSettingsId": 1,
  "UserId": "user1",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": false,
  "PushNotificationsEnabled": true
}
```

### Yeni Bir Bildirim Ayar� Ekle

**Endpoint:** `POST /api/NotificationSettings`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Yeni bir bildirim ayar� ekler.

**Parametreler:**
- **notificationSettingsDto (NotificationSettingsCreateDto):** Bildirim ayarlar� detaylar�

**Yan�t:**

- **201 Created:** Bildirim ayar� ba�ar�yla eklendi.
- **400 Bad Request:** Bildirim ayarlar� detaylar� yanl��sa.

**�rnek Yan�t:**
```json
{
  "NotificationSettingsId": 3,
  "UserId": "user2",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": true,
  "PushNotificationsEnabled": false
}
```

### Belirli Bir ID'ye Sahip Bildirim Ayarlar�n� G�ncelle

**Endpoint:** `PUT /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirim ayarlar�n� g�nceller.

**Parametreler:**
- **id (int):** Bildirim ayarlar� ID'si
- **notificationSettingsDto (NotificationSettingsUpdateDto):** G�ncellenmi� bildirim ayarlar� detaylar�

**Yan�t:**

- **204 No Content:** Bildirim ayarlar� ba�ar�yla g�ncellendi.
- **400 Bad Request:** Bildirim ayarlar� ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Bildirim ayarlar� bulunamad�.

### Belirli Bir ID'ye Sahip Bildirim Ayarlar�n� Siler

**Endpoint:** `DELETE /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirim ayarlar�n� siler.

**Parametreler:**
- **id (int):** Bildirim ayarlar� ID'si

**Yan�t:**

- **204 No Content:** Bildirim ayarlar� ba�ar�yla silindi.
- **404 Not Found:** Bildirim ayarlar� bulunamad�.