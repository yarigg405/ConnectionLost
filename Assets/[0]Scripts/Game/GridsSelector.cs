using UnityEngine;


namespace Game
{
    internal sealed class GridsSelector : MonoBehaviour
    {
        [SerializeField] private Grid[] grids;
        private int _currentGridNum = -1;
        private Grid _currGrid => grids[_currentGridNum];


        public Transform GetCurrentGrid()
        {
            return _currGrid.NodesContainer;
        }

       

        public void NextGrid()
        {
            DeselectCurrent();

            _currentGridNum++;
            if (_currentGridNum >= grids.Length)
            {
                _currentGridNum = 0;
            }

            SelectNewCurrent();
        }

        private void DeselectCurrent()
        {
            if (_currentGridNum < 0) return;

            if (_currGrid)
                _currGrid.SetDeselected();
        }

        private void SelectNewCurrent()
        {
            if (_currentGridNum < 0) return;

            _currGrid.SetSelected();
        }
    }
}
