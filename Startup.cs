using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Handlers;
using HoraDaBelezaApi.Infra.Contexto;
using HoraDaBelezaApi.Infra.Seguranca;
using HoraDaBelezaApi.Infra.Servicos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace HoraDaBelezaApi
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
             );

            services.AddDbContext<IdentityContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddDbContext<ServerContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("ServerConnection"));
            });

            services.AddScoped<UsuarioService>();

            services.AddScoped<VendaService>();

            services.AddScoped<SalaoService>();

            services.AddScoped<IAuthService, AuthService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<AccessManager>();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfiguracao();
            new ConfigureFromConfigurationOptions<TokenConfiguracao>(
                Configuration.GetSection("TokenConfiguration"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddJwtSecurity(
                signingConfigurations, tokenConfigurations);

            services.AddCors();
            services.AddControllers();

            services.AddCors(opts =>
            {
                opts.AddPolicy("Production", opt => opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IdentityContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            new IdentityInitializer(context, userManager, roleManager)
            .Initialize();

            app.UseHttpsRedirection();

            app.UseCors("Production");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
