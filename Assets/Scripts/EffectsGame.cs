using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsGame : MonoBehaviour
{

    public AudioClip[] audioClipsSuspense;
    public AudioClip[] audioClipsHit;
    public AudioClip[] audioClipsBackground;
    public AudioSource audioSourceBackground;
    public AudioSource audioSourceSuspense;
    public AudioSource audioSourceHit;


    private Data data = new Data();
    public void playSound(int idAudio,bool oneShort, bool repeat, float volume)
    {
        if(data.Sound == 0)
        {
            return;
        }

        if (oneShort)
        {
            //chạy nhạc troll với hiệu ứng gió
            switch (idAudio)
            {
                case 3:
                    audioSourceBackground.PlayOneShot(audioClipsBackground[Random.Range(0, audioClipsBackground.Length)]);
                    stopSound("suspense");
                    break;
                case 4:
                    audioSourceSuspense.clip = audioClipsSuspense[Random.Range(0, audioClipsSuspense.Length)];
                    break;
            }
        }
        else
        {
            // chạy nhạc hồi hộp và tiếng đập
            

            switch (idAudio)
            {
                case 1:
                    audioSourceSuspense.Stop();

                    audioSourceSuspense.clip = audioClipsSuspense[Random.Range(0, audioClipsSuspense.Length)];
                    audioSourceSuspense.loop = repeat;
                    audioSourceSuspense.volume = volume;

                    audioSourceSuspense.Play();
                    break;
                case 2:
                    audioSourceHit.PlayOneShot(audioClipsHit[Random.Range(0, audioClipsHit.Length)]);
                    stopSound("suspense");
                    stopSound("hit");
                    break;
            }



        }

    }


    public void stopSound(string typeSound)
    {
        if (typeSound.Equals("hit"))
        {
            StartCoroutine(stopSoundAffterTime(1));
        }
        else
        {
            audioSourceSuspense.Stop();
        }
        
    }

    
    IEnumerator stopSoundAffterTime(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        audioSourceHit.Stop();
    }
}
