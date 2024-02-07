using System;
using GTA;
using GTA.Math;

namespace ImprovedPoliceAndResponseNotificationSys
{
    public class JuggernautSys : Script
    {
        // Variables
		bool iswanted = false;
		bool juggernautpresent = false;
        // Initialization
		public JuggernautSys()
        {
            Tick += OnTick;
			GTA.UI.Notification.Show("~r~Juggernaut ~y~System ~w~Initialized.");
		}
        private void NotifyPlr(string icon, string user, string subject, string msg)
        {
			if (icon == "Call911")
			{
				GTA.UI.Notification.Show(GTA.UI.NotificationIcon.Call911, user, subject, msg, true, false);
			}
			else if (icon == "Franklin")
			{
				GTA.UI.Notification.Show(GTA.UI.NotificationIcon.Franklin, user, subject, msg, true, false);
			}
			else if (icon == "Trevor")
			{
				GTA.UI.Notification.Show(GTA.UI.NotificationIcon.Trevor, user, subject, msg, true, false);
			}
			else if (icon == "Micheal")
			{
				GTA.UI.Notification.Show(GTA.UI.NotificationIcon.Michael, user, subject, msg, true, false);
			}
			else if (icon == "Unknown")
			{
				GTA.UI.Notification.Show(GTA.UI.NotificationIcon.HumanDefault, user, subject, msg, true, false);
			}
		}
        private void OnTick(object sender, EventArgs notif)
        {
            if ((Game.Player.WantedLevel >= 1) && (Game.IsPaused == false) && (Game.IsLoading == false))
            {
                if (iswanted == false)
                {
                    iswanted = true;
                }
                else
                {
                    if ((Game.Player.WantedLevel >= 3) && (juggernautpresent == false))
                    {
                        juggernautpresent = true;
                        Random rand = new Random();
                        int counter = 0;
                        int randomnumber = rand.Next(1200, 8000);
                        while ((counter < randomnumber) && (Game.Player.WantedLevel >= 3))
                        {
                            Wait(50 / (Game.Player.WantedLevel / 3));
                            counter++;
                        }
                        if (Game.Player.WantedLevel >= 3) 
                        {
                            NotifyPlr("Unknown", "The Military", "~g~Juggernaut Notification", "~g~The Lilitary ~w~sent a ~r~Juggernaut ~w~to ~r~Eliminate You~w~!");
                            var newped = World.CreatePed(PedHash.Marine03SMY, World.GetNextPositionOnStreet(Game.Player.Character.GetOffsetPosition(new Vector3(rand.Next(20, 75), 0, rand.Next(20, 75)))));
                            newped.Weapons.Give(WeaponHash.Minigun, 999999999, true, true);
                            newped.CanSufferCriticalHits = false;
                            newped.CanSwitchWeapons = false;
                            newped.CanRagdoll = false;
                            newped.CanWrithe = false;
                            newped.CanWearHelmet = true;
                            newped.Health = 2500;
                            newped.ShootRate = 3000;
                            newped.Task.FightAgainst(Game.Player.Character);
                            newped.AddBlip();
                            var quickcounter = 0;
                            while ((newped.IsDead == false) && (Game.Player.WantedLevel >= 3) && (quickcounter < 2400) && (Vector3.Distance(newped.Position,Game.Player.Character.Position)) <= 500)
                            {
                                Wait(50);
                                quickcounter++;
                            }
                            newped.AttachedBlip?.Delete();
                            newped.Kill();
                            newped.IsPersistent = false;
                            NotifyPlr("Unknown", "The Military", "~g~Juggernaut Notification", "The ~r~Juggernaut ~w~is ~y~no longer ~w~in ~r~The Area~w~!");
                            juggernautpresent = false;
                        }
                        else
                        {
                            juggernautpresent = false;
                        }
                    }
                }
            }
            else if ((Game.Player.WantedLevel == 0) && (Game.IsPaused == false) && (Game.IsLoading == false))
            {
                if (iswanted == true)
                {
                    iswanted = false;
                }
            }
        }
    }
}
