**Simple Code Finite State Machines for Unity3D**

**How To Use**
Declare and initialize the FSM with some states and variables. You'll need your own states based on the class FSMState.
```C#

private class ExampleClass : MonoBehaviour
{
	public const int STATE_1 = 1;
	public const int STATE_2 = 1;

	public const int VARIABLE_1 = 1;

	private FSM fsm;

	private void Start()
	{
		fsm = new FSM();

		fsm.AddVariable(VARIABLE_1, false);

		fsm.AddState(STATE_1, new StateOne(this));
		fsm.AddState(STATE_1, new StateTwo(this));
	}

	private void Update()
	{
		fsm.Update();
	}

	public FSM FSM
	{
		get => fsm;
	}
}
```

Now you can create your own states with conditions to run to one another.
```C#
private class StateOne : FSMState
{
	private ExampleClass exampleClass;

	public StateOne(ExampleClass exampleClass)
	{
		this.exampleClass = exampleClass;
		FSMTransition toStateTwo = new FSMTransition(ExampleClass.STATE_2);
	    toStateTwo.AddCondition(ExampleClass.VARIABLE_1, true);
	    transitions.Add(toStateTwo);
	}

	public override void Update()
	{
		// Do things here
		exampleClass.FSM.UpdateVariable(ExampleClass.VARIABLE_1, true);
	}
}

private class StateTwo : FSMState
{
	private ExampleClass exampleClass;

	public StateTwo(ExampleClass exampleClass)
	{
		this.exampleClass = exampleClass;
		FSMTransition toStateOne = new FSMTransition(ExampleClass.STATE_2);
	    toStateOne.AddCondition(ExampleClass.VARIABLE_1, false);
	    transitions.Add(toStateOne);
	}

	public override void Update()
	{
		// Do things here
		exampleClass.FSM.UpdateVariable(ExampleClass.VARIABLE_1, false);
	}
}
```