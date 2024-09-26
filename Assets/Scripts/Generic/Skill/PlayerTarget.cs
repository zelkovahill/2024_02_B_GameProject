using UnityEngine;

public class PlayerTarget : MonoBehaviour, ISkillTarget
{
    public int Health { get; set; } = 100;

    public void ApplyEffect(IskillEffect effect)
    {
        effect.Apply(this);
    }
}
