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