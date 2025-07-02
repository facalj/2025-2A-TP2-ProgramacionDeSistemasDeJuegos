using System;
using UnityEngine;

public class SquashAndStretch : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float minFallingSpeed = -2.0f;
    [SerializeField] private float maxFallingSpeed = 2.0f;
    [SerializeField] private Vector3 minScale = Vector3.one * 0.5f;
    [SerializeField] private Vector3 maxScale = Vector3.one * 1.5f;
    private Vector3 _originalScale;
    private float _currentState;

    private void Reset()
    {
        rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void Awake()
    {
        if (!rigidbody2D)
            rigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (!rigidbody2D)
        {
            Debug.LogError($"{name} <color=grey>({nameof(SquashAndStretch)})</color>: {nameof(rigidbody2D)} is null!");
            enabled = false;
        }

        _originalScale = transform.localScale;
    }

    private void FixedUpdate()
    {
        _currentState = (rigidbody2D.linearVelocity.y - minFallingSpeed) / (maxFallingSpeed - minFallingSpeed);
        transform.localScale = Vector3.Lerp(minScale, maxScale, _currentState);
    }
}