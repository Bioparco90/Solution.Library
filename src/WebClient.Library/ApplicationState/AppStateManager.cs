namespace WebClient.Library.ApplicationState
{
    public class AppStateManager
    {
        private AppState? _state;
        public bool IsAuthenticated => _state is not null && _state.IsAuthenticated;

        public void CreateAppState()
        {
            _state = new()
            {
                IsAuthenticated = true,
            };
        }

        public AppState? GetAppState() => _state;
    }
}
