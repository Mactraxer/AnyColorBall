using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public class LoadLevelState : IPayloadableState<string>
    {
        private const string LevelDemoPath = "Levels/LevelDemo";
        private const string PlayerPath = "Player/Player";
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingUI _loadingUI;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingUI loadingUI)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingUI = loadingUI;
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
            Instantiate(LevelDemoPath);

            var playerInitialPoint = GameObject.FindGameObjectWithTag(InitialPointTag);
            var player = Instantiate(PlayerPath, playerInitialPoint.transform.position);
            CameraFollow(player);

            _stateMachine.Enter<GameLoopState>();
        }

        private GameObject Instantiate(string path, Vector3 position)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, position, Quaternion.identity);
        }

        private static GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        private static void CameraFollow(GameObject player)
        {
            Camera.main.GetComponent<CameraFollower>().Follow(player);
        }
    }
}