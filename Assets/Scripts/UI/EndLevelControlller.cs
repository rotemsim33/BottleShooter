#pragma warning disable 649

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelControlller : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private AudioSource pickBtnSound;
    [SerializeField] private String selectableTag = "selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private GameObject highScoresPopUp;
    [SerializeField] private GameObject levelLoader;
    [SerializeField] private bool isWining =false;
    [SerializeField] private ParticleSystem[] confettis;

    //for Rycast
    private Transform _selection;
    RaycastHit hit;
    private string m_hited;
    private bool m_hitedObj;
    private bool hittSelcetedTag;

    //for fading level
    private LevelLoader level;


    void Start()
    {

        level = levelLoader.GetComponent<LevelLoader>();
        if (isWining)
        {
            confettis[0].Play();
            confettis[1].Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for back from highlighted
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Image>();
            selectionRenderer.material = null;
            _selection = null;
        }

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            var selection = hit.transform;
            hittSelcetedTag = selection.CompareTag(selectableTag);
            if (hittSelcetedTag)
            {
                var selectionRenderer = selection.GetComponent<Image>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;

            }
            if (hit.rigidbody != null)
            {
                m_hited = hit.rigidbody.transform.name.ToLower();
                m_hitedObj = true;
            }
        }

        if (Input.anyKeyDown && m_hitedObj && hittSelcetedTag)
        {
            pickBtnSound.Play();
            m_hitedObj = false;
            switch (m_hited)
            {
                case "restart":
                    level.startLoder(1);
                    break;
                case "backtomainmenubtn":
                    level.startLoder(0);
                    //reset total score
                    StaticObjectScript.totalScore = 0;
                    break;
                case "nextlevelbtn":
                    level.startLoder(SceneManager.GetActiveScene().buildIndex + 1);
                    break;
                case "highestscoresbtn":
                    highScoresPopUp.SetActive(true);
                    break;
                case "xbutton":
                    highScoresPopUp.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }


}



