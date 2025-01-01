using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TotalCash : MonoBehaviour
{
    public static TotalCash _TotalCashManager;

    [SerializeField] private int _TotalCash;
    [SerializeField] private string _TotalCashTextTagName;

    public List<CashButtonHandler> _CashButtonHandlers;

    [System.Serializable]
    public class CashButtonHandler
    {
        public Button _button;
        public bool _Adding;
        public int _CashAmount;
        public bool _RandomAmount;
        public int _RandomMin;
        public int _RandomMax;
    }

    public List<TMP_Text> _TotalMoneyTMPList = new List<TMP_Text>();

    private void Awake()
    {
        if (_TotalCashManager == null)
        {
            _TotalCashManager = this;
            DontDestroyOnLoad(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (_TotalCashManager != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeTMPList();
        InitializeButtonHandlers();
        UpdateText();
    }

    private void OnDestroy()
    {
        if (_TotalCashManager == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeTMPList();
        UpdateText();
    }

    private void InitializeTMPList()
    {
        _TotalMoneyTMPList.Clear();

        // Find all TMP_Text components with the specified tag
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(_TotalCashTextTagName);
        foreach (GameObject obj in taggedObjects)
        {
            TMP_Text tmpText = obj.GetComponent<TMP_Text>();
            if (tmpText != null)
            {
                _TotalMoneyTMPList.Add(tmpText);
            }
        }
    }

    private void InitializeButtonHandlers()
    {
        foreach (var handler in _CashButtonHandlers)
        {
            if (handler._button != null)
            {
                handler._button.onClick.RemoveAllListeners(); // Clear previous listeners to avoid duplicates
                handler._button.onClick.AddListener(() =>
                    CashHandler(handler._Adding, handler._CashAmount, handler._RandomAmount, handler._RandomMin, handler._RandomMax));
            }
        }
    }

    public void CashHandler(bool _Adding, int _CashAmount, bool _RandomAmount, int _RandomMin, int _RandomMax)
    {
        if (_RandomAmount)
        {
            if (_Adding)
            {
                _TotalCash += Random.Range(_RandomMin, _RandomMax);
            }
            else
            {
                _TotalCash -= Random.Range(_RandomMin, _RandomMax);
            }
        }
        else
        {
            if (_Adding)
            {
                _TotalCash += _CashAmount;
            }
            else
            {
                _TotalCash -= _CashAmount;
            }
        }

        UpdateText();
    }

    public void IncrementCash(int _CashAmount)
    {
        _TotalCash += _CashAmount;
        UpdateText();
    }

    public void DecrementCash(int _CashAmount)
    {
        _TotalCash -= _CashAmount;
        UpdateText();
    }

    public void UpdateText()
    {
        foreach (var item in _TotalMoneyTMPList)
        {
            item.text = "Cash " + _TotalCash.ToString();
        }
    }
}
