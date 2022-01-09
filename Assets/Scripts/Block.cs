using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField]
    private GameObject diamondPrefab, comboPrefab;

    private GameObject currentDiamond, player;

    private void Start()
    {
        player = GameObject.Find("Player");
        if (Random.Range(0, 5) > 0) return;
        currentDiamond = Instantiate(diamondPrefab);
        currentDiamond.transform.position = transform.position + diamondPrefab.transform.position;
    }

    private void Update()
    {
        if (!player) return;
        float playerPosZ = player.transform.position.z;
        float currentZ = transform.position.z;
        if(playerPosZ - currentZ > 15f)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.updateComboAnimation -= UpdateComboAnimation;
        if (!currentDiamond) return;
        Destroy(currentDiamond);
    }

    public void SubscribeToMethod()
    {
        GameManager.instance.updateComboAnimation += UpdateComboAnimation;
    }

    void UpdateComboAnimation(bool isCombo)
    {
        comboPrefab.SetActive(isCombo);
    }
}
