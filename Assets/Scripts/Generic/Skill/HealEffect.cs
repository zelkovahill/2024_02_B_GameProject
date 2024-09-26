using UnityEngine;

public class HealEffect : IskillEffect
{
    public int HealAmount { get; private set; }

    public HealEffect(int healAmount)
    {
        HealAmount = healAmount;
    }

    public void Apply(ISkillTarget target)
    {
        if (target is PlayerTarget playerTarget)
        {
            playerTarget.Health += HealAmount;
            Debug.Log($"Player healed for {HealAmount}. Remaining health : {playerTarget.Health}");
        }
        else if (target is PlayerTarget enemyTarget)
        {
            enemyTarget.Health += HealAmount;
            Debug.Log($"Enemy healed for {HealAmount}. Remaining health : {enemyTarget.Health}");
        }
    }

}
