using App.Api.Middlewares;

namespace App.Api
{
    internal static class RequestPipeLine
    {
        internal static void UseRequestPipeLine(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //Todo = need to change cors config
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            //app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseExceptionHandler("/error");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
