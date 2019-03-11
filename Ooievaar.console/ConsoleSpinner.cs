using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ooievaar.Client
{
    public class ConsoleSpinner
    {
        public ConsoleSpinner()
        {
            _animations = new string[,] {
                { "/", "-", "\\", "|" },
                { "\\o\\", "|o|", "/o/", "|o|" },
                { ".   ", "..  ", "... ", "...." },
                { "=>   ", "==>  ", "===> ", "====>" }
            };
        }

        private int _counter { get; set; } = 0;
        private readonly string[,] _animations = null;
        public TimeSpan Dalay { get; set; } = TimeSpan.FromMilliseconds(100);

        public void Turn(string pMessage, int pAnimationSequence = 0)
        {
            _counter++;
            Thread.Sleep(Dalay);

            pAnimationSequence = pAnimationSequence > _animations.Length - 1 ? 0 : pAnimationSequence;
            int counterValue = _counter % 4;

            string message = $"{pMessage} {_animations[pAnimationSequence, counterValue]}";
            Console.Write(message);
            Console.SetCursorPosition(Console.CursorLeft - message.Length, Console.CursorTop);
        }

    }
}
