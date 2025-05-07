using AnyColorBall.Services.Data;
using System;

namespace AnyColorBall.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, IInputService inputService, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            RegisterServices(inputService);
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(InitialSceneName, EnterLoadProgress);
        }

        public void Exit()
        {

        }

        private void RegisterServices(IInputService inputService)
        {
            _services.RegisterSingle<IInputService>(inputService);
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IGameFactory>(), _services.Single<IPersistentProgressService>()));
        }

        private void EnterLoadProgress()
        {
            _stateMachine.Enter<LoadProgressState>();
        }
    }
}