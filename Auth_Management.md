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
