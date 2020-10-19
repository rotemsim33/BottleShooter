#pragma warning disable 649
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCam;

    [SerializeField] private GameObject howToPlayPopUp1;
    [SerializeField] private GameObject howToPlayPopUp2;

    [SerializeField] private String selectableTag = "selectable";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private AudioSource pickBtnSound;
    [SerializeField] private GameObject levelLoader;

    private Transform _selection;
    private RaycastHit hit;
    private string m_hited;
    private bool m_hitedObj;
    private bool hittSelcetedTag;

    private LevelLoader level;


    void Start()
    {
        mainCam.transform.Rotate(0, 3, 0, Space.Self);
        level = levelLoader.GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {

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
                case "play":
                    level.startLoder(1);
                    break;
                case "howtoplay":
                    howToPlayPopUp1.SetActive(true);
                    break;
                case "xbutton":
                    howToPlayPopUp1.SetActive(false);
                    howToPlayPopUp2.SetActive(false);
                    break;
                case "next":
                    howToPlayPopUp2.SetActive(true);
                    howToPlayPopUp1.SetActive(false);
                    break;
                case "back":
                    howToPlayPopUp2.SetActive(false);
                    howToPlayPopUp1.SetActive(true);
                    break;
                case "exit":
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }


}


/* if (_selection ! )
        {
            var selectionRenderer = _selection.GetComponent<Image>();
            selectionRenderer.material  ;
            _selection  ;
        }
        m_ray = m_mainCam.ScreenPointToRay(new Vector3(Screen.width / 4f, Screen.height / 2f, 0f));
        if (Physics.Raycast(m_ray, out m_hit))
        {
            var selection = m_hit.transform;
            hittSelcetedTag = selection.CompareTag(selectableTag);
            if (hittSelcetedTag)
            {
                var selectionRenderer = selection.GetComponent<Image>();
                if (selectionRenderer ! )
                {
                    selectionRenderer.material = highlightMaterial;


                }
                _selection = selection;

            }
            if (m_hit.rigidbody ! )
            { 
                m_hited = m_hit.rigidbody.transform.name.ToLower();
                m_hitedObj = true;
            }
        }

    */