using UnityEngine;

public class ParticlesSpawn : MonoBehaviour
{
    public float hSliderValue = 5.0f;
    private ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var emission = ps.emission;
        emission.rateOverTime = hSliderValue;
    }

    //void OnGUI()
    //{
    //    hSliderValue = GUI.HorizontalSlider(new Rect(25, 45, 100, 30), hSliderValue, 5.0f, 200.0f);
    //}
}