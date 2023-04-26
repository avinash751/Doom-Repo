using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Transform cameraToShake;

    private float time;
    [Range(0, 100)]
    [SerializeField] float frequency;
    [Range(0, 2)]
    [SerializeField] float amplitude;
    [SerializeField] float duration;

    [SerializeField] bool cameraShakeEnabled = false;
    [SerializeField] bool isInfinite;
    void Start()
    {
        startPosition = cameraToShake.localPosition;
    }

    private void Update()
    {
        CameraShakeOnceWithDuration();
        InfiniteCameraShake();
    }

    void CameraShakeOnceWithDuration()
    {
        if (time < duration && cameraShakeEnabled && !isInfinite)
        {
            time += Time.deltaTime;
            cameraShakeEnabled = true;
            float Xshake = amplitude * Mathf.Sin(Time.time * frequency);
            float Yshake = amplitude * Mathf.Cos(Time.time * frequency);
            Camera.main.transform.localPosition += new Vector3(Xshake, Yshake, 0); // adding current position of x and xshake becasue camera  is moving at x axis every frame
            return;
        }
        EnableCamersShake(false, false);
    }

    public void InfiniteCameraShake()
    {
        if (cameraShakeEnabled && isInfinite)
        {
            float xShake = amplitude * Mathf.Sin(Time.time * frequency);
            float yShake = amplitude * Mathf.Cos(Time.time * frequency);
            cameraToShake.localPosition += new Vector3(xShake, yShake, 0);
        }
    }

    public void EnableCamersShake(bool enable, bool isInfinite)
    {
        time = 0;
        cameraShakeEnabled = enable;
        this.isInfinite = isInfinite;
        cameraToShake.localPosition = startPosition;

    }

    public void SetCameraShakeValues(float frequency, float amplitude, float duration)
    {
        this.amplitude = amplitude;
        this.frequency = frequency;
        this.duration = duration;
    }

}

