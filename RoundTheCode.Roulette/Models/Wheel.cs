using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RoundTheCode.Roulette.Models
{
	public class Wheel
	{
		public readonly WheelNumber[] WheelNumbers =
	{
		new WheelNumber(0, WheelNumberColourEnum.Green),
		new WheelNumber(32, WheelNumberColourEnum.Red),
		new WheelNumber(15, WheelNumberColourEnum.Black),
		new WheelNumber(19, WheelNumberColourEnum.Red),
		new WheelNumber(4, WheelNumberColourEnum.Black),
		new WheelNumber(21, WheelNumberColourEnum.Red),
		new WheelNumber(2, WheelNumberColourEnum.Black),
		new WheelNumber(25, WheelNumberColourEnum.Red),
		new WheelNumber(17, WheelNumberColourEnum.Black),
		new WheelNumber(34, WheelNumberColourEnum.Red),
		new WheelNumber(6, WheelNumberColourEnum.Black),
		new WheelNumber(27, WheelNumberColourEnum.Red),
		new WheelNumber(13, WheelNumberColourEnum.Black),
		new WheelNumber(36, WheelNumberColourEnum.Red),
		new WheelNumber(11, WheelNumberColourEnum.Black),
		new WheelNumber(30, WheelNumberColourEnum.Red),
		new WheelNumber(8, WheelNumberColourEnum.Black),
		new WheelNumber(23, WheelNumberColourEnum.Red),
		new WheelNumber(10, WheelNumberColourEnum.Black),
		new WheelNumber(5, WheelNumberColourEnum.Red),
		new WheelNumber(24, WheelNumberColourEnum.Black),
		new WheelNumber(16, WheelNumberColourEnum.Red),
		new WheelNumber(33, WheelNumberColourEnum.Black),
		new WheelNumber(1, WheelNumberColourEnum.Red),
		new WheelNumber(20, WheelNumberColourEnum.Black),
		new WheelNumber(14, WheelNumberColourEnum.Red),
		new WheelNumber(31, WheelNumberColourEnum.Black),
		new WheelNumber(9, WheelNumberColourEnum.Red),
		new WheelNumber(22, WheelNumberColourEnum.Black),
		new WheelNumber(18, WheelNumberColourEnum.Red),
		new WheelNumber(29, WheelNumberColourEnum.Black),
		new WheelNumber(7, WheelNumberColourEnum.Red),
		new WheelNumber(28, WheelNumberColourEnum.Black),
		new WheelNumber(12, WheelNumberColourEnum.Red),
		new WheelNumber(35, WheelNumberColourEnum.Black),
		new WheelNumber(3, WheelNumberColourEnum.Red),		
		new WheelNumber(26, WheelNumberColourEnum.Black)
	};

		public int CurrentNumberIndex { get; protected set; }

		public WheelNumber WinningNumber { get; protected set; }

		public WheelNumberColourEnum? Colour { get; set; }

		public bool Running { get; protected set; }

		public event Func<Task> OnStartAsync;

		public event Func<Task> OnFinishAsync;

		public event Func<Task> OnNumberChangedAsync;

		public async Task RollTheBallAsync()
		{
			WinningNumber = null;
			Running = true;

			if (OnStartAsync != null)
            {
				await OnStartAsync.Invoke();
            }

			var running = true;
			var random = new Random();
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var lengthOfSpin = new TimeSpan(0, 0, random.Next(5, 15));

			random = new Random();
			var speed = new TimeSpan(random.Next(30000, 40000));

			while (running)
            {
				CurrentNumberIndex += 1;

				if (CurrentNumberIndex > WheelNumbers.GetUpperBound(0))
                {
					CurrentNumberIndex = 0;
				}
				if (OnNumberChangedAsync != null)
                {
					await OnNumberChangedAsync.Invoke();
				}

				await Task.Delay(speed);

				if (stopwatch.Elapsed.TotalSeconds > lengthOfSpin.TotalSeconds - 5)
                {
					random = new Random();
					speed = new TimeSpan(random.Next(100000, 200000));
                }
				if (stopwatch.Elapsed.TotalSeconds > lengthOfSpin.TotalSeconds - 2)
				{
					random = new Random();
					speed = new TimeSpan(random.Next(500000, 700000));
				}
				if (stopwatch.Elapsed.TotalSeconds > lengthOfSpin.TotalSeconds)
                {
					running = false;
                }
			}

			WinningNumber = WheelNumbers[CurrentNumberIndex];

			Running = false;

			if (OnFinishAsync != null)
            {
				await OnFinishAsync.Invoke();
            }
		}
	}
}
