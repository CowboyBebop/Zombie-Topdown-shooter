using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

namespace Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private IWeapon currentWeapon;

        Pistol pistol = new Pistol();
        Rifle rifle = new Rifle();

        private void Awake()
        {


            //var currentWeaponComponents = currentWeapon.GetType().Assembly.GetTypes().
            //Where(type => type.GetInterface(typeof(IWeaponComponent).Name) != null);

            pistol.Initialize();
            rifle.Initialize();

            currentWeapon = pistol as IWeapon;
        }

        private void Update()
        {
            //Debug.Log(currentWeapon.WeaponActions);

            foreach (WeaponAction weaponAction in currentWeapon.WeaponActions)
            {
                //GetKeyDown may cause a problem in the future since some actions may need GetKey or GetKeyUp instead
                if (Input.GetKeyDown(weaponAction.actionKey))
                {
                    weaponAction.actionEvent();
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentWeapon = pistol as IWeapon;
                Debug.Log("Switching to Pistol");
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                currentWeapon = rifle as IWeapon;
                Debug.Log("Switching to Rifle");
            }
        }
    }
}