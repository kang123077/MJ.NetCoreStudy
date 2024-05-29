using System.ComponentModel.DataAnnotations;

namespace AspnetNote.MVC.Models
{
    public class User
    {
        /// <summary>
        /// 사용자 번호
        /// </summary>
        [Key] // 프라이머리 키, PK 설정 어노테이션
        public int UserNum { get; set; }

        /// <summary>
        /// 사용자 이름
        /// </summary>
        [Required(ErrorMessage = "사용자 이름을 입력하세요")] // not null 설정
        public string UserName { get; set; }

        /// <summary>
        /// 사용자 ID
        /// </summary>
        [Required(ErrorMessage = "사용자 ID를 입력하세요")] // not null 설정
        public string UserId { get; set; }

        /// <summary>
        /// 사용자 비밀번호
        /// </summary>
        [Required(ErrorMessage = "사용자 비밀번호 입력하세요")] // not null 설정
        public string UserPassword { get; set;}
    }
}
