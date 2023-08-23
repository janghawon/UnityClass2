using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBar : MonoBehaviour
{
    private UIDocument _uiDoc;
    private VisualElement _panelElement;
    private DialogPanelUI _panelUI;
    private BulletUI _bulletUI;

    private bool _isOn = false;
    public bool IsOn
    {
        get => _isOn;
        set
        {
            _panelUI.Show(value);
            _isOn = value;
        }
    }

    private Camera _mainCam;

    public void SetText(string text)
    {
        _panelUI.Text = text;
    }

    private void Awake()
    {
        _uiDoc = GetComponent<UIDocument>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void OnEnable()
    {
        var panelRoot = _uiDoc.rootVisualElement.Q("DialogPanel");
        _panelUI = new DialogPanelUI(panelRoot, "..");

        var bulletRoot = _uiDoc.rootVisualElement.Q("BulletContainer");
        _bulletUI = new BulletUI(bulletRoot, 7);
    }

    public void SetBullet(int cnt)
    {
        _bulletUI.BulletCount = cnt;
    }

    public void LookToCamera()
    {
        transform.rotation = _mainCam.transform.rotation;
    }
}
