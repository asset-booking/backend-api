using Asset.Booking.Application;
using Asset.Booking.Domain.AssetSchedule.Abstractions;
using Asset.Booking.Domain.Client.Abstractions;
using Asset.Booking.Infrastructure;
using Asset.Booking.SharedKernel.Abstractions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAssetBookingApplication();
builder.Services.AddScoped<IUnitOfWork, fakeunitofwork>();
builder.Services.AddScoped<IClientRepository, fakeclientrepo>();
builder.Services.AddScoped<IAssetScheduleRepository, assetschedulerepo>();

var localhostOrigins = "_localhostCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(localhostOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:9000");
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(localhostOrigins);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
