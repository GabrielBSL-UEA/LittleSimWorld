using Audio;
using Main.Inventory;
using Scenario;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main.UI
{ 
    /*
     *  Class responsible for operate the change clothes panel
     *
     */
    public class HouseInterface : GameInterface
    {
        [Header("Main")]
        [SerializeField] private UIObjectEffector clothingPanel;

        [Header("Player Frame")]
        [SerializeField] private Image hairImage;
        [SerializeField] private Image hatImage;
        [SerializeField] private Image outfitImage;

        [Header("Content Box")]
        [SerializeField] private RectTransform clothesContentBox;
        [SerializeField] private GameObject buttonTemplate;

        [Header("Option Buttons")]
        [SerializeField] private Button hairButton; //button id: 1
        [SerializeField] private Button hatButton; //button id: 2
        [SerializeField] private Button outfitButton; //button id: 3

        private InventoryManager _inventory;
        private List<Clothing> _clothes;

        //Loads the current player clothes into memory and sets up the content panel
        public override void OpenSpecialPanel()
        {
            base.OpenSpecialPanel();

            if (_inventory == null)
            {
                _inventory = GameManager.Instance.Inventory();
            }

            UpdatePlayerFrame();

            _clothes = GameManager.Instance.GetAvailableClothes(toBuy: false);
            SetUpContentForOutfit();

            clothingPanel.gameObject.SetActive(true);
        }

        //shows all the available hairs
        public void SetUpContentForHair()
        {
            SetButtonActivation(1);
            var hairs = _clothes.FindAll(c => c.GetType() == typeof(Hair));

            foreach (Transform option in clothesContentBox)
            {
                Destroy(option.gameObject);
            }

            foreach (var outfit in hairs)
            {
                LoadClothes(outfit);
            }
        }

        //shows all the available hats
        public void SetUpContentForHats()
        {
            SetButtonActivation(2);
            var hats = _clothes.FindAll(c => c.GetType() == typeof(Hat));

            foreach (Transform option in clothesContentBox)
            {
                Destroy(option.gameObject);
            }

            foreach (var outfit in hats)
            {
                LoadClothes(outfit);
            }
        }

        //shows all the available outfits
        public void SetUpContentForOutfit()
        {
            SetButtonActivation(3);
            var outfits = _clothes.FindAll(c => c.GetType() == typeof(Outfit));

            foreach (Transform option in clothesContentBox)
            {
                Destroy(option.gameObject);
            }

            foreach (var outfit in outfits)
            {
                LoadClothes(outfit);
            }
        }
        
        //Deactivates the current clothes option button to avoid double click
        private void SetButtonActivation(int buttonCode)
        {
            hairButton.interactable = buttonCode != 1;
            hatButton.interactable = buttonCode != 2;
            outfitButton.interactable = buttonCode != 3;
        }

        //Sets up the buttons responsible for change the player's clothes
        public void LoadClothes(Clothing clothing)
        {
            var newButtonObject = Instantiate(buttonTemplate, clothesContentBox);
            newButtonObject.transform.TryGetComponent(out ClothingButton newButton);

            //Item icon
            newButton.Icon().sprite = clothing.Icon();

            //Item name
            newButton.Name().text = clothing.Name();

            newButton.Button().onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySFX("s_ChangeClothes");
                _inventory.DressClothingPiece(clothing);
                UpdatePlayerFrame();
            });
        }

        //Update the reference player frame after changing clothes
        private void UpdatePlayerFrame()
        {
            hairImage.sprite = _inventory.CurrentHair.IdleDownSprite();
            hatImage.sprite = _inventory.CurrentHat.IdleDownSprite();
            outfitImage.sprite = _inventory.CurrentOutfit.IdleDownSprite();
        }

        public override void CloseSpecialPanel()
        {
            base.CloseSpecialPanel();

            clothingPanel.Deactivate(action:() => 
            {
                GameManager.Instance.SetPlayerControl(true);
            });
        }
    }
}