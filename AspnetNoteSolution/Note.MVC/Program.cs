using Note.BLL;
using Note.DAL;
using Note.IDAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// MVC에서 의존성 주입이 가능한 3가지 방법이 있다.
// 1. .AddSingleton<T>();
// 생성자가 여러 차례 호출되더라도 객체는 하나이며, 최초 생성 이후에 호출 된 생성자는 사전에 생성 된 생성자를 호출한다.
// 전역 객체 Static으로서, 메모리에 한 번 생성되면 프로그램이 종료 될 때까지 존재한다고 간주 할 수 있는 객체를 주입.
// 2. .AddScoped<T>();
// 프로그램(웹사이트) 1번의 요청이 있을 때 메모리 상에 유지 되는 객체 주입
// 3. .AddTransient<T>();
// 프로그램(웹사이트)가 시작되어 각 요청마다 새롭게 생성되는 객체 주입
// 특별한 경우가 아니고서는, 가장 가볍고 Stateless 하다고 써 있다.

builder.Services.AddMvc();
// 이를 통해 IOC 컨테이너를 제공한다
builder.Services.AddTransient<UserBll>();
builder.Services.AddTransient<IUserDal, UserDal>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
