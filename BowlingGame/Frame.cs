using System;
using System.Collections.Generic;
using System.Text;

namespace BowlingApp
{
    public class Frame
    {
        public Frame(int frameNumber)
        {
            RemainingPins = 10;
            FrameNumber = frameNumber;
        }

        public int Roll()
        {
            Console.WriteLine("Press any key to make a Roll");
            Console.ReadKey();
            //We want to score, so adding some rules to give us some points and no sucky games!!!
            if (Score == 0 && FrameNumber % 2 != 0)
                return new Random().Next(5, RemainingPins);
            if (Score == 0 && FrameNumber % 2 == 0)
                return 10;
            return new Random().Next(1, RemainingPins);
        }

        public int FrameNumber { get; set; }

        public int Score { get; set; }

        public int RollsToScore { get; set; }

        public int RemainingPins { get; set; }

        public bool Strike { get; set; }

        public bool Spare { get; set; }

    }
}
