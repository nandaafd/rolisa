using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Rolisa.API.Service;
using Rolisa.DataModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetSection("Key").Value;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//service dependency injection
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<ArticleService>();
builder.Services.AddScoped<ConditionService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<InventoryCategoryService>();
builder.Services.AddScoped<InventoryService>();
builder.Services.AddScoped<ProductCategoryService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<VoucherService>();
builder.Services.AddScoped<LoginService>();

//jwt bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("nanda afdlolul basyar secret key json web token dotnet api learn"))
        };
    });


//sql connection
builder.Services.AddDbContext<RolisaContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
