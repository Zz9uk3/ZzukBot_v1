using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ZzukBot.Constants;
using ZzukBot.Helpers;

namespace ZzukBot.Engines.Grind
{
    internal class GrindProfile
    {
        #region constructor

        internal GrindProfile(string parProfilePath)
        {
            try
            {
                // open the supplied xml

                var profileArr = File.ReadAllLines(parProfilePath);
                var profile = profileArr.Aggregate("", (current, t) => current + t.TrimStart('\t', ' ') + Environment.NewLine);

                var doc = XDocument.Parse(profile);
                var sub = doc.Element("Profile");
                // get subelements of xml profile
                var tmpHotspots = sub.Element("Hotspots");
                var tmpIgnoreZAxis = sub.Element("IgnoreZAxis");
                var tmpVendorHotspots = sub.Element("VendorHotspots");
                var tmpGhostrHotspots = sub.Element("GhostHotspots");
                var tmpFactions = sub.Element("Factions");
                var tmpVendor = sub.Element("Vendor");
                var tmpRepair = sub.Element("Repair");
                var tmpRestock = sub.Element("Restock");
                var tmpRestockItems = sub.Element("RestockItems");
                Shared.IgnoreZAxis = tmpIgnoreZAxis != null;

                ExtractHotspots(tmpHotspots);
                // parse all faction ids if avaible
                ExtractFactions(tmpFactions);
                // parse the vendor
                ExtractVendor(tmpVendor);
                // parse the Repair
                ExtractRepairNpcAndWaypoints(tmpVendorHotspots, tmpRepair);
                // parse the Restock
                ExtractRestockNpcAndItems(tmpRestock, tmpRestockItems);
                ExtractGhosthotspots(tmpGhostrHotspots);
                // the profile is valid
                ProfileValid = true;
            }
            catch
            {
                // the profile is invalid
                ProfileValid = false;
            }
        }

        private void ExtractRestockNpcAndItems(XElement tmpRestock, XElement tmpRestockItems)
        {
            if (tmpRestock != null)
            {
                var vec3 = new XYZ(0, 0, 0)
                {
                    X = Convert.ToSingle(
                        tmpRestock.Element("Position").Element("X").Value),
                    Y = Convert.ToSingle(
                        tmpRestock.Element("Position").Element("Y").Value),
                    Z = Convert.ToSingle(
                        tmpRestock.Element("Position").Element("Z").Value)
                };

                RestockNPC = new NPC(tmpRestock.Element("Name").Value,
                    vec3, "");


                // parse the Restockitems
                if (tmpRestockItems != null)
                {
                    RestockItems = (from x in tmpRestockItems.Nodes()
                        select x as XElement
                        into tmpX
                        where tmpX.Name == "Item"
                        select new RestockItem
                        {
                            Item = tmpX.Element("Name").Value, RestockUpTo = Convert.ToInt32(tmpX.Element("RestockUpTo").Value)
                        }).ToArray();
                }
            }
        }

        private void ExtractGhosthotspots(XElement tmpGhostHotspots)
        {
            if (tmpGhostHotspots != null)
            {
                var tmpListGhostHotspots = new List<Waypoint>();
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var x in tmpGhostHotspots.Nodes().ToList())
                {
                    var tmpX = x as XElement;
                    if (tmpX.Name == "GhostHotspot")
                    {
                        var _vec3 = new XYZ
                        {
                            X = Convert.ToSingle(tmpX.Element("X").Value),
                            Y = Convert.ToSingle(tmpX.Element("Y").Value),
                            Z = Convert.ToSingle(tmpX.Element("Z").Value)
                        };

                        var tmpWp = new Waypoint
                        {
                            Position = _vec3,
                            Type = (Enums.PositionType)
                                Enum.Parse(typeof (Enums.PositionType), tmpX.Element("Type").Value, true)
                        };

                        tmpListGhostHotspots.Add(tmpWp);
                    }
                }
                GhostHotspots = tmpListGhostHotspots.ToArray();
            }
        }

