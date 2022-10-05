using pesmissionbase.Data;
using pesmissionbase.Models;

namespace pesmissionbase.Services.AddPermissions
{
    public interface IAddPermission
    {
        ResultDto<ResultAddPermissionDto> Excute(PerMissionsForAdd request);

    }


    public class AddPermission : IAddPermission
    {
        DataBaseContext _context;
        public AddPermission(DataBaseContext context)
        {
            _context = context;

        }
        public ResultDto<ResultAddPermissionDto> Excute(PerMissionsForAdd request)
        {

            foreach (var item in _context.Permissions)
            {
                if (item.ControlerName == request.ControllerName && item.ActionName == request.ActionName)
                {
                    var role = _context.Roles.Find(request.RoleId);
                    UsersPermisions usersPermisions = new UsersPermisions()
                    {
                        RoleId = request.RoleId,
                        Roles = role,
                        Avalilable = request.IsAllowed,
                        PermissionId = item.PermissionsId,
                        Permissions = item

                    };
                    _context.UsersPermisions.Add(usersPermisions);

                }

            }
            _context.SaveChanges();
            var result = new ResultDto<ResultAddPermissionDto>()
            {

                IsSucces = true,

            };
            return result;

        }
    }

    //public class RequestAddPermissnsDto
    //{
    //    List<PerMissionsForAdd> perMissionsForAdds { get; set; }
    //}


    public class PerMissionsForAdd
    {
        public string RoleId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public bool IsAllowed { get; set; }

    }
    public class ResultAddPermissionDto
    {
        public long Id { get; set; }
    }
    public class ResultDto<T>
    {
        public bool IsSucces { get; set; }
        public string Massage { get; set; }
        public T Data { get; set; }
    }
}
