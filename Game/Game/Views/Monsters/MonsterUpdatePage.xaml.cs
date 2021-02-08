﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.ViewModels;
using Game.Models;
using Game.GameRules;

namespace Game.Views
{
    /// <summary>
    /// Item Update Page
    /// </summary>
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterUpdatePage : ContentPage
    {
        public int MaxAttributeValue = 9;
        public int MinAttributeValue = 0;

        // View Model for Item
        public readonly GenericViewModel<MonsterModel> ViewModel;

        // Empty Constructor for Tests
        public MonsterUpdatePage(bool UnitTest) { }

        /// <summary>
        /// Constructor that takes and existing data item
        /// </summary>
        public MonsterUpdatePage(GenericViewModel<MonsterModel> data)
        {
            InitializeComponent();

            this.ViewModel = data;

            UpdatePageBindingContext();
        }

        /// <summary>
        /// Redo the Binding to cause a refresh
        /// </summary>
        /// <returns></returns>
        public bool UpdatePageBindingContext()
        {
            // Temp store off the Level
            var data = this.ViewModel.Data;

            // Clear the Binding and reset it
            BindingContext = null;
            this.ViewModel.Data = data;
            this.ViewModel.Title = data.Name;

            BindingContext = this.ViewModel;

            // This resets the Picker to the Character's level
    
            DifficultyPicker.SelectedItem = ViewModel.Data.Difficulty.ToString();
            
            SetEnableStateAttributeButtons();

            return true;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Update", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel and close this page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Randomize Character Values and Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RandomButton_Clicked(object sender, EventArgs e)
        {
            this.ViewModel.Data.Update(RandomPlayerHelper.GetRandomMonster(1));

            UpdatePageBindingContext();

            return;
        }

        #region Picker

        #endregion Picker

        #region AttributeButtons
        #region SetEnableStateAttributeButtons
        /// <summary>
        /// Walk each button and set the enabled to true or false
        /// </summary>
        /// <returns></returns>
        public bool SetEnableStateAttributeButtons()
        {
            AttackUpButton.IsEnabled = true;
            if (ViewModel.Data.Attack == MaxAttributeValue)
            {
                AttackUpButton.IsEnabled = false;
            }

            AttackDownButton.IsEnabled = true;
            if (ViewModel.Data.Attack == MinAttributeValue)
            {
                AttackDownButton.IsEnabled = false;
            }

            DefenseUpButton.IsEnabled = true;
            if (ViewModel.Data.Defense == MaxAttributeValue)
            {
                DefenseUpButton.IsEnabled = false;
            }

            DefenseDownButton.IsEnabled = true;
            if (ViewModel.Data.Defense == MinAttributeValue)
            {
                DefenseDownButton.IsEnabled = false;
            }

            SpeedUpButton.IsEnabled = true;
            if (ViewModel.Data.Speed == MaxAttributeValue)
            {
                SpeedUpButton.IsEnabled = false;
            }

            SpeedDownButton.IsEnabled = true;
            if (ViewModel.Data.Speed == MinAttributeValue)
            {
                SpeedDownButton.IsEnabled = false;
            }

            return true;
        }

        #endregion SetEnableStateAttributeButtons

        #region AttackButton
        /// <summary>
        /// Manage the Attack Up Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackUpButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.Attack++;

            if (ViewModel.Data.Attack > MaxAttributeValue)
            {
                ViewModel.Data.Attack = MaxAttributeValue;
            }

            UpdatePageBindingContext();
        }

        /// <summary>
        /// Manage the Attack Down Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackDownButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.Attack--;

            if (ViewModel.Data.Attack < MinAttributeValue)
            {
                ViewModel.Data.Attack = MinAttributeValue;
            }

            UpdatePageBindingContext();
        }
        #endregion AttackButton

        #region DefenseButton
        /// <summary>
        /// Manage the Defense Up Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DefenseUpButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.Defense++;

            if (ViewModel.Data.Defense > MaxAttributeValue)
            {
                ViewModel.Data.Defense = MaxAttributeValue;
            }

            UpdatePageBindingContext();
        }

        /// <summary>
        /// Manage the Defense Down Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DefenseDownButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.Defense--;

            if (ViewModel.Data.Defense < MinAttributeValue)
            {
                ViewModel.Data.Defense = MinAttributeValue;
            }

            UpdatePageBindingContext();
        }
        #endregion DefenseButton

        #region SpeedButton
        /// <summary>
        /// Manage the Speed Up Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SpeedUpButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.Speed++;

            if (ViewModel.Data.Speed > MaxAttributeValue)
            {
                ViewModel.Data.Speed = MaxAttributeValue;
            }

            UpdatePageBindingContext();
        }

        /// <summary>
        /// Manage the Speed Down Button Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void SpeedDownButton_Clicked(object sender, EventArgs e)
        {
            ViewModel.Data.Speed--;

            if (ViewModel.Data.Speed < MinAttributeValue)
            {
                ViewModel.Data.Speed = MinAttributeValue;
            }

            UpdatePageBindingContext();
        }
        #endregion SpeedButton
        #endregion AttributeButtons
    }
}