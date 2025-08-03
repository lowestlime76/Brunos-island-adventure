using UnityEngine.UIElements;
using UnityEngine;
using RPG.Core;

namespace RPG.UI
{
    public class UIMainMenuState : UIBaseState
    {
        private int sceneIndex;
        public UIMainMenuState(UIController ui) : base(ui) { }
        public override void EnterState()
        {
            if (PlayerPrefs.HasKey("SceneIndex"))
            {
                sceneIndex = PlayerPrefs.GetInt("SceneIndex");
                AddButton();
            }

            controller.mainMenuContainer.style.display = DisplayStyle.Flex;

            controller.buttons = controller.mainMenuContainer
            .Query<Button>(null, "menu-button")
            .ToList();

            controller.buttons[0].AddToClassList("active");
        }

        public override void SelectButton()
        {
            Button btn = controller.buttons[controller.currentSelection];

            if (btn.name == "start-button")
            {
                PlayerPrefs.DeleteAll();
                SceneTransition.Initiate(1);
            }
            else
            {
                SceneTransition.Initiate(sceneIndex);
            }
        }

        private void AddButton()
        {
            Button continueButton = new Button();

            continueButton.AddToClassList("menu-button");
            continueButton.text = "Continue";

            VisualElement mainMenuButtons = controller.mainMenuContainer
                .Q<VisualElement>("buttons");

            mainMenuButtons.Add(continueButton);
        }
    }
}