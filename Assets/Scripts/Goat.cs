using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Goat : MonoBehaviour
{
    [SerializeField]
    private float _eatDuration = 1f;

    [SerializeField]
    private float _moveSpeed = 2f;

    [SerializeField]
    private Animator _animator;

    private List<EdiblePlant> _allGrass;

    private void Start()
    {
        _allGrass = GameManager.Instance.GetAllGrass();
        StartCoroutine(EatingRoutine());
    }

    private IEnumerator EatingRoutine()
    {
        while (true)
        {
            var availableGrass = _allGrass.Where(grass => grass.IsAvailable).ToList();

            if (availableGrass.Count > 0)
            {
                EdiblePlant target = availableGrass[Random.Range(0, availableGrass.Count)];

                yield return StartCoroutine(MoveToTarget(target.transform.position));


                _animator.SetBool("Eat", true);

                yield return new WaitForSeconds(_eatDuration);

                _animator.SetBool("Eat", false);

                target.GetEaten();
            }

            yield return null;
        }
    }

    private IEnumerator MoveToTarget(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        float distance = Vector3.Distance(startPos, targetPos);
        float duration = distance / _moveSpeed;

        _animator.SetBool("Walking", true);

        Vector3 direction = (targetPos - startPos).normalized;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }

        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        _animator.SetBool("Walking", false);
    }
}