namespace pesmissionbase.Models
{
    public class Permissions
    {
        public string PermissionsId { get; set; }
        public string ActionName { get; set; }
        public string ControlerName { get; set; }

        //    public bool Avalilable { get; set; }

        public ICollection<UsersPermisions> UsersPermisions { get; set; }

    }
}
