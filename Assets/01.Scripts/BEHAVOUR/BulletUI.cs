using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletUI : MonoBehaviour
{
    private int _currentCnt;
    private int _maxCount;
    private List<VisualElement> _bulletList;
    public int BulletCount
    {
        get => _currentCnt;
        set
        {
            _currentCnt = Mathf.Clamp(value, 0, _maxCount);
            DrawBulllet();
        }
    }
    public BulletUI(VisualElement root, int maxCount)
    {
        _bulletList = root.Query(className: "bullet").ToList();
    }

    private void DrawBulllet()
    {
        for(int i = 0; i < _bulletList.Count; i++)
        {
            if(i < _currentCnt - 1)
            {
                _bulletList[i].visible = true;
            }
            else
            {
                _bulletList[i].visible = false;
            }
        }
    }
}
