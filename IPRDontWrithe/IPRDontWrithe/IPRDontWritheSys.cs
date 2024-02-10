using System;
using System.Collections.Generic;
using GTA;

namespace IPRDontWrithe
{
    public class IPRDontWritheSys : Script
    {
		// Variables
		bool Initialized = false;
		readonly List<Ped> pedlist = new List<Ped>();
		public IPRDontWritheSys()
        {
            Tick += OnTick;
        }
        // Main Logic
        private void OnTick(object sender, EventArgs e)
        {
            if ((Game.IsPaused == false) && (Game.IsLoading == false))
            {
                if (Initialized == false)
                {
                    Initialized = true;
                    GTA.UI.Notification.Show("~r~IPR ~b~Dont Writhe ~y~System ~w~Initialized.");
                }
                foreach (Ped ped in World.GetAllPeds())
                {
                    if ((ped != null) && (!pedlist.Contains(ped)))
                    {
                        ped.CanWrithe = false;
                        pedlist.Add(ped);
                    }
                }
            }
        }
    }
}
