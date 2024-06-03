using AspnetNote.MVC.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspnetNote.MVC.Models
{
    public class Note
    {
        /// <summary>
        /// 게시물 번호
        /// </summary>
        [Key]
        public int NoteNum { get; set; }

        /// <summary>
        /// 게시물 제목
        /// </summary>
        [Required(ErrorMessage ="제목을 입력하세요.")]
        public string NoteTitle { get; set; }

        /// <summary>
        /// 게시물 내용
        /// </summary>
        [Required(ErrorMessage = "내용을 입력하세요.")]
        public string NoteContents { get; set; }

        /// <summary>
        /// 작성자 번호
        /// </summary>
        [Required]
        public int UserNum { get; set; }

        // Code First 에서 사용하는 Join 방법으로,
        // UserNum만으로 작성자를 표현할 수 없기때문에
        // 사용자 이름도 얻을 수 있도록 Join 시킨다.
        // 이 때 foreign key가 필요하다.
        // virtual은 레이지 로딩이라고 해서, 주 데이터를 먼저 출력을 하고
        // 나중에 천천히 비 동기적으로 가져온다는 의미에서 virtual을 선언함. 안써도 정상작동은 한다.
        [ForeignKey("UserNum")]
        public virtual User User { get; set; }

        public Note() { }

        public Note(NoteAddModel model)
        {
            NoteTitle = model.NoteTitle;
            NoteContents = model.NoteContents;
            UserNum = model.UserNum;
        }

        public Note(Note model)
        {
            NoteTitle = model.NoteTitle;
            NoteContents = model.NoteContents;
            UserNum = model.UserNum;
        }
    }
}
