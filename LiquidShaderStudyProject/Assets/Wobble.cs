using UnityEngine;

public class Wobble : MonoBehaviour
{
    Renderer _renderer;

    Vector3 _lastPosition;
    Quaternion _lastRotation;
    Vector3 _velocity;
    Vector3 _angularVelocity;

    [SerializeField] private float maxWobble = 1.0f;
    [SerializeField] private float wobbleSpeed = 1.0f;
    [SerializeField] private float recoverySpeed = 1.0f;

    float _wobbleAmountX;
    float _wobbleAmountZ;
    float _wobbleAmountToAddX;
    float _wobbleAmountToAddZ;
    float _pulse;
    float _time = 0.5f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _lastPosition = transform.position;
        _lastRotation = transform.rotation;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        _wobbleAmountToAddX = Mathf.Lerp(_wobbleAmountToAddX, 0, Time.deltaTime * recoverySpeed);
        _wobbleAmountToAddZ = Mathf.Lerp(_wobbleAmountToAddZ, 0, Time.deltaTime * recoverySpeed);

        _pulse = 2 * Mathf.PI * wobbleSpeed;

        _wobbleAmountX = _wobbleAmountToAddX * Mathf.Sin(_pulse * _time);
        _wobbleAmountZ = _wobbleAmountToAddZ * Mathf.Sin(_pulse * _time);

        _renderer.material.SetFloat("_WobbleX", _wobbleAmountX);
        _renderer.material.SetFloat("_WobbleZ", _wobbleAmountZ);

        _velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _angularVelocity = (transform.rotation.eulerAngles - _lastRotation.eulerAngles) / Time.deltaTime;

        _wobbleAmountToAddX += Mathf.Clamp((_velocity.x + _angularVelocity.z * 0.2f) * maxWobble, -maxWobble, maxWobble);
        _wobbleAmountToAddZ += Mathf.Clamp((_velocity.z + _angularVelocity.x * 0.2f) * maxWobble, -maxWobble, maxWobble);

        _lastPosition = transform.position;
        _lastRotation = transform.rotation;
    }
}
