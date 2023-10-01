using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileBrowser : MonoBehaviour
{
    private DPMLogger _dpmLogger;

    public Transform content; // Referencia al contenido del ScrollView
    public GameObject fileButtonPrefab; // Prefab del botón de archivo
    
    private float _currentY = 0f; // Inicializa la posición Y actual
    private float _verticalSpacing = 30f; // Espacio vertical entre los botones

    private void Awake()
    {
        _dpmLogger = new DPMLogger(this.name, "#FF5733");
    }

    private void Start()
    {
        LoadFileList();
    }

    public void LoadFileList()
    {
        string[] files = Directory.GetFiles(ConstantResources.FolderPath);
        _currentY = 0f;

        foreach (string filePath in files)
        {
            // If file is a midi.
            if (filePath.EndsWith(ConstantResources.FileExtensionMidi)) ProcessMidiFile(filePath);
            
            // If file is a mp3.
            if (filePath.EndsWith(ConstantResources.FileExtensionMp3)) ProcessMp3File(filePath);
        }
    }

    private void ProcessMidiFile(string filePath)
    {
        string fileName = Path.GetFileName(filePath);
        GameObject fileButton = Instantiate(fileButtonPrefab, content);
        Button buttonText = fileButton.GetComponent<Button>();
        TextMeshProUGUI text = buttonText.GetComponentInChildren<TextMeshProUGUI>();
        text.text = fileName.TrimEnd(ConstantResources.FileExtensionMidi.ToCharArray());

        // Calcula la posición Y del botón y actualiza la posición actual
        RectTransform buttonRectTransform = fileButton.GetComponent<RectTransform>();
        buttonRectTransform.anchoredPosition = new Vector2(0f, -_currentY);
        _currentY += buttonRectTransform.rect.height + _verticalSpacing;

        // Agrega un Listener para manejar la selección del archivo
        Button buttonComponent = fileButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => SelectFile(filePath));
    }

    private void ProcessMp3File(string filePath)
    {
        
    }

    private void SelectFile(string filePath)
    {
        Debug.Log("File Selected: " + filePath);
        SongHolder.Instance.songPath = filePath;
        SceneManager.LoadScene(ConstantResources.Scenes.LoadingScene);
    }
}
