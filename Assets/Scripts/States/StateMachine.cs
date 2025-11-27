using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class StateMachine
{
    private readonly Dictionary<Type, StateController> _states = new ();

    private StateController _currentStateController;
    private StateController _previousStateController;
    
    public StateMachine(IEnumerable<StateController> states)
    {
        foreach (StateController state in states)
        {
            _states.Add(state.GetType(), state);
            state.SetMachine(this);
        }
    }

    public async UniTask GoToState<T>() where T : StateController => await ChangeState(typeof(T));

    public async UniTask GoToPreviousState()
    {
        if (_previousStateController == null)
            throw new Exception($"Previous state does not exist.");

        await ChangeState(_previousStateController.GetType());
    }

    private async UniTask ChangeState(Type type)
    {
        if (_currentStateController != null)
        {
            _previousStateController = _currentStateController;
            await _currentStateController.ExitState();
        }

        var state = _states[type];
        _currentStateController = state;
        await state.EnterState();
    }
}
