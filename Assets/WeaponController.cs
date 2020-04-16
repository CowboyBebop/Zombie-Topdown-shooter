using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

namespace Weapon
{
    public class WeaponController : SerializedMonoBehaviour
    {
        [OdinSerialize] private IWeapon currentWeapon;

        private void Awake()
        {
            /**
             * Current Weapon is an IWeapon that will contain a List of Actions
            **/


            //var currentWeaponComponents = currentWeapon.GetType().Assembly.GetTypes().
            //Where(type => type.GetInterface(typeof(IWeaponComponent).Name) != null);


            currentWeapon.Initialize();
        }

        private void Update()
        {
        }
    }
}