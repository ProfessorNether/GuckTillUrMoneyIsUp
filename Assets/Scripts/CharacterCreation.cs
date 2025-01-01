using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterCreation : MonoBehaviour
{
    public List<CharacterItemMenu> _CharacterItemMenu;
    public string PrefabPath; // Path in Resources folder, e.g., "Prefabs/MyPrefab"

    [System.Serializable]
    public class CharacterItemMenu
    {
        public string _ItemName;
        public Button _LeftButton;
        public Button _RightButton;
        public TMP_Text _ItemTitle;
        public SpriteRenderer _BaseSprite;
        public List<Sprite> _WearableItemsList = new List<Sprite>();
        public int _CurrentIndex = 0;
    }

    private void Start()
    {
        InitializeCharacterItemsHandlers();
    }

    private void InitializeCharacterItemsHandlers()
    {
        for (int i = 0; i < _CharacterItemMenu.Count; i++)
        {
            int index = i;
            var handler = _CharacterItemMenu[index];

            if (handler._WearableItemsList != null)
            {
                handler._LeftButton.onClick.RemoveAllListeners();
                handler._RightButton.onClick.RemoveAllListeners();
                handler._LeftButton.onClick.AddListener(() => CharacterItemChange("Left", index));
                handler._RightButton.onClick.AddListener(() => CharacterItemChange("Right", index));
            }
        }
    }

    private void CharacterItemChange(string LeftOrRight, int _CurrentItemNum)
    {
        var menu = _CharacterItemMenu[_CurrentItemNum];

        if (LeftOrRight == "Left")
        {
            menu._CurrentIndex = Mathf.Max(0, menu._CurrentIndex - 1);
        }
        else if (LeftOrRight == "Right")
        {
            menu._CurrentIndex = Mathf.Min(menu._WearableItemsList.Count - 1, menu._CurrentIndex + 1);
        }

        menu._ItemTitle.text = $"{menu._ItemName} {menu._CurrentIndex + 1}";
        menu._BaseSprite.sprite = menu._WearableItemsList[menu._CurrentIndex];
    }
}