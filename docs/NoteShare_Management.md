## Not Payla��mlar� Y�netimi

### T�m Not Payla��mlar�n� Al

**Endpoint:** `GET /api/NoteShares`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** T�m not payla��mlar�n� al�r.

**Yan�t:**

- **200 OK:** Not payla��mlar�n�n listesi ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
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

### Belirli Bir ID'ye Sahip Not Payla��m�n� Al

**Endpoint:** `GET /api/NoteShares/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip not payla��m�n� al�r.

**Parametreler:**
- **id (int):** Not payla��m� ID'si

**Yan�t:**

- **200 OK:** Not payla��m� detaylar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Not payla��m� bulunamad�.

**�rnek Yan�t:**
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

### Yeni Bir Not Payla��m� Ekle

**Endpoint:** `POST /api/NoteShares`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Yeni bir not payla��m� ekler.

**Parametreler:**
- **noteShareDto (NoteShareCreateDto):** Not payla��m� detaylar�

**Yan�t:**

- **201 Created:** Not payla��m� ba�ar�yla eklendi.
- **400 Bad Request:** Not payla��m� detaylar� yanl��sa.

**�rnek Yan�t:**
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

### Belirli Bir ID'ye Sahip Not Payla��m�n� G�ncelle

**Endpoint:** `PUT /api/NoteShares/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip not payla��m�n� g�nceller.

**Parametreler:**
- **id (int):** Not payla��m� ID'si
- **noteShareDto (NoteShareUpdateDto):** G�ncellenmi� not payla��m� detaylar�

**Yan�t:**

- **204 No Content:** Not payla��m� ba�ar�yla g�ncellendi.
- **400 Bad Request:** Not payla��m� ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Not payla��m� bulunamad�.

### Belirli Bir ID'ye Sahip Not Payla��m�n� Sil

**Endpoint:** `DELETE /api/NoteShares/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip not payla��m�n� siler.

**Parametreler:**
- **id (int):** Not payla��m� ID'si

**Yan�t:**

- **204 No Content:** Not payla��m� ba�ar�yla silindi.
- **404 Not Found:** Not payla��m� bulunamad�.