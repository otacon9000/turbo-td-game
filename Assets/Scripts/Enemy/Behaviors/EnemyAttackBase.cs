using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    public abstract bool CanAttack { get; }
    public abstract void ExecuteAttack();
    public abstract void SetData(EnemyData data);
}
