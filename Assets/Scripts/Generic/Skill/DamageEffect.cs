using System.Runtime.InteropServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DamageEffect : IskillEffect
{
    public int Damage { get; private set; }

    public DamageEffect(int damage)
    {
        Damage = damage;
    }

    public void Apply(ISkillTarget target)
    {
        
        if (target is PlayerTarget playerTarget)
        {
            playerTarget.Health -= Damage;
            Debug.Log($"Player took {Damage} damage. Remaining health : {playerTarget.Health}");
        }
        else if (target is EnemyTarget enemyTarget)
        {
            enemyTarget.Health -= Damage;
            Debug.Log($"Enemy took {Damage} damage. Remaining health : {enemyTarget.Health}");
        }
    }
}
