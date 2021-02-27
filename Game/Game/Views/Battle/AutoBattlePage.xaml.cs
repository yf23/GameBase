using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AutoBattlePage : ContentPage
	{
		// Hold the Engine, so it can be swapped out for unit testing
		public IAutoBattleInterface AutoBattle = BattleEngineViewModel.Instance.AutoBattleEngine;

		// Flag that this is running under Tests
		public bool UnitTestRunning;

		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage (bool UnitTest = false)
		{
			// Flag if running under tests for animation loop control
			UnitTestRunning = UnitTest;

			InitializeComponent ();
		}

		/// <summary>
		/// Defines what happens when the AutoBattleButton is clicked.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void AutobattleButton_Clicked(object sender, EventArgs e)
		{
			// Call into Auto Battle from here to do the Battle...

			// To See Level UP happening, a character needs to be close to the next level
			var Character = new CharacterModel
			{
				ExperienceTotal = 300,    // Enough for next level
				Name = "Mike Level Example",
				Speed = 100,	// Go first
			};

			var CharacterPlayer = new PlayerInfoModel(Character);

			// Turn on the Koenig version for now...
			BattleEngineViewModel.Instance.SetBattleEngineToKoenig();

			BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

			await BattleEngineViewModel.Instance.AutoBattleEngine.RunAutoBattle();
			
			var BattleMessage = string.Format("Done {0} Rounds", AutoBattle.Battle.EngineSettings.BattleScore.RoundCount);

			BattleMessageValue.Text = BattleMessage;

			AutobattleImage.Source = "troll6_d.gif";
		}

		#region AnimationExamples
		/// <summary>
		/// Animation for Dice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void RollDice_Clicked(object sender, EventArgs e)
		{
			DiceAnimationHandeler();

			return;
		}

		/// <summary>
		/// Example of Animation on Dice
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public async void RollDiceMove_Clicked(object sender, EventArgs e)
		{
			// Start Spinning the dice

			ImageButton image = RollDiceMove;
			uint duration = 150;

			var parentAnimation = new Animation();

			// Spin the Image, it will go forever because of the ()=>true at the end
			var rotateAnimation = new Animation(v => image.Rotation = v, 0, 360);
			parentAnimation.Add(0, 1, rotateAnimation);

			/*
			 * The commit animation if repeating, will hang the unit test harness, so need to set it to false for testing
			 * The UT constructor allows it to be called and set the UT flag
			 * 
			 */ 
			var repeat = true;

			if (UnitTestRunning)
            {
				repeat = false;
            }

			parentAnimation.Commit(this, "SpinAnimation", 16, duration * 2, null, null, () => repeat);

			var Boxwidth = DiceBox.Width;
			var BoxHeight = DiceBox.Height;

			await RollDiceMove.TranslateTo(0, 0, duration, Easing.CubicIn);
			
			await RollDiceMove.TranslateTo(0, BoxHeight,duration, Easing.SinIn);

			int bounceHeight = (int)BoxHeight / 2;
			int bounceSize = (int)BoxHeight / 4;
			int bounceWidth = (int)Boxwidth / 4;

			for(var i=0; i< Boxwidth-bounceWidth; i +=bounceWidth)
            {
				await RollDiceMove.TranslateTo(i, BoxHeight, duration);
				await RollDiceMove.TranslateTo(i+(bounceWidth/2), DiceBox.Height-bounceHeight, duration);

				bounceHeight -= bounceSize;
			}

			await RollDiceMove.TranslateTo(Boxwidth, BoxHeight, duration,Easing.CubicOut);


			// Cancel the spin animation
			this.AbortAnimation("SpinAnimation");
			return;
		}

		/// <summary>
		/// Dice Animation Handeler
		/// </summary>
		/// <returns></returns>
		public bool DiceAnimationHandeler()
		{
			// Animate the Rolling of the Dice
			ImageButton image = RollDice;
			uint duration = 1000;

			var parentAnimation = new Animation();

			// Grow the image Size
			var scaleUpAnimation = new Animation(v => image.Scale = v, 1, 2, Easing.SpringIn);

			// Spin the Image
			var rotateAnimation = new Animation(v => image.Rotation = v, 0, 360);

			// Shrink the Image
			var scaleDownAnimation = new Animation(v => image.Scale = v, 2, 1, Easing.SpringOut);

			parentAnimation.Add(0, 0.5, scaleUpAnimation);
			parentAnimation.Add(0, 1, rotateAnimation);
			parentAnimation.Add(0.5, 1, scaleDownAnimation);

			parentAnimation.Commit(this, "ChildAnimations", 16, duration, null, null);

			return true;
		}
		#endregion AnimationExamples
	}
}