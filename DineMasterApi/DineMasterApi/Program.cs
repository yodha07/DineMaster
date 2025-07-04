using DineMasterApi.Data;
using DineMasterApi.Mapping;
using DineMasterApi.Repo;
using DineMasterApi.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRolesRepo, RolesService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<IInventory, InventoryServices>();
builder.Services.AddScoped<IRecipeitem, RecipeitemServices>();
builder.Services.AddScoped<IExpense, ExpenseService>();

builder.Services.AddScoped<ITableRepo, TableService>();
builder.Services.AddScoped<IReservationRepo, ReservationService>();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddAutoMapper(typeof(MappingData));

builder.Services.AddDbContext<ApplicationDbContext>
    (
        options => options.UseSqlServer
        (
            builder.Configuration.GetConnectionString("con")
        )
    );

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSession(); 

app.MapControllers();

app.Run();
