## Kullan�c� Y�netimi

### T�m Kullan�c�lar� Al

**URL:** `/api/Users`

**Y�ntem:** `GET`

**Yetki:** `Admin`

**A��klama:** T�m kullan�c�lar� al�r.

**Yan�t:**

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

**Yan�t Kodlar�:**

- `200 OK` - Kullan�c�lar�n listesi ba�ar�yla d�nd�r�ld�

### Kullan�c� Detaylar�n� Al

**URL:** `/api/Users/{id}`

**Y�ntem:** `GET`

**Yetki:** `Admin`

**A��klama:** Belirli bir ID'ye sahip kullan�c�y� al�r.

**�stek Parametreleri:**

- `id` (string): Kullan�c� ID'si

**Yan�t:**

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

**Yan�t Kodlar�:**

- `200 OK` - Kullan�c� ba�ar�yla d�nd�r�ld�
- `404 Not Found` - Kullan�c� bulunamad�

### Yeni Kullan�c� Kaydet

**URL:** `/api/Users`

**Y�ntem:** `POST`

**Yetki:** `Admin`

**A��klama:** Yeni bir kullan�c� kaydeder.

**�stek:**

```json
{
  "UserName": "newuser",
  "Email": "newuser@example.com",
  "Password": "YourPassword123",
  "Name": "Jane",
  "Surname": "Doe"
}
```

**Yan�t:**

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

**Yan�t Kodlar�:**

- `201 Created` - Kullan�c� ba�ar�yla kaydedildi
- `400 Bad Request` - Kullan�c� detaylar� yanl��sa

### Kullan�c� G�ncelle

**URL:** `/api/Users/{id}`

**Y�ntem:** `PUT`

**Yetki:** `Admin`

**A��klama:** Belirli bir ID'ye sahip kullan�c�y� g�nceller.

**�stek Parametreleri:**

- `id` (string): Kullan�c� ID'si

**�stek:**

```json
{
  "Id": "user_id",
  "Email": "updateduser@example.com",
  "Name": "Updated",
  "Surname": "User"
}
```

**Yan�t Kodlar�:**

- `204 No Content` - Kullan�c� ba�ar�yla g�ncellendi
- `400 Bad Request` - Kullan�c� ID uyumsuzlu�u veya detaylar� yanl��sa
- `404 Not Found` - Kullan�c� bulunamad�

### Kullan�c� Sil

**URL:** `/api/Users/{id}`

**Y�ntem:** `DELETE`

**Yetki:** `Admin`

**A��klama:** Belirli bir ID'ye sahip kullan�c�y� siler.

**�stek Parametreleri:**

- `id` (string): Kullan�c� ID'si

**Yan�t Kodlar�:**

- `204 No Content` - Kullan�c� ba�ar�yla silindi
- `400 Bad Request` - Kullan�c� bulunamad�
