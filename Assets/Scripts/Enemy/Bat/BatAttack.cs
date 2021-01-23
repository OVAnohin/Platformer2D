using System.Collections;
using UnityEngine;

public class BatAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _timeBetweenAtacks;

    private bool _isUnderAttack = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player target) && _isUnderAttack == false)
        {
            StartCoroutine(Attack());
            target.TakeDamage(_damage);
        }
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
