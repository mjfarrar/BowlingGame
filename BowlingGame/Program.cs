using System;

namespace BowlingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();

            Console.Write("Let's Bowl... Press any key to Begin the Game!");
            Console.ReadKey();

            //We'll take the player through all 10 Frames
            for (var i = 1; i <= 10; i++)
            {
                Frame frame = new Frame(i);
                Console.WriteLine("");
                Console.WriteLine("FRAME " + i + ": GAME SCORE " + game.GetScore());
                //Roll One
                PlayerRoll(game, frame, 1);
                //Roll Two
                if (!frame.Strike || frame.FrameNumber == 10)
                    PlayerRoll(game, frame, 2);
                //Roll Three only if we're at the 10th Frame
                if (frame.FrameNumber == 10)
                    PlayerRoll(game, frame, 3);
                game.FramesPlayed.Add(frame);
            }

            Console.WriteLine("");
            Console.WriteLine("FINAL SCORE = " + game.GetScore());
        }

        public static int PlayerRoll(Game game, Frame frame, int numberOfRoll)
        {
            int roll = frame.Roll();
            //Let's see if we got a Strike or a Spare
            switch (numberOfRoll)
            {
                //You can only roll a strike if it's your first roll and you knock down all 10 pins
                //Unless it's the 10th frame, then you can roll 3 strikes on the same frame!!!
                case 1:
                    frame.Strike = roll == 10 ? true : false;
                    if (frame.Strike)
                        frame.RollsToScore = 2;
                    break;

                //You can only role a spare if you knock down all remaining pins on your second roll
                //If you already have a strike, and it's the 10th frame, you can roll a strike again
                case 2:
                    if (frame.FrameNumber == 10 && frame.Strike)
                    {
                        frame.Strike = roll == 10 ? true : false;
                    }
                    else
                    {
                        frame.Spare = roll == frame.RemainingPins ? true : false;
                        if (frame.Spare)
                            frame.RollsToScore = 1;
                    }
                    break;
                //We can only get a strike on the 3rd roll if we got a strike on the second roll
                //We can only get a spare on the 3rd roll if it wasn't a strike on the second roll and we knock down all remaining pins
                case 3:
                    if (frame.Strike)
                        frame.Strike = roll == 10 ? true : false;
                    if (!frame.Strike)
                        frame.Spare = roll == frame.RemainingPins ? true : false;
                    break;
            }
            //These are both conditions that will be neccessary for the 10th frame
            //If we roll a strike we don't want to edit the remaining Pins
            //If we roll a spare then we want to reset the count of Pins to 10
            if (!frame.Strike)
                frame.RemainingPins -= roll;
            if (frame.FrameNumber == 10 && frame.Spare)
                frame.RemainingPins = 10;
            frame.Score += roll;
            Console.WriteLine("You knocked down " + roll + " pins.");
            switch (true)
            {
                case true when frame.Strike:
                    Console.WriteLine("You Rolled A Strike!!! Great Job!");
                    break;
                case true when frame.Spare:
                    Console.WriteLine("You Rolled A Spare! Nice!");
                    break;
            }
            game.UpdatePastFramesScore(roll);
            return roll;
        }
    }
}
