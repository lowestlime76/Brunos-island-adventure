using UnityEngine;
using UnityEngine.Playables;
using RPG.Utility;

namespace RPG.Core
{
    public class CinematicController : MonoBehaviour
    {

        PlayableDirector playableDirectorCmp;
        Collider colliderCmp;

        private void Awake()
        {
            playableDirectorCmp = GetComponent<PlayableDirector>();
            colliderCmp = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            playableDirectorCmp.played += HandlePlayed;
            playableDirectorCmp.stopped += HandleStopped;
        }

        private void OnDisable()
        {
            playableDirectorCmp.played -= HandlePlayed;
            playableDirectorCmp.stopped -= HandleStopped;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Constants.PLAYER_TAG)) return;

            colliderCmp.enabled = false;
            playableDirectorCmp.Play();
        }

        private void HandlePlayed(PlayableDirector pd)
        {
            EventManager.RaiseCutsceneUpdated(false);
        }

        private void HandleStopped(PlayableDirector pd)
        {
            EventManager.RaiseCutsceneUpdated(true);
        }
    }
}

