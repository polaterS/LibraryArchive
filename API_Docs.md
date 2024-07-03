# LibraryArchive API Kullan�m K�lavuzu

Bu k�lavuz, LibraryArchive API'sinin kullan�m�n� a��klamaktad�r. 

API, kitapl�k ar�iv y�netimi, kullan�c� etkile�imi ve e-ticaret �zelliklerini i�erir.

## Genel Bilgiler

- Base URL: `https://api.libraryarchive.com`
- API S�r�m�: v1
- Yetkilendirme: JWT tabanl� kimlik do�rulama kullan�r. Her istekte `Authorization` ba�l���nda `Bearer <token>` format�nda g�nderilmelidir.

## Kimlik Do�rulama ve Yetkilendirme

### Kay�t Olma

**URL:** `/api/Auth/Register`

**Y�ntem:** `POST`

**A��klama:** Yeni bir kullan�c� kaydeder ve JWT belirteci d�ner.



**�stek:**

```json
{
  "Email": "example@example.com",
  "Password": "YourPassword123",
  "Name": "John",
  "Surname": "Doe"
}
```

**Yan�t:**

```json
{
  "Token": "your.jwt.token.here"
}
```

**Yan�t Kodlar�:**

- `200 OK` - Kay�tl� kullan�c� i�in JWT token d�nd�r�r
- `400 Bad Request` - Kay�t ayr�nt�lar� yanl��sa

### Giri� Yapma

**URL:** `/api/Auth/Login`

**Y�ntem:** `POST`

**A��klama:** Kullan�c� giri�i yapar ve JWT belirteci d�ner.

**�stek:**

```json
{
  "Email": "example@example.com",
  "Password": "YourPassword123"
}
```

**Yan�t:**

```json
{
  "Token": "your.jwt.token.here"
}
```

**Yan�t Kodlar�:**

- `200 OK` - JWT token d�nd�r�r
- `400 Bad Request` - Giri� bilgileri yanl��sa

### Rol Atama

**URL:** `/api/Auth/AssignRole`

**Y�ntem:** `POST`

**Yetki:** `Admin`

**A��klama:** Bir kullan�c�ya rol atar.

**�stek:**

```json
{
  "Email": "user@example.com",
  "RoleName": "Admin"
}
```

**Yan�t:**

```json
{
  "Message": "Rol ba�ar�yla atand�."
}
```

**Yan�t Kodlar�:**

- `200 OK` - Rol ba�ar�yla atand�
- `400 Bad Request` - Rol atama detaylar� yanl��sa
- `403 Forbidden` - Kullan�c� yetkili de�ilse

### Rol Olu�turma

**URL:** `/api/Auth/CreateRole`

**Y�ntem:** `POST`

**Yetki:** `Admin`

**A��klama:** Yeni bir rol olu�turur.

**�stek:**

```json
{
  "RoleName": "Admin"
}
```

**Yan�t:**

```json
{
  "Message": "Rol ba�ar�yla olu�turuldu"
}
```

**Yan�t Kodlar�:**

- `200 OK` - Rol ba�ar�yla olu�turuldu
- `400 Bad Request` - Rol detaylar� yanl��sa
- `403 Forbidden` - Kullan�c� yetkili de�ilse




## Kullan�c� Y�netimi

### T�m Kullan�c�lar� Al

**URL:** `/api/Users`

**Y�ntem:** `GET`

**Yetki:** `Admin`

**A��klama:** T�m kullan�c�lar� al�r.

**Yan�t:**

```json
[
  {
    "Id": "user_id",
    "UserName": "username",
    "Email": "user@example.com",
    "PhoneNumber": "1234567890",
    "Name": "John",
    "Surname": "Doe",
    "IsActive": true,
    "ProfilePictureUrl": "https://example.com/picture.jpg",
    "Roles": ["User"],
    "Books": [],
    "Notes": [],
    "Orders": [],
    "Addresses": []
  }
]
```

**Yan�t Kodlar�:**

- `200 OK` - Kullan�c�lar�n listesi ba�ar�yla d�nd�r�ld�

### Kullan�c� Detaylar�n� Al

**URL:** `/api/Users/{id}`

**Y�ntem:** `GET`

**Yetki:** `Admin`

**A��klama:** Belirli bir ID'ye sahip kullan�c�y� al�r.

