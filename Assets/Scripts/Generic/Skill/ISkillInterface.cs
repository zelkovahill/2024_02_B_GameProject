public interface ISkillTarget
{
    void ApplyEffect(IskillEffect effect);

}

public interface IskillEffect
{
    void Apply(ISkillTarget target);
}


