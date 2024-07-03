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