**�stek Parametreleri:**

- `id` (string): Kullan�c� ID'si

**Yan�t:**

```json
{
  "Id": "user_id",
  "UserName": "username",
  "Email": "user@example.com",
  "PhoneNumber": "1234567890",
  "Name": "John",
  "Surname": "Doe",
  "IsActive": true,
  "ProfilePictureUrl": "https://example.com/picture.jpg",
  "Roles": ["User"],
  "Books": [],
  "Notes": [],
  "Orders": [],
  "Addresses": []
}
```

**Yan�t Kodlar�:**

- `200 OK` - Kullan�c� ba�ar�yla d�nd�r�ld�
- `404 Not Found` - Kullan�c� bulunamad�

### Yeni Kullan�c� Kaydet

**URL:** `/api/Users`

**Y�ntem:** `POST`

**Yetki:** `Admin`

**A��klama:** Yeni bir kullan�c� kaydeder.

**�stek:**

```json
{
  "UserName": "newuser",
  "Email": "newuser@example.com",
  "Password": "YourPassword123",
  "Name": "Jane",
  "Surname": "Doe"
}
```

**Yan�t:**

```json
{
  "Id": "new_user_id",
  "UserName": "newuser",
  "Email": "newuser@example.com",
  "PhoneNumber": "1234567890",
  "Name": "Jane",
  "Surname": "Doe",
  "IsActive": true,
  "ProfilePictureUrl": "https://example.com/picture.jpg",
  "Roles": ["User"],
  "Books": [],
  "Notes": [],
  "Orders": [],
  "Addresses": []
}
```

**Yan�t Kodlar�:**

- `201 Created` - Kullan�c� ba�ar�yla kaydedildi
- `400 Bad Request` - Kullan�c� detaylar� yanl��sa

### Kullan�c� G�ncelle

**URL:** `/api/Users/{id}`

**Y�ntem:** `PUT`

**Yetki:** `Admin`

**A��klama:** Belirli bir ID'ye sahip kullan�c�y� g�nceller.

**�stek Parametreleri:**

- `id` (string): Kullan�c� ID'si

**�stek:**

```json
{
  "Id": "user_id",
  "Email": "updateduser@example.com",
  "Name": "Updated",
  "Surname": "User"
}
```

**Yan�t Kodlar�:**

- `204 No Content` - Kullan�c� ba�ar�yla g�ncellendi
- `400 Bad Request` - Kullan�c� ID uyumsuzlu�u veya detaylar� yanl��sa
- `404 Not Found` - Kullan�c� bulunamad�

### Kullan�c� Sil

**URL:** `/api/Users/{id}`

**Y�ntem:** `DELETE`

**Yetki:** `Admin`

**A��klama:** Belirli bir ID'ye sahip kullan�c�y� siler.

**�stek Parametreleri:**

- `id` (string): Kullan�c� ID'si

**Yan�t Kodlar�:**

- `204 No Content` - Kullan�c� ba�ar�yla silindi
- `400 Bad Request` - Kullan�c� bulunamad�



## Profil Y�netimi

### Kullan�c� Profili Al

**URL:** `/api/Profile`

**Y�ntem:** `GET`

**Yetki:** `Admin, User`

**A��klama:** Kullan�c� profilini al�r.

**Yan�t:**

```json
{
  "Name": "John",
  "Surname": "Doe",
  "Email": "user@example.com",
  "ProfilePictureUrl": "https://example.com/picture.jpg"
}
```

**Yan�t Kodlar�:**

- `200 OK` - Kullan�c� profili ba�ar�yla d�nd�r�ld�
- `404 Not Found` - Kullan�c� bulunamad�

### Kullan�c� Profilini G�ncelle

**URL:** `/api/Profile/profile`

**Y�ntem:** `PUT`

**Yetki:** `Admin, User`

**A��klama:** Kullan�c� profilini g�nceller.

**�stek:**

```json
{
  "Name": "Updated",
  "Surname": "User",
  "ProfilePictureUrl": "https://example.com/newpicture.jpg"
}
```

**Yan�t Kodlar�:**

- `204 No Content` - Profil ba�ar�yla g�ncellendi
- `400 Bad Request` - Profil g�ncelleme ba�ar�s�z

### Kullan�c� Email Adresini G�ncelle

**URL:** `/api/Profile/email`

