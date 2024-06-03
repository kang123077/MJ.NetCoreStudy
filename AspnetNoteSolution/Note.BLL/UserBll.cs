using Note.IDAL;
using Note.Model;

// BLL의 경우 어떤 플랫폼에서도 변함이 없는 것들을 모을 예정이기에
// 별도로 인터페이스를 생성 하거나 상속하지 않는다
namespace Note.BLL
{
    // 느슨한 결합을 위한 인터페이스 구현
    public class UserBll // : IUserDal 을 한번은 등록시켜서 타이핑 수를 줄일 수 있다
    {
        // 고전적으론 다음과 같이 새 객체를 생성하였음. 강한 결합
        //private UserDal _userDal = new UserDal();
        
        // 인터페이스를 사용하여 다음과 같이 느슨한 결합
        private readonly IUserDal _userDal;

        // 외부에서 인터페이스를 주입 해 준다
        public UserBll(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<User> GetUserList()
        {
            return _userDal.GetUserList();
        }
        
        public User GetUser(int userNum)
        {
            if (userNum <= 0)
            {
                throw new ArgumentException();
            }
            return _userDal.GetUser(userNum);
        }

        public bool SaveUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentException();
            }
            return _userDal.SaveUser(user);
        }
    }
}
