using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Consoleum;
using Consoleum.PageObjects;
using FluentAssertions;

namespace Battleship.Ascii.UITests
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [TestClass]
    public class PlayGameTest
    {
        public PlayGameTest()
        {
        }

        List<string> shipPositions = new List<string>()
            {
                "A1", "A2", "A3", "A4", "A5",
                "B1", "B2", "B3", "B4",
                "C1", "C2", "C3",
                "D1", "D2", "D3",
                "E1", "E2"
            };

        [TestMethod]
        [TestCategory("UI")]
        [DeploymentItem("Battleship.Ascii.exe")]
        public void PlayGameAndHitComputerShip()
        {
            using (var driver = new ConsoleDriver("Battleship.Ascii.exe"))
            {
                driver.Start();

                foreach (var shipPosition in shipPositions)
                {
                    driver.Keyboard.TextEntry(shipPosition);
                    driver.Keyboard.TextEntry(System.Environment.NewLine);
                }


                var result = driver.Output.Capture();
                result.Should().Contain("Player, it's your turn");

                driver.Keyboard.TextEntry("C3" + System.Environment.NewLine);

                result = driver.Output.Capture();
                result.Should().Contain("Yeah ! Nice hit !");

                driver.Keyboard.TextEntry("A1" + System.Environment.NewLine);

                result = driver.Output.Capture();
                result.Should().Contain("Miss");

            }
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
