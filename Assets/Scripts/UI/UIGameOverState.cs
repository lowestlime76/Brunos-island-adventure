using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using RPG.Utility;
using RPG.Core;

namespace RPG.UI
{
    public class UIGameOverState : UIBaseState
    {
        public UIGameOverState(UIController uiController) : base(uiController) { }

        public override void EnterState()
        {
            PlayerInput playerInputCmp = GameObject.FindGameObjectWithTag(
                Constants.GAME_MANAGER_TAG
            ).GetComponent<PlayerInput>();

            VisualElement gameOverContainer = controller.root
                .Q<VisualElement>("game-over-container");

            playerInputCmp.SwitchCurrentActionMap(
                Constants.UI_ACTION_MAP
            );
            gameOverContainer.style.display = DisplayStyle.Flex;

            controller.audioSourceCmp.PlayOneShot(
                controller.gameOverAudio
            );

            controller.canPause = false;
        }

        public override void SelectButton()
        {
            PlayerPrefs.DeleteAll();
            controller.StartCoroutine(SceneTransition.Initiate(0));
        }
    }
}
