

using UnityEngine;
using UnityEngine.AI;

public class SpiderPattern
{
    public bool activation;

    public virtual void EnterPattern(Vector3 curPos)
    {
        
    }

    public virtual void DoPattern(Vector3 curPos)
    {
        
    }

    public virtual void ExitPattern()
    {
        
    }
    public bool IsActive()
    {
        if (activation)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetActive(bool ac)
    {
        activation = ac;
    }
    
}
