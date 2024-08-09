using UnityEngine;
using UnityEngine.AI;

public class SpiderCombat : SpiderPattern
{
    private float speed = 2f;
    private float acc = 1000f;
    public BaseTracking baseTracking;
    private float timer;
    public SpiderCombat(BaseTracking baseTracking)
    {
        this.baseTracking = baseTracking;
    }
    public override void EnterPattern(Vector3 curPos)
    {
        SetActive(true);
        baseTracking.Tracking(speed, acc);
        
    }
    public override void DoPattern(Vector3 curPos)
    {
        Debug.Log("Spider: Combat");
        baseTracking.Tracking(speed, acc);
    }
    public override void ExitPattern()
    {
        SetActive(false);
    }
}