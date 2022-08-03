namespace Farmasi_Project_API.Infrastructure.Extensions
{
  public static class ApplicationBuilderExtensions
  {
    public static void Configure(this WebApplication app)
    {
      if (app.Environment.IsDevelopment())
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();

      app.Run();
    }
  }
}
