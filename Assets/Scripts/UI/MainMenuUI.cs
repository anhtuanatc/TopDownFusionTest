using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public InputField nameInput;
    public Slider colorSlider;
    public Image colorPreview;
    public Button startButton;

    public GameObject connectingPanel;

    public static Color selectedColor = Color.green;

    private void Start()
    {
        colorSlider.onValueChanged.AddListener(OnColorSliderChanged);
        startButton.onClick.AddListener(OnStartGame);
        OnColorSliderChanged(colorSlider.value); // Init Color

        connectingPanel.SetActive(false);
    }

    private void OnColorSliderChanged(float value)
    {
        selectedColor = Color.HSVToRGB(value, 1, 1); // Convert slider to color
        colorPreview.color = selectedColor;
    }

    private void OnStartGame()
    {
        string playerName = nameInput.text.Trim();
        if (string.IsNullOrEmpty(playerName))
            playerName = "Player_" + Random.Range(1000, 9999);

        GameSettings.PlayerName = playerName;
        GameSettings.PlayerColor = selectedColor;

        //Hide MainMenu and Show Connecting
        gameObject.SetActive(false);
        connectingPanel.SetActive(true);

        NetworkManager.Instance.StartMultiplayer();
    }
}
