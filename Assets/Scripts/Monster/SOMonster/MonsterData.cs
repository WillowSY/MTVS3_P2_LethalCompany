using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/MonsterData", order = 2)]
public class SOMonster : ScriptableObject
{
    [SerializeField] 
    private string _monsterName;      // 몬스터 이름
    public string monsterName
    {
        get { return _monsterName; }
    }
    
    [SerializeField] 
    private int _maxHp;              // 몬스터 데미지
    public int maxHp
    {
        get { return maxHp; }
    }
    
    [SerializeField]
    private int _curHp;              // 몬스터 데미지
    public int curHp
    {
        get { return curHp; }
        set { _curHp = value; }
    }
    
    [SerializeField] 
    private int _damage;              // 몬스터 데미지
    public int damage
    {
        get { return _damage; }
    }
    
    [SerializeField] 
    private int _detectionType;       // 플레이어 감지 방식     [0] : 시각    [1] : 청각
    public int detectionType
    {
        get { return _detectionType; }
    }
    
    [SerializeField] 
    private float _damageDelay;       // 몬스터 공격 딜레이
    public float damageDelay
    {
        get { return _damageDelay; }
    }
    
    [SerializeField] 
    private bool _isSwarm;            // 무리 습성 존재
    public bool isSwarm
    {
        get { return _isSwarm; }
    }
    
    [SerializeField] 
    private bool _isSwarmActive;            // 무리 공격 활성화 여부
    public bool isSwarmActive
    {
        get { return _isSwarmActive; }
        set { _isSwarmActive = value; }
    }
}
