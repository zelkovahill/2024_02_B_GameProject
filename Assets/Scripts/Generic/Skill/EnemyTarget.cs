using UnityEngine;

public class EnemyTarget : MonoBehaviour, ISkillTarget
{
    public int Health { get; set; } = 50;

    public void ApplyEffect(IskillEffect effect)
    {
        effect.Apply(this);
    }
}
