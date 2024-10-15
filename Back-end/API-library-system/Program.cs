using API_library_system.Data;
using API_library_system.Mapper;
using API_library_system.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options
 =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IReservationServices, ReservationServices>();
builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("Database"))
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
