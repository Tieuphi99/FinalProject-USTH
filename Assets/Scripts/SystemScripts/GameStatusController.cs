using TMPro;
using UnityEngine;

namespace SystemScripts
{
    public class GameStatusController : MonoBehaviour
    {
        public TextMeshProUGUI playerNameText;
        public TextMeshProUGUI playerScoreText;
        public TextMeshProUGUI collectedCoinText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI secondsText;

        public void SetScore(string score)
        {
            switch (score.Length)
            {
                case 1:
                    playerScoreText.SetText("000000");
                    break;
                case 3:
                    playerScoreText.SetText($"000{score}");
                    break;
                case 4:
                    playerScoreText.SetText($"00{score}");
                    break;
                case 5:
                    playerScoreText.SetText($"0{score}");
                    break;
                case 6:
                    playerScoreText.SetText(score);
                    break;
            }
        }

        public void SetCoin(int coin)
        {
            if (coin > 0)
            {
                collectedCoinText.SetText($"x0{coin}");
                if (coin <= 9) return;
                collectedCoinText.SetText($"x{coin}");
                if (coin > 99)
                {
                    collectedCoinText.SetText("x00");
                }
            }
            else
            {
                collectedCoinText.SetText("x00");
            }
        }

        public void SetName(string playerName)
        {
            playerNameText.SetText(playerName);
        }

        public void SetTime(float second)
        {
            if (second > 0)
            {
                if (second >= 100)
                {
                    secondsText.SetText(Mathf.RoundToInt(second).ToString());
                }
                else if (second >= 10)
                {
                    secondsText.SetText($"0{Mathf.RoundToInt(second).ToString()}");
                }
                else
                {
                    secondsText.SetText($"00{Mathf.RoundToInt(second).ToString()}");
                }
            }
            else
            {
                secondsText.SetText("000");
            }
        }

        public void SetLevel(string level)
        {
            levelText.SetText(level);
        }
    }
}