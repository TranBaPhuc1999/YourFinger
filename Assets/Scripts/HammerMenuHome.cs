using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMenuHome : MonoBehaviour
{
    public float timeRotation = 3f;
    float oldTime = 0;
    public Animator ani;

    // Update is called once per frame
    void Update()
    {

        //Hiệu ứng búa đập tự chạy
        if (Time.time > oldTime + timeRotation)
        {
            oldTime = Time.time;
            ani.SetTrigger("Hit");
        }
    }

}
