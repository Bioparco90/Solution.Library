using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WebClient.Library.ApplicationState
{
    public class AppStateManager
    {
        private AppState? _state;
        private readonly ProtectedSessionStorage _storage;

        public AppStateManager(ProtectedSessionStorage storage)
        {
            _storage = storage;
        }

        public async Task CreateAppState()
        {
            _state = new()
            {
                IsAuthenticated = true,
            };

            await _storage.SetAsync("appState", _state);
        }

        public async Task<AppState?> GetAppState()
        {
            _state ??= (await _storage.GetAsync<AppState>("appState")).Value;
            return _state;
        }
    }
}
