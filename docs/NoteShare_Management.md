## Not Paylaþýmlarý Yönetimi

### Tüm Not Paylaþýmlarýný Al

**Endpoint:** `GET /api/NoteShares`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Tüm not paylaþýmlarýný alýr.

**Yanýt:**

- **200 OK:** Not paylaþýmlarýnýn listesi baþarýyla döndürüldü.

**Örnek Yanýt:**
```json
[
  {
    "NoteShareId": 1,
    "NoteId": 123,
    "SharedWithUserId": "user1",
    "NoteContent": "This is a shared note.",
    "SharedWithUserName": "John Doe",
    "ShareType": "Public"
  },
  {
    "NoteShareId": 2,
    "NoteId": 456,
    "SharedWithUserId": "user2",
    "NoteContent": "Another shared note.",
    "SharedWithUserName": "Jane Doe",
    "ShareType": "Private"
  }
]
```

### Belirli Bir ID'ye Sahip Not Paylaþýmýný Al

**Endpoint:** `GET /api/NoteShares/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip not paylaþýmýný alýr.

**Parametreler:**
- **id (int):** Not paylaþýmý ID'si

**Yanýt:**

- **200 OK:** Not paylaþýmý detaylarý baþarýyla döndürüldü.
- **404 Not Found:** Not paylaþýmý bulunamadý.

**Örnek Yanýt:**
```json
{
  "NoteShareId": 1,
  "NoteId": 123,
  "SharedWithUserId": "user1",
  "NoteContent": "This is a shared note.",
  "SharedWithUserName": "John Doe",
  "ShareType": "Public"
}
```

### Yeni Bir Not Paylaþýmý Ekle

**Endpoint:** `POST /api/NoteShares`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Yeni bir not paylaþýmý ekler.

**Parametreler:**
- **noteShareDto (NoteShareCreateDto):** Not paylaþýmý detaylarý

**Yanýt:**

- **201 Created:** Not paylaþýmý baþarýyla eklendi.
- **400 Bad Request:** Not paylaþýmý detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "NoteShareId": 3,
  "NoteId": 789,
  "SharedWithUserId": "user3",
  "NoteContent": "New shared note.",
  "SharedWithUserName": "New User",
  "ShareType": "Public"
}
```

### Belirli Bir ID'ye Sahip Not Paylaþýmýný Güncelle

**Endpoint:** `PUT /api/NoteShares/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip not paylaþýmýný günceller.

**Parametreler:**
- **id (int):** Not paylaþýmý ID'si
- **noteShareDto (NoteShareUpdateDto):** Güncellenmiþ not paylaþýmý detaylarý

**Yanýt:**

- **204 No Content:** Not paylaþýmý baþarýyla güncellendi.
- **400 Bad Request:** Not paylaþýmý ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Not paylaþýmý bulunamadý.

### Belirli Bir ID'ye Sahip Not Paylaþýmýný Sil

**Endpoint:** `DELETE /api/NoteShares/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip not paylaþýmýný siler.

**Parametreler:**
- **id (int):** Not paylaþýmý ID'si

**Yanýt:**

- **204 No Content:** Not paylaþýmý baþarýyla silindi.
- **404 Not Found:** Not paylaþýmý bulunamadý.