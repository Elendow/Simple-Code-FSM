**Simple Code Finite State Machines for Unity3D**

**How To Use**

Declare and initialize the FSM with some states and variables. You'll need your own states based on the class FSMState.
```C#

private class ExampleClass : MonoBehaviour
{
	public const int STATE_1 = 1;
	public const int STATE_2 = 2;

	public const int VARIABLE_1 = 1;

	private FSM fsm;

	private void Start()
	{
		// Initialize your FSM

		fsm = new FSM();

		fsm.AddVariable(VARIABLE_1, false);

		fsm.AddState(STATE_1, new StateOne(this));
		fsm.AddState(STATE_1, new StateTwo(this));
	}

	private void Update()
	{
		// Update the FSM
		fsm.Update();
	}
}
```

Now you can create your own states with conditions to run to one another.
```C#
private class StateOne : FSMState
{
	private ExampleClass exampleClass;
	private float timer;

	public StateOne(ExampleClass exampleClass)
	{
		this.exampleClass = exampleClass;
		AddTransition(ExampleClass.STATE_2, false, new FSMCondition(ExampleClass.VARIABLE_1, true));
	}

	public override void Start()
	{
		timer = 0;
	}

	public override void Update()
	{
		// Do things here and update your variables to trigger the transitions
		timer += Time.deltaTime;
		if (timer > 5)
		{
			fsm.UpdateVariable(ExampleClass.VARIABLE_1, true);
		}
	}
}

private class StateTwo : FSMState
{
	private ExampleClass exampleClass;
	private float timer;

	public StateTwo(ExampleClass exampleClass)
	{
		this.exampleClass = exampleClass;
		AddTransition(ExampleClass.STATE_1, false, new FSMCondition(ExampleClass.VARIABLE_1, false));
	}

	public override void Start()
	{
		timer = 0;
	}

	public override void Update()
	{
		// Do things here and update your variables to trigger the transitions
		timer += Time.deltaTime;
		if (timer > 5)
		{
			fsm.UpdateVariable(ExampleClass.VARIABLE_1, false);
		}
	}
}
```
