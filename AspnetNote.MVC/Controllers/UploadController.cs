using Microsoft.AspNetCore.Mvc;

namespace AspnetNote.MVC.Controllers
{
    public class UploadController : Controller
    {
        // 디펜던시 인젝션 IHostingEnvironment을 asp.net core단에서
        private readonly IWebHostEnvironment _environment;

        // 생성자. ctor 탭 사용
        // 어떠한 폴더에 접근하고 싶으면 이를 사용할 수 있음
        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // # URL 접근 방식
        // ASP.NET - 호스트명/api/upload
        // JavaScript - 호스트명 + api/upload => http://www.example.comapi/
        // JavaScript - 호스트명 + / + api/upload => http://www.example.com/api/upload

        // 파일 전송할땐 일반적으론 post 사용하고, 라우트를 통해서 URL 축약할 것
        // http://www.example.com/Upload/ImageUpLoad >> http://www.example.com/api/upload
        [HttpPost, Route("api/upload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // wwwroot/images에 이미지를 저장해야 함
            // # 이미지나 파일을 업로드 할 때 필요한 구성

            // 1. Path(경로) - 어디에다가 저장을 할지
            // (1/, 2/, 3/, 4/) >> 1/2/3/4/
            var path = Path.Combine(_environment.WebRootPath, @"images\upload");

            // 2. Name(이름) - Datetime, GUID, GUID + GUID
            var fileFullName = file.FileName.Split('.'); // 업로드 본래 이름
            // 이 때, 이미지 이름이 동일해질 우려가 있어 filename을 쪼개주었다.
            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";
            // 동일한 이름의 파일이 업로드 될 경우 이름이 달라진다.
            // 이런 경우 확장자도 그대로 따라온다고 보면 된다.

            // 3. Extension(확장자) -  jpg, png, txt...

            // 서버에 저장하려고 함. 그러나 이 파일의 크기 등 변수가 많기 때문에
            // 이에 대한 기준이 필요하며, 메모리 해제를 잘 해주어야 함.
            // 다른 작업들에서 using문을 통해 db에 접근했듯이 자원 해제를 해 줄것.
            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                // file.Copy(fileStream);
                // 해당 구문의 경우 사진의 용량이 클 경우 병목현상이 발생할 수 있다.
                // 문제는 다른 사용자들도 웹사이트가 멈출 수 있다. 때문에 비동기적 처리가 필요하다.
                await file.CopyToAsync(fileStream); // await 오퍼레이터는 async와 항상 따라다니는 친구
            }
            // 업로드만 받을 것이므로 view 페이지가 필요가 없음
            // 대신 Ok를 통해서, trumbowyg측에 파일이 정상적으로 업로드 되었는지 여부를
            // 파일 위치를 보내줌으로서 확인시켜줄 수 있음. 여기서도 경로 설정 시
            // 앞에 / 를 하나 넣어서 보내줘야 함.
            return Ok(new { file = "/images/upload/" + fileName, success = true });
        }
    }
}
