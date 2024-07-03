## Not Yönetimi

### Tüm Notlarý Al

**Endpoint:** `GET /api/Notes`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Tüm notlarý alýr.

**Yanýt:**

- **200 OK:** Notlarýn listesi baþarýyla döndürüldü.

**Örnek Yanýt:**
```json
[
  {
    "NoteId": 1,
    "BookId": 123,
    "Content": "This is a note.",
    "IsPrivate": false,
    "UserName": "John Doe",
    "BookTitle": "Sample Book"
  },
  {
    "NoteId": 2,
    "BookId": 456,
    "Content": "Another note.",
    "IsPrivate": true,
    "UserName": "Jane Doe",
    "BookTitle": "Another Book"
  }
]
```

### Belirli Bir ID'ye Sahip Notu Al

**Endpoint:** `GET /api/Notes/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip notu alýr.

**Parametreler:**
- **id (int):** Not ID'si

**Yanýt:**

- **200 OK:** Not detaylarý baþarýyla döndürüldü.
- **404 Not Found:** Not bulunamadý.

**Örnek Yanýt:**
```json
{
  "NoteId": 1,
  "BookId": 123,
  "Content": "This is a note.",
  "IsPrivate": false,
  "UserName": "John Doe",
  "BookTitle": "Sample Book"
}
```

### Yeni Bir Not Ekle

**Endpoint:** `POST /api/Notes`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Yeni bir not ekler.

**Parametreler:**
- **noteDto (NoteCreateDto):** Not detaylarý

**Yanýt:**

- **201 Created:** Not baþarýyla eklendi.
- **400 Bad Request:** Not detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "NoteId": 3,
  "BookId": 789,
  "Content": "New note content.",
  "IsPrivate": false,
  "UserName": "New User",
  "BookTitle": "New Book"
}
```

### Belirli Bir ID'ye Sahip Notu Güncelle

**Endpoint:** `PUT /api/Notes/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip notu günceller.

**Parametreler:**
- **id (int):** Not ID'si
- **noteDto (NoteUpdateDto):** Güncellenmiþ not detaylarý

**Yanýt:**

- **204 No Content:** Not baþarýyla güncellendi.
- **400 Bad Request:** Not ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Not bulunamadý.

### Belirli Bir ID'ye Sahip Notu Sil

**Endpoint:** `DELETE /api/Notes/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip notu siler.

**Parametreler:**
- **id (int):** Not ID'si

**Yanýt:**

- **204 No Content:** Not baþarýyla silindi.
- **404 Not Found:** Not bulunamadý.