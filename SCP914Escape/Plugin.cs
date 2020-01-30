using System;
using System.Collections.Generic;
using EXILED;

namespace SCP914Escape
{
	public class Plugin : EXILED.Plugin
	{
		//Instance variable for eventhandlers
		public EventHandlers EventHandlers;

		public List<string> teleportPoints;
		public float humanDamage;
		public float scpDamage;
		public int damageType;
		public bool debug;

		public override void OnEnable()
		{
			try
			{
				if(!Config.GetBool("escape_enabled", true))
				{
					return;
				}
				Debug("Initializing event handlers..");
				
				EventHandlers = new EventHandlers(this);

				Events.Scp914UpgradeEvent += EventHandlers.OnScp914Upgrade;

				LoadConfig();

				Info($"SCP914Escape plugin loaded.");
			}
			catch (Exception e)
			{
				Error($"There was an error loading the plugin: {e}");
			}
		}

		public void LoadConfig()
		{
			teleportPoints = Config.GetString($"escape_teleport_points", "LC_CAFE,CROSSING").ToList();
			humanDamage = Config.GetFloat("escape_damage_human", 50);
			scpDamage = Config.GetFloat("escape_damage_scp", 50);
			damageType = Config.GetInt("escape_damage_type", 0);
		}

		public override void OnDisable()
		{
			Events.Scp914UpgradeEvent -= EventHandlers.OnScp914Upgrade;

			EventHandlers = null;
		}

		public override void OnReload()
		{
			
		}

		public override string getName { get; } = "SCP914Escape";
	}
}