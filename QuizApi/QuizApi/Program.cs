using System.Reflection;
using Microsoft.OpenApi.Models;
using QuizApi.Context;
using QuizApi.Repositories;
using QuizApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Add header documentation in swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Quiz System API",
        Description = "The most effective API for handling student quizzes",
        Contact = new OpenApiContact
        {
            Name = "Group 6 - F1",
            Url = new Uri("https://github.com/CITUCCS/csit327-project-group-6")
        },
    });
    // Feed generated xml api docs to swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Configure our services
ConfigureServices(builder.Services);

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

app.Run();

void ConfigureServices(IServiceCollection services)
{
    // Trasient -> create new instance of DapperContext everytime.
    services.AddTransient<DapperContext>();
    // Configure Automapper
    services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //Services
    services.AddScoped<ITakerService, TakerService>();
    services.AddScoped<IQuizResultService, QuizResultService>();
    services.AddScoped<ITopicService, TopicService>();
    services.AddScoped<IQuizService, QuizService>();
    services.AddScoped<IQuestionService, QuestionService>();

    // Repos
    services.AddScoped<ITakerRepository, TakerRepository>();
    services.AddScoped<IQuizResultRepository, QuizResultRepository>();
    services.AddScoped<ITopicRepository, TopicRepository>();
    services.AddScoped<IQuizRepository, QuizRepository>();
    services.AddScoped<IQuestionRepository, QuestionRepository>();

}