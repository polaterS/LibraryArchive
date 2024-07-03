## Kategori Y�netimi

### T�m Kategorileri Al

**Endpoint:** `GET /api/Categories`

**Yetki:** Yetki gerektirmez (AllowAnonymous)

**A��klama:** T�m kategorileri al�r.

**Yan�t:**

- **200 OK:** Kategori listesi ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
```json
[
  {
    "CategoryId": 1,
    "Name": "Fiction",
    "BooksCount": 10,
    "Books": []
  },
  {
    "CategoryId": 2,
    "Name": "Science",
    "BooksCount": 5,
    "Books": []
  }
]
```

### Belirli Bir ID'ye Sahip Kategoriyi Al

**Endpoint:** `GET /api/Categories/{id}`

**Yetki:** Yetki gerektirmez (AllowAnonymous)

**A��klama:** Belirli bir ID'ye sahip kategoriyi al�r.

**Parametreler:**
- **id (int):** Kategori ID'si

**Yan�t:**

- **200 OK:** Kategori detaylar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Kategori bulunamad�.

**�rnek Yan�t:**
```json
{
  "CategoryId": 1,
  "Name": "Fiction",
  "BooksCount": 10,
  "Books": []
}
```

### Yeni Bir Kategori Ekle

**Endpoint:** `POST /api/Categories`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Yeni bir kategori ekler.

**Parametreler:**
- **categoryDto (CategoryCreateDto):** Kategori detaylar�

**Yan�t:**

- **201 Created:** Kategori ba�ar�yla eklendi.
- **400 Bad Request:** Kategori detaylar� yanl��sa.

**�rnek Yan�t:**
```json
{
  "CategoryId": 3,
  "Name": "History",
  "BooksCount": 0,
  "Books": []
}
```

### Belirli Bir ID'ye Sahip Kategoriyi G�ncelle

**Endpoint:** `PUT /api/Categories/{id}`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip kategoriyi g�nceller.

**Parametreler:**
- **id (int):** Kategori ID'si
- **categoryDto (CategoryUpdateDto):** G�ncellenmi� kategori detaylar�

**Yan�t:**

- **204 No Content:** Kategori ba�ar�yla g�ncellendi.
- **400 Bad Request:** Kategori ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Kategori bulunamad�.

### Belirli Bir ID'ye Sahip Kategoriyi Sil

**Endpoint:** `DELETE /api/Categories/{id}`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip kategoriyi siler.

**Parametreler:**
- **id (int):** Kategori ID'si

**Yan�t:**

- **204 No Content:** Kategori ba�ar�yla silindi.
- **404 Not Found:** Kategori bulunamad�.