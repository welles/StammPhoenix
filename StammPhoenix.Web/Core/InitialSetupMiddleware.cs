using StammPhoenix.Persistence;
using StammPhoenix.Persistence.Constants;

namespace StammPhoenix.Web.Core;

public class InitialSetupMiddleware : IMiddleware
{
    private IDatabaseContext DatabaseContext { get; }

    public InitialSetupMiddleware(IDatabaseContext databaseContext)
    {
        this.DatabaseContext = databaseContext;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Path != new PathString("/initialsetup")
            && !await this.DatabaseContext.GetSetting<bool>(SettingNames.InitialSetupComplete))
        {
            context.Response.Redirect("/initialsetup");
        }

        await next(context).ConfigureAwait(true);
    }
}