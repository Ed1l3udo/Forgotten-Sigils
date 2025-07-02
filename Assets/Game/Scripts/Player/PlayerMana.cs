using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private ManaUI manaPanel;
    [SerializeField] private int maxMana = 10;
    [SerializeField] public float manaRegenRate = 5f;
    private float manaRegenTimer = 0f;
    private int currentMana;

    void Start()
    {
        currentMana = maxMana;
        manaPanel.UpdateUI(currentMana);
    }

    void RegenerateMana(){

    if (currentMana < maxMana){
        manaRegenTimer += manaRegenRate * Time.deltaTime;

        if (manaRegenTimer >= 1f)
        {
            int manaToAdd = Mathf.FloorToInt(manaRegenTimer);
            currentMana = Mathf.Min(currentMana + manaToAdd, maxMana);
            manaRegenTimer -= manaToAdd;
            manaPanel.UpdateUI(currentMana);
        }
    }
    
    else{
        manaRegenTimer = 0f;
    }
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

    public bool TemMana(int amount){
        return currentMana >= amount;
    }

    void Update(){
        RegenerateMana();
    }

}
