using AutoMapper;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// db context config
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// mapper configuration
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper(); // mapper instcance
builder.Services.AddSingleton(mapper);  // register it
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    // for dependency injection

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// auto migration 
ApplyMigration();
app.Run();


// apply automatically any pending migration without having to meed to update-database in PMC
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetService<ApplicationDBContext>();

        if(_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }
    }
}