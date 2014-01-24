﻿using System;
using System.Windows.Forms;
using ExileClipboardListener.Classes;

namespace ExileClipboardListener.WinForms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            //Load the key mod combos
            GlobalMethods.StuffCombo("SELECT ModName FROM [Mod] ORDER BY 1;", KeyMod1);
            GlobalMethods.StuffCombo("SELECT ModName FROM [Mod] ORDER BY 1;", KeyMod2);
            GlobalMethods.StuffCombo("SELECT ModName FROM [Mod] ORDER BY 1;", KeyMod3);
            GlobalMethods.StuffCombo("SELECT ModName FROM [Mod] ORDER BY 1;", KeyMod4);
            GlobalMethods.StuffCombo("SELECT ModName FROM [Mod] ORDER BY 1;", KeyMod5);

            //Show the current settings
            CollectionMode.Checked = Properties.Settings.Default.StashMode == (int)GlobalMethods.COLLECTION_MODE;
            DuplicatesYes.Checked = Properties.Settings.Default.StashDuplicates;
            DuplicatesNo.Checked = !Properties.Settings.Default.StashDuplicates;
            KeyMod1.SelectedIndex = Properties.Settings.Default.KeyMod1;
            KeyMod2.SelectedIndex = Properties.Settings.Default.KeyMod2;
            KeyMod3.SelectedIndex = Properties.Settings.Default.KeyMod3;
            KeyMod4.SelectedIndex = Properties.Settings.Default.KeyMod4;
            KeyMod5.SelectedIndex = Properties.Settings.Default.KeyMod5;
            TolerancePoorTo.Value = Properties.Settings.Default.TolerancePoorTo;
            ToleranceAverageFrom.Value = Properties.Settings.Default.ToleranceAverageFrom;
            ToleranceAverageTo.Value = Properties.Settings.Default.ToleranceAverageTo;
            ToleranceGoodFrom.Value = Properties.Settings.Default.ToleranceGoodFrom;
            CompareBest.Checked = Properties.Settings.Default.RatingMode == 0;
            CompareLevel.Checked = Properties.Settings.Default.RatingMode == 1;
            StashNoPopUp.Checked = Properties.Settings.Default.StashPopUpMode == 0;
            StashPopUpTimed.Checked = Properties.Settings.Default.StashPopUpMode == 1;
            StashPopUpPerm.Checked = Properties.Settings.Default.StashPopUpMode == 2;
            StashPopUpSeconds.Value = Properties.Settings.Default.StashPopUpSeconds;
            CollectionPopUpTimed.Checked = Properties.Settings.Default.CollectionPopUpMode == 1;
            CollectionPopUpPerm.Checked = Properties.Settings.Default.CollectionPopUpMode == 2;
            CollectionPopUpSeconds.Value = Properties.Settings.Default.CollectionPopUpSeconds;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (StashMode.Checked)
                Properties.Settings.Default.StashMode = (int)GlobalMethods.STASH_MODE;
            else
                Properties.Settings.Default.StashMode = (int)GlobalMethods.COLLECTION_MODE;
            Properties.Settings.Default.KeyMod1 = KeyMod1.SelectedIndex;
            Properties.Settings.Default.KeyMod2 = KeyMod2.SelectedIndex;
            Properties.Settings.Default.KeyMod3 = KeyMod3.SelectedIndex;
            Properties.Settings.Default.KeyMod4 = KeyMod4.SelectedIndex;
            Properties.Settings.Default.KeyMod5 = KeyMod5.SelectedIndex;
            Properties.Settings.Default.KeyMod1Value = GlobalMethods.GetScalarInt("SELECT ModId FROM [Mod] WHERE ModName = '" + KeyMod1.Text + "';");
            Properties.Settings.Default.KeyMod2Value = GlobalMethods.GetScalarInt("SELECT ModId FROM [Mod] WHERE ModName = '" + KeyMod2.Text + "';");
            Properties.Settings.Default.KeyMod3Value = GlobalMethods.GetScalarInt("SELECT ModId FROM [Mod] WHERE ModName = '" + KeyMod3.Text + "';");
            Properties.Settings.Default.KeyMod4Value = GlobalMethods.GetScalarInt("SELECT ModId FROM [Mod] WHERE ModName = '" + KeyMod4.Text + "';");
            Properties.Settings.Default.KeyMod5Value = GlobalMethods.GetScalarInt("SELECT ModId FROM [Mod] WHERE ModName = '" + KeyMod5.Text + "';");
            Properties.Settings.Default.TolerancePoorTo = (int)TolerancePoorTo.Value;
            Properties.Settings.Default.ToleranceAverageFrom = (int)ToleranceAverageFrom.Value;
            Properties.Settings.Default.ToleranceAverageTo = (int)ToleranceAverageTo.Value;
            Properties.Settings.Default.ToleranceGoodFrom = (int)ToleranceGoodFrom.Value;
            Properties.Settings.Default.StashDuplicates = DuplicatesYes.Checked;
            Properties.Settings.Default.RatingMode = (CompareBest.Checked ? 0 : 1);
            Properties.Settings.Default.StashPopUpMode = (StashNoPopUp.Checked ? 0 : (StashPopUpTimed.Checked ? 1 : 2));
            Properties.Settings.Default.StashPopUpSeconds = (int)StashPopUpSeconds.Value;
            Properties.Settings.Default.CollectionPopUpMode = (CollectionPopUpTimed.Checked ? 1 : 2);
            Properties.Settings.Default.CollectionPopUpSeconds = (int)CollectionPopUpSeconds.Value;
            Properties.Settings.Default.Save();
            Hide();
        }

        private void TolerancePoorTo_ValueChanged(object sender, EventArgs e)
        {
            if (ToleranceAverageFrom.Value != TolerancePoorTo.Value + 1)
                ToleranceAverageFrom.Value = TolerancePoorTo.Value + 1;
        }

        private void ToleranceAverageFrom_ValueChanged(object sender, EventArgs e)
        {
            if (TolerancePoorTo.Value != ToleranceAverageFrom.Value - 1)
                TolerancePoorTo.Value = ToleranceAverageFrom.Value - 1;
            if (ToleranceAverageFrom.Value > ToleranceAverageTo.Value)
                ToleranceAverageTo.Value = ToleranceAverageFrom.Value;
        }

        private void ToleranceAverageTo_ValueChanged(object sender, EventArgs e)
        {
            if (ToleranceGoodFrom.Value != ToleranceAverageTo.Value + 1)
                ToleranceGoodFrom.Value = ToleranceAverageTo.Value + 1;
            if (ToleranceAverageTo.Value < ToleranceAverageFrom.Value)
                ToleranceAverageFrom.Value = ToleranceAverageTo.Value;
        }

        private void ToleranceGoodFrom_ValueChanged(object sender, EventArgs e)
        {
            if (ToleranceAverageTo.Value != ToleranceGoodFrom.Value - 1)
                ToleranceAverageTo.Value = ToleranceGoodFrom.Value - 1;
        }

        private void PopUpMode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
