using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingScript : MonoBehaviour
{
    public float AnimationTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ShowCanvas(this.gameObject));
        }
    }

    private IEnumerator ShowCanvas(GameObject obj)
    {
        if (obj != null)
        {

            while (true)
            {
                Debug.Log("Fading");

                Color fade = obj.GetComponent<Renderer>().material.color;
                float fadeAmount = fade.a * (Time.deltaTime * 0.4f);
                
                fade = new Color(fade.r, fade.g, fade.b, fadeAmount);

                obj.GetComponent<Renderer>().material.color = fade;
                yield return null;
            }
        }
    }
}
