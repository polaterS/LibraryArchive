## Kategori Yönetimi

### Tüm Kategorileri Al

**Endpoint:** `GET /api/Categories`

**Yetki:** Yetki gerektirmez (AllowAnonymous)

**Açýklama:** Tüm kategorileri alýr.

**Yanýt:**

- **200 OK:** Kategori listesi baþarýyla döndürüldü.

**Örnek Yanýt:**
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

**Açýklama:** Belirli bir ID'ye sahip kategoriyi alýr.

**Parametreler:**
- **id (int):** Kategori ID'si

**Yanýt:**

- **200 OK:** Kategori detaylarý baþarýyla döndürüldü.
- **404 Not Found:** Kategori bulunamadý.

**Örnek Yanýt:**
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

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Yeni bir kategori ekler.

**Parametreler:**
- **categoryDto (CategoryCreateDto):** Kategori detaylarý

**Yanýt:**

- **201 Created:** Kategori baþarýyla eklendi.
- **400 Bad Request:** Kategori detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "CategoryId": 3,
  "Name": "History",
  "BooksCount": 0,
  "Books": []
}
```

### Belirli Bir ID'ye Sahip Kategoriyi Güncelle

**Endpoint:** `PUT /api/Categories/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip kategoriyi günceller.

**Parametreler:**
- **id (int):** Kategori ID'si
- **categoryDto (CategoryUpdateDto):** Güncellenmiþ kategori detaylarý

**Yanýt:**

- **204 No Content:** Kategori baþarýyla güncellendi.
- **400 Bad Request:** Kategori ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Kategori bulunamadý.

### Belirli Bir ID'ye Sahip Kategoriyi Sil

**Endpoint:** `DELETE /api/Categories/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip kategoriyi siler.

**Parametreler:**
- **id (int):** Kategori ID'si

**Yanýt:**

- **204 No Content:** Kategori baþarýyla silindi.
- **404 Not Found:** Kategori bulunamadý.