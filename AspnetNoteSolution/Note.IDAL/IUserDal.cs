using Note.Model;

namespace Note.IDAL
{
    public interface IUserDal
    {
        List<User> GetUserList();
        User GetUser(int userNum);
        bool SaveUser(User user);
    }
}
