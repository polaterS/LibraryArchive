## Kullanýcý Yönetimi

### Tüm Kullanýcýlarý Al

**URL:** `/api/Users`

**Yöntem:** `GET`

**Yetki:** `Admin`

**Açýklama:** Tüm kullanýcýlarý alýr.

**Yanýt:**

```json
[
  {
    "Id": "user_id",
    "UserName": "username",
    "Email": "user@example.com",
    "PhoneNumber": "1234567890",
    "Name": "John",
    "Surname": "Doe",
    "IsActive": true,
    "ProfilePictureUrl": "https://example.com/picture.jpg",
    "Roles": ["User"],
    "Books": [],
    "Notes": [],
    "Orders": [],
    "Addresses": []
  }
]
```

**Yanýt Kodlarý:**

- `200 OK` - Kullanýcýlarýn listesi baþarýyla döndürüldü

### Kullanýcý Detaylarýný Al

**URL:** `/api/Users/{id}`

**Yöntem:** `GET`

**Yetki:** `Admin`

**Açýklama:** Belirli bir ID'ye sahip kullanýcýyý alýr.

**Ýstek Parametreleri:**

- `id` (string): Kullanýcý ID'si

**Yanýt:**

```json
{
  "Id": "user_id",
  "UserName": "username",
  "Email": "user@example.com",
  "PhoneNumber": "1234567890",
  "Name": "John",
  "Surname": "Doe",
  "IsActive": true,
  "ProfilePictureUrl": "https://example.com/picture.jpg",
  "Roles": ["User"],
  "Books": [],
  "Notes": [],
  "Orders": [],
  "Addresses": []
}
```

**Yanýt Kodlarý:**

- `200 OK` - Kullanýcý baþarýyla döndürüldü
- `404 Not Found` - Kullanýcý bulunamadý

### Yeni Kullanýcý Kaydet

**URL:** `/api/Users`

**Yöntem:** `POST`

**Yetki:** `Admin`

**Açýklama:** Yeni bir kullanýcý kaydeder.

**Ýstek:**

```json
{
  "UserName": "newuser",
  "Email": "newuser@example.com",
  "Password": "YourPassword123",
  "Name": "Jane",
  "Surname": "Doe"
}
```

**Yanýt:**

```json
{
  "Id": "new_user_id",
  "UserName": "newuser",
  "Email": "newuser@example.com",
  "PhoneNumber": "1234567890",
  "Name": "Jane",
  "Surname": "Doe",
  "IsActive": true,
  "ProfilePictureUrl": "https://example.com/picture.jpg",
  "Roles": ["User"],
  "Books": [],
  "Notes": [],
  "Orders": [],
  "Addresses": []
}
```

**Yanýt Kodlarý:**

- `201 Created` - Kullanýcý baþarýyla kaydedildi
- `400 Bad Request` - Kullanýcý detaylarý yanlýþsa

### Kullanýcý Güncelle

**URL:** `/api/Users/{id}`

**Yöntem:** `PUT`

**Yetki:** `Admin`

**Açýklama:** Belirli bir ID'ye sahip kullanýcýyý günceller.

**Ýstek Parametreleri:**

- `id` (string): Kullanýcý ID'si

**Ýstek:**

```json
{
  "Id": "user_id",
  "Email": "updateduser@example.com",
  "Name": "Updated",
  "Surname": "User"
}
```

**Yanýt Kodlarý:**

- `204 No Content` - Kullanýcý baþarýyla güncellendi
- `400 Bad Request` - Kullanýcý ID uyumsuzluðu veya detaylarý yanlýþsa
- `404 Not Found` - Kullanýcý bulunamadý

### Kullanýcý Sil

**URL:** `/api/Users/{id}`

**Yöntem:** `DELETE`

**Yetki:** `Admin`

**Açýklama:** Belirli bir ID'ye sahip kullanýcýyý siler.

**Ýstek Parametreleri:**

- `id` (string): Kullanýcý ID'si

**Yanýt Kodlarý:**

- `204 No Content` - Kullanýcý baþarýyla silindi
- `400 Bad Request` - Kullanýcý bulunamadý
