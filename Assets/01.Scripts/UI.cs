using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    PlayerInputlogic pl;
    UIDocument _doc;

    VisualElement root;
    Label _jumbLabel;
    Label _fireLabel;

    private void Awake()
    {
        pl = FindObjectOfType<PlayerInputlogic>();
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        root = _doc.rootVisualElement;
        _jumbLabel = root.Q<Label>("KeyLabel_jump");
        _fireLabel = root.Q<Label>("KeyLabel_fire");

        _jumbLabel.RegisterCallback<MouseDownEvent>(OnJumpClicked);
        _fireLabel.RegisterCallback<MouseDownEvent>(OnFIreClicked);
    }

    private void OnJumpClicked(MouseDownEvent evt)
    {
        _jumbLabel.text = "Listening...";
        KeyReplace(pl.keyAction.Player.Jump);
    }

    private void OnFIreClicked(MouseDownEvent evt)
    {
        _jumbLabel.text = "Listening...";
        KeyReplace(pl.keyAction.Player.Fire);
    }

    public void KeyReplace(InputAction dd)
    {
        pl.keyAction.Player.Disable();
        dd.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<keyboard>/escape")
            .OnComplete(op =>
            {
                Debug.Log(op.selectedControl);
                op.Dispose();
                pl.keyAction.Player.Enable();
                SaveKeyInfo();
            })
            .OnCancel(op =>
            {
                Debug.Log("Cancel");
                op.Dispose();
                pl.keyAction.Player.Enable();
            })
            .Start();
    }

    private void SaveKeyInfo()
    {
        string json = pl.keyAction.SaveBindingOverridesAsJson();
        Debug.Log(json);

        PlayerPrefs.SetString("keyInfo", json);
    }
}
