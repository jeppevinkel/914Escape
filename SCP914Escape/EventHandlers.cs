using System;
using System.Collections.Generic;
using EXILED;
using Grenades;
using MEC;
using UnityEngine;

namespace SCP914Escape
{
    public class EventHandlers
    {
        public Plugin plugin;
        public EventHandlers(Plugin plugin) => this.plugin = plugin;

        System.Random random = new System.Random();

        public void OnRoundStart()
        {
            foreach (ReferenceHub hub in Plugin.GetHubs())
            {
                //Since this event fires before everyone has initially spawned, you need to wait before doing things like changing their health, adding items, etc
                Timing.RunCoroutine(GiveBall(hub));
            }
        }

        public IEnumerator<float> GiveBall(ReferenceHub hub)
        {
            //Wait 3 seconds to make sure everyone is spawned in correctly
            yield return Timing.WaitForSeconds(3f);
            //Give everybody 5 balls
            for (int i = 0; i < 5; i++)
                hub.inventory.AddNewItem(ItemType.SCP018);
        }

        public void OnScp914Upgrade(ref SCP914UpgradeEvent ev)
        {
            if (ev.KnobSetting != Scp914.Scp914Knob.Coarse && plugin.teleportPoints.Count >= 1) { return; }
            Plugin.Debug($"SCP914 Teleport triggered.");
            UnityEngine.GameObject[] rooms = UnityEngine.GameObject.FindGameObjectsWithTag("RoomID");
            Plugin.Debug($"Found {rooms.Length} rooms.");
            Plugin.Debug($"Existing rooms: ");
            foreach (var room in rooms)
            {
                Plugin.Debug($"{room.GetComponent<Rid>().id}");
            }
            List<ReferenceHub> inputs = ev.Players;
            Plugin.Debug($"Found {inputs.Count} players.");
            foreach (ReferenceHub player in inputs)
            {
                if (Plugin.GetTeam(player.characterClassManager.CurClass) != Team.SCP && Plugin.GetTeam(player.characterClassManager.CurClass) != Team.RIP)
                {
                    Vector3 _pos;
                    Plugin.Debug($"Possible rooms: {string.Join(", ", plugin.teleportPoints)}.");
                    string roomToUse = plugin.teleportPoints[random.Next(0, plugin.teleportPoints.Count)];
                    Plugin.Debug($"Using room: {roomToUse}.");
                    foreach (UnityEngine.GameObject room in rooms)
                    {
                        var pos = room.transform.position;
                        var id = room.GetComponent<Rid>().id;
                        if (id == roomToUse)
                        {
                            _pos = new Vector3(pos.x, pos.y + 2, pos.z);
                            Plugin.Debug($"Teleporting {player.nicknameSync.MyNick} to {roomToUse}.");
                            if (plugin.damageType == 0)
                            {
                                player.playerStats.health = (player.playerStats.health - player.playerStats.health * (plugin.humanDamage / 100f));
                            }
                            else
                            {
                                player.playerStats.health = (int)(player.playerStats.health - plugin.humanDamage);
                            }
                            Timing.RunCoroutine(Teleport(player, _pos));
                            break;
                        }
                    }
                }
                else if (Plugin.GetTeam(player.characterClassManager.CurClass) == Team.SCP && Plugin.GetTeam(player.characterClassManager.CurClass) != Team.RIP)
                {
                    Vector3 _pos;
                    string roomToUse = plugin.teleportPoints[random.Next(0, plugin.teleportPoints.Count)];
                    foreach (UnityEngine.GameObject room in rooms)
                    {
                        var pos = room.transform.position;
                        var id = room.GetComponent<Rid>().id;
                        if (id == roomToUse)
                        {
                            _pos = new Vector3(pos.x, pos.y + 2, pos.z);
                            Plugin.Debug($"Teleporting {player.nicknameSync.MyNick} to {roomToUse}.");
                            if (plugin.damageType == 0)
                            {
                                player.playerStats.health = (player.playerStats.health - player.playerStats.health * (plugin.scpDamage / 100f));
                            }
                            else
                            {
                                player.playerStats.health = (int)(player.playerStats.health - plugin.humanDamage);
                            }
                            Timing.RunCoroutine(Teleport(player, _pos));
                            break;
                        }
                    }
                }
            }
        }

        private IEnumerator<float> Teleport(ReferenceHub player, Vector3 v)
        {
            yield return Timing.WaitForSeconds(0.1f);

            player.plyMovementSync.OverridePosition(v, player.transform.rotation.y);
        }
    }
}