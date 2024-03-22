using BusinessObject.DTO;
using BusinessObject.MappingProfile;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using DataAccess.IRepository.Repository;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Security.Policy;


static IEdmModel GetEdmModel()
{
    ODataModelBuilder builder = new ODataConventionModelBuilder();

    builder.EntitySet<Building>("building"); // EntitySet tương ứng với bảng Authors trong migration của bạn


    return builder.GetEdmModel();
}
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(option => option.Select()
      .Filter().Count().OrderBy().Expand().SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<finsContext>();
builder.Services.AddDbContext<finsContext>((serviceProvider, options) =>
{
    var serverVersion = new MySqlServerVersion(new Version(8, 0, 23)); // Thay thế bằng phiên bản MySQL Server bạn đang sử dụng
    options.UseMySql(builder.Configuration.GetConnectionString("Project"), serverVersion);
});
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddScoped<BuildingDAO>();

builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