**Y�ntem:** `PUT`

**Yetki:** `Admin, User`

**A��klama:** Kullan�c� email adresini g�nceller.

**�stek:**

```json
{
  "Email": "updateduser@example.com",
  "CurrentPassword": "CurrentPassword123"
}
```

**Yan�t Kodlar�:**

- `204 No Content` - Email ba�ar�yla g�ncellendi
- `400 Bad Request` - Email g�ncelleme ba�ar�s�z

### Kullan�c� �ifresini G�ncelle

**URL:** `/api/Profile/password`

**Y�ntem:** `PUT`

**Yetki:** `Admin, User`

**A��klama:** Kullan�c� �ifresini g�nceller.

**�stek:**

```json
{
  "CurrentPassword": "CurrentPassword123",
  "NewPassword": "NewPassword456"
}
```

**Yan�t Kodlar�:**

- `204 No Content` - �ifre ba�ar�yla g�ncellendi
- `400 Bad Request` - �ifre g�ncelleme ba�ar�s�z

### Kullan�c� Profilini Sil

**URL:** `/api/Profile`

**Y�ntem:** `DELETE`

**Yetki:** `Admin, User`

**A��klama:** Kullan�c� profilini siler.

**Yan�t Kodlar�:**

- `204 No Content` - Profil ba�ar�yla silindi
- `400 Bad Request` - Profil silme ba�ar�s�z




## Adres Y�netimi

### Kullan�c�n�n T�m Adreslerini Al

**URL:** `/api/Addresses`

**Y�ntem:** `GET`

**Yetki:** `Admin, User`

**A��klama:** Kullan�c�n�n t�m adreslerini al�r.

**Yan�t:**

```json
[
  {
    "AddressId": 1,
    "UserId": "user123",
    "Street": "123 Main St",
    "City": "Anytown",
    "State": "CA",
    "PostalCode": "12345",
    "Country": "USA"
  },
  {
    "AddressId": 2,
    "UserId": "user123",
    "Street": "456 Maple Ave",
    "City": "Othertown",
    "State": "NY",
    "PostalCode": "67890",
    "Country": "USA"
  }
]
```

**Yan�t Kodlar�:**

- `200 OK` - Adresler ba�ar�yla d�nd�r�ld�

### Belirli Bir ID'ye Sahip Adresi Al

**URL:** `/api/Addresses/{id}`

**Y�ntem:** `GET`

**Yetki:** `Admin, User`

**A��klama:** Belirli bir ID'ye sahip adresi al�r.

**Yan�t:**

```json
{
  "AddressId": 1,
  "UserId": "user123",
  "Street": "123 Main St",
  "City": "Anytown",
  "State": "CA",
  "PostalCode": "12345",
  "Country": "USA"
}
```

**Yan�t Kodlar�:**

- `200 OK` - Adres detaylar� ba�ar�yla d�nd�r�ld�
- `404 Not Found` - Adres bulunamad�

### Yeni Bir Adres Ekle

**URL:** `/api/Addresses`

**Y�ntem:** `POST`

**Yetki:** `Admin, User`

**A��klama:** Yeni bir adres ekler.

**�stek:**

```json
{
  "Street": "123 Main St",
  "City": "Anytown",
  "State": "CA",
  "PostalCode": "12345",
  "Country": "USA"
}
```

**Yan�t:**

```json
{
  "AddressId": 1,
  "UserId": "user123",
  "Street": "123 Main St",
  "City": "Anytown",
  "State": "CA",
  "PostalCode": "12345",
  "Country": "USA"
}
```

**Yan�t Kodlar�:**

- `201 Created` - Adres ba�ar�yla eklendi
- `400 Bad Request` - Adres detaylar� yanl��sa

### Belirli Bir ID'ye Sahip Adresi G�ncelle

**URL:** `/api/Addresses/{id}`

**Y�ntem:** `PUT`

**Yetki:** `Admin, User`

**A��klama:** Belirli bir ID'ye sahip adresi g�nceller.

**�stek:**

```json
{
  "AddressId": 1,
  "Street": "456 Maple Ave",
  "City": "Othertown",
  "State": "NY",
  "PostalCode": "67890",
  "Country": "USA"
}
```

**Yan�t Kodlar�:**

