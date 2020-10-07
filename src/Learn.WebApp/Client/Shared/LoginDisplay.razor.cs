using System.Threading.Tasks;

namespace Learn.WebApp.Client.Shared
{
    public partial class LoginDisplay
    {
        private async Task BeginSignOutAsync()
        {
            await SignOutManager.SetSignOutState().ConfigureAwait(true);

            Navigation.NavigateTo("authentication/logout");
        }
    }
}