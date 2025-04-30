using AnyColorBall.Infrastructure;

public class Game
{
    public GameStateMachine StateMachine;
    public static IInputService InputService;

    public Game(LoadingUI loadingUI, IInputService inputService)
    {
        StateMachine = new GameStateMachine(new SceneLoader(), loadingUI);
        InputService = inputService;
    }
}