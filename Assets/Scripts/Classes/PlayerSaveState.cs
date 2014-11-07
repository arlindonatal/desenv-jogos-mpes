using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerSaveState {

    [SerializeField]
    [NonSerialized]
    public string Name;
    public int Age;
    public string Faction;
    public string Occupation;
    public int Level;
    public int Health;
    public int Strength;
    public int Magic;
    public int Defense;
    public int Speed;
    public int Damage;
    public int Armor;
    public int NoOfAttacks;
    public string Weapon;
    public Vector2 Position;
    public List<string> Inventory;
}

public static class PlayerSaveStateExtensions
{
    public static PlayerSaveState GetPlayerSaveState(this Player input)
    {
        PlayerSaveState newSaveState = new PlayerSaveState();
        newSaveState.Age = input.Age;
        newSaveState.Armor = input.Armor;
        newSaveState.Damage = input.Damage;
        newSaveState.Defense = input.Defense;
        newSaveState.Faction = input.Faction;
        newSaveState.Health = input.Health;
        newSaveState.Level = input.Level;
        newSaveState.Magic = input.Magic;
        newSaveState.Name = input.Name;
        newSaveState.NoOfAttacks = input.NoOfAttacks;
        newSaveState.Occupation = input.Occupation;
        newSaveState.Position = input.Position;
        newSaveState.Speed = input.Speed;
        newSaveState.Strength = input.Strength;
        newSaveState.Weapon = input.Weapon;

        newSaveState.Inventory = new List<string>();
        foreach (var item in input.Inventory)
        {
            newSaveState.Inventory.Add(item.name);
        }

        return newSaveState;
    }

    public static Player LoadPlayerSaveState(this PlayerSaveState input, Player player)
    {
        player.Age = input.Age;
        player.Armor = input.Armor;
        player.Damage = input.Damage;
        player.Defense = input.Defense;
        player.Faction = input.Faction;
        player.Health = input.Health;
        player.Level = input.Level;
        player.Magic = input.Magic;
        player.Name = input.Name;
        player.NoOfAttacks = input.NoOfAttacks;
        player.Occupation = input.Occupation;
        player.Position = input.Position;
        player.Speed = input.Speed;
        player.Strength = input.Strength;
        player.Weapon = input.Weapon;
        player.Inventory = new List<InventoryItem>();
        foreach (var item in input.Inventory)
        {
            player.Inventory.Add((InventoryItem)Resources.Load("Inventory Items/" + item));
        }
        return player;
    }
}