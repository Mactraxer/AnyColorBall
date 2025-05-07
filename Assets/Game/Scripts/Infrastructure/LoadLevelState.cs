using AnyColorBall.Services.Data;
using System;
using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public class LoadLevelState : IPayloadableState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingUI _loadingUI;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingUI loadingUI, IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingUI = loadingUI;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void Enter(string payload)
        {
            _loadingUI.Show();
            _sceneLoader.LoadScene(payload, OnLoaded);
        }

        public void Exit()
        {
            _loadingUI.Hide();
        }

        private void OnLoaded()
        {
            CreateGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach (IReadablePlayerProgress reader in _gameFactory.ProgressReaders)
            {
                reader.ReadProgress(_progressService.Progress);
            }
        }

        private void CreateGameWorld()
        {
            _gameFactory.CreateLevel();

            var playerInitialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            var player = _gameFactory.CreatePlayer(playerInitialPoint.transform.position);

            CameraFollow(player);
        }

        private static void CameraFollow(GameObject player)
        {
            Camera.main.GetComponent<CameraFollower>().Follow(player);
        }
    }
}