using AspnetNote.MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetNote.MVC.DataContext
{
    public class AspnetNoteDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        // Db컨텍스트에 원래 있는 거로, JAVA의 커넥션 스트링처럼
        // DB 주소, 접속 아이디 비밀번호, 옵션 등을 거기에 작성하는 것 처럼
        // @는 안에있는 string을 정확하게 전달하겠다는 의미
        // Encrypt=false를 추가해줘야 인증서 오류가 안뜬다
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=TRITECHJS;Database=AspnetNoteDb;User Id=sa;Password=sa1234;Encrypt=false");
        }
    }
}
