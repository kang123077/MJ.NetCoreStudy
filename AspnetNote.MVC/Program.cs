var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// DI 의존성 주입 옛날에는 ASP.NET MVC 4, 5 + Unity(게임 ㄴㄴ)
// var user = new User();
// 다음과 같은 경우는 메소드 내에서의 선언이기 때문에 객체 관리가 쉽지 않다
// 때문에 의존성 주입을 통해서 생성자에 주입을 시켜주면 어떤 메서드에서도 동일한 객체를 사용 가능하다.
// 이제는 미들웨어로 처리 가능하다. Session, Identity, Web API 관련 기능
// Session 서비스에 등록
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1);//Session Timeout.
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// application 단위로 세션을 사용하겠다
app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
