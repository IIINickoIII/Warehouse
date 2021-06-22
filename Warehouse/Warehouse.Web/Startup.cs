using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Warehouse.Bll.Interfaces;
using Warehouse.Bll.Mapper;
using Warehouse.Bll.Services;
using Warehouse.Dal.Contexts;
using Warehouse.Dal.Interfaces;
using Warehouse.Dal.Repositories;
using Warehouse.Web.Mapper;

namespace Warehouse.Web
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
            services.AddDbContext<WarehouseContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("WarehouseConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<WarehouseContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            //Dal
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Bll
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IPdfCreatorService, PdfCreatorService>();
            services.AddScoped<IPaymentService, PaymentService>();

            var mappingConfig = new MapperConfiguration(mappingConfig =>
            {
                mappingConfig.AddProfile<MapperProfileBll>();
                mappingConfig.AddProfile<MapperProfileWeb>();
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
