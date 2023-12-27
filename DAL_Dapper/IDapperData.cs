using DAL_Dapper.Models;

public interface IDapperData
{

    Task<int> TagAdd(Tag obj);

    Task GetTags(IEnumerable<Obj> objs, string objName);
    Task<IEnumerable<Tag>> TagsByObj(int objId, string objName);
    Task TagsByObjDel(string objName, int objId);
   
    Task TagsAdd(IEnumerable<Tag> tags);
    
    Task<IEnumerable<User>> UsersActive();
    Task<User> UserByEmail(string email);
    Task<IEnumerable<User>> UsersByDealer(int? dealerId);
    Task<User> UserById(int id);

    Task<User> UserByAccessToken(string accessToken);
    Task<User> UserByRefreshToken(string refreshToken);
    Task<User> UserByNamePassword(NamePassword namePassword);

    Task<int> UserAdd(User obj);
    Task UserUpd(User user);
    Task UserRolesDelete(int userId);
}