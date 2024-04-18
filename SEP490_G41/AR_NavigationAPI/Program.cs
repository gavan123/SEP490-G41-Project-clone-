using BusinessObject.MappingProfile;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using DataAccess.IRepository.Repository;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Text.Json.Serialization;




static IEdmModel GetEdmModel()
{
    ODataModelBuilder builder = new ODataConventionModelBuilder();

    builder.EntitySet<Building>("building");
    builder.EntitySet<Facility>("facilities");
    builder.EntitySet<Mappoint>("mappoint");
    builder.EntitySet<Floor>("floor");
    builder.EntitySet<Map>("map");


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
    var serverVersion = new MySqlServerVersion(new Version(10, 6, 10)); 
    options.UseMySql(builder.Configuration.GetConnectionString("Project"), serverVersion,
        mysqlOptions => mysqlOptions.UseNetTopologySuite()); 

});
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCors(policy =>
{
    policy.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddScoped<BuildingDAO>();
builder.Services.AddScoped<FacilityDAO>();
builder.Services.AddScoped<MapDAO>();
builder.Services.AddScoped<MappointDAO>();
builder.Services.AddScoped<FloorDAO>();
<<<<<<< Updated upstream
builder.Services.AddScoped<MemberDAO>();
=======

builder.Services.AddScoped<MemberDAO>();

>>>>>>> Stashed changes

builder.Services.AddScoped<IBuildingRepository, BuildingRepository>();
builder.Services.AddScoped<IFacilityRepository, FacilityRepository>();
builder.Services.AddScoped<IMapRepository, MapRepository>();
builder.Services.AddScoped<IMapPointRepository, MapPointRepository>();
builder.Services.AddScoped<IFloorRepository, FloorRepository>();
<<<<<<< HEAD

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    });
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
=======
<<<<<<< Updated upstream
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
=======


builder.Services.AddScoped<IMemberRepository, MemberRepository>();

>>>>>>> Stashed changes
/*builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });*/
>>>>>>> minhpc

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
