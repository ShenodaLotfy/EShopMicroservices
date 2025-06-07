
using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// ------------------ Add services to the DI container ------------------------
var assembly = typeof(Program).Assembly;


// scans the assembly to find all handlers
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
// scans the assembly to find all fluent validation validators
builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions(); // using light weight session for read/write operations

//  ------------------ App Created -------------------------------------------
var app = builder.Build();

// ------------------ Configure the HTTP request pipeline --------------------
app.MapCarter();

app.Run();