        private void ExtractRepairNpcAndWaypoints(XElement tmpVendorHotspots, XElement tmpRepair)
        {
            if (tmpRepair != null)
            {
                if (tmpVendorHotspots != null)
                {
                    var tmpListVendorHotspots = new List<Waypoint>();
                    // ReSharper disable once LoopCanBeConvertedToQuery
                    foreach (var x in tmpVendorHotspots.Nodes().ToList())
                    {
                        var tmpX = x as XElement;
                        if (tmpX.Name == "VendorHotspot")
                        {
                            var _vec3 = new XYZ
                            {
                                X = Convert.ToSingle(tmpX.Element("X").Value),
                                Y = Convert.ToSingle(tmpX.Element("Y").Value),
                                Z = Convert.ToSingle(tmpX.Element("Z").Value)
                            };

                            var tmpWp = new Waypoint
                            {
                                Position = _vec3,
                                Type = (Enums.PositionType)
                                    Enum.Parse(typeof (Enums.PositionType), tmpX.Element("Type").Value, true)
                            };

                            tmpListVendorHotspots.Add(tmpWp);
                        }
                    }
                    VendorHotspots = tmpListVendorHotspots.ToArray();
                }

                var vec3 = new XYZ
                {
                    X = Convert.ToSingle(
                        tmpRepair.Element("Position").Element("X").Value),
                    Y = Convert.ToSingle(
                        tmpRepair.Element("Position").Element("Y").Value),
                    Z = Convert.ToSingle(
                        tmpRepair.Element("Position").Element("Z").Value)
                };

                RepairNPC = new NPC(tmpRepair.Element("Name").Value,
                    vec3, "");
            }
        }

        private void ExtractVendor(XElement tmpVendor)
        {
            if (tmpVendor != null)
            {
                var vec3 = new XYZ
                {
                    X = Convert.ToSingle(
                        tmpVendor.Element("Position").Element("X").Value),
                    Y = Convert.ToSingle(
                        tmpVendor.Element("Position").Element("Y").Value),
                    Z = Convert.ToSingle(
                        tmpVendor.Element("Position").Element("Z").Value)
                };

                VendorNPC = new NPC(tmpVendor.Element("Name").Value,
                    vec3, "");
            }
        }

        private void ExtractFactions(XElement tmpFactions)
        {
            if (tmpFactions != null)
            {
                var tmpListFactions = new List<int>();
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var x in tmpFactions.Nodes().ToList())
                {
                    var tmpX = x as XElement;
                    if (tmpX.Name == "Faction")
                        tmpListFactions.Add(Convert.ToInt32(tmpX.Value));

                }
                Factions = tmpListFactions.ToArray();
            }
        }

        private void ExtractHotspots(XElement tmpHotspots)
        {
            // Parse all hotspots
            var tmpListHotspots = new List<Waypoint>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var x in tmpHotspots.Nodes().ToList())
            {
                var tmpX = x as XElement;
                if (tmpX.Name == "Hotspot")
                {
                    var vec3 = new XYZ
                    {
                        X = Convert.ToSingle(tmpX.Element("X").Value),
                        Y = Convert.ToSingle(tmpX.Element("Y").Value),
                        Z = Convert.ToSingle(tmpX.Element("Z").Value)
                    };

                    var tmpWp = new Waypoint
                    {
                        Position = vec3,
                        Type =
                            (Enums.PositionType)
                                Enum.Parse(typeof (Enums.PositionType), tmpX.Element("Type").Value, true)
                    };

                    tmpListHotspots.Add(tmpWp);
                }
            }
            Hotspots = tmpListHotspots.ToArray();
            OriginalHotspots = Hotspots;
        }

        #endregion

        #region fields

        // to see if the supplied profile in the constructor is valid
        internal bool ProfileValid { get; private set; }
        // Holding the hotspots of the currently loaded profile
        internal Waypoint[] Hotspots { get; set; }
        internal Waypoint[] OriginalHotspots { get; private set; }

        // Holding the vendor hotspots of the currently loaded profile
        internal Waypoint[] VendorHotspots { get; private set; }

        internal Waypoint[] GhostHotspots { get; private set; }
        // Holding all factions to kill
        internal int[] Factions { get; private set; }
        // Vendor infos
        internal NPC VendorNPC { get; private set; }
        // Repair infos
        internal NPC RepairNPC { get; private set; }
        // Restock infos
        internal NPC RestockNPC { get; private set; }
        // Items to restock
        internal RestockItem[] RestockItems { get; private set; }

        #endregion
    }
}