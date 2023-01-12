using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Main.Inventory;
using System;
using Audio;

namespace Main.UI
{
    /*
     *  Class responsible for operate the shopping panel
     *
     */
    public class ShopInterface : GameInterface
    {
        [Header("Shop")]
        [SerializeField] private TextMeshProUGUI contentBoxUpperText;
        [SerializeField] private UIObjectEffector shoppingPanel;
        [SerializeField] private RectTransform shopContentBox;
        [SerializeField] private GameObject buttonTemplate;
        [SerializeField] private TextMeshProUGUI moneyText;

        [Header("Confirmation")]
        [SerializeField] private GameObject confirmationCanvas;
        [SerializeField] private UIObjectEffector confirmationPanel;
        [SerializeField] private Image confirmationSprite;
        [SerializeField] private TextMeshProUGUI confirmationName;
        [SerializeField] private TextMeshProUGUI confirmationPrice;
        [SerializeField] private Button confirmButton;

        [Header("Option Buttons")]
        [SerializeField] private Button purchaseModeButton;
        [SerializeField] private Button sellingModeButton;

        private GameObject _currentButton;
        private Clothing _clothingToBuy;
        private Item _itemToSell;

        //Open the shopping panel
        public override void OpenSpecialPanel()
        {
            base.OpenSpecialPanel();

            moneyText.text = "Money $" + GameManager.Instance.CurrentMoney();
            LoadPurchaseOptions();

            shoppingPanel.gameObject.SetActive(true);
        }

        //Close the shopping panel
        public override void CloseSpecialPanel()
        {
            base.CloseSpecialPanel();

            shoppingPanel.Deactivate(action: () =>
            {
                GameManager.Instance.SetPlayerControl(true);
            });
        }

        #region Purchase

        //Load all the available clothes to purchase
        public void LoadPurchaseOptions()
        {
            SetOptionButtons(true);
            contentBoxUpperText.text = "On sale!";

            var availableClothes = GameManager.Instance.GetAvailableClothes();

            foreach (Transform option in shopContentBox)
            {
                Destroy(option.gameObject);
            }

            foreach (var clothing in availableClothes)
            {
                SetUpNewPurchaseButton(clothing);
            }
        }

        //Sets up the buttons responsible for buy it's clothing
        //This update the price text, icon sprite and name text
        private void SetUpNewPurchaseButton(Clothing clothing)
        {
            var newButtonObject = Instantiate(buttonTemplate, shopContentBox);
            newButtonObject.transform.TryGetComponent(out ShopButton newButton);

            //Item icon
            newButton.Icon().sprite = clothing.Icon();

            //Item name
            newButton.Name().text = clothing.Name();

            //Item price
            newButton.Price().text = "$" + clothing.PurchasePrice();
            newButton.Price().color = clothing.PurchasePrice() > GameManager.Instance.CurrentMoney() ? Color.red : Color.green;

            newButton.Button().onClick.AddListener(() =>
            {
                _currentButton = newButtonObject;
                _clothingToBuy = clothing;
                PlayUIClick();

                UpdateConfirmationPanel(clothing, ConfirmPurchase, false);
                confirmationPanel.gameObject.SetActive(true);
                confirmationCanvas.SetActive(true);
            });
        }

        //Update the confirmation panel to set up based on the item of the triggered button
        private void UpdateConfirmationPanel(Item item, Action confirmationAction, bool defaultColor)
        {
            //Item icon
            confirmationSprite.sprite = item.Icon();

            //Item name
            confirmationName.text = item.Name();

            //Item price
            confirmationPrice.text = "$" + item.PurchasePrice();

            if (defaultColor)
            {
                confirmationPrice.color = Color.white;
            }
            else
            {
                confirmationPrice.color = item.PurchasePrice() > GameManager.Instance.CurrentMoney() ? Color.red : Color.green;
            }

            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(() => 
            {
                confirmationAction.Invoke();
            });
        }

        //Confirm the purchase and update the sale list, but only if the player has enough money
        public void ConfirmPurchase()
        {
            if (!GameManager.Instance.RemoveMoney(_clothingToBuy.PurchasePrice()))
            {
                AudioManager.Instance.PlaySFX("s_OperationFailed");
                return;
            }

            AudioManager.Instance.PlaySFX("s_OperationSuccessful");
            CloseConfirmationPanel();
            Destroy(_currentButton);

            GameManager.Instance.RegisterNewItemToPlayerInventory(_clothingToBuy);
            moneyText.text = "Money $" + GameManager.Instance.CurrentMoney();

            UpdateButtonPriceTexts();
        }

        //After purchase, update the button's price text color: green to "can buy" and red to "too expensive"
        private void UpdateButtonPriceTexts()
        {
            foreach (Transform option in shopContentBox)
            {
                option.transform.TryGetComponent(out ShopButton buttonToChange);
                int optionValue = int.Parse(buttonToChange.Price().text.Substring(1));

                buttonToChange.Price().color = optionValue > GameManager.Instance.CurrentMoney() ? Color.red : Color.green;
            }
        }
        #endregion

        #region Selling

        //Load all the items in the player inventory with a selling price bigger than 0
        public void LoadItemsToSell()
        {
            SetOptionButtons(false);
            contentBoxUpperText.text = "Your inventory";

            var items = GameManager.Instance.GetPlayerInventory(true);

            foreach (Transform option in shopContentBox)
            {
                Destroy(option.gameObject);
            }

            foreach (var item in items)
            {
                SetUpItemToSell(item);
            }
        }

        //Sets up the buttons responsible selling it's items
        //This update the selling text, icon sprite and name text
        private void SetUpItemToSell(Item item)
        {
            var newButtonObject = Instantiate(buttonTemplate, shopContentBox);
            newButtonObject.transform.TryGetComponent(out ShopButton newButton);

            //Item icon
            newButton.Icon().sprite = item.Icon();

            //Item name
            newButton.Name().text = item.Name();

            //Item price
            newButton.Price().text = "$" + item.PurchasePrice();

            newButton.Button().onClick.AddListener(() =>
            {
                _currentButton = newButtonObject;
                _itemToSell = item;

                UpdateConfirmationPanel(item, ConfirmItemSelling, true);
                confirmationPanel.gameObject.SetActive(true);
                confirmationCanvas.SetActive(true);
            });
        }

        //Confirm the item sale and updates the player inventory
        private void ConfirmItemSelling()
        {
            CloseConfirmationPanel();
            Destroy(_currentButton);
            AudioManager.Instance.PlaySFX("s_OperationSuccessful");

            GameManager.Instance.RemoveItemFromPlayerInventory(_itemToSell);
            GameManager.Instance.AddMoney(_itemToSell.PurchasePrice());
            moneyText.text = "Money $" + GameManager.Instance.CurrentMoney();
        }
        #endregion

        //Close the confirmation panel
        public void CloseConfirmationPanel()
        {
            confirmationPanel.Deactivate(action: () =>
            {
                confirmationCanvas.SetActive(false);
            });
        }

        //Function to avoid triggering the same button twice
        private void SetOptionButtons(bool purchasing)
        {
            purchaseModeButton.interactable = !purchasing;
            sellingModeButton.interactable = purchasing;
        }
    }
}
