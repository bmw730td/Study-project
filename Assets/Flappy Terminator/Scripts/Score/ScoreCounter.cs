using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private ObjectSpawner _probeSpawner;
    
    private int _score;

    public event Action<int> ScoreChanged;

    public int Score => _score;

    private void OnEnable()
    {
        _probeSpawner.CreatedNewObject += SignUpProbe;
    }

    private void OnDisable()
    {
        _probeSpawner.CreatedNewObject -= SignUpProbe;
    }

    public void Reset()
    {
        _score = 0;
        ScoreChanged?.Invoke(Score);
    }

    private void SignUpProbe(SelfReturner probe)
    {
        if (probe.TryGetComponent(out ProbeCollisionProcessor processor))
            processor.HitByPlayer += IncreaseScore;
    }

    private void IncreaseScore()
    {
        _score++;
        ScoreChanged?.Invoke(Score);
    }
}
