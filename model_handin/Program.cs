using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using model_handin.Data;
using model_handin.Hubs;
using model_handin.Interfaces;
using model_handin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ModelDb>(options => options.UseInMemoryDatabase("modeldb"));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ModelDb>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddTransient<IModelService, ModelService>();
builder.Services.AddTransient<IJobService, JobService>();

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

app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ExpenseNotification>("/expensenotification");

app.Run();
