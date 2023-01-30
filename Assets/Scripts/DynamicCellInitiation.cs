using System.Collections.Generic;
using UnityEngine;

public class DynamicCellInitiation : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject _cellPrefab;
    private RectTransform _rectTransform;
    private readonly List<GameObject> _cells;

    #endregion


    #region Unity LifeCycle

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    #endregion


    #region Private Methods

    
    private void HpChanged(int hp)
    {
        DestroyCurrentCells();
        CreateNewCell(hp);
    }

    private void CreateNewCell(int hp)
    {
        for (int i = 0; i < hp; i++)
        {
            GameObject cell = Instantiate(_cellPrefab, _rectTransform);
            _cells.Add(cell);
        }
    }

    private void DestroyCurrentCells()
    {
        foreach (GameObject cell in _cells)
        {
            Destroy(cell);
        }

        _cells.Clear();
    }

    #endregion
}
  
