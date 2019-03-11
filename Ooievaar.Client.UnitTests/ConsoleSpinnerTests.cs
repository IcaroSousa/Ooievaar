using System;
using NUnit.Framework;

namespace Ooievaar.Client.UnitTests
{
    [TestFixture]
    public class ConsoleSpinnerTests
    {
        private ConsoleSpinner _consoleSpinner { get; set; }

        [SetUp]
        public void SetUp() 
        {
            _consoleSpinner = new ConsoleSpinner();
            _consoleSpinner.Delay = TimeSpan.FromMilliseconds(10);
        }

        [TearDown]
        public void TearDown() 
        {
            if (_consoleSpinner == null)
                return;

            ((IDisposable)_consoleSpinner).Dispose();
        }
    }
}
