using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileBrowser : MonoBehaviourDpm
{
    private string _folderPath;
    [SerializeField] private Transform content; // Reference of scroll view content.
    [SerializeField] private GameObject fileButtonPrefab; // Button prefab for select files.
    
    // TODO: style
    private float _currentY = 0f; // Inicializa la posición Y actual
    private float _verticalSpacing = 30f; // Espacio vertical entre los botones

    private void Awake()
    {
        SetLogger(name, "#208AAE");
        // _folderPath = "C:\\dpm/";
        _folderPath = Application.streamingAssetsPath + "/";
    }

    private void Start()
    {
        LoadFileList();
    }

    public void LoadFileList()
    {
        DpmLogger.Log("Loading file list... " + _folderPath);

        _currentY = 0f;
        
        string[] files = Directory.GetFiles(_folderPath);
        var group = files.GroupBy(Path.GetFileNameWithoutExtension);

        DpmLogger.Log("Mounting buttons... ");
        foreach (var pair in group)
        {
            string fileName = pair.Key;
            string midiFile = pair.FirstOrDefault(file => file.EndsWith(ConstantResources.FileExtensionMidi));
            string mp3File = pair.FirstOrDefault(file => file.EndsWith(ConstantResources.FileExtensionMp3));
            
            // If pair exists.
            if (!string.IsNullOrEmpty(midiFile) && !string.IsNullOrEmpty(mp3File)) MountButton(fileName);
        }
    }

    private void MountButton(string filePath)
    {
        GameObject fileButton = Instantiate(fileButtonPrefab, content);
        Button buttonText = fileButton.GetComponent<Button>();
        TextMeshProUGUI text = buttonText.GetComponentInChildren<TextMeshProUGUI>();
        
        string fileName = Path.GetFileName(filePath);
        text.text = fileName.TrimEnd(ConstantResources.FileExtensionMidi.ToCharArray());

        // Calcula la posición Y del botón y actualiza la posición actual
        RectTransform buttonRectTransform = fileButton.GetComponent<RectTransform>();
        buttonRectTransform.anchoredPosition = new Vector2(0f, -_currentY);
        _currentY += buttonRectTransform.rect.height + _verticalSpacing;
        
        // Agrega un Listener para manejar la selección del archivo
        Button buttonComponent = fileButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => SelectFile(filePath));
    }

    private void SelectFile(string fileName)
    {
        DpmLogger.Log("File Selected: " + fileName);
        SongHolder.Instance.midiPath = _folderPath + fileName + ConstantResources.FileExtensionMidi;
        SongHolder.Instance.mp3Path = _folderPath + fileName + ConstantResources.FileExtensionMp3;
        SongHolder.Instance.songTitle = fileName;
        SceneManager.LoadScene(ConstantResources.Scenes.MainGameScene);
    }
}
