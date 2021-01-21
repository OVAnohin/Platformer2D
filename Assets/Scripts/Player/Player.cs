using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private SpriteRenderer _weaponRender;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private AudioClip _hurtSound;

    public int Money { get; private set; }
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<Weapon> WeaponChanged;
    public event UnityAction PlayerDied;
    public Weapon CurrenUseWeapon => _weapon;
    public List<Weapon> Weapons { get; } = new List<Weapon>();

    private int _currentHealth;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Weapons.Add(_weapon);
        _weapon.IsUse = true;
        _currentHealth = _health;
        _weaponRender.sprite = _weapon.GetSprite;

        HealthChanged?.Invoke(_currentHealth, _health);
        WeaponChanged?.Invoke(_weapon);
        MoneyChanged?.Invoke(Money);
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChanged?.Invoke(Money);
    }

    public void TakeDamage(int damage)
    {
        _audioSource.clip = _hurtSound;
        _audioSource.Play();
        _currentHealth -= damage;
        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            PlayerDied?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void TakeBonusHealth()
    {
        _health += 1;
        _currentHealth += 1;
        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void TakeHealthPotion()
    {
        _currentHealth += 1;
        HealthChanged?.Invoke(_currentHealth, _health);
    }

    public void Equip(Weapon weapon)
    {
        if (_weapon.GetLabel != weapon.GetLabel)
            _weapon.IsUse = false;

        TryToAddWeapon(weapon);
        _weapon = weapon;
        _weaponRender.sprite = weapon.GetSprite;
        _weapon.IsUse = true;
        WeaponChanged?.Invoke(_weapon);
    }

    private void TryToAddWeapon(Weapon weapon)
    {
        var foundWeapon = Weapons.Where(w => w.GetLabel == weapon.GetLabel).FirstOrDefault();
        if (foundWeapon == null)
            Weapons.Add(weapon);
    }
}
