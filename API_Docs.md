# LibraryArchive API Kullaným Kýlavuzu

Bu kýlavuz, LibraryArchive API'sinin kullanýmýný açýklamaktadýr. 

API, kitaplýk arþiv yönetimi, kullanýcý etkileþimi ve e-ticaret özelliklerini içerir.

## Genel Bilgiler

- Base URL: `https://api.libraryarchive.com`
- API Sürümü: v1
- Yetkilendirme: JWT tabanlý kimlik doðrulama kullanýr. Her istekte `Authorization` baþlýðýnda `Bearer <token>` formatýnda gönderilmelidir.

## Kimlik Doðrulama ve Yetkilendirme

### Kayýt Olma

**URL:** `/api/Auth/Register`

**Yöntem:** `POST`

**Açýklama:** Yeni bir kullanýcý kaydeder ve JWT belirteci döner.



**Ýstek:**

```json
{
  "Email": "example@example.com",
  "Password": "YourPassword123",
  "Name": "John",
  "Surname": "Doe"
}
```

**Yanýt:**

```json
{
  "Token": "your.jwt.token.here"
}
```

**Yanýt Kodlarý:**

- `200 OK` - Kayýtlý kullanýcý için JWT token döndürür
- `400 Bad Request` - Kayýt ayrýntýlarý yanlýþsa

### Giriþ Yapma

**URL:** `/api/Auth/Login`

**Yöntem:** `POST`

**Açýklama:** Kullanýcý giriþi yapar ve JWT belirteci döner.

**Ýstek:**

```json
{
  "Email": "example@example.com",
  "Password": "YourPassword123"
}
```

**Yanýt:**

```json
{
  "Token": "your.jwt.token.here"
}
```

**Yanýt Kodlarý:**

- `200 OK` - JWT token döndürür
- `400 Bad Request` - Giriþ bilgileri yanlýþsa

### Rol Atama

**URL:** `/api/Auth/AssignRole`

**Yöntem:** `POST`

**Yetki:** `Admin`

**Açýklama:** Bir kullanýcýya rol atar.

**Ýstek:**

```json
{
  "Email": "user@example.com",
  "RoleName": "Admin"
}
```

**Yanýt:**

```json
{
  "Message": "Rol baþarýyla atandý."
}
```

**Yanýt Kodlarý:**

- `200 OK` - Rol baþarýyla atandý
- `400 Bad Request` - Rol atama detaylarý yanlýþsa
- `403 Forbidden` - Kullanýcý yetkili deðilse

### Rol Oluþturma

**URL:** `/api/Auth/CreateRole`

**Yöntem:** `POST`

**Yetki:** `Admin`

**Açýklama:** Yeni bir rol oluþturur.

**Ýstek:**

```json
{
  "RoleName": "Admin"
}
```

**Yanýt:**

```json
{
  "Message": "Rol baþarýyla oluþturuldu"
}
```

**Yanýt Kodlarý:**

- `200 OK` - Rol baþarýyla oluþturuldu
- `400 Bad Request` - Rol detaylarý yanlýþsa
- `403 Forbidden` - Kullanýcý yetkili deðilse




## Kullanýcý Yönetimi

### Tüm Kullanýcýlarý Al

**URL:** `/api/Users`

**Yöntem:** `GET`

**Yetki:** `Admin`

**Açýklama:** Tüm kullanýcýlarý alýr.

**Yanýt:**

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

**Yanýt Kodlarý:**

- `200 OK` - Kullanýcýlarýn listesi baþarýyla döndürüldü

### Kullanýcý Detaylarýný Al

**URL:** `/api/Users/{id}`

**Yöntem:** `GET`

**Yetki:** `Admin`

**Açýklama:** Belirli bir ID'ye sahip kullanýcýyý alýr.

**Ýstek Parametreleri:**

- `id` (string): Kullanýcý ID'si

**Yanýt:**

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

**Yanýt Kodlarý:**

- `200 OK` - Kullanýcý baþarýyla döndürüldü
- `404 Not Found` - Kullanýcý bulunamadý

