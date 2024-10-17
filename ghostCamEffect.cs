using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GhostCamEffect : MonoBehaviour
{
    public Volume volume; 
    public float distortionSpeed = 1f;
    public float chromaticSpeed = 1f; 
    public float otherEffectsSpeed = 0.5f;

   
    public float distortionMin = -0.5f;
    public float distortionMax = 0.5f;
    public float chromaticMin = 0f;
    public float chromaticMax = 1f;

    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette; 
    private FilmGrain filmGrain; 
    private Bloom bloom; 

    private float time;

    void Start()
    {
       
        if (volume.profile.TryGet(out lensDistortion) &&
            volume.profile.TryGet(out chromaticAberration) &&
            volume.profile.TryGet(out vignette) &&
            volume.profile.TryGet(out filmGrain) &&
            volume.profile.TryGet(out bloom))
        {
          
            lensDistortion.intensity.value = 0f;
            chromaticAberration.intensity.value = 0f;
            vignette.intensity.value = 0f;
            filmGrain.intensity.value = 0f;
            bloom.intensity.value = 0f;
        }
        else
        {
            Debug.LogError("One or more Volume overrides are missing!");
        }
    }

    void Update()
    {
       
        time += Time.deltaTime;

        
        if (lensDistortion != null)
        {
            float distortionValue = Mathf.Lerp(distortionMin, distortionMax, (Mathf.Sin(time * distortionSpeed) + 1f) / 2f);
            lensDistortion.intensity.value = distortionValue;
        }

       
        if (chromaticAberration != null)
        {
            float chromaticValue = Mathf.Lerp(chromaticMin, chromaticMax, (Mathf.Sin(time * chromaticSpeed) + 1f) / 2f);
            chromaticAberration.intensity.value = chromaticValue;
        }

        if (vignette != null)
        {
            float vignetteValue = Mathf.Lerp(0.2f, 0.45f, (Mathf.Sin(time * otherEffectsSpeed) + 1f) / 2f);
            vignette.intensity.value = vignetteValue;
        }

      
        if (filmGrain != null)
        {
            float filmGrainValue = Mathf.Lerp(0.1f, 0.35f, (Mathf.Sin(time * otherEffectsSpeed * 1.2f) + 1f) / 2f);
            filmGrain.intensity.value = filmGrainValue;
        }

       
        if (bloom != null)
        {
            float bloomValue = Mathf.Lerp(0.2f, 0.4f, (Mathf.Sin(time * otherEffectsSpeed * 0.8f) + 1f) / 2f);
            bloom.intensity.value = bloomValue;
        }
    }
}
