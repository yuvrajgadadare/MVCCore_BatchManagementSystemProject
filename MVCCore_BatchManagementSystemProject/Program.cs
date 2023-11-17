using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MVCCore_BatchManagementSystemProject.Models;
using MVCCore_BatchManagementSystemProject.Services.Implementations;
using MVCCore_BatchManagementSystemProject.Services.Interfaces;
using NuGet.Protocol.Core.Types;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews(); 
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<ITopicService,TopicService>();
builder.Services.AddTransient<ITopicContentService, TopicContentService>();
builder.Services.AddTransient<ITrainingCourseService, TrainingCourseService>();
builder.Services.AddTransient<IStudentService,StudentService>();
builder.Services.AddTransient<ITrainerService,TrainerService>();
builder.Services.AddTransient<IExtraService,ExtraService>();
builder.Services.AddTransient<IBatchService,BatchService>();
builder.Services.AddTransient<IAdminService,AdminService>();
builder.Services.AddDbContext<BatchManagementDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyCon"));
});
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
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
app.UseSession();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Dashboard}/{action=Index}"
    );
app.MapAreaControllerRoute(
    name: "Student",
    areaName: "Student",
    pattern: "Student/{controller=StudentDashboard}/{action=Index}"
    );
app.MapAreaControllerRoute(
    name: "Trainer",
    areaName: "Trainer",
    pattern: "Trainer/{controller=TrainerDashboard}/{action=Index}"
    );
app.Run();