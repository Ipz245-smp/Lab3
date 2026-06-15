using System;

interface IHero
{
    string GetName();
    int GetAttack();
    int GetDefense();
    string GetDescription();
}

class Warrior : IHero
{
    public string GetName() => "Warrior";
    public int GetAttack() => 15;
    public int GetDefense() => 10;
    public string GetDescription() => $"{GetName()} | ATK:{GetAttack()} DEF:{GetDefense()}";
}

class Mage : IHero
{
    public string GetName() => "Mage";
    public int GetAttack() => 20;
    public int GetDefense() => 5;
    public string GetDescription() => $"{GetName()} | ATK:{GetAttack()} DEF:{GetDefense()}";
}

class Palladin : IHero
{
    public string GetName() => "Palladin";
    public int GetAttack() => 12;
    public int GetDefense() => 15;
    public string GetDescription() => $"{GetName()} | ATK:{GetAttack()} DEF:{GetDefense()}";
}

abstract class InventoryDecorator : IHero
{
    protected IHero _hero;

    public InventoryDecorator(IHero hero)
    {
        _hero = hero;
    }

    public virtual string GetName() => _hero.GetName();
    public virtual int GetAttack() => _hero.GetAttack();
    public virtual int GetDefense() => _hero.GetDefense();
    public virtual string GetDescription() => _hero.GetDescription();
}

class SwordDecorator : InventoryDecorator
{
    public SwordDecorator(IHero hero) : base(hero) { }
    public override int GetAttack() => base.GetAttack() + 10;
    public override string GetDescription() => base.GetDescription() + " + Sword(+10 ATK)";
}

class ShieldDecorator : InventoryDecorator
{
    public ShieldDecorator(IHero hero) : base(hero) { }
    public override int GetDefense() => base.GetDefense() + 8;
    public override string GetDescription() => base.GetDescription() + " + Shield(+8 DEF)";
}

class ArmorDecorator : InventoryDecorator
{
    public ArmorDecorator(IHero hero) : base(hero) { }
    public override int GetDefense() => base.GetDefense() + 15;
    public override string GetDescription() => base.GetDescription() + " + Armor(+15 DEF)";
}

class MagicRingDecorator : InventoryDecorator
{
    public MagicRingDecorator(IHero hero) : base(hero) { }
    public override int GetAttack() => base.GetAttack() + 5;
    public override int GetDefense() => base.GetDefense() + 5;
    public override string GetDescription() => base.GetDescription() + " + MagicRing(+5 ATK, +5 DEF)";
}
