using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AudioTextR.Core.Extensions;
using AudioTextR.Core.Abstractions.Models;
using AudioTextR.Core.Models;
using Telegram.Bot;

namespace AudioTextR.Sample.TelegramBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<ITelegramBotClient>(new TelegramBotClient(Configuration["TelegramApi:Token"]));

            var recognizeModel = new WitAiModel
            {
                BaseAddress = new Uri(Configuration["WitAi:BaseAddress"]),
                ServerToken = Configuration["WitAi:Token"]
            };

            services.AddAudioTextR(recognizeModel);
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            await serviceProvider.GetService<ITelegramBotClient>().SetWebhookAsync(Configuration["TelegramApi:WebHook"]);
            app.UseMvc();
        }
    }
}
