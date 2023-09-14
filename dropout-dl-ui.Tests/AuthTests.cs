using dropout_dl.Common.Logic;

namespace dropout_dl_ui.Tests
{
    public class AuthTests
    {
        [Fact]
        public async Task TestLoginWithCredentials()
        {
            await AuthLogic.LoginWithCredentials("BradMJustice@gmail.com", "Fender04Dropout!");
        }
    }
}