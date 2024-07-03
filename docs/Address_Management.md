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