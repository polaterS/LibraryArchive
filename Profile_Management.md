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