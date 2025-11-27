using Cysharp.Threading.Tasks;

public class StateController
{
    protected StateMachine StateMachine;
    
    public void SetMachine(StateMachine stateMachine)
    {
        StateMachine = stateMachine;
    }
    
    public virtual async UniTask EnterState()
    {
        
    }

    public virtual async UniTask ExitState()
    {
        
    }

    protected async UniTask GoToState<T>() where T : StateController => await StateMachine.GoToState<T>();
    protected async UniTask GoToPreviousState() => await StateMachine.GoToPreviousState();
}
