using System.Collections.Generic;
using UnityEngine;

public class FSMTransition
{
    private bool allTrue;
    private int to;
    private Dictionary<int, bool> conditions;

    public FSMTransition(int to, bool allTrue)
    {
        this.to = to;
        this.allTrue = allTrue;
        conditions = new Dictionary<int, bool>();
    }

    public bool CheckCondition(int variable, bool value)
    {
        if (conditions.TryGetValue(variable, out bool condition))
        {
            return value == condition;
        }

        return false;
    }

    public void AddCondition(int variable, bool value)
    {
        if (!conditions.ContainsKey(variable))
        {
            conditions.Add(variable, value);
        }
        else
        {
            Debug.LogWarning("Condition already added");
        }
    }

    public int To
    {
        get => to;
    }

    public Dictionary<int, bool> Conditions
    {
        get => conditions;
    }

    public int ConditionsRequired
    {
        get
        {
            if (!allTrue)
            {
                return 1;
            }
            else
            {
                return conditions.Count;
            }
        }
    }
}
