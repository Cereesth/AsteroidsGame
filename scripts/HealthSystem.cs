using Godot;
using System;

public partial class HealthSystem : Node
{
    [Signal] public delegate void OnHealthChangedEventHandler(int health);
    [Signal] public delegate void OnDeathEventHandler();

    [Export] private int MaxHealth { get; set; }
    public int Health { get; private set; }

    public override void _Ready()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        Health -= damageAmount;
        
        if (Health < 0)
        {
            Health = 0;
        }

        EmitSignal(SignalName.OnHealthChanged, Health);
        
        if (Health == 0)
        {
            EmitSignal(SignalName.OnDeath);
        }
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }

        EmitSignal(SignalName.OnHealthChanged, Health);
    }

    public float GetHealthPercent()
    {
        return (float)Health / MaxHealth;
    }
}
