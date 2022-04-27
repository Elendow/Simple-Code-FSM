using System.Collections.Generic;

public abstract class FSMState
{
    protected FSM fsm;
    protected List<FSMTransition> transitions;

    public FSMState()
    {
        transitions = new List<FSMTransition>();
    }

    public abstract void Start();
    public abstract void Update();
    public abstract void End();

    public void AddTransition(int toState, bool allTrue, params FSMCondition[] conditions)
    {
        FSMTransition transition = new FSMTransition(toState, allTrue);

        for (int i  = 0; i < conditions.Length; i++)
        {
            transition.AddCondition(conditions[i].variable, conditions[i].value);
        }

        transitions.Add(transition);
    }

    public List<FSMTransition> Transitions
    {
        get => transitions;
    }

    public FSM FSM
    {
        set => fsm = value;
    }
}
