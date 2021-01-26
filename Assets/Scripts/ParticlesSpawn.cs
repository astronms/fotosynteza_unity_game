using UnityEngine;
using System.Collections;

public class ParticlesSpawn : MonoBehaviour
{
    private ParticleSystem ps;
    public float hSliderValue = 5.0f;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        var emission = ps.emission;
        emission.rateOverTime = hSliderValue;
    }

    //void OnGUI()
    //{
    //    hSliderValue = GUI.HorizontalSlider(new Rect(25, 45, 100, 30), hSliderValue, 5.0f, 200.0f);
    //}
}