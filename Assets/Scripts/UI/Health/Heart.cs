using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Heart : MonoBehaviour
{
  [SerializeField] private Sprite _heartLive = default;
  [SerializeField] private Sprite _heartBroken = default;

  private Image _image;

  private void Awake()
  {
    _image = GetComponent<Image>();
    _image.fillAmount = 1;
    _image.sprite = _heartLive;
  }

  public void SetToBroken()
  {
    _image.sprite = _heartBroken;
  }

  public void SetToLive()
  {
    _image.sprite = _heartLive;
  }
}
