using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour

{//IN THIS SCRIPT: This is the audio manager of the "Options" panel available in the LobbyScene
     private static AudioManager _instance ; // when we change the scene, our game object won't get destroyed
 
     void Awake()
     {
         // If we don't have an [_instance] set yet
         if(!_instance)
             _instance = this ;
         // otherwise, if we do, kill this thing
         else
             Destroy(this.gameObject) ;
 
 
         //DontDestroyOnLoad(this.gameObject) ;            UNMARK THIS IN ORDER TO KEEP AUDIO MANAGER for other scenes!
     }
    [SerializeField] public GameObject MutedPanel = null; // The red cross sign when muted button is pressed

   public int MutedButtonClick; // This int is created in order to keep track of Mute button clicks(to know if it's pressed once, and again, to show 
                                // accurate UI)
    public AudioClip[] musicClips;
    private int currentTrack;
    private AudioSource source;

    public TMP_Text clipTitleText;
    public TMP_Text clipTimeText;

    private int fulllength;
    private int playTime;
    private int seconds;
    private int minutes;

    void Start()
    {
        source = GetComponent<AudioSource>();
        // PLAY MUSIC
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (source.isPlaying)
        {
            return;
        }

        currentTrack--;
        if(currentTrack <0)
        {
            currentTrack = musicClips.Length - 1;

        }
     StartCoroutine("WaitForMusicEnd");
    }
    IEnumerator WaitForMusicEnd()
    {
        while (source.isPlaying)
        {
            playTime = (int)source.time;
            ShowPlayTime();
            yield return null;
        
        }
        NextTitle();
    }

    public void NextTitle()
    {
        source.Stop();
        currentTrack++;
        if(currentTrack >musicClips.Length - 1)
        {
            currentTrack =0;
        }

        source.clip = musicClips[currentTrack];
        source.Play();

    // show title
    ShowCurrentTitle();
    StartCoroutine("WaitForMusicEnd");
    }

     public void PreviousTitle()
    {
        source.Stop();
        currentTrack--;
        if(currentTrack < 0)
        {
            currentTrack = musicClips.Length -1;
        }

        source.clip = musicClips[currentTrack];
        source.Play();

    // show title
    ShowCurrentTitle();
    StartCoroutine("WaitForMusicEnd");
    }

    public void StopMusic()
    {   
        
        StopCoroutine("WaitForMusicEnd");
        source.Stop();
    }
   
   public void MuteMusic()
   {
    source.mute = !source.mute;
    MutedButtonClick++;
    if(MutedButtonClick == 1){
    MutedPanel.SetActive(true);
    }
    if(MutedButtonClick == 2){
    MutedPanel.SetActive(false);
    MutedButtonClick = 0;
    }
   }

    void ShowCurrentTitle()
    {
        clipTitleText.text = source.clip.name;
        fulllength = (int)source.clip.length;
    }

    void ShowPlayTime()
    {
        seconds = playTime % 60;
        minutes = (playTime /60)% 60;
        clipTimeText.text = minutes + ":" + seconds.ToString("D2") + "/" + ((fulllength /60) % 60) + ":" + (fulllength % 60).ToString("D2");
    }
}
