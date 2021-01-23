using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;
    [SerializeField] private Image _icon;
    [SerializeField] private Button _useButton;

    public event UnityAction<Weapon, WeaponView> UseButtonClick;
    public string Label => _label.text;

    private Weapon _weapon;

    private void OnEnable()
    {
        _useButton.onClick.AddListener(OnButtonClick);
        _useButton.onClick.AddListener(TryLockItem);
    }

    private void OnDisable()
    {
        _useButton.onClick.RemoveListener(OnButtonClick);
        _useButton.onClick.RemoveListener(TryLockItem);
    }

    public void Render(Weapon weapon)
    {
        _weapon = weapon;

        _label.text = weapon.GetLabel;
        _icon.sprite = weapon.GetSprite;

        if (_weapon.IsUse)
            _useButton.interactable = false;
        else
            _useButton.interactable = true;
    }

    private void TryLockItem()
    {
        if (_useButton.interactable)
            _useButton.interactable = false;
    }

    private void OnButtonClick()
    {
        UseButtonClick?.Invoke(_weapon, this);
    }
}
