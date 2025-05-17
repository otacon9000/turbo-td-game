using UnityEngine;

public interface IAttackHandler
{
    void Execute(Vector2 inputPosition);
    bool CanAttack { get; }
}