- `204 No Content` - Adres ba�ar�yla g�ncellendi
- `400 Bad Request` - Adres ID uyumsuzlu�u veya detaylar� yanl��sa
- `404 Not Found` - Adres bulunamad�

### Belirli Bir ID'ye Sahip Adresi Siler

**URL:** `/api/Addresses/{id}`

**Y�ntem:** `DELETE`

**Yetki:** `Admin, User`

**A��klama:** Belirli bir ID'ye sahip adresi siler.

**Yan�t Kodlar�:**

- `204 No Content` - Adres ba�ar�yla silindi
- `404 Not Found` - Adres bulunamad�




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




## Bildirim Y�netimi

### Belirli Bir Kullan�c�ya Ait T�m Bildirimleri Al

**Endpoint:** `GET /api/Notifications`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir kullan�c�ya ait t�m bildirimleri al�r.

**Yan�t:**

- **200 OK:** Bildirimler ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
```json
[
  {
    "NotificationId": 1,
    "UserId": "user1",
    "Title": "New Book Added",
    "Message": "A new book has been added to your library.",
    "Date": "2023-07-01T12:00:00Z",
    "NotificationType": "Email"
  },
  {
    "NotificationId": 2,
    "UserId": "user1",
    "Title": "Order Shipped",
    "Message": "Your order has been shipped.",
    "Date": "2023-07-02T15:30:00Z",
    "NotificationType": "SMS"
  }
]
```

### Belirli Bir ID'ye Sahip Bildirimi Al

**Endpoint:** `GET /api/Notifications/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirimi al�r.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yan�t:**

- **200 OK:** Bildirim detaylar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Bildirim bulunamad�.

**�rnek Yan�t:**
```json
{
  "NotificationId": 1,
  "UserId": "user1",
  "Title": "New Book Added",
  "Message": "A new book has been added to your library.",
  "Date": "2023-07-01T12:00:00Z",
  "NotificationType": "Email"
}
```

### Yeni Bir Bildirim Ekle

**Endpoint:** `POST /api/Notifications`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Yeni bir bildirim ekler.

**Parametreler:**
- **notificationDto (NotificationCreateDto):** Bildirim detaylar�

**Yan�t:**

- **201 Created:** Bildirim ba�ar�yla eklendi.
- **400 Bad Request:** Bildirim detaylar� yanl��sa.

**�rnek Yan�t:**
```json
{
  "NotificationId": 3,
  "UserId": "user2",
  "Title": "Book Returned",
  "Message": "Your book has been returned.",
  "Date": "2023-07-03T10:15:00Z",
  "NotificationType": "PushNotification"
}
```

### Belirli Bir ID'ye Sahip Bildirimi G�ncelle

**Endpoint:** `PUT /api/Notifications/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirimi g�nceller.

**Parametreler:**
- **id (int):** Bildirim ID'si
- **notificationDto (NotificationUpdateDto):** G�ncellenmi� bildirim detaylar�

**Yan�t:**

- **204 No Content:** Bildirim ba�ar�yla g�ncellendi.
- **400 Bad Request:** Bildirim ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Bildirim bulunamad�.

### Belirli Bir ID'ye Sahip Bildirimi Siler

**Endpoint:** `DELETE /api/Notifications/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirimi siler.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yan�t:**

- **204 No Content:** Bildirim ba�ar�yla silindi.
- **404 Not Found:** Bildirim bulunamad�.




## Bildirim Ayarlar� Y�netimi

### Belirli Bir Kullan�c�ya Ait T�m Bildirim Ayarlar�n� Al

**Endpoint:** `GET /api/NotificationSettings`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir kullan�c�ya ait t�m bildirim ayarlar�n� al�r.

**Yan�t:**

- **200 OK:** Bildirim ayarlar� ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
```json
[
  {
    "NotificationSettingsId": 1,
    "UserId": "user1",
    "EmailNotificationsEnabled": true,
    "SmsNotificationsEnabled": false,
    "PushNotificationsEnabled": true
  },
  {
    "NotificationSettingsId": 2,
    "UserId": "user1",
    "EmailNotificationsEnabled": true,
    "SmsNotificationsEnabled": true,
    "PushNotificationsEnabled": false
  }
]
```

### Belirli Bir ID'ye Sahip Bildirim Ayarlar�n� Al

**Endpoint:** `GET /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirim ayarlar�n� al�r.

