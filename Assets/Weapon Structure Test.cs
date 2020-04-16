using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Weapon
{
    public enum WeaponType
    {
        RangedWeapon,
        MeleeWeapon
    }
    public enum WeaponState
    {
        NoBullets,
        OnAttackCooldown,
        WeaponJammed,
    }

    public interface IWeapon
    {
        int BaseAttackDamage { get; set; }
        float WeaponKnockback { get; set; }
        WeaponState WeaponState { get; set; }

        WeaponType WeaponType { get; set; }

        void DoAttack();
        void Initialize();

    }

    public abstract class RangedWeapon : IWeapon
    {
        public abstract int BaseAttackDamage { get; set; }
        public float WeaponKnockback { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public WeaponState WeaponState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public WeaponType WeaponType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public int fireRate;
        public int attackRate;

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public virtual void DoAttack()
        {
            Debug.Log("Pew Pew");
        }

        public virtual void Reload()
        {
            Debug.Log("Pew Pew");
        }


    }

    public abstract class MeleeWeapon : IWeapon
    {
        public abstract int BaseAttackDamage { get; set; }
        public float WeaponKnockback { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public WeaponState WeaponState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public WeaponType WeaponType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public float attackRange;
        public int attackRate;

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }

        public virtual void DoAttack()
        {
            Debug.Log("Slash Slash");
        }


        public virtual void GuardThing()
        {
            Debug.Log("Guard thing");
        }
    }

    public class SwordA : MeleeWeapon
    {
        public override int BaseAttackDamage { get; set; }

        public override void DoAttack()
        {
            base.DoAttack();
        }
    }

    public class Pistol : RangedWeapon
    {
        public override int BaseAttackDamage { get; set; }

        public Vector3 gunPointTransform; // = to a hard coded constant because i am a lazy shit

        public override void DoAttack()
        {
            base.DoAttack();
        }

    }
    
}



