using UnityEngine;

public class PointOfView : MonoBehaviour
{
    private Collider col;
    public Transform playerPos;
    public MonsterPattern monster;
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Spider : POV Trigger");
            monster.curPattern.ExitPattern();
            monster.curPattern = monster.patterns[(int)PatternNumber.Pattern.Combat];
            monster.curPattern.EnterPattern(Vector3.zero);
        }
    }
}
