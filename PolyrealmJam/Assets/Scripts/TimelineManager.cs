using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] List<PlayableDirector> playableDirector;
    public PlayableDirector currentDirector;

    [SerializeField] bool isPlayingScene;

    public void PlayCutscene(int cutSceneIndex)
    {
        //ayableDirector[cutSceneIndex].Play();
        currentDirector = playableDirector[cutSceneIndex];
        currentDirector.Play();
    }

    public void PlayChainedCutScene()
    {
        StartCoroutine(PlayScene());
    }

    IEnumerator PlayScene()
    {
        for (int i = 0; i < playableDirector.Count; i++)
        {
            currentDirector = playableDirector[i];
            currentDirector.Play();

            yield return new WaitUntil(() => currentDirector.state != PlayState.Playing);
        }
        yield return null;
    }
}
