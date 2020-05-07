using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using Quartz.Impl.Calendar;

namespace Quartz.AspNetCore.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddQuartz();
            services.AddSimplifiedHostedService(null);

            services.AddSwaggerGen(options => { options.SwaggerDoc("v1", new OpenApiInfo {Title = "Quartz - V1", Version = "v1"}); });

            services.AddControllers();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseQuartz(scheduler =>
            {
                scheduler.AddCalendar(nameof(AnnualCalendar), new AnnualCalendar(), false, false);
                scheduler.AddCalendar(nameof(MonthlyCalendar), new MonthlyCalendar(), false, false);
                scheduler.AddCalendar(nameof(WeeklyCalendar), new WeeklyCalendar(), false, false);
                scheduler.AddCalendar(nameof(DailyCalendar), new DailyCalendar("00:00:00", "23:59:59"), false, false);
                scheduler.AddCalendar(nameof(HolidayCalendar), new HolidayCalendar(), false, false);
                scheduler.AddCalendar(nameof(CronCalendar), new CronCalendar("0 0/5 * * * ?"), false, false);
            });

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DocumentTitle = "Quartz Scheduler";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Quartz");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}