## Not Y�netimi

### T�m Notlar� Al

**Endpoint:** `GET /api/Notes`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** T�m notlar� al�r.

**Yan�t:**

- **200 OK:** Notlar�n listesi ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
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

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip notu al�r.

**Parametreler:**
- **id (int):** Not ID'si

**Yan�t:**

- **200 OK:** Not detaylar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Not bulunamad�.

**�rnek Yan�t:**
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

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Yeni bir not ekler.

**Parametreler:**
- **noteDto (NoteCreateDto):** Not detaylar�

**Yan�t:**

- **201 Created:** Not ba�ar�yla eklendi.
- **400 Bad Request:** Not detaylar� yanl��sa.

**�rnek Yan�t:**
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

### Belirli Bir ID'ye Sahip Notu G�ncelle

**Endpoint:** `PUT /api/Notes/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip notu g�nceller.

**Parametreler:**
- **id (int):** Not ID'si
- **noteDto (NoteUpdateDto):** G�ncellenmi� not detaylar�

**Yan�t:**

- **204 No Content:** Not ba�ar�yla g�ncellendi.
- **400 Bad Request:** Not ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Not bulunamad�.

### Belirli Bir ID'ye Sahip Notu Sil

**Endpoint:** `DELETE /api/Notes/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip notu siler.

**Parametreler:**
- **id (int):** Not ID'si

**Yan�t:**

- **204 No Content:** Not ba�ar�yla silindi.
- **404 Not Found:** Not bulunamad�.