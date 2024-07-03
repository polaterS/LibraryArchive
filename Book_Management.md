## Kitap Yönetimi

### Tüm Kitaplarý Al

**URL:** `/api/Books`

**Yöntem:** `GET`

**Yetki:** `Admin, Moderator`

**Açýklama:** Tüm kitaplarý alýr.

**Yanýt:**

```json
[
  {
    "BookId": 1,
    "Title": "Kitap Baþlýðý",
    "Author": "Yazar Adý",
    "ISBN": "123456789",
    "CoverImageUrl": "http://example.com/cover.jpg",
    "ShelfLocation": "Raf Yeri",
    "CategoryName": "Kategori Adý"
  },
  {
    "BookId": 2,
    "Title": "Baþka Kitap Baþlýðý",
    "Author": "Baþka Yazar Adý",
    "ISBN": "987654321",
    "CoverImageUrl": "http://example.com/cover2.jpg",
    "ShelfLocation": "Baþka Raf Yeri",
    "CategoryName": "Baþka Kategori Adý"
  }
]
```

**Yanýt Kodlarý:**

- `200 OK` - Kitap listesi baþarýyla döndürüldü

### Belirli Bir ID'ye Sahip Kitabý Al

**URL:** `/api/Books/{id}`

**Yöntem:** `GET`

**Yetki:** `Admin, Moderator`

**Açýklama:** Belirli bir ID'ye sahip kitabý alýr.

**Yanýt:**

```json
{
  "BookId": 1,
  "Title": "Kitap Baþlýðý",
  "Author": "Yazar Adý",
  "ISBN": "123456789",
  "CoverImageUrl": "http://example.com/cover.jpg",
  "ShelfLocation": "Raf Yeri",
  "CategoryName": "Kategori Adý"
}
```

**Yanýt Kodlarý:**

- `200 OK` - Kitap detaylarý baþarýyla döndürüldü
- `404 Not Found` - Kitap bulunamadý

### Yeni Bir Kitap Ekle

**URL:** `/api/Books`

**Yöntem:** `POST`

**Yetki:** `Admin, Moderator`

**Açýklama:** Yeni bir kitap ekler.

**Ýstek:**

```json
{
  "Title": "Yeni Kitap Baþlýðý",
  "Author": "Yeni Yazar Adý",
  "ISBN": "111111111",
  "CoverImageUrl": "http://example.com/covernew.jpg",
  "ShelfLocation": "Yeni Raf Yeri",
  "CategoryId": 1
}
```

**Yanýt:**

```json
{
  "BookId": 3,
  "Title": "Yeni Kitap Baþlýðý",
  "Author": "Yeni Yazar Adý",
  "ISBN": "111111111",
  "CoverImageUrl": "http://example.com/covernew.jpg",
  "ShelfLocation": "Yeni Raf Yeri",
  "CategoryName": "Kategori Adý"
}
```

**Yanýt Kodlarý:**

- `200 OK` - Kitap baþarýyla eklendi
- `400 Bad Request` - Kitap detaylarý yanlýþsa

### Belirli Bir ID'ye Sahip Kitabý Güncelle

**URL:** `/api/Books/{id}`

**Yöntem:** `PUT`

**Yetki:** `Admin, Moderator`

**Açýklama:** Belirli bir ID'ye sahip kitabý günceller.

**Ýstek:**

```json
{
  "BookId": 1,
  "Title": "Güncellenmiþ Kitap Baþlýðý",
  "Author": "Güncellenmiþ Yazar Adý",
  "ISBN": "123456789",
  "CoverImageUrl": "http://example.com/coverupdated.jpg",
  "ShelfLocation": "Güncellenmiþ Raf Yeri",
  "CategoryId": 2
}
```

**Yanýt Kodlarý:**

- `204 No Content` - Kitap baþarýyla güncellendi
- `400 Bad Request` - Kitap ID uyumsuzluðu veya detaylarý yanlýþsa
- `404 Not Found` - Kitap bulunamadý

### Belirli Bir ID'ye Sahip Kitabý Siler

**URL:** `/api/Books/{id}`

**Yöntem:** `DELETE`

**Yetki:** `Admin, Moderator`

**Açýklama:** Belirli bir ID'ye sahip kitabý siler.

**Yanýt Kodlarý:**

- `204 No Content` - Kitap baþarýyla silindi
- `404 Not Found` - Kitap bulunamadý

### Arama Terimine Göre Kitaplarý Arar

**URL:** `/api/Books/search`

**Yöntem:** `GET`

**Yetki:** `AllowAnonymous`

**Açýklama:** Arama terimine göre kitaplarý arar.

**Ýstek:**

```
?term=kitap
```

**Yanýt:**

```json
[
  {
    "BookId": 1,
    "Title": "Kitap Baþlýðý",
    "Author": "Yazar Adý",
    "ISBN": "123456789",
    "CoverImageUrl": "http://example.com/cover.jpg",
    "ShelfLocation": "Raf Yeri",
    "CategoryName": "Kategori Adý"
  }
]
```

**Yanýt Kodlarý:**

- `200 OK` - Arama sonuçlarý baþarýyla döndürüldü