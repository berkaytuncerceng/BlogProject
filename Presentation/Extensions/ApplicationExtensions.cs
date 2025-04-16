namespace Presentation.Extensions
{
    public static class ApplicationExtensions
    {
        public static void ConfigureMiddleware(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }

        public static void ConfigureEndpoints(this WebApplication app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=Index}/{id?}");
            app.MapRazorPages();
        }
    }
}