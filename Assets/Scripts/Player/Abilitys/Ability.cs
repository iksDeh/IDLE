using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/Ability")]
public class Ability : ScriptableObject
{
    public ParticleSystem particle;
    public Sprite icon;
    public string animationName;
    public int _minParticleDamage;
    public int _maxParticleDamage;
    public float scaleLevelParticleDamage;
    public int _requiredLevel;
    public int levelInterval;
    public int baseSkillPointsNeeded;
    public float scaleSkillPointMultipli;
    public int maxAbilityLevel;
    public string discription;
    public int cooldown;
    private int level;
    private int requiredSkillPoints;
    private int minParticleDamage;
    private int maxParticleDamage;
    private int requiredLevel;

    public virtual void Init()
    {
        minParticleDamage = _minParticleDamage;
        maxParticleDamage = _maxParticleDamage;
        level = 0;
        requiredLevel = _requiredLevel;

        for (int i = 0; i <= requiredLevel; i++)
        {
            if (i > 0)
                requiredSkillPoints += (int)Mathf.Round(requiredSkillPoints * scaleSkillPointMultipli);
            else
                requiredSkillPoints = baseSkillPointsNeeded;
        }

    }

    public virtual void Reset()
    {       
        minParticleDamage = _minParticleDamage;
        maxParticleDamage = _maxParticleDamage;
        requiredLevel = _requiredLevel;

        //Skill Points müssen noch übertragen wereden!!
        int returnSkillPoints = 0;
        for(int i = 0; i <= level; i++ )
        {
            requiredSkillPoints = (int)Mathf.Round(requiredSkillPoints / (100 + scaleLevelParticleDamage));
            returnSkillPoints += requiredSkillPoints;
        }
            
        level = 0;
    }

    public virtual void ChangeLevel(int lvl)
    {
        if(level < maxAbilityLevel)
        {
            level += lvl;
            requiredLevel += levelInterval;
            requiredSkillPoints += (int)Mathf.Round(requiredSkillPoints * scaleSkillPointMultipli);
            minParticleDamage += (int)Mathf.Round(minParticleDamage * scaleLevelParticleDamage);
            maxParticleDamage += (int)Mathf.Round(maxParticleDamage * scaleLevelParticleDamage);
        }
        else
        {
            Debug.Log("Error: Max Ability Level!");
        }

    }
    public float GetDamage()
    {
        return Mathf.Round((Random.Range(minParticleDamage, maxParticleDamage) * ((float)level * scaleLevelParticleDamage)));
    }
}
