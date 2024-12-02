namespace Model_BD.API.Model
{
    public class ModelDTO
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string MobileNo { get; set; }

        public string Email { get; set; }

    }
    public class AddModel : ModelDTO
    {
        public long? RoleId { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
    }
    public class EditModel : ModelDTO
    {
        public long? RoleId { get; set; }
        public long Id { get; set; }

    }
}