### Yeni Kullanýcý Kaydet

**URL:** `/api/Users`

**Yöntem:** `POST`

**Yetki:** `Admin`

**Açýklama:** Yeni bir kullanýcý kaydeder.

**Ýstek:**

```json
{
  "UserName": "newuser",
  "Email": "newuser@example.com",
  "Password": "YourPassword123",
  "Name": "Jane",
  "Surname": "Doe"
}
```

**Yanýt:**

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

**Yanýt Kodlarý:**

- `201 Created` - Kullanýcý baþarýyla kaydedildi
- `400 Bad Request` - Kullanýcý detaylarý yanlýþsa

### Kullanýcý Güncelle

**URL:** `/api/Users/{id}`

**Yöntem:** `PUT`

**Yetki:** `Admin`

**Açýklama:** Belirli bir ID'ye sahip kullanýcýyý günceller.

**Ýstek Parametreleri:**

- `id` (string): Kullanýcý ID'si

**Ýstek:**

```json
{
  "Id": "user_id",
  "Email": "updateduser@example.com",
  "Name": "Updated",
  "Surname": "User"
}
```

**Yanýt Kodlarý:**

- `204 No Content` - Kullanýcý baþarýyla güncellendi
- `400 Bad Request` - Kullanýcý ID uyumsuzluðu veya detaylarý yanlýþsa
- `404 Not Found` - Kullanýcý bulunamadý

### Kullanýcý Sil

**URL:** `/api/Users/{id}`

**Yöntem:** `DELETE`

**Yetki:** `Admin`

**Açýklama:** Belirli bir ID'ye sahip kullanýcýyý siler.

**Ýstek Parametreleri:**

- `id` (string): Kullanýcý ID'si

**Yanýt Kodlarý:**

- `204 No Content` - Kullanýcý baþarýyla silindi
- `400 Bad Request` - Kullanýcý bulunamadý



## Profil Yönetimi

### Kullanýcý Profili Al

**URL:** `/api/Profile`

**Yöntem:** `GET`

**Yetki:** `Admin, User`

**Açýklama:** Kullanýcý profilini alýr.

**Yanýt:**

```json
{
  "Name": "John",
  "Surname": "Doe",
  "Email": "user@example.com",
  "ProfilePictureUrl": "https://example.com/picture.jpg"
}
```

**Yanýt Kodlarý:**

- `200 OK` - Kullanýcý profili baþarýyla döndürüldü
- `404 Not Found` - Kullanýcý bulunamadý

### Kullanýcý Profilini Güncelle

**URL:** `/api/Profile/profile`

**Yöntem:** `PUT`

**Yetki:** `Admin, User`

**Açýklama:** Kullanýcý profilini günceller.

**Ýstek:**

```json
{
  "Name": "Updated",
  "Surname": "User",
  "ProfilePictureUrl": "https://example.com/newpicture.jpg"
}
```

**Yanýt Kodlarý:**

- `204 No Content` - Profil baþarýyla güncellendi
- `400 Bad Request` - Profil güncelleme baþarýsýz

### Kullanýcý Email Adresini Güncelle

**URL:** `/api/Profile/email`

**Yöntem:** `PUT`

**Yetki:** `Admin, User`

**Açýklama:** Kullanýcý email adresini günceller.

**Ýstek:**

```json
{
  "Email": "updateduser@example.com",
  "CurrentPassword": "CurrentPassword123"
}
```

**Yanýt Kodlarý:**

- `204 No Content` - Email baþarýyla güncellendi
- `400 Bad Request` - Email güncelleme baþarýsýz

### Kullanýcý Þifresini Güncelle

**URL:** `/api/Profile/password`

**Yöntem:** `PUT`

**Yetki:** `Admin, User`

**Açýklama:** Kullanýcý þifresini günceller.

**Ýstek:**

```json
{
  "CurrentPassword": "CurrentPassword123",
  "NewPassword": "NewPassword456"
}
```

**Yanýt Kodlarý:**

- `204 No Content` - Þifre baþarýyla güncellendi
- `400 Bad Request` - Þifre güncelleme baþarýsýz