**Parametreler:**
- **id (int):** Bildirim ayarlar� ID'si

**Yan�t:**

- **200 OK:** Bildirim ayarlar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Bildirim ayarlar� bulunamad�.

**�rnek Yan�t:**
```json
{
  "NotificationSettingsId": 1,
  "UserId": "user1",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": false,
  "PushNotificationsEnabled": true
}
```

### Yeni Bir Bildirim Ayar� Ekle

**Endpoint:** `POST /api/NotificationSettings`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Yeni bir bildirim ayar� ekler.

**Parametreler:**
- **notificationSettingsDto (NotificationSettingsCreateDto):** Bildirim ayarlar� detaylar�

**Yan�t:**

- **201 Created:** Bildirim ayar� ba�ar�yla eklendi.
- **400 Bad Request:** Bildirim ayarlar� detaylar� yanl��sa.

**�rnek Yan�t:**
```json
{
  "NotificationSettingsId": 3,
  "UserId": "user2",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": true,
  "PushNotificationsEnabled": false
}
```

### Belirli Bir ID'ye Sahip Bildirim Ayarlar�n� G�ncelle

**Endpoint:** `PUT /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirim ayarlar�n� g�nceller.

**Parametreler:**
- **id (int):** Bildirim ayarlar� ID'si
- **notificationSettingsDto (NotificationSettingsUpdateDto):** G�ncellenmi� bildirim ayarlar� detaylar�

**Yan�t:**

- **204 No Content:** Bildirim ayarlar� ba�ar�yla g�ncellendi.
- **400 Bad Request:** Bildirim ayarlar� ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Bildirim ayarlar� bulunamad�.

### Belirli Bir ID'ye Sahip Bildirim Ayarlar�n� Siler

**Endpoint:** `DELETE /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip bildirim ayarlar�n� siler.

**Parametreler:**
- **id (int):** Bildirim ayarlar� ID'si

**Yan�t:**

- **204 No Content:** Bildirim ayarlar� ba�ar�yla silindi.
- **404 Not Found:** Bildirim ayarlar� bulunamad�.




## Sipari� Y�netimi

### T�m Sipari�leri Al

**Endpoint:** `GET /api/Orders`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** T�m sipari�leri al�r.

**Yan�t:**

- **200 OK:** Sipari�lerin listesi ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
```json
[
  {
    "OrderId": 1,
    "OrderDate": "2023-06-30T12:00:00Z",
    "UserName": "JohnDoe",
    "OrderDetails": [
      {
        "OrderDetailId": 1,
        "BookId": 1,
        "BookTitle": "Book Title",
        "Quantity": 2,
        "Price": 29.99
      }
    ]
  },
  {
    "OrderId": 2,
    "OrderDate": "2023-06-30T13:00:00Z",
    "UserName": "JaneDoe",
    "OrderDetails": [
      {
        "OrderDetailId": 2,
        "BookId": 2,
        "BookTitle": "Another Book Title",
        "Quantity": 1,
        "Price": 19.99
      }
    ]
  }
]
```

### Belirli Bir ID'ye Sahip Sipari�i Al

**Endpoint:** `GET /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari�i al�r.

**Parametreler:**
- **id (int):** Sipari� ID'si

**Yan�t:**

- **200 OK:** Sipari� detaylar� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Sipari� bulunamad�.

**�rnek Yan�t:**
```json
{
  "OrderId": 1,
  "OrderDate": "2023-06-30T12:00:00Z",
  "UserName": "JohnDoe",
  "OrderDetails": [
    {
      "OrderDetailId": 1,
      "BookId": 1,
      "BookTitle": "Book Title",
      "Quantity": 2,
      "Price": 29.99
    }
  ]
}
```

### Yeni Bir Sipari� Ekle

**Endpoint:** `POST /api/Orders`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Yeni bir sipari� ekler.

**Parametreler:**
- **orderDto (OrderCreateDto):** Sipari� detaylar�

**Yan�t:**

- **201 Created:** Sipari� ba�ar�yla eklendi.
- **400 Bad Request:** Sipari� detaylar� yanl��sa.
- **401 Unauthorized:** Kullan�c� kimli�i bulunamad�ysa.

**�rnek Yan�t:**
```json
{
  "OrderId": 1,
  "OrderDate": "2023-06-30T12:00:00Z",
  "UserName": "JohnDoe",
  "OrderDetails": [
    {
      "OrderDetailId": 1,
      "BookId": 1,
      "BookTitle": "Book Title",
      "Quantity": 2,
      "Price": 29.99
    }
  ]
}
```

### Belirli Bir ID'ye Sahip Sipari�i G�ncelle

**Endpoint:** `PUT /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari�i g�nceller.

