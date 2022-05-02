using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BallSortPuzzle
{
    using Utilities;

    [CreateAssetMenu(fileName = "Level Data", menuName = "Ball Sort Puzzle/Data/Level Data")]
    public class LevelData : ScriptableObject
    {
        [System.Serializable]
        public class TubeData
        {
            public List<int> indexes;

            public TubeData() => indexes = new List<int>();
        }

        #region Field
        [SerializeField] private int levelNumber;
        [SerializeField, Range(2, 13)] private int tubeAmount;
        [SerializeField, Range(1, 2)] private int extraTube;
        [SerializeField, Range(4, 5)] private int ballPerTube;
        [SerializeField] private int rewardAmount;
        [SerializeField] private ColorPallete colorPallete;
        [SerializeField] private List<TubeData> tubeDatas;
        [SerializeField] private bool solvable;
        #endregion

        #region Properties
        public int TubeAmount => tubeAmount;
        public int BallPerTube => ballPerTube;
        public int ExtraTube => extraTube;
        public int RewardAmount => rewardAmount;
        public List<TubeData> TubeDatas => tubeDatas;
        public ColorPallete ColorPallete => colorPallete;
        #endregion

        #region Helper
        private void OnValidate()
        {
            levelNumber = int.Parse(Regex.Match(name, @"\d+").Value);
            if (tubeDatas != null)
            {
                if (tubeAmount != tubeDatas.Count || ballPerTube != tubeDatas[0].indexes.Count)
                    GenerateData();
            }
        }
        public string GetLevelName(string type) => string.Concat(type, levelNumber);

        public void GenerateData()
        {
            if (tubeDatas != null)
            {
                tubeDatas.Clear();
                tubeDatas = null;
            }


            if (tubeAmount <= 0)
            {
                Debug.Log("Please Specify how many initial tube");
                return;
            }

            if (ballPerTube <= 0)
            {
                Debug.Log("Please specify how many ball present on tube");
                return;
            }

            List<int> temp = new List<int>();
            for (int i = 0; i < tubeAmount; i++)
            {
                for (int j = 0; j < ballPerTube; j++)
                    temp.Add(i);
            }

            temp.Shuffle();
            tubeDatas = new List<TubeData>();
            int counter = 0;
            for (int i = 0; i < tubeAmount; i++)
            {
                TubeData tube = new TubeData();
                for (int j = 0; j < ballPerTube; j++, counter++)
                    tube.indexes.Add(temp[counter]);
                tubeDatas.Add(tube);
            }
        }

        //public void CheckValidity()
        //{
        //    var grid = SolverUtility.DataToGrid(this);
        //    var valid = SolverUtility.ValidGrid(grid, ballPerTube);
        //    Debug.Log(valid);
        //}
        #endregion
    }
}