### Kullanýcý Profilini Sil

**URL:** `/api/Profile`

**Yöntem:** `DELETE`

**Yetki:** `Admin, User`

**Açýklama:** Kullanýcý profilini siler.

**Yanýt Kodlarý:**

- `204 No Content` - Profil baþarýyla silindi
- `400 Bad Request` - Profil silme baþarýsýz




## Adres Yönetimi

### Kullanýcýnýn Tüm Adreslerini Al

**URL:** `/api/Addresses`

**Yöntem:** `GET`

**Yetki:** `Admin, User`

**Açýklama:** Kullanýcýnýn tüm adreslerini alýr.

**Yanýt:**

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

**Yanýt Kodlarý:**

- `200 OK` - Adresler baþarýyla döndürüldü

### Belirli Bir ID'ye Sahip Adresi Al

**URL:** `/api/Addresses/{id}`

**Yöntem:** `GET`

**Yetki:** `Admin, User`

**Açýklama:** Belirli bir ID'ye sahip adresi alýr.

**Yanýt:**

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

**Yanýt Kodlarý:**

- `200 OK` - Adres detaylarý baþarýyla döndürüldü
- `404 Not Found` - Adres bulunamadý

### Yeni Bir Adres Ekle

**URL:** `/api/Addresses`

**Yöntem:** `POST`

**Yetki:** `Admin, User`

**Açýklama:** Yeni bir adres ekler.

**Ýstek:**

```json
{
  "Street": "123 Main St",
  "City": "Anytown",
  "State": "CA",
  "PostalCode": "12345",
  "Country": "USA"
}
```

**Yanýt:**

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

**Yanýt Kodlarý:**

- `201 Created` - Adres baþarýyla eklendi
- `400 Bad Request` - Adres detaylarý yanlýþsa

### Belirli Bir ID'ye Sahip Adresi Güncelle

**URL:** `/api/Addresses/{id}`

**Yöntem:** `PUT`

**Yetki:** `Admin, User`

**Açýklama:** Belirli bir ID'ye sahip adresi günceller.

**Ýstek:**

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

**Yanýt Kodlarý:**

- `204 No Content` - Adres baþarýyla güncellendi
- `400 Bad Request` - Adres ID uyumsuzluðu veya detaylarý yanlýþsa
- `404 Not Found` - Adres bulunamadý

### Belirli Bir ID'ye Sahip Adresi Siler

**URL:** `/api/Addresses/{id}`

**Yöntem:** `DELETE`

**Yetki:** `Admin, User`

**Açýklama:** Belirli bir ID'ye sahip adresi siler.

**Yanýt Kodlarý:**

- `204 No Content` - Adres baþarýyla silindi
- `404 Not Found` - Adres bulunamadý




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




## Bildirim Yönetimi

### Belirli Bir Kullanýcýya Ait Tüm Bildirimleri Al

**Endpoint:** `GET /api/Notifications`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir kullanýcýya ait tüm bildirimleri alýr.

**Yanýt:**

- **200 OK:** Bildirimler baþarýyla döndürüldü.

**Örnek Yanýt:**
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

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirimi alýr.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yanýt:**

- **200 OK:** Bildirim detaylarý baþarýyla döndürüldü.
- **404 Not Found:** Bildirim bulunamadý.

**Örnek Yanýt:**
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

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Yeni bir bildirim ekler.

**Parametreler:**
- **notificationDto (NotificationCreateDto):** Bildirim detaylarý

**Yanýt:**

- **201 Created:** Bildirim baþarýyla eklendi.
- **400 Bad Request:** Bildirim detaylarý yanlýþsa.

**Örnek Yanýt:**
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

### Belirli Bir ID'ye Sahip Bildirimi Güncelle

**Endpoint:** `PUT /api/Notifications/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirimi günceller.

**Parametreler:**
- **id (int):** Bildirim ID'si
- **notificationDto (NotificationUpdateDto):** Güncellenmiþ bildirim detaylarý

**Yanýt:**

- **204 No Content:** Bildirim baþarýyla güncellendi.
- **400 Bad Request:** Bildirim ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Bildirim bulunamadý.

### Belirli Bir ID'ye Sahip Bildirimi Siler

**Endpoint:** `DELETE /api/Notifications/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirimi siler.

