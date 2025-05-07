using AnyColorBall.Infrastructure;

public class Game
{
    public GameStateMachine StateMachine;

    public Game(LoadingUI loadingUI, IInputService inputService)
    {
        StateMachine = new GameStateMachine(new SceneLoader(), loadingUI, inputService, AllServices.Container);
    }
}