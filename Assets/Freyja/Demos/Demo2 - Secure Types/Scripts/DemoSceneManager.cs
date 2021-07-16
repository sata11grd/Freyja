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
        [SerializeField] private Button spawnButton;
        [SerializeField] private Button showButton;
        [SerializeField] private Button damageButton;
        [SerializeField] private Button recoverButton;

        private SecureInt _shp;
        private SecureInt _smp;
        
        private void Awake()
        {
            spawnButton.onClick.AddListener(Spawn);
            damageButton.onClick.AddListener(() => Damage(UnityEngine.Random.Range(5, 16), UnityEngine.Random.Range(5, 16)));
            recoverButton.onClick.AddListener(() => Recover(UnityEngine.Random.Range(5, 16), UnityEngine.Random.Range(5, 16)));
            showButton.onClick.AddListener(Show);
        }

        private void Spawn()
        {
            _shp = new SecureInt(100, "hp");
            //_smp = new SecureInt(100, "mp");
        }

        private void Show()
        {
            hpLabel.text = "HP: " + _shp;
            //mpLabel.text = "MP: " + _smp;
        }

        private void Damage(int hp, int mp)
        {
            if (_shp.Value - hp > 0)
            {
                _shp.Value -= hp;
            }
            else
            {
                _shp.Value = 0;
            }

            if (_smp.Value - mp > 0)
            {
                _smp.Value -= mp;
            }
            else
            {
                _smp.Value = 0;
            }
        }

        private void Recover(int hp, int mp)
        {
            if (_shp.Value + hp <= 200)
            {
                _shp.Value += hp;
            }
            else
            {
                _shp.Value = 200;
            }
            
            if (_smp.Value + mp <= 200)
            {
                _smp.Value += mp;
            }
            else
            {
                _smp.Value = 200;
            }
        }
    }
}