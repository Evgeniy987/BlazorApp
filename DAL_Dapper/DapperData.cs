
using DAL_Dapper.Models;

public class DapperData : IDapperData
{
    private readonly ISqlDataAccess _db;

    public DapperData(ISqlDataAccess db)
    {
        this._db = db;
    }

   

    public Task<IEnumerable<Tag>> TagsByObj(int objId, string objName)
    {
        return _db.LoadItems<Tag, dynamic>("dbo.TagsByObj", new { objId, objName });
    }

    public async Task GetTags(IEnumerable<Obj> objs, string objName)
    {
        if (objs.Any())
        {
            var tags = await TagsByObjs(objs.Min(p => p.Id), objs.Max(p => p.Id), objName);
            var prIds = tags.GroupBy(p => p.ObjId).Select(g => g.Key).ToList();

            foreach (var pr in objs.Where(p => prIds.Contains(p.Id)))
            {
                pr.Tags = tags.Where(p => p.ObjId == pr.Id).ToList();
            }
        }

    }

    public Task<IEnumerable<Tag>> TagsByObjs(int objIdMin, int objIdMax, string objName)
    {
        return _db.LoadItems<Tag, dynamic>("dbo.TagsByObjs", new { objIdMin, objIdMax, objName });
    }

    public Task TagsAdd(IEnumerable<Tag> tags)
    {
        string sql = @"insert into dbo.Tags  
                            (ObjId, ObjName, CreatedAt, CreatedBy, Title, BadgeColor, IsRemovable)
                            VALUES (@ObjId, @ObjName, @CreatedAt, @CreatedBy, @Title, @BadgeColor, @IsRemovable);";

        return _db.SaveData(sql, tags);
    }

    public Task<int> TagAdd(Tag tag)
    {
        string sql = @"insert into dbo.Tags  
                            (ObjId, ObjName, CreatedAt, CreatedBy, Title, BadgeColor, IsRemovable)
                            VALUES (@ObjId, @ObjName, @CreatedAt, @CreatedBy, @Title, @BadgeColor, @IsRemovable);";

        return _db.InsertData(sql, tag);
    }

    public Task TagsByObjDel(string objName, int objId)
    {
        return _db.SaveData("DELETE Tags WHERE ObjName = @objName AND ObjId = @objId", new { objName, objId });
    }

   
    public Task ObjDel(string tbl, int id)
    {
        string sql = @$"DELETE {tbl} WHERE Id = @Id;";
        return _db.SaveData(sql, new { id });
    }

    
    public Task<User> UserById(int id)
    {
        return _db.LoadItem<User, dynamic>("dbo.UserById", new { id });
    }

    public Task<User> UserByEmail(string email)
    {
        return _db.LoadItem<User, dynamic>("dbo.UserByEmail", new { email });
    }

    public Task<User?> UserByKeyPass(string keyPass)
    {
        return _db.LoadItem<User?, dynamic>("dbo.UserByKeyPass", new { keyPass });
    }

    public Task<IEnumerable<User>> UsersByDealer(int? dealerId)
    {
        return _db.LoadItems<User, dynamic>("dbo.UsersByDealer", new { dealerId });
    }

    public Task<IEnumerable<User>> UsersActive()
    {
        return _db.LoadItems<User, dynamic>("dbo.UsersActive", new { });
    }

    public Task<int> UserAdd(User obj)
    {
        string sql = @"insert into dbo.Users  
                            (Name, LongName, DealerId, Notes, CreatedAt, CreatedBy, PhoneNumber, EmailAddress, BadgeColor)
                            OUTPUT INSERTED.Id
                            VALUES (@Name, @LongName, @DealerId, @Notes, @CreatedAt, @CreatedBy, @PhoneNumber, @EmailAddress, @BadgeColor);";

        return _db.InsertData(sql, obj);
    }

    public Task UserUpd(User user)
    {
        string sql = @"UPDATE dbo.Users SET
                            Name = @Name, 
                            LongName = @LongName,
                            Notes = @Notes, 
                            CreatedAt = @CreatedAt, 
                            CreatedBy = @CreatedBy,
                            IsDisabled = @IsDisabled,
                            DisabledBy = @DisabledBy,
                            PhoneNumber = @PhoneNumber,
                            BadgeColor = @BadgeColor
                            WHERE Id = @Id;";

        return _db.SaveData(sql, user);
    }

    public Task<User> UserByAccessToken(string accessToken)
    {
        return _db.LoadItem<User, dynamic>("dbo.UserByAccessToken", new { accessToken });
    }

    public Task<User> UserByRefreshToken(string refreshToken)
    {
        return _db.LoadItem<User, dynamic>("dbo.UserByRefreshToken", new { refreshToken });
    }

    public Task<User> UserByNamePassword(NamePassword namePassword)
    {
        var emailAddress = namePassword.EmailAddress;
        var password = Utility.Encrypt(namePassword.EmailAddress + namePassword.Password);

        return _db.LoadItem<User, dynamic>("dbo.UserByNamePassword", new { emailAddress, password });
    }

    public async Task UserRolesDelete(int userId)
    {
        await _db.Execute<dynamic>("UserRolesDelete", new { userId });
    }

}
