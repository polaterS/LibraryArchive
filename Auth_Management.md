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
