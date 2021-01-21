using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Warlock))]

public class WarlockAttack : MonoBehaviour
{
  
  [SerializeField] private FireBall _fireBall = default;
  [SerializeField] private Transform _shotPoint = default;
  [SerializeField] private float _attackDistanceCheck = 0;
  [SerializeField] private float _timeBetweenAtacks = 0;

  private Player _target;
  private LayerMask _targetLayerMask;
  private bool _isUnderAttack = false;

  private void Start()
  {
    _target = GetComponent<Warlock>().GetTarget;
    _targetLayerMask.value = LayerMask.GetMask(LayerMask.LayerToName(_target.gameObject.layer));
  }


  private void Update()
  {
    bool isTargetFound;
    if (SetWhereAreWeGoing())
      isTargetFound = Physics2D.Raycast(_shotPoint.position, Vector2.left, _attackDistanceCheck, _targetLayerMask);
    else
      isTargetFound = Physics2D.Raycast(_shotPoint.position, Vector2.right, _attackDistanceCheck, _targetLayerMask);

    if (isTargetFound && _isUnderAttack == false)
    {
      StartCoroutine(Attack());
      var instantiateObj = Instantiate(_fireBall, _shotPoint.position, Quaternion.identity);
      instantiateObj.Init(SetWhereAreWeGoing());
    }
  }

  private bool SetWhereAreWeGoing()
  {
    return transform.eulerAngles.y == 0;
  }

  private IEnumerator Attack()
  {
    float elapsedTime = _timeBetweenAtacks;

    _isUnderAttack = true;

    while (elapsedTime > 0)
    {
      elapsedTime -= Time.deltaTime;
      yield return null;
    }

    _isUnderAttack = false;
  }
}