**Parametreler:**
- **id (int):** Bildirim ID'si

**Yanýt:**

- **204 No Content:** Bildirim baþarýyla silindi.
- **404 Not Found:** Bildirim bulunamadý.




## Bildirim Ayarlarý Yönetimi

### Belirli Bir Kullanýcýya Ait Tüm Bildirim Ayarlarýný Al

**Endpoint:** `GET /api/NotificationSettings`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir kullanýcýya ait tüm bildirim ayarlarýný alýr.

**Yanýt:**

- **200 OK:** Bildirim ayarlarý baþarýyla döndürüldü.

**Örnek Yanýt:**
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

### Belirli Bir ID'ye Sahip Bildirim Ayarlarýný Al

**Endpoint:** `GET /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirim ayarlarýný alýr.

**Parametreler:**
- **id (int):** Bildirim ayarlarý ID'si

**Yanýt:**

- **200 OK:** Bildirim ayarlarý baþarýyla döndürüldü.
- **404 Not Found:** Bildirim ayarlarý bulunamadý.

**Örnek Yanýt:**
```json
{
  "NotificationSettingsId": 1,
  "UserId": "user1",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": false,
  "PushNotificationsEnabled": true
}
```

### Yeni Bir Bildirim Ayarý Ekle

**Endpoint:** `POST /api/NotificationSettings`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Yeni bir bildirim ayarý ekler.

**Parametreler:**
- **notificationSettingsDto (NotificationSettingsCreateDto):** Bildirim ayarlarý detaylarý

**Yanýt:**

- **201 Created:** Bildirim ayarý baþarýyla eklendi.
- **400 Bad Request:** Bildirim ayarlarý detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "NotificationSettingsId": 3,
  "UserId": "user2",
  "EmailNotificationsEnabled": true,
  "SmsNotificationsEnabled": true,
  "PushNotificationsEnabled": false
}
```

### Belirli Bir ID'ye Sahip Bildirim Ayarlarýný Güncelle

**Endpoint:** `PUT /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirim ayarlarýný günceller.

**Parametreler:**
- **id (int):** Bildirim ayarlarý ID'si
- **notificationSettingsDto (NotificationSettingsUpdateDto):** Güncellenmiþ bildirim ayarlarý detaylarý

**Yanýt:**

- **204 No Content:** Bildirim ayarlarý baþarýyla güncellendi.
- **400 Bad Request:** Bildirim ayarlarý ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Bildirim ayarlarý bulunamadý.

### Belirli Bir ID'ye Sahip Bildirim Ayarlarýný Siler

**Endpoint:** `DELETE /api/NotificationSettings/{id}`

**Yetki:** Admin veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip bildirim ayarlarýný siler.

**Parametreler:**
- **id (int):** Bildirim ayarlarý ID'si

**Yanýt:**

- **204 No Content:** Bildirim ayarlarý baþarýyla silindi.
- **404 Not Found:** Bildirim ayarlarý bulunamadý.




## Sipariþ Yönetimi

### Tüm Sipariþleri Al

**Endpoint:** `GET /api/Orders`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Tüm sipariþleri alýr.

**Yanýt:**

- **200 OK:** Sipariþlerin listesi baþarýyla döndürüldü.

**Örnek Yanýt:**
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

### Belirli Bir ID'ye Sahip Sipariþi Al

**Endpoint:** `GET /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþi alýr.

**Parametreler:**
- **id (int):** Sipariþ ID'si

**Yanýt:**

- **200 OK:** Sipariþ detaylarý baþarýyla döndürüldü.
- **404 Not Found:** Sipariþ bulunamadý.

**Örnek Yanýt:**
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

### Yeni Bir Sipariþ Ekle

**Endpoint:** `POST /api/Orders`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Yeni bir sipariþ ekler.

