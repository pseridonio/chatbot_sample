using Birra.API.Data;
using Birra.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Birra.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"databasesettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"databasesettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            this._configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Data.BirraDataContext>(options =>
            {
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSingleton<IConfiguration>(_configuration);
            services.AddSingleton<SystemParametersService, SystemParametersService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();

            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Birra.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, BirraDataContext birraDataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Birra.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Accepting any origin just for the test
            app.UseCors(police => police.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            birraDataContext.Database.Migrate();

            this.InsertInitialData(birraDataContext);
        }

        /// <summary>
        /// Méthod to includ the initial data in the tables.
        /// Not recomended this kind of method, just for the test.
        /// </summary>
        /// <param name="dataContext"></param>
        private void InsertInitialData(BirraDataContext dataContext)
        {
            if (dataContext.Questions.Count() == 0 && dataContext.Answers.Count() == 0)
            {
                using (IDbContextTransaction transaction = dataContext.Database.BeginTransaction())
                {
                    dataContext.Questions.Add(new Data.Entities.Question()
                    {
                        QuestionID = 1,
                        QuestionText = "Você tem 18 anos ou mais?",
                        QuestionHint = "Sim ou Não"
                    });

                    dataContext.Questions.Add(new Data.Entities.Question()
                    {
                        QuestionID = 2,
                        QuestionText= "Qual tipo de cerveja prefere?",
                        QuestionHint = "Amarga ou Leve"
                    });

                    dataContext.Questions.Add(new Data.Entities.Question()
                    {
                        QuestionID = 3,
                        QuestionText = "Prefere cerveja clara ou escura?",
                        QuestionHint = "Clara ou Escura"
                    });

                    dataContext.Questions.Add(new Data.Entities.Question()
                    {
                        QuestionID = 4,
                        QuestionText = "Como gosta do sabor do malte?",
                        QuestionHint = "Suave ou Marcante"
                    });

                    dataContext.Questions.Add(new Data.Entities.Question()
                    {
                        QuestionID = 5,
                        QuestionText = "Gostaria de uma nova sugestão?",
                        QuestionHint = "Sim ou Não"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 1,
                        AnswerText = "Sim"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 2,
                        AnswerText = "Não"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 3,
                        AnswerText = "Amarga"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 4, 
                        AnswerText = "Leve"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 5,
                        AnswerText = "Clara"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 6,
                        AnswerText = "Escura"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 7,
                        AnswerText = "Suave"
                    });

                    dataContext.Answers.Add(new Data.Entities.Answer()
                    {
                        AnswerID = 8,
                        AnswerText = "Marcante"
                    });

                    dataContext.Messages.Add(new Data.Entities.Message()
                    {
                        MessageID = 1,
                        MessageText = "Seja bem vind@ à Birra Cervejaria Online, eu serei seu ajudante."
                    });

                    dataContext.Messages.Add(new Data.Entities.Message()
                    {
                        MessageID = 2,
                        MessageText = "Desculpe, você não tem permissão para acessar este site."
                    });

                    dataContext.Messages.Add(new Data.Entities.Message()
                    {
                        MessageID = 3,
                        MessageText = "Recomendo a você uma cerveja IPA"
                    });

                    dataContext.Messages.Add(new Data.Entities.Message()
                    {
                        MessageID = 4,
                        MessageText = "Recomendo a você uma cerveja Stout"
                    });

                    dataContext.Messages.Add(new Data.Entities.Message()
                    {
                        MessageID = 5,
                        MessageText = "Recomendo a você uma cerveja Pilsen"
                    });

                    dataContext.Messages.Add(new Data.Entities.Message()
                    {
                        MessageID = 6,
                        MessageText = "Recomendo a você uma cerveja Premium Lager"
                    });

                    dataContext.Messages.Add(new Data.Entities.Message()
                    {
                        MessageID = 7,
                        MessageText = "Obrigado por comprar com a Birra. Volte Sempre."
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 1,
                        AnswerID = 1,
                        NextQuestionID = 2,
                        MessageID = null
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 1,
                        AnswerID = 2,
                        NextQuestionID = null,
                        MessageID = 2
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 2,
                        AnswerID = 3,
                        NextQuestionID = 3,
                        MessageID = null
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 2,
                        AnswerID = 4,
                        NextQuestionID = 4,
                        MessageID = null
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 3,
                        AnswerID = 5,
                        NextQuestionID = 5,
                        MessageID = 3
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 3,
                        AnswerID = 6,
                        NextQuestionID = 5,
                        MessageID = 4
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 4,
                        AnswerID = 7,
                        NextQuestionID = 5,
                        MessageID = 5
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 4,
                        AnswerID = 8,
                        NextQuestionID = 5,
                        MessageID = 6
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 5,
                        AnswerID = 1,
                        NextQuestionID = 2,
                        MessageID = null
                    });

                    dataContext.RelationQuestionAnswers.Add(new Data.Entities.RelationQuestionAnswer()
                    {
                        QuestionID = 5,
                        AnswerID = 2,
                        NextQuestionID = null,
                        MessageID = 7
                    });

                    dataContext.SaveChanges();
                    transaction.Commit();
                }
            }

    
        }
    }


}
