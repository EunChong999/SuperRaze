using UnityEngine;

public class ScreenBlock : MonoBehaviour
{
    public bool on;

    public float LerpTime;
    public float CurrentTime;

    public GameObject Block_Screen;

    public Transform Release_Start_Position;
    public Transform Release_End_Position;

    private void Start()
    {
        this.transform.position = Release_Start_Position.position;
    }

    private void Update()
    {
        if (on == true)
        {
            CurrentTime += Time.deltaTime;

            if (CurrentTime >= LerpTime)
            {
                CurrentTime = LerpTime;
            }

            float t = CurrentTime / LerpTime;

            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            Block_Screen.transform.position = Vector3.Lerp(Release_Start_Position.position, Release_End_Position.position, t);
        }
    }

    public void BlockScreen()
    {
        if (on == false)
        {
            CurrentTime = 0;

            Block_Screen.transform.position = Release_Start_Position.position;

            Invoke("CreateWall", 1.25f);
        }
    }

    public void CreateWall()
    {
        on = true;

        Block_Screen.SetActive(true);

        Invoke("ReleaseWall", 0.5f);
    }

    public void ReleaseWall()
    {
        on = false;

        Block_Screen.SetActive(false);
    }
}
