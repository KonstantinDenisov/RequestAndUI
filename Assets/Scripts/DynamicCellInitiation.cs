using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DynamicCellInitiation : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _cellPrefab;
    private RectTransform _rectTransform;
    private List<GameObject> _cells;

    #endregion
    
    #region Unity LifeCycle

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    #endregion
    
    #region Public Methods

    public void DestroyCurrentCells()
    {
        foreach (GameObject cell in _cells)
        {
            Destroy(cell);
        }

        _cells.Clear();
    }

    public IEnumerator CreateNewCell(List<User> _users)
    {
        for (int i = 0; i < _users.Count; i++)
        {
            GameObject cell = Instantiate(_cellPrefab, _rectTransform);
            
            SpriteRenderer spriteRenderer = cell.GetComponentInChildren<SpriteRenderer>();
            //spriteRenderer.sprite = Sprite.Create();
            
            TextMeshProUGUI textMeshProUGUI = cell.GetComponentInChildren<TextMeshProUGUI>();
            textMeshProUGUI.text = $"{_users[i].Username} + {_users[i].Points}";
            
            _cells.Add(cell);
            yield return new WaitForSeconds(1);
        }

        yield return null;
    }

    #endregion
}
  
