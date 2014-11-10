using UnityEngine;
public class Attributes : ScriptableObject
{
    public string Name;
    [Range(10, 100)]
    public int Age;
    [PopUp("Imperial", "Independant", "Evil")]
    public string Faction;
    [PopUp("Mayor", "Wizard", "Layabout")]
    public string Occupation;
    [Range(1, 10)]
    public int Level;
    [Range(1, 100)]
    public int Health;
    [Range(1, 50)]
    public int Strength;
    [Range(1, 50)]
    public int Magic;
    [Range(1, 50)]
    public int Defense;
    [Range(1, 50)]
    public int Speed;
    [Range(1, 50)]
    public int Damage;
    [Range(1, 50)]
    public int Armor;
    [Range(1, 5)]
    public int NoOfAttacks = 1;
    public string Weapon;
    public Vector2 Position;

    public void TakeDamage(int Amount) { Health -= (Amount - Armor); }
	public void Attack(Attributes Attributes) { Attributes.TakeDamage(Strength); }
}
