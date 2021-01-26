using UnityEngine;

public class Camera_rotation : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward *2* Time.deltaTime);
            this.transform.Translate(Vector3.down *2* Time.deltaTime);
            MainGameUI.Instance.ExitFieldMenu();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector3.back *2* Time.deltaTime);
            this.transform.Translate(Vector3.up *2* Time.deltaTime);
            MainGameUI.Instance.ExitFieldMenu();
        }
        */


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up, -1);
            MainGameUI.Instance.ExitFieldMenu();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, 1);
            MainGameUI.Instance.ExitFieldMenu();
        }
    }
}