using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ResourceScanner))]

public class ScannerActivator : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _cooldown;

    private ResourceScanner _scanner;

    private WaitForSeconds _waitCooldown;
    private Coroutine _activatingCoroutine;

    private void Awake()
    {
        _scanner = GetComponent<ResourceScanner>();
    }

    private void OnEnable()
    {
        if (_activatingCoroutine != null)
            StopCoroutine(_activatingCoroutine);

        _activatingCoroutine = StartCoroutine(ActivateScanner());
    }

    private void OnDisable()
    {
        if (_activatingCoroutine != null)
            StopCoroutine(_activatingCoroutine);
    }

    private IEnumerator ActivateScanner()
    {
        _waitCooldown ??= new WaitForSeconds(_cooldown);

        while (enabled)
        {
            yield return _waitCooldown;

            _scanner.ScanGround();
        }
    }
}
