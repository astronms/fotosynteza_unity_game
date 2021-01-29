using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class Sun_Rotation : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] public int sun_position;

    public Sun_Rotation()
    {
        sun_position = 0;
    }

    public static Sun_Rotation Instance { get; private set; }

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        int value;
        if (_gameManager.PendingLoad.TryGetValue("sun_position", out value))
        {
            sun_position = value;
            _gameManager.PendingLoad.Remove("sun_position");
            for (int i = 0; i < sun_position; i++) StartCoroutine(Rotate(Vector3.up, 60, 0f));
        }

        DontDestroyOnLoad(transform.gameObject);
    }

    public void Next_Sun_Position()
    {
        StartCoroutine(Rotate(Vector3.up, 60));
        sun_position += 1;
        if (sun_position > 5) sun_position = 0;
    }


    private IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = to;

        StopAllCoroutines();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}