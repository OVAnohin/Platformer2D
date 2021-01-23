using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WeaponView _template;
    [SerializeField] private GameObject _itemContainer;

    private List<Weapon> _weapons = new List<Weapon>();

    private void OnEnable()
    {
        _weapons = _player.Weapons;

        List<Transform> weaponViews = GetChildredItemContainer();

        if (weaponViews.Count == 0)
        {
            for (int i = 0; i < _weapons.Count; i++)
                AddItem(_weapons[i]);
        }
        else
        {
            for (int i = 0; i < _weapons.Count; i++)
            {
                bool isInView = weaponViews.Where(wv => wv.GetComponent<WeaponView>().Label == _weapons[i].GetLabel).FirstOrDefault();

                if (isInView == false)
                    AddItem(_weapons[i]);

                weaponViews = GetChildredItemContainer();
                WeaponView weaponView = weaponViews.Where(wv => wv.GetComponent<WeaponView>().Label == _weapons[i].GetLabel).FirstOrDefault().GetComponent<WeaponView>();
                weaponView.Render(_weapons[i]);
                weaponView.UseButtonClick += OnUseButtonClick;
            }
        }
    }

    private void OnDisable()
    {
        List<Transform> weaponViews = GetChildredItemContainer();
        foreach (Transform item in weaponViews)
            item.GetComponent<WeaponView>().UseButtonClick -= OnUseButtonClick;
    }

    private List<Transform> GetChildredItemContainer()
    {
        List<Transform> childrens = _itemContainer.GetComponentsInChildren<Transform>().ToList();
        var weaponViews = childrens.Where(wv => wv.GetComponent<WeaponView>() != false).ToList();
        return weaponViews;
    }

    private void AddItem(Weapon weapon)
    {
        var view = Instantiate(_template, _itemContainer.transform);
        view.UseButtonClick += OnUseButtonClick;
        view.Render(weapon);
    }

    private void OnUseButtonClick(Weapon weapon, WeaponView weaponView)
    {
        TryUseWeapon(weapon);
    }

    private void TryUseWeapon(Weapon weapon)
    {
        Weapon playerWeapon = _player.CurrenUseWeapon;

        _player.Equip(weapon);

        List<Transform> weaponViews = GetChildredItemContainer();
        WeaponView elem = weaponViews.Where(wv => wv.GetComponent<WeaponView>().Label == playerWeapon.GetLabel).FirstOrDefault().GetComponent<WeaponView>();
        elem.Render(playerWeapon);
    }
}