**Parametreler:**
- **id (int):** Sipari� ID'si
- **orderDto (OrderUpdateDto):** G�ncellenmi� sipari� detaylar�

**Yan�t:**

- **204 No Content:** Sipari� ba�ar�yla g�ncellendi.
- **400 Bad Request:** Sipari� ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Sipari� bulunamad�.

### Belirli Bir ID'ye Sahip Sipari�i Siler

**Endpoint:** `DELETE /api/Orders/{id}`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari�i siler.

**Parametreler:**
- **id (int):** Sipari� ID'si

**Yan�t:**

- **204 No Content:** Sipari� ba�ar�yla silindi.
- **404 Not Found:** Sipari� bulunamad�.




## Sipari� Detaylar� Y�netimi

### T�m Sipari� Detaylar�n� Al

**Endpoint:** `GET /api/OrderDetails`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** T�m sipari� detaylar�n� al�r.

**Yan�t:**

- **200 OK:** Sipari� detaylar�n�n listesi ba�ar�yla d�nd�r�ld�.

**�rnek Yan�t:**
```json
[
  {
    "OrderDetailId": 1,
    "OrderId": 1,
    "BookId": 1,
    "Quantity": 2,
    "Price": 29.99
  },
  {
    "OrderDetailId": 2,
    "OrderId": 1,
    "BookId": 2,
    "Quantity": 1,
    "Price": 19.99
  }
]
```

### Belirli Bir ID'ye Sahip Sipari� Detay�n� Al

**Endpoint:** `GET /api/OrderDetails/{id}`

**Yetki:** Admin, Moderator veya User rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari� detay�n� al�r.

**Parametreler:**
- **id (int):** Sipari� detay� ID'si

**Yan�t:**

- **200 OK:** Sipari� detay� ba�ar�yla d�nd�r�ld�.
- **404 Not Found:** Sipari� detay� bulunamad�.

**�rnek Yan�t:**
```json
{
  "OrderDetailId": 1,
  "OrderId": 1,
  "BookId": 1,
  "Quantity": 2,
  "Price": 29.99
}
```

### Yeni Bir Sipari� Detay� Ekle

**Endpoint:** `POST /api/OrderDetails`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Yeni bir sipari� detay� ekler.

**Parametreler:**
- **orderDetailDto (OrderDetailCreateDto):** Sipari� detay� detaylar�

**Yan�t:**

- **201 Created:** Sipari� detay� ba�ar�yla eklendi.
- **400 Bad Request:** Sipari� detay� detaylar� yanl��sa.

**�rnek Yan�t:**
```json
{
  "OrderDetailId": 1,
  "OrderId": 1,
  "BookId": 1,
  "Quantity": 2,
  "Price": 29.99
}
```

### Belirli Bir ID'ye Sahip Sipari� Detay�n� G�ncelle

**Endpoint:** `PUT /api/OrderDetails/{id}`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari� detay�n� g�nceller.

**Parametreler:**
- **id (int):** Sipari� detay� ID'si
- **orderDetailDto (OrderDetailUpdateDto):** G�ncellenmi� sipari� detay� detaylar�

**Yan�t:**

- **204 No Content:** Sipari� detay� ba�ar�yla g�ncellendi.
- **400 Bad Request:** Sipari� detay� ID uyumsuzlu�u veya detaylar� yanl��sa.
- **404 Not Found:** Sipari� detay� bulunamad�.

### Belirli Bir ID'ye Sahip Sipari� Detay�n� Siler

**Endpoint:** `DELETE /api/OrderDetails/{id}`

**Yetki:** Admin veya Moderator rol� gerektirir.

**A��klama:** Belirli bir ID'ye sahip sipari� detay�n� siler.

**Parametreler:**
- **id (int):** Sipari� detay� ID'si

**Yan�t:**

- **204 No Content:** Sipari� detay� ba�ar�yla silindi.
- **404 Not Found:** Sipari� detay� bulunamad�.