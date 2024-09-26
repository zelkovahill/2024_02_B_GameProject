using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public PlayerTarget player;
    public EnemyTarget enemy;

    public List<EnemyTarget> enemyTargets;

    public Skill<ISkillTarget, DamageEffect> fireball;
    public Skill<PlayerTarget, HealEffect> heallSpell;
    public Skill<ISkillTarget, DamageEffect> multiTargetSkill;

    private void Start()
    {
        fireball = new Skill<ISkillTarget, DamageEffect>("Fireball", new DamageEffect(20));
        heallSpell = new Skill<PlayerTarget, HealEffect>("Heal", new HealEffect(30));
        multiTargetSkill = new Skill<ISkillTarget, DamageEffect>("AoE Attack", new DamageEffect(10));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireball.Use(enemy);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            heallSpell.Use(player);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foreach (var target in enemyTargets)
            {
                multiTargetSkill.Use(target);
            }
        }
    }
}
