using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    private int currentStateIndex;
    private FSMState currentState;

    private Dictionary<int, FSMState> states;
    private Dictionary<int, bool> variables;

    public FSM()
    {
        states = new Dictionary<int, FSMState>();
        variables = new Dictionary<int, bool>();
    }

    public void Start(int firstState)
    {
        if (states.TryGetValue(firstState, out currentState))
        {
            currentStateIndex = firstState;
            currentState.Start();
        }
        else
        {
            Debug.LogWarning("State not found");
        }
    }

    public void ForceState(int state)
    {
        if (states.TryGetValue(state, out currentState))
        {
            currentStateIndex = state;
            currentState.Start();
        }
        else
        {
            Debug.LogWarning("State not found");
        }
    }

    public void UpdateVariable(int variableID, bool value)
    {
        if (variables.ContainsKey(variableID))
        {
            variables[variableID] = value;
        }
        else
        {
            Debug.LogWarning("Variable not found");
        }
    }

    public void AddVariable(int variableID, bool value)
    {
        if (!variables.ContainsKey(variableID))
        {
            variables.Add(variableID, value);
        }
        else
        {
            Debug.LogWarning("Variable already added");
        }
    }

    public bool CheckVariable(int variableID)
    {
        if (variables.TryGetValue(variableID, out bool value))
        {
            return value;
        }
        else
        {
            Debug.LogWarning("Variable not found");
            return false;
        }
    }

    public void AddState(int stateID, FSMState state)
    {
        if(!states.ContainsKey(stateID))
        {
            states.Add(stateID, state);
        }
        else
        {
            Debug.LogWarning("State already added");
        }
    }

    public void Update()
    {
        if (currentState == null)
            return;

        currentState.Update();
        CheckConditions();
    }

    private void CheckConditions()
    {
        int conditionsFullfiled;
        for (int i = 0; i < currentState.Transitions.Count; i++)
        {
            conditionsFullfiled = 0;

            foreach (KeyValuePair<int, bool> conditions in currentState.Transitions[i].Conditions)
            {
                if (currentState.Transitions[i].CheckCondition(conditions.Key, variables[conditions.Key]))
                {
                    conditionsFullfiled++;
                }
            }

            if (conditionsFullfiled == currentState.Transitions[i].ConditionsRequired)
            {
                currentState.End();
                currentStateIndex = currentState.Transitions[i].To;
                currentState = states[currentState.Transitions[i].To];
                currentState.Start();
                return;
            }
        }
    }

    public FSMState GetState(int state)
    {
        if(states.TryGetValue(state, out FSMState result))
        {
            return result;
        }
        else
        {
            Debug.LogWarning("State not found");
            return null;
        }
    }

    public int CurrentStateIndex
    {
        get { return currentStateIndex; }
    }
}
