using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public int damageBonus;

    public Enemy enemyHoldingWeapon;


    private void Awake()
    {
        enemyHoldingWeapon = gameObject.GetComponent<Enemy>(); //Getting Golem which is the subclass
        EnemyEatsWeapon(enemyHoldingWeapon);
    }

    protected void EnemyEatsWeapon(Enemy enemy)
    {
        Debug.Log("Enemy eats weapon");
    }


    public void Recharge()
    {
        Debug.Log("Recharging Weapon!");
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
