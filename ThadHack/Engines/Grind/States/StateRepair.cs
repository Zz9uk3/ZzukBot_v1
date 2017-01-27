using System.Collections.Generic;
using System.Linq;
using ZzukBot.Constants;
using ZzukBot.Engines.Grind.Info.Path.Base;
using ZzukBot.FSM;
using ZzukBot.Helpers;
using ZzukBot.Mem;

namespace ZzukBot.Engines.Grind.States
{
    internal class StateRepair : State
    {
        private bool BackToPath;

        internal override int Priority => 40;

        internal override bool NeedToRun => Grinder.Access.Info.Vendor.NeedToVendor
                                            || ObjectManager.Player.Inventory.DurabilityPercentage < 30;

        internal override string Name => "Vendoring / Repairing";

        internal override void Run()
        {
            // close enough to vendor?
            if (Calc.Distance2D(ObjectManager.Player.Position, Grinder.Access.Profile.RepairNPC.Coordinates) < 4.0f)
            {
                Grinder.Access.Info.PathAfterFightToWaypoint.DisableAfterFightMovement();
                ObjectManager.Player.CtmStopMovement();

                // open vendor interface and skip gossip
                var vendor = ObjectManager.Npcs
                    .FirstOrDefault(i => i.Name == Grinder.Access.Profile.RepairNPC.Name);
                if (vendor == null) return;
                if (!Grinder.Access.Info.Vendor.GossipOpen)
                {
                    ObjectManager.Player.CancelShapeshift();
                    ObjectManager.Player.RightClick(vendor);
                    Functions.DoString(Strings.SkipGossip);
                }
                else
                {
                    // sell our shit
                    if (!Wait.For("SellItemsTimer112", Grinder.Access.Info.Latency*2 + 200)) return;
                    BackToPath =
                        !ObjectManager.Player.Inventory.VendorItems();
                    if (BackToPath)
                    {
                        ObjectManager.Player.Inventory.RepairAll();
                        Grinder.Access.Info.Vendor.DoneVendoring();
                        Grinder.Access.Info.Vendor.GoBackToGrindAfterVendor = true;

                        Grinder.Access.Info.Waypoints.ResetGrindPath();
                        var tmpList = new List<Waypoint>();

                        if (Grinder.Access.Profile.VendorHotspots != null &&
                            Grinder.Access.Profile.VendorHotspots.Length != 0)
                        {
                            for (var i = Grinder.Access.Profile.VendorHotspots.Length - 1; i >= 0; i--)
                            {
                                tmpList.Add(Grinder.Access.Profile.VendorHotspots[i]);
                            }
                        }
                        tmpList.Add(Grinder.Access.Profile.Hotspots[0]);

                        Grinder.Access.Info.PathManager.VendorToGrind = new BasePath(tmpList);
                    }
                }
            }
            // not close enough? lets travel to the vendor using another state!
            else
            {
                Grinder.Access.Info.Vendor.TravelingToVendor = true;
                var tmpList = new List<Waypoint>();

                //if (Grinder.Profile.VendorHotspots == null || Grinder.Profile.VendorHotspots.Length == 0)
                //    return;

                Grinder.Access.Info.Waypoints.RevertHotspotsToOriginal();
                var curHotspot = Grinder.Access.Info.Waypoints.CurrentHotspotIndex;

                if (curHotspot > 0)
                {
                    for (var i = curHotspot - 1; i >= 0; i--)
                    {
                        tmpList.Add(Grinder.Access.Profile.Hotspots[i]);
                    }
                }
                if (Grinder.Access.Profile.VendorHotspots != null && Grinder.Access.Profile.VendorHotspots.Length != 0)
                {
                    tmpList.AddRange(Grinder.Access.Profile.VendorHotspots);
                }
                var tmpWp = new Waypoint
                {
                    Position = Grinder.Access.Profile.RepairNPC.Coordinates,
                    Type = Enums.PositionType.Hotspot
                };
                tmpList.Add(tmpWp);

                Grinder.Access.Info.PathManager.GrindToVendor = new BasePath(tmpList);
            }
        }
    }
}