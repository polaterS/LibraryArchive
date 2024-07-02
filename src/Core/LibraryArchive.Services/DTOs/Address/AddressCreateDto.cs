﻿namespace LibraryArchive.Services.DTOs.Address
{
    public class AddressCreateDto
    {
        public string UserId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
