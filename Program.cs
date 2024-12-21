using Serilog;
using Serilog.Formatting.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

using ERP.Services.Interfaces;

using FirebaseAdmin.Messaging;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication;
using ERP.Models;
namespace ERP
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var env = builder.Environment;
			// Add services to the container.
			builder.Services.AddControllersWithViews();
			//Add signalR
			builder.Services.AddSignalR(options =>
			{
				options.EnableDetailedErrors = true;
			});

			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory,
				   serviceAccountFileName = "push-notification-firebase-adminsdk-edserp.json",
				   serviceAccountFilePath = Path.Combine(baseDirectory, "Firebase", "ServiceAccount", serviceAccountFileName);

			FirebaseApp.Create(new AppOptions()
			{
				Credential = GoogleCredential.FromFile(serviceAccountFilePath)
			});

			builder.Services.AddSingleton(FirebaseMessaging.DefaultInstance);


			builder.Services.AddScoped<NotificationHub>();

            builder.Services.AddHttpContextAccessor();



			
            // Add authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.AccessDeniedPath = "/Error/403";
					options.LoginPath = "/Auth/Login";
					options.ExpireTimeSpan = TimeSpan.FromHours(48);

					options.Events = new CookieAuthenticationEvents
					{
						OnValidatePrincipal = async context =>
						{
							if (context.Principal == null || !context.Principal.Identity.IsAuthenticated)
							{
								context.RejectPrincipal();
								await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
								context.HttpContext.Response.Redirect("/Auth/Login");
							}
						}
					};
				});

			builder.Services.AddResponseCompression(options =>
			{
				options.EnableForHttps = true;
				options.Providers.Add<BrotliCompressionProvider>();
				options.Providers.Add<GzipCompressionProvider>();
				options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json", "image/svg+xml" }); // Include more MIME types
			});

			builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Fastest; // Fine-tune compression levels
			});

			builder.Services.Configure<GzipCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Fastest; // Fine-tune compression levels
			});


			builder.Services.AddAuthorization();

			builder.Services.AddLogging();
			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();
			
			if (!env.IsDevelopment())
			{
				builder.Logging.ClearProviders();
				Log.Logger = new LoggerConfiguration().WriteTo.Console()
				   .MinimumLevel.Warning()
				   .WriteTo.File(
					path: $"Logs/log-{DateTime.Now:yyyy-MM-dd}.txt",
					formatter: new JsonFormatter(),
					rollingInterval: RollingInterval.Day,
					rollOnFileSizeLimit: true,
					buffered: true,// Buffering log events in memory before writing to disk
					flushToDiskInterval: TimeSpan.FromSeconds(1),// Flush buffer to disk every second
					shared: false,
					encoding: System.Text.Encoding.UTF8
				   )
				   .CreateLogger();
				builder.Host.UseSerilog();
			}

			builder.Services.AddDbContext<DBContext>(options =>
			{
				// Get the connection string based on the environment
				string? connectionString = env.IsDevelopment() ?
					builder.Configuration.GetConnectionString("DefaultConnection") :
					builder.Configuration.GetConnectionString("ProductionConnection");

                // Configure the MySQL provider with the connection string and enable retry on failure
				//for mysql database
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mysqlOptions =>
                {
                    //mysqlOptions.EnableRetryOnFailure();
                    mysqlOptions.EnableRetryOnFailure(
                         maxRetryCount: 5,        // Maximum number of retry attempts
                         maxRetryDelay: TimeSpan.FromSeconds(30),  // Maximum delay between retries
                         errorNumbersToAdd: null  // Optional: List of specific error codes to retry on
                     );
                });

				//for sql database
                //options.UseSqlServer(connectionString, sqlOptions =>
                //{
                //    sqlOptions.EnableRetryOnFailure(
                //        maxRetryCount: 5,       // Number of retries
                //        maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
                //        errorNumbersToAdd: null); // Optional: Add specific error numbers to retry on
                //});
            });


            using (var scope = builder.Services.BuildServiceProvider().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();

                // Ensure the database is created or migrate to the latest version
                if (!dbContext.Database.CanConnect())
                {
                    dbContext.Database.EnsureCreated(); // Creates the database and tables if they don't exist
                }
                else
                {
                    dbContext.Database.Migrate(); // Applies any pending migrations
                }
            }


            var app = builder.Build();

			// Configure the HTTP request pipeline.
			app.UseStatusCodePagesWithReExecute("/Error/{0}");
			if (!app.Environment.IsDevelopment())
			{
				app.UseStatusCodePagesWithReExecute("/Error/{0}");
				app.UseResponseCompression();

				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				//app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseAntiforgery();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Auth}/{action=Login}");

			app.MapHub<NotificationHub>("/Notifications");
			app.Run();
		}
	}
}
