using System.Collections.Generic;

public abstract class FSMState
{
    protected List<FSMTransition> transitions;

    public FSMState()
    {
        transitions = new List<FSMTransition>();
    }

    public abstract void Start();
    public abstract void Update();
    public abstract void End();

    public List<FSMTransition> Transitions
    {
        get => transitions;
    }
}
