using System;
using System.Collections;
using System.Collections.Generic;
using Freyja.Core;
using Freyja.Types;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Freyja.Demo.Demo2
{
    public class DemoSceneManager : MonoBehaviour
    {
        [SerializeField] private Text hpLabel;
        [SerializeField] private Text mpLabel;
        [SerializeField] private Button damageButton;
        [SerializeField] private Button recoverButton;

        private SecureInt _shp;
        private SecureInt _smp;

        private void Refresh()
        {
            hpLabel.text = "HP: " + _shp;
            mpLabel.text = "MP: " + _smp;
        }
        
        private void Awake()
        {
            damageButton.onClick.AddListener(() => Damage(UnityEngine.Random.Range(5, 16), UnityEngine.Random.Range(5, 16)));
            recoverButton.onClick.AddListener(() => Recover(UnityEngine.Random.Range(5, 16), UnityEngine.Random.Range(5, 16)));

            _shp = 100;
            _smp = 100;
            
            Refresh();
            return;
            var stats = new Dictionary<string, (Type, object)>();
            
            stats.Add("hp", (typeof(int), 100));
            stats.Add("mp", (typeof(int), 100));
            
            Freyja.WriteCall(stats);
        }

        private void Damage(int hp, int mp)
        {
            if (_shp - hp > 0)
            {
                _shp -= hp;
            }
            else
            {
                _shp = 0;
            }

            if (_smp - mp > 0)
            {
                _smp -= mp;
            }
            else
            {
                _smp = 0;
            }
            
            Refresh();
            
            return;
            var stats = Freyja.Convert(Freyja.ReadCall());

            stats["hp"] = (int)stats["hp"] - hp;
            stats["mp"] = (int)stats["mp"] - mp;
            
            Freyja.WriteCall(stats);
        }

        private void Recover(int hp, int mp)
        {
            if (_shp + hp <= 200)
            {
                _shp += hp;
            }
            else
            {
                _shp = 200;
            }
            
            if (_smp + mp <= 200)
            {
                _smp += mp;
            }
            else
            {
                _smp = 200;
            }
            
            Refresh();
        }
    }
}