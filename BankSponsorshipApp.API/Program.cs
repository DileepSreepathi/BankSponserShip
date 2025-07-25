var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<BankSponsorshipApp.Abstractions.ISponsorshipRepository, BankSponsorshipApp.Data.Repositories.InMemorySponsorshipRepository>();
builder.Services.AddScoped<BankSponsorshipApp.Core.Services.SponsorshipManager>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();




