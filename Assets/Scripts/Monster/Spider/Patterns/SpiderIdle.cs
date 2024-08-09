using UnityEngine;
public class SpiderIdle : SpiderPattern
{
    public override void EnterPattern(Vector3 curPos)
    {
    }
    public override void DoPattern(Vector3 curPos)
    {
        Debug.Log("Spider : Idle");
    }
    public override void ExitPattern()
    {
        SetActive(false);
    }
}
