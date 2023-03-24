using System.Collections.Generic;
public enum BehaviorState
{
    Success,
    Failure,
    Running
}
public abstract class BehaviorNode
{
    protected List<BehaviorNode> childNodes = new List<BehaviorNode>();
    protected bool reset = false;

    public void AddChild(BehaviorNode childNode)
    {
        childNodes.Add(childNode);
    }

    public void RemoveChild(BehaviorNode childNode)
    {
        childNodes.Remove(childNode);
    }

    public abstract BehaviorState Evaluate();

    protected virtual BehaviorState EvaluateChildNodes()
    {
        foreach (BehaviorNode childNode in childNodes)
        {
            BehaviorState childState = childNode.Evaluate();
            if (childState != BehaviorState.Success)
            {
                return childState;
            }
        }

        return BehaviorState.Success;
    }

    public virtual void Reset()
    {
        if (reset)
        {
            reset = false;
            return;
        }

        foreach (BehaviorNode childNode in childNodes)
        {
            childNode.Reset();
        }

        reset = true;


    }
}
