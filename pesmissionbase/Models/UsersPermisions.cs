namespace pesmissionbase.Models
{
    public class UsersPermisions
    {
        public long Id { get; set; }
        public virtual Roles Roles { get; set; }
        public string RoleId { get; set; }

        public virtual Permissions Permissions { get; set; }
        public string PermissionId { get; set; }

        public bool Avalilable { get; set; }
    }
}
