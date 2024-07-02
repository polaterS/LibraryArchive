﻿namespace LibraryArchive.Data.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Book Book { get; set; }
        public virtual Order Order { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
