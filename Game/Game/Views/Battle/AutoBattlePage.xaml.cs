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

		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage ()
		{
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
		public void RollDice_Clicked(object sender, EventArgs e)
		{
			DiceAnimationHandeler();

			return;
		}

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