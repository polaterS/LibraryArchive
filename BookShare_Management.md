## Kitap Paylaþýmlarý Yönetimi

### Tüm Kitap Paylaþýmlarýný Al

**URL:** `/api/BookShares`

**Yöntem:** `GET`

**Yetki:** `Admin, Moderator, User`

**Açýklama:** Tüm kitap paylaþýmlarýný alýr.

**Yanýt:**

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

**Yanýt Kodlarý:**

- `200 OK` - Kitap paylaþým listesi baþarýyla döndürüldü

### Belirli Bir ID'ye Sahip Kitap Paylaþýmýný Al

**URL:** `/api/BookShares/{id}`

**Yöntem:** `GET`

**Yetki:** `Admin, Moderator, User`

**Açýklama:** Belirli bir ID'ye sahip kitap paylaþýmýný alýr.

**Yanýt:**

```json
{
  "BookShareId": 1,
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Public",
  "CreatedDate": "2024-07-01T12:34:56Z"
}
```

**Yanýt Kodlarý:**

- `200 OK` - Kitap paylaþým detaylarý baþarýyla döndürüldü
- `404 Not Found` - Kitap paylaþýmý bulunamadý

### Yeni Bir Kitap Paylaþýmý Ekle

**URL:** `/api/BookShares`

**Yöntem:** `POST`

**Yetki:** `Admin, Moderator`

**Açýklama:** Yeni bir kitap paylaþýmý ekler.

**Ýstek:**

```json
{
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Public"
}
```

**Yanýt:**

```json
{
  "BookShareId": 3,
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Public",
  "CreatedDate": "2024-07-01T12:36:56Z"
}
```

**Yanýt Kodlarý:**

- `201 Created` - Kitap paylaþýmý baþarýyla eklendi
- `400 Bad Request` - Kitap paylaþým detaylarý yanlýþsa

### Belirli Bir ID'ye Sahip Kitap Paylaþýmýný Güncelle

**URL:** `/api/BookShares/{id}`

**Yöntem:** `PUT`

**Yetki:** `Admin, Moderator`

**Açýklama:** Belirli bir ID'ye sahip kitap paylaþýmýný günceller.

**Ýstek:**

```json
{
  "BookShareId": 1,
  "NoteId": 1,
  "SharedWithUserId": "user123",
  "ShareType": "Private"
}
```

**Yanýt Kodlarý:**

- `204 No Content` - Kitap paylaþýmý baþarýyla güncellendi
- `400 Bad Request` - Kitap paylaþým ID uyumsuzluðu veya detaylarý yanlýþsa
- `404 Not Found` - Kitap paylaþýmý bulunamadý

### Belirli Bir ID'ye Sahip Kitap Paylaþýmýný Siler

**URL:** `/api/BookShares/{id}`

**Yöntem:** `DELETE`

**Yetki:** `Admin, Moderator`

**Açýklama:** Belirli bir ID'ye sahip kitap paylaþýmýný siler.

**Yanýt Kodlarý:**

- `204 No Content` - Kitap paylaþýmý baþarýyla silindi
- `404 Not Found` - Kitap paylaþýmý bulunamadý