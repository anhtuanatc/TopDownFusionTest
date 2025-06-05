using Fusion;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [Networked] private Color _playerColor { get; set; }
    [Networked] public string PlayerName { get; set; }

    public Color PlayerColor
    {
        get => _playerColor;
        set => _playerColor = value;
    }

    [SerializeField] private Renderer playerRenderer;
    [SerializeField] private TMPro.TextMeshPro nameTag;

    void Awake()
    {
        if (playerRenderer == null)
            playerRenderer = GetComponentInChildren<Renderer>();
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            PlayerName = "Player_" + Random.Range(1000, 9999);
            PlayerColor = Random.ColorHSV();
            RPC_UpdateAppearance(PlayerName, PlayerColor);

            // Add camera follow
            CameraFollow camFollow = Camera.main.GetComponent<CameraFollow>();
            if (camFollow != null)
                camFollow.target = this.transform;
        }

        UpdateVisuals();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_UpdateAppearance(string name, Color color)
    {
        PlayerName = name;
        PlayerColor = color;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (playerRenderer != null)
        {
            playerRenderer.material.color = PlayerColor;
        }

        if (nameTag != null)
        {
            nameTag.text = PlayerName;
        }
    }

}
