using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingApp
{
    public class Game
    {
        public Game()
        {
            FramesPlayed = new List<Frame>();
        }

        public void UpdatePastFramesScore(int roll)
        {
            foreach (var frame in FramesPlayed.Where(x => x.RollsToScore != 0))
            {
                frame.Score += roll;
                frame.RollsToScore--;
            }
        }

        public int GetScore()
        {
            return FramesPlayed.Select(x => x.Score).Sum();
        }

        public List<Frame> FramesPlayed { get; set; }
    }
}
