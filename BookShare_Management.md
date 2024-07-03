## Kitap Payla��mlar� Y�netimi

### T�m Kitap Payla��mlar�n� Al

**URL:** `/api/BookShares`

**Y�ntem:** `GET`

**Yetki:** `Admin, Moderator, User`

**A��klama:** T�m kitap payla��mlar�n� al�r.

**Yan�t:**

```json
[
  {
    "BookShareId": 1,
    "NoteId": 1,
    "SharedWithUserId": "user123",
    "ShareType": "Public",
    "CreatedDate": "2024-07-01T12:34:56Z"
  },
  {
    "BookShareId": 2,
    "NoteId": 2,
    "SharedWithUserId": "user456",
    "ShareType": "Private",
    "CreatedDate": "2024-07-01T12:35:56Z"
  }
]
```

**Yan�t Kodlar�:**

- `200 OK` - Kitap payla��m listesi ba�ar�yla d�nd�r�ld�

### Belirli Bir ID'ye Sahip Kitap Payla��m�n� Al

**URL:** `/api/BookShares/{id}`

**Y�ntem:** `GET`

**Yetki:** `Admin, Moderator, User`

**A��klama:** Belirli bir ID'ye sahip kitap payla��m�n� al�r.

**Yan�t:**

```json
{
  "BookShareId": 1,
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Public",
  "CreatedDate": "2024-07-01T12:34:56Z"
}
```

**Yan�t Kodlar�:**

- `200 OK` - Kitap payla��m detaylar� ba�ar�yla d�nd�r�ld�
- `404 Not Found` - Kitap payla��m� bulunamad�

### Yeni Bir Kitap Payla��m� Ekle

**URL:** `/api/BookShares`

**Y�ntem:** `POST`

**Yetki:** `Admin, Moderator`

**A��klama:** Yeni bir kitap payla��m� ekler.

**�stek:**

```json
{
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Public"
}
```

**Yan�t:**

```json
{
  "BookShareId": 3,
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Public",
  "CreatedDate": "2024-07-01T12:36:56Z"
}
```

**Yan�t Kodlar�:**

- `201 Created` - Kitap payla��m� ba�ar�yla eklendi
- `400 Bad Request` - Kitap payla��m detaylar� yanl��sa

### Belirli Bir ID'ye Sahip Kitap Payla��m�n� G�ncelle

**URL:** `/api/BookShares/{id}`

**Y�ntem:** `PUT`

**Yetki:** `Admin, Moderator`

**A��klama:** Belirli bir ID'ye sahip kitap payla��m�n� g�nceller.

**�stek:**

```json
{
  "BookShareId": 1,
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Private"
}
```

**Yan�t Kodlar�:**

- `204 No Content` - Kitap payla��m� ba�ar�yla g�ncellendi
- `400 Bad Request` - Kitap payla��m ID uyumsuzlu�u veya detaylar� yanl��sa
- `404 Not Found` - Kitap payla��m� bulunamad�

### Belirli Bir ID'ye Sahip Kitap Payla��m�n� Siler

**URL:** `/api/BookShares/{id}`

**Y�ntem:** `DELETE`

**Yetki:** `Admin, Moderator`

**A��klama:** Belirli bir ID'ye sahip kitap payla��m�n� siler.

**Yan�t Kodlar�:**

- `204 No Content` - Kitap payla��m� ba�ar�yla silindi
- `404 Not Found` - Kitap payla��m� bulunamad