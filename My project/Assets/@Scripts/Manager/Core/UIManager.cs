using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enum;

public class UIManager : SingletonBase<UIManager>
{
    private Transform _root;
    private Dictionary<UIType, GameObject> _uiDic = new Dictionary<UIType, GameObject>();
    [SerializeField] private Stack<UIBase> _popupSystem = new Stack<UIBase>();
    private int _currentHighOrder;
    public void Init()
    {
        _currentHighOrder = 0;

        if (_root == null)
        {
            _root = new GameObject().transform;
            _root.gameObject.name = "@UIRoot(720x1280)";
            _root.transform.position = Vector3.zero;
            _root.transform.rotation = Quaternion.identity;
            _root.transform.localScale = Vector3.one;
            DontDestroyOnLoad(_root.gameObject);
        }
        LoadUIPopup();
    }

    private void LoadUIPopup()
    {
        _uiDic.Clear();
        _popupSystem.Clear();

        GameObject[] _uiPopups = Resources.LoadAll<GameObject>(Constant.UI_PREFAB_PATH);

        foreach (GameObject uiBase in _uiPopups)
        {
            UIType type = (UIType)System.Enum.Parse(typeof(UIType), uiBase.name);
            _uiDic.Add(type, uiBase);
        }

    }

    public void Push(UIType type)
    {
        GameObject targetPopup = null;
        if(_uiDic.TryGetValue(type, out targetPopup))
        {
            //UI Popup Instantiate
            GameObject go = Instantiate(targetPopup);
            go.transform.SetParent(_root);
            go.transform.position = Vector3.zero;
            go.transform.rotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            
            UIBase uiBase = go.GetComponent<UIBase>();
            if(uiBase != null)
            {
                

                //UI Popup Init
                uiBase.Init();

                //Set UI Order
                _currentHighOrder++;
                uiBase.SetCanvasLayer(_currentHighOrder);

                //add Stack
                _popupSystem.Push(uiBase);
                Debug.Log("Stack : " + _popupSystem.Count);
            }
            else
            {
                Debug.LogError($"{type.ToString()} UIBase not added!");
            }
        }
        else
        {
            Debug.LogError($"{type.ToString()} not exist!");
        }

    }

    public void Pop()
    {
        UIBase targetUI = _popupSystem.Pop();
        Destroy(targetUI.gameObject);
        _currentHighOrder--;
    }

    public bool Clear()
    {
        if (_popupSystem.Count == 0)
            return true;

        while(_popupSystem.Count > 0)
        {
            UIBase targetUI = _popupSystem.Pop();
            Destroy(targetUI.gameObject);
            _currentHighOrder--;
        }
        return true;
    }

    public void Change(UIType type)
    {
        if (Clear())
            Push(type);
    }
}
