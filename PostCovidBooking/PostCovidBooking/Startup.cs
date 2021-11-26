using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PostCovidBooking.Core.Interfaces;
using PostCovidBooking.Core.Services;
using PostCovidBooking.Data;
using PostCovidBooking.Data.Interfaces;
using PostCovidBooking.Data.Repositories;
using System;
using AutoMapper;

namespace PostCovidBooking
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
            services.AddControllers();
            services.AddDbContext<BookingContext>(o => o.UseCosmos(Configuration["BookingDatabaseSettings:ConnectionString"],
                                                                   Configuration["BookingDatabaseSettings:Key"],
                                                                   databaseName: Configuration["BookingDatabaseSettings:DatabaseName"]));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IQueryableUnitOfWork, BookingContext>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Post Covid Booking API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Post Covid Booking {groupName}",
                    Version = groupName,
                    Description = "Post Covid Booking API",
                    Contact = new OpenApiContact
                    {
                        Name = "Cancun Hotel Resort",
                        Email = string.Empty,
                        Url = new Uri("https://dummy.com/"),
                    }
                });
            });
        }
    }
}
