using System.Collections.Generic;
using RPG.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
using RPG.Core;

namespace RPG.Quest
{
    public class TreasureChest : MonoBehaviour
    {
        [SerializeField] private QuestItemSO questItem;
        public Animator animatorCmp;
        private bool isInteractable = false;
        private bool hasBeenOpened = false;

        private void Start()
        {
            if (PlayerPrefs.HasKey("PlayerItems"))
            {
                List<string> playerItems = PlayerPrefsUtility.GetString("PlayerItems");

                playerItems.ForEach(CheckItem);
            }
        }
        private void OnTriggerEnter()
        {
            isInteractable = true;
        }

        private void OnTriggerExit()
        {
            isInteractable = false;
        }

        public void HandleInteract(InputAction.CallbackContext context)
        {
            if (!isInteractable || hasBeenOpened || !context.performed) return;

            EventManager.RaiseTreasureChestUnlocked(questItem, true);

            animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);
            hasBeenOpened = true;
        }

        private void CheckItem(string itemName)
        {
            if (itemName != questItem.name) return;

            hasBeenOpened = true;
            animatorCmp.SetBool(Constants.IS_SHAKING_ANIMATOR_PARAM, false);

            EventManager.RaiseTreasureChestUnlocked(questItem, false);
        }
    }
}

