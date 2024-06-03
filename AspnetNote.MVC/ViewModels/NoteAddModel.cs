using AspnetNote.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace AspnetNote.MVC.ViewModels
{
    public class NoteAddModel
    {
        /// <summary>
        /// 게시물 제목
        /// </summary>
        [Required(ErrorMessage = "제목을 입력하세요.")]
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

        public NoteAddModel() { }

        public NoteAddModel(Note model)
        {
            NoteTitle = model.NoteTitle;
            NoteContents = model.NoteContents;
            UserNum = model.UserNum;
        }
    }
}
