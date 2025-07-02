using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private ManaUI manaPanel;
    [SerializeField] private int maxMana = 10;
    private int currentMana;

    void Start()
    {
        currentMana = maxMana;
        manaPanel.UpdateUI(currentMana);
    }

    public void UseMana(int amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana); // impede mana negativa ou maior que o máximo
        manaPanel.UpdateUI(currentMana);
    }

    public void RecoverMana(int amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        manaPanel.UpdateUI(currentMana);
    }
}
