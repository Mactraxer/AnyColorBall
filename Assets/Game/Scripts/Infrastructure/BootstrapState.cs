namespace AnyColorBall.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private const string GameSceneName = "Game";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.LoadScene(InitialSceneName, EnterLoadLevel);
        }

        public void Exit()
        {

        }

        private void RegisterServices()
        {
            
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(GameSceneName);
        }
    }
}