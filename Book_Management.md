## Kitap Y�netimi

### T�m Kitaplar� Al

**URL:** `/api/Books`

**Y�ntem:** `GET`

**Yetki:** `Admin, Moderator`

**A��klama:** T�m kitaplar� al�r.

**Yan�t:**

```json
[
  {
    "BookId": 1,
    "Title": "Kitap Ba�l���",
    "Author": "Yazar Ad�",
    "ISBN": "123456789",
    "CoverImageUrl": "http://example.com/cover.jpg",
    "ShelfLocation": "Raf Yeri",
    "CategoryName": "Kategori Ad�"
  },
  {
    "BookId": 2,
    "Title": "Ba�ka Kitap Ba�l���",
    "Author": "Ba�ka Yazar Ad�",
    "ISBN": "987654321",
    "CoverImageUrl": "http://example.com/cover2.jpg",
    "ShelfLocation": "Ba�ka Raf Yeri",
    "CategoryName": "Ba�ka Kategori Ad�"
  }
]
```

**Yan�t Kodlar�:**

- `200 OK` - Kitap listesi ba�ar�yla d�nd�r�ld�

### Belirli Bir ID'ye Sahip Kitab� Al

**URL:** `/api/Books/{id}`

**Y�ntem:** `GET`

**Yetki:** `Admin, Moderator`

**A��klama:** Belirli bir ID'ye sahip kitab� al�r.

**Yan�t:**

```json
{
  "BookId": 1,
  "Title": "Kitap Ba�l���",
  "Author": "Yazar Ad�",
  "ISBN": "123456789",
  "CoverImageUrl": "http://example.com/cover.jpg",
  "ShelfLocation": "Raf Yeri",
  "CategoryName": "Kategori Ad�"
}
```

**Yan�t Kodlar�:**

- `200 OK` - Kitap detaylar� ba�ar�yla d�nd�r�ld�
- `404 Not Found` - Kitap bulunamad�

### Yeni Bir Kitap Ekle

**URL:** `/api/Books`

**Y�ntem:** `POST`

**Yetki:** `Admin, Moderator`

**A��klama:** Yeni bir kitap ekler.

**�stek:**

```json
{
  "Title": "Yeni Kitap Ba�l���",
  "Author": "Yeni Yazar Ad�",
  "ISBN": "111111111",
  "CoverImageUrl": "http://example.com/covernew.jpg",
  "ShelfLocation": "Yeni Raf Yeri",
  "CategoryId": 1
}
```

**Yan�t:**

```json
{
  "BookId": 3,
  "Title": "Yeni Kitap Ba�l���",
  "Author": "Yeni Yazar Ad�",
  "ISBN": "111111111",
  "CoverImageUrl": "http://example.com/covernew.jpg",
  "ShelfLocation": "Yeni Raf Yeri",
  "CategoryName": "Kategori Ad�"
}
```

**Yan�t Kodlar�:**

- `200 OK` - Kitap ba�ar�yla eklendi
- `400 Bad Request` - Kitap detaylar� yanl��sa

### Belirli Bir ID'ye Sahip Kitab� G�ncelle

**URL:** `/api/Books/{id}`

**Y�ntem:** `PUT`

**Yetki:** `Admin, Moderator`

**A��klama:** Belirli bir ID'ye sahip kitab� g�nceller.

**�stek:**

```json
{
  "BookId": 1,
  "Title": "G�ncellenmi� Kitap Ba�l���",
  "Author": "G�ncellenmi� Yazar Ad�",
  "ISBN": "123456789",
  "CoverImageUrl": "http://example.com/coverupdated.jpg",
  "ShelfLocation": "G�ncellenmi� Raf Yeri",
  "CategoryId": 2
}
```

**Yan�t Kodlar�:**

- `204 No Content` - Kitap ba�ar�yla g�ncellendi
- `400 Bad Request` - Kitap ID uyumsuzlu�u veya detaylar� yanl��sa
- `404 Not Found` - Kitap bulunamad�

### Belirli Bir ID'ye Sahip Kitab� Siler

**URL:** `/api/Books/{id}`

**Y�ntem:** `DELETE`

**Yetki:** `Admin, Moderator`

**A��klama:** Belirli bir ID'ye sahip kitab� siler.

**Yan�t Kodlar�:**

- `204 No Content` - Kitap ba�ar�yla silindi
- `404 Not Found` - Kitap bulunamad�

### Arama Terimine G�re Kitaplar� Arar

**URL:** `/api/Books/search`

**Y�ntem:** `GET`

**Yetki:** `AllowAnonymous`

**A��klama:** Arama terimine g�re kitaplar� arar.

**�stek:**

```
?term=kitap
```

**Yan�t:**

```json
[
  {
    "BookId": 1,
    "Title": "Kitap Ba�l���",
    "Author": "Yazar Ad�",
    "ISBN": "123456789",
    "CoverImageUrl": "http://example.com/cover.jpg",
    "ShelfLocation": "Raf Yeri",
    "CategoryName": "Kategori Ad�"
  }
]
```

**Yan�t Kodlar�:**

- `200 OK` - Arama sonu�lar� ba�ar�yla d�nd�r�ld