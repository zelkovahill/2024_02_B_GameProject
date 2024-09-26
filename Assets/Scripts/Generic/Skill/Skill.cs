using UnityEngine;

public class Skill<TTarget, TEffect>
                    where TTarget : ISkillTarget
                    where TEffect : IskillEffect
{
    public string Name { get; private set; }
    public TEffect Effect { get; private set; }

    public Skill(string name, TEffect effect)
    {
        Name = name;
        Effect = effect;
    }

    public void Use(TTarget target)
    {
        Debug.Log($"Using Skill : {Name}");
        target.ApplyEffect(Effect);
    }

}
