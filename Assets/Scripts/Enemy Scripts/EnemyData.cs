using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Data such as damage or health of enemies are kept in a seperate file, getters are make to access
public class EnemyData : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;

    public void DamageEnemy(int dmg) {
        health -= dmg;
        Debug.Log(health);
        CheckStatus();
    }

    public int GetDamage() {
        return damage;
    }

    public float GetSpeed() {
        return speed;
    }

    void CheckStatus() {
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
