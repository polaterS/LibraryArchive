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