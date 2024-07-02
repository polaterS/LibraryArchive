namespace LibraryArchive.Services.DTOs.User
{
    public class UserPasswordUpdateDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
