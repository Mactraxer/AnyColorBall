using UnityEngine;

namespace AnyColorBall.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingUI _loadingUI;
        [SerializeField] private KeyboardInputService _keyboardInputService;

        private Game _game;

        private void Awake()
        {
            _game = new Game(_loadingUI, _keyboardInputService);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}