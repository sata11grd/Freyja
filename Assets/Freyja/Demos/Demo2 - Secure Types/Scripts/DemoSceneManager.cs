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
        [SerializeField] private int minDamage = 5;
        [SerializeField] private int maxDamage = 15;

        private SecureInt _shp;
        private SecureInt _smp;
        
        private void Awake()
        {
            spawnButton.onClick.AddListener(Spawn);
            damageButton.onClick.AddListener(() => Damage(UnityEngine.Random.Range(minDamage, maxDamage), UnityEngine.Random.Range(minDamage, maxDamage)));
            recoverButton.onClick.AddListener(() => Recover(UnityEngine.Random.Range(minDamage, maxDamage), UnityEngine.Random.Range(minDamage, maxDamage)));
            showButton.onClick.AddListener(Show);
        }

        private void Spawn()
        {
            _shp = new SecureInt(100, "hp");
            _smp = new SecureInt(100, "mp");
        }

        private void Show()
        {
            hpLabel.text = "HP: " + _shp;
            mpLabel.text = "MP: " + _smp;
        }

        private void Damage(int hp, int mp)
        {
            _shp.Value = Mathf.Max(0, _shp - hp);
            _smp.Value = Mathf.Max(0, _smp - mp);
        }

        private void Recover(int hp, int mp)
        {
            _shp.Value = Mathf.Min(200, _shp + hp);
            _smp.Value = Mathf.Min(200, _smp + mp);
        }
    }
}