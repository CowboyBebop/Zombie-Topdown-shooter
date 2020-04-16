using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Weapon.Projectile;
using System;

namespace Weapon
{
    public enum WeaponState
    {
        NoBullets,
        OnAttackCooldown,
        WeaponJammed,
    }

    public delegate void WeaponActionEvent();

    public struct WeaponAction
    {
        internal KeyCode actionKey;

        internal WeaponActionEvent actionEvent;
        //This would later be refactored to check the Input manager for the action key
        
    }

    public interface IWeapon
    {
        int BaseAttackDamage { get; set; }
        float WeaponKnockback { get; set; }
        WeaponState WeaponState { get; set; }
        List<WeaponAction> WeaponActions { get; set; }

        void Initialize();
        void Reload();
    }

    public abstract class RangedWeapon : IWeapon
    {
        [OdinSerialize] public ProjectileBase Projectile { get; set; }
        [OdinSerialize] public int RateOfFire { get; set; }
        public abstract int BaseAttackDamage { get; set; }
        public float WeaponKnockback { get; set; }
        public WeaponState WeaponState { get; set; }
        public int MagazineSize { get; set; }
        public float Velocity { get; set; }

        public Action OnShoot { get; set; }
        public Action OnReload { get; set; }

        public bool CanShoot => LastShot + 1f / RateOfFire <= Time.time && CurrentAmmoCount > 0;
        public bool CanReload => true;

        [OdinSerialize] public float ReloadTime { get; set; }
        [OdinSerialize] public Transform Position { get; set; }
        public List<WeaponAction> WeaponActions { get { return weaponActions; } set { weaponActions = value; } }

        protected List<WeaponAction> weaponActions = new List<WeaponAction>();

        [OdinSerialize] public bool hasInfiniteAmmo = false;

        [OdinSerialize] protected float LastShot;
        [OdinSerialize] protected int TotalAmmoCount;
        [OdinSerialize] protected int CurrentAmmoCount;

        public virtual void Initialize()
        {
            /**
            * the actionKey should be passed down from an Input class or something
            * It's quite awkward to do it here though tbh, so what the fuck should i do? 
            * (╯°□°）╯︵ ┻━┻
            * 
            * An alternative would be creating WeaponActions outside and just passing them down
            * Which would be cleaner here but would sometimes require longer names like LaserPreChargeGunAAction
            * Although that wouldn't happen most of the time tbh, more so it wouldn't be specific to a gun but who the fuck knows
            **/

            // TODO: Move the WeaponActions outside and just add them without creating them here

            WeaponAction reloadAction = new WeaponAction();
            reloadAction.actionKey = KeyCode.R;
            reloadAction.actionEvent += new WeaponActionEvent(Reload);

            WeaponAction ShootAction = new WeaponAction();
            ShootAction.actionKey = KeyCode.Mouse0;
            ShootAction.actionEvent += new WeaponActionEvent(Shoot);

            WeaponActions.Add(reloadAction);
            WeaponActions.Add(ShootAction);


            Debug.Log("Weapon: " + this.GetType().Name + " is Initialized");
        }

        public virtual void Reload()
        {
            int toReload = MagazineSize - CurrentAmmoCount;

            if (TotalAmmoCount >= toReload)
            {
                TotalAmmoCount -= toReload;
                CurrentAmmoCount = MagazineSize;
            }
            else
            {
                CurrentAmmoCount += TotalAmmoCount;
                TotalAmmoCount = 0;
            }

            OnReload?.Invoke();
        }

        public virtual void Shoot()
        {
        }

        public virtual ProjectileBase GetProjectile()
        {
            return Projectile;
        }

    }

    public abstract class MeleeWeapon : IWeapon
    {
        public abstract int BaseAttackDamage { get; set; }
        public float WeaponKnockback { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public WeaponState WeaponState { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        int IWeapon.BaseAttackDamage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        float IWeapon.WeaponKnockback { get; set; }
        WeaponState IWeapon.WeaponState { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        List<WeaponAction> IWeapon.WeaponActions { get; set ; }

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

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        void IWeapon.Initialize()
        {
            throw new NotImplementedException();
        }

        void IWeapon.Reload()
        {
            throw new NotImplementedException();
        }
    }

    public class Pistol : RangedWeapon
    {
        public override int BaseAttackDamage { get; set; }

        public Vector3 gunPointTransform; // = to a hard coded constant because i am a lazy shit

        public override void Reload()
        {
            base.Reload();
            Debug.Log("Reloading n' stuff shwoooom shwoooom");

        }

        public override void Shoot()
        {
            base.Shoot();
            Debug.Log("So anyway i started blasting");
        }

        public override void Initialize()
        {
            base.Initialize();

        }

    }
    public class Rifle : RangedWeapon
    {
        public override int BaseAttackDamage { get; set; }

        public Vector3 gunPointTransform; // = to a hard coded constant because i am a lazy shit

        public override void Reload()
        {
            base.Reload();
            Debug.Log("Reloading RIFLE HAHAHAHAn' stuff shwoooom shwoooom");
        }


        public override void Shoot()
        {
            base.Shoot();
            Debug.Log("So anyway i started blasting doubly so because it's a rifle");
        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}

namespace Weapon.Projectile
{
    public abstract class ProjectileBase
    {
        /// <summary>
        /// Appearance prefab
        /// </summary>
        [OdinSerialize] public GameObject Appearance { get; set; }

        /// <summary>
        /// Base damage before fallout
        /// </summary>
        [OdinSerialize]
        public int BaseDamage { get; set; }

        /// <summary>
        /// Falling damage of projectile over distance in unit
        /// </summary>
        [OdinSerialize]
        public AnimationCurve FalloutCurve { get; set; }

        /// <summary>
        /// Falling Y of projectile over distance in unit / second in unity
        /// </summary>
        [OdinSerialize]
        public AnimationCurve BulletDropCurve { get; set; }

        /// <summary>
        /// Speed of the bullet in unit/second
        /// </summary>
        [OdinSerialize]
        public float BulletSpeed { get; set; }

        /// <summary>
        /// Event played on hit
        /// </summary>
        public Action<Collision> OnHit { get; set; }

        /// <summary>
        /// Initialize position data
        /// </summary>
        public abstract void InitializePositionData(Transform transform);

        /// <summary>
        /// Get position to translate to this frame
        /// </summary>
        public abstract Vector3 GetNextPosition(Transform transform);

        public abstract ProjectileBase Clone();

    }
}

