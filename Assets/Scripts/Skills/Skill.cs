
using UnityEngine;

[CreateAssetMenu(fileName ="New Skill",menuName ="Skills/Skill")]
public class Skill : ScriptableObject
{
    public string name;
    public string descrition;
    public Sprite icon;
    public Level requiredLevel;
    public int requiredSkillPoints;


}
