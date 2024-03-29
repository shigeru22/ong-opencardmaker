﻿using System.Windows;

namespace OpenCardMaker.Dialogs
{
    /// <summary>
    /// Interaction logic for IncreaseCardLevel.xaml
    /// </summary>
    public partial class IncreaseCardLevel : Window
    {
        public int levelNumber { get; private set; }
        readonly int level, maxLevel;

        public IncreaseCardLevel(int level, int maxLevel)
        {
            levelNumber = 1;
            this.level = level;
            this.maxLevel = maxLevel;
            InitializeComponent();

            MainText.Text = $"Increase card level by (maximum {this.maxLevel}):";
            UpdateText();
            DecreaseLevelButton.IsEnabled = false;
        }

        void UpdateText()
        {
            LevelNumberText.Text = $"{levelNumber}";
            TargetLevelText.Text = $"Target level: {level} -> {level + levelNumber}";
        }

        public void btnDecreaseLevel(object sender, RoutedEventArgs e)
        {
            if (levelNumber > 1)
            {
                levelNumber--;
                UpdateText();

                if (levelNumber == 1) DecreaseLevelButton.IsEnabled = false;
                else if (!IncreaseLevelButton.IsEnabled && levelNumber < maxLevel - level) IncreaseLevelButton.IsEnabled = true;
            }
        }

        public void btnIncreaseLevel(object sender, RoutedEventArgs e)
        {
            if (levelNumber < maxLevel - level)
            {
                levelNumber++;
                UpdateText();

                if (levelNumber == maxLevel - level) IncreaseLevelButton.IsEnabled = false;
                else if (!DecreaseLevelButton.IsEnabled && levelNumber > 1) DecreaseLevelButton.IsEnabled = true;
            }
        }

        public void btnOKClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        public void btnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
