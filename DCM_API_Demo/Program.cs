var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.Authority = "http://localhost:5240";
        opt.Audience = "dcmapi";
        opt.MapInboundClaims = true;
    });


builder.Services.AddAuthorization(authOpt =>
{
    authOpt.AddPolicy("IsVicDoctor", policyBuilder =>
    {
        policyBuilder.RequireAuthenticatedUser();
        policyBuilder.RequireClaim("state", "vic");
        policyBuilder.RequireClaim("position", "doctor");
    });
});

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
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers()
//         .RequireAuthorization("dcmapi");
// });

app.MapControllers();

app.Run();