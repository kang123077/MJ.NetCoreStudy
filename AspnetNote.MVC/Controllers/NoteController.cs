using AspnetNote.MVC.DataContext;
using AspnetNote.MVC.Models;
using AspnetNote.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspnetNote.MVC.Controllers
{

    [Authorize, CheckSession]
    public class NoteController : Controller
    {
        /// <summary>
        /// 게시판 리스트
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            using (var db = new AspnetNoteDbContext())
            {
                var list = db.Notes.ToList();
                return View(list);
            }
        }

        /// <summary>
        /// 게시판 상세
        /// </summary>
        /// <param name="noteNum"></param>
        /// <returns></returns>
        public IActionResult Detail(int noteNum)
        {
            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNum.Equals(noteNum));
                return View(note);
            }
        }

        /// <summary>
        /// 게시물 추가
        /// </summary>
        /// <returns></returns>
        public IActionResult Add()
        {
            if (TempData["ModifyNote"] != null)
            {
                NoteAddModel model = new NoteAddModel((Note)TempData["ModifyNote"]);
                return View(model);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add(NoteAddModel model)
        {
            model.UserNum = HttpContext.Session.GetInt32("USER_LOGIN_KEY").Value;

            if (ModelState.IsValid)
            {
                Note newNote = new Note(model);
                using (var db = new AspnetNoteDbContext())
                {
                    db.Notes.Add(newNote);

                    if (db.SaveChanges() > 0) // Commit, 이는 원칙적으론 성공한 갯수를 반환한다.
                    {
                        // 아래는 인덱스의 홈으로 보낼 수 있고, 하단은 그냥 같은 계층에서 Index로 간다.
                        // return RedirectToAction("Index", "Home");
                        return Redirect("Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "게시물을 저장할 수 없습니다.");
            }
            // 모델이 잘못되었다면
            ModelState.AddModelError(string.Empty, "모델이 잘못 되었습니다.");
            return View(model);
        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 게시물 삭제
        /// </summary>
        /// <returns></returns>
        public IActionResult Delete()
        {
            if (HttpContext.Session.GetInt32("USER_LOGIN_KEY") == null)
            {
                // 로그인이 안 된 상태
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int noteNum)
        {
            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNum.Equals(noteNum));
                db.Notes.Remove(note);

                if (db.SaveChanges() > 0)
                {
                    return Redirect("Index");
                }
                else
                {
                    return View(note);
                }
            }
        }

        /// <summary>
        /// 게시물 수정
        /// </summary>
        /// <param name="noteNum"></param>
        /// <returns></returns>
        public IActionResult Modify(int noteNum)
        {
            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNum.Equals(noteNum));

                if (note != null)
                {
                    return View(note);
                }
                else
                {
                    return Redirect("Index");
                }
            }
        }

        [HttpPost]
        public IActionResult Modify(Note model)
        {
            using (var db = new AspnetNoteDbContext())
            {
                var note = db.Notes.FirstOrDefault(n => n.NoteNum.Equals(model.NoteNum));

                note.NoteTitle = model.NoteTitle;
                note.NoteContents = model.NoteContents;

                if (db.SaveChanges() > 0)
                {
                    return Redirect("Index");
                }
                else
                {
                    return View(note);
                }
            }
        }
    }
}