**Parametreler:**
- **orderDto (OrderCreateDto):** Sipariþ detaylarý

**Yanýt:**

- **201 Created:** Sipariþ baþarýyla eklendi.
- **400 Bad Request:** Sipariþ detaylarý yanlýþsa.
- **401 Unauthorized:** Kullanýcý kimliði bulunamadýysa.

**Örnek Yanýt:**
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

### Belirli Bir ID'ye Sahip Sipariþi Güncelle

**Endpoint:** `PUT /api/Orders/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþi günceller.

**Parametreler:**
- **id (int):** Sipariþ ID'si
- **orderDto (OrderUpdateDto):** Güncellenmiþ sipariþ detaylarý

**Yanýt:**

- **204 No Content:** Sipariþ baþarýyla güncellendi.
- **400 Bad Request:** Sipariþ ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Sipariþ bulunamadý.

### Belirli Bir ID'ye Sahip Sipariþi Siler

**Endpoint:** `DELETE /api/Orders/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþi siler.

**Parametreler:**
- **id (int):** Sipariþ ID'si

**Yanýt:**

- **204 No Content:** Sipariþ baþarýyla silindi.
- **404 Not Found:** Sipariþ bulunamadý.




## Sipariþ Detaylarý Yönetimi

### Tüm Sipariþ Detaylarýný Al

**Endpoint:** `GET /api/OrderDetails`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Tüm sipariþ detaylarýný alýr.

**Yanýt:**

- **200 OK:** Sipariþ detaylarýnýn listesi baþarýyla döndürüldü.

**Örnek Yanýt:**
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

### Belirli Bir ID'ye Sahip Sipariþ Detayýný Al

**Endpoint:** `GET /api/OrderDetails/{id}`

**Yetki:** Admin, Moderator veya User rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþ detayýný alýr.

**Parametreler:**
- **id (int):** Sipariþ detayý ID'si

**Yanýt:**

- **200 OK:** Sipariþ detayý baþarýyla döndürüldü.
- **404 Not Found:** Sipariþ detayý bulunamadý.

**Örnek Yanýt:**
```json
{
  "OrderDetailId": 1,
  "OrderId": 1,
  "BookId": 1,
  "Quantity": 2,
  "Price": 29.99
}
```

### Yeni Bir Sipariþ Detayý Ekle

**Endpoint:** `POST /api/OrderDetails`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Yeni bir sipariþ detayý ekler.

**Parametreler:**
- **orderDetailDto (OrderDetailCreateDto):** Sipariþ detayý detaylarý

**Yanýt:**

- **201 Created:** Sipariþ detayý baþarýyla eklendi.
- **400 Bad Request:** Sipariþ detayý detaylarý yanlýþsa.

**Örnek Yanýt:**
```json
{
  "OrderDetailId": 1,
  "OrderId": 1,
  "BookId": 1,
  "Quantity": 2,
  "Price": 29.99
}
```

### Belirli Bir ID'ye Sahip Sipariþ Detayýný Güncelle

**Endpoint:** `PUT /api/OrderDetails/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþ detayýný günceller.

**Parametreler:**
- **id (int):** Sipariþ detayý ID'si
- **orderDetailDto (OrderDetailUpdateDto):** Güncellenmiþ sipariþ detayý detaylarý

**Yanýt:**

- **204 No Content:** Sipariþ detayý baþarýyla güncellendi.
- **400 Bad Request:** Sipariþ detayý ID uyumsuzluðu veya detaylarý yanlýþsa.
- **404 Not Found:** Sipariþ detayý bulunamadý.

### Belirli Bir ID'ye Sahip Sipariþ Detayýný Siler

**Endpoint:** `DELETE /api/OrderDetails/{id}`

**Yetki:** Admin veya Moderator rolü gerektirir.

**Açýklama:** Belirli bir ID'ye sahip sipariþ detayýný siler.

**Parametreler:**
- **id (int):** Sipariþ detayý ID'si

**Yanýt:**

- **204 No Content:** Sipariþ detayý baþarýyla silindi.
- **404 Not Found:** Sipariþ detayý bulunamadý.