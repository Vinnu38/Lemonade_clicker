using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Button makeLemonadeButton, buyLemonButton, buySugarButton, buyCupButton, buyAutoSqueezerButton;
    public TextMeshProUGUI lemonadeCountText, moneyText, lemonText, sugarText, cupText, empireMessageText;
    public Slider priceSlider;

    private int lemonadeCount = 0;
    private float money = 0;
    private int lemons = 10, sugar = 10, cups = 10;
    private float lemonadePrice = 1;
    private bool hasAutoSqueezer = false;
    private float autoTimer = 0f;

    void Start()
    {
        makeLemonadeButton.onClick.AddListener(MakeLemonade);
        buyLemonButton.onClick.AddListener(() => BuyResource(ref lemons, 1, 1));
        buySugarButton.onClick.AddListener(() => BuyResource(ref sugar, 1, 1));
        buyCupButton.onClick.AddListener(() => BuyResource(ref cups, 1, 1));
        buyAutoSqueezerButton.onClick.AddListener(BuyAutoSqueezer);
        priceSlider.onValueChanged.AddListener(UpdateLemonadePrice);
        UpdateUI();
    }

    void Update()
    {
        if (hasAutoSqueezer)
        {
            autoTimer += Time.deltaTime;
            if (autoTimer >= 1f)
            {
                autoTimer = 0f;
                MakeLemonade();
            }
        }

        if (lemonadeCount >= 50 && empireMessageText.text == "")
        {
            empireMessageText.text = "The Lemonade Empire has begun!";
        }
    }

    void MakeLemonade()
    {
        if (lemons >= 1 && sugar >= 1 && cups >= 1)
        {
            lemons--;
            sugar--;
            cups--;
            lemonadeCount++;
            money += lemonadePrice;
            UpdateUI();
        }
    }

    void BuyResource(ref int resource, int amount, float cost)
    {
        if (money >= cost)
        {
            money -= cost;
            resource += amount;
            UpdateUI();
        }
    }

    void BuyAutoSqueezer()
    {
        if (!hasAutoSqueezer && money >= 100)
        {
            money -= 100;
            hasAutoSqueezer = true;
            buyAutoSqueezerButton.interactable = false;
            UpdateUI();
        }
    }

    void UpdateLemonadePrice(float value)
    {
        lemonadePrice = Mathf.Round(value);
        UpdateUI();
    }

    void UpdateUI()
    {
        lemonadeCountText.text = $"Lemonades Made: {lemonadeCount}";
        moneyText.text = $"Money: ${money:F2}";
        lemonText.text = $"Lemons: {lemons}";
        sugarText.text = $"Sugar: {sugar}";
        cupText.text = $"Cups: {cups}";
        priceSlider.GetComponentInChildren<TextMeshProUGUI>().text = $"Price: ${lemonadePrice}";
    }
}
