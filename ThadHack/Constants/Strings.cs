namespace ZzukBot.Constants
{
    internal class Strings
    {
        /// <summary>
        ///     Const for Attack and Wand functions
        /// </summary>
        internal const string WandStop = "if IsAutoRepeatAction(23) == 1 then CastSpellByName('Shoot') end";

        internal const string WandStart = "if IsAutoRepeatAction(23) == nil then CastSpellByName('Shoot') end";
        internal const string Attack = "if IsCurrentAction('24') == nil then CastSpellByName('Attack') end";
        internal const string StopAttack = "if IsCurrentAction('24') ~= nil then CastSpellByName('Attack') end";
        internal const string CheckWand = "thisHasWandEquipped = HasWandEquipped()";

        internal const string RangedAttackStart =
            "if IsAutoRepeatAction(22) == nil then CastSpellByName('Auto Shot') end";

        internal const string RangedAttackStop = "if IsAutoRepeatAction(22) == 1 then CastSpellByName('Auto Shot') end";

        /// <summary>
        ///     Const to check enchant on main or offhand
        /// </summary>
        internal const string IsMainhandEnchanted = "mainhand1 = GetWeaponEnchantInfo()";

        internal const string GT_IsMainhandEnchanted = "mainhand1";
        internal const string IsOffhandEnchanted = "_, _, _, offhand1 = GetWeaponEnchantInfo()";
        internal const string GT_IsOffhandEnchanted = "offhand1";

        /// <summary>
        ///     Const to check if vendor window is open
        /// </summary>
        internal const string IsVendorOpen =
            "if MerchantFrame:IsVisible() then vendorSh1 = 'true' else vendorSh1 = 'false' end";

        internal const string GT_IsVendorOpen = "vendorSh1";

        internal const string RepairAll =
            "if MerchantRepairAllButton:IsVisible() then MerchantRepairAllButton:Click() end";

        /// <summary>
        ///     Const to pickup main and offhand
        /// </summary>
        internal const string EnchantMainhand = "PickupInventoryItem(16)";

        internal const string EnchantOffhand = "PickupInventoryItem(17)";

        internal const string PosInfos =
            "px,py=GetPlayerMapPosition('player') px=px*100 py=py*100 justPos = format('%i/%i', px,py) posInfos = GetZoneText() .. ' ' .. justPos";

        internal const string GT_PosInfos = "posInfos";

        internal const string SkipGossip =
            "arg = { GetGossipOptions() }; count = 1; typ = 2; while true do if arg[typ] ~= nil then if arg[typ] == 'vendor' then SelectGossipOption(count); break; else count = count + 1; typ = typ + 2; end else break end end";

        internal const string CtmOn = "ConsoleExec('Autointeract 1')";
        internal const string CtmOff = "ConsoleExec('Autointeract 0')";

        // <summary>
        // Const to contol pets
        // </summary>
        internal const string CastPetSpell1 = "for index = 1,11,1 do curName = GetPetActionInfo(index); if curName == '";
        internal const string CastPetSpell2 = "' then CastPetAction(index); break end end";

        internal const string GetPetSpellCd1 =
            "PetSpellEnabled = 0; for index = 1,11,1 do curName = GetPetActionInfo(index); if curName == '";

        internal const string GetPetSpellCd2 =
            "' then startTime, duration, enable = GetPetActionCooldown(index); PetSpellEnabled = duration; end end";

        internal const string GetPetHappiness =
            "happiness, damagePercentage, loyaltyRate = GetPetHappiness(); MyPetHappiness = happiness;";

        internal const string CheckFeedPet = "CanFeedMyPet = 0; if CursorHasSpell() then CanFeedMyPet = 1 end;";
        internal const string FeedPet = "CastSpellByName('Feed Pet'); TargetUnit('Pet');";

        internal const string UsePetFood1 =
            "for bag = 0,4 do for slot = 1,GetContainerNumSlots(bag) do local item = GetContainerItemLink(bag,slot) if item then if string.find(item, '";

        internal const string UsePetFood2 = "') then PickupContainerItem(bag,slot) break end end end end";

        internal const string GetUnitRace = "zzRace = UnitRace('unit')";
        internal const string GT_GetUnitRace = "zzRace";

        internal const string GetLatency = "_, _, zzDrei = GetNetStats()";
        internal const string GT_GetLatency = "zzDrei";

        internal const string TurnOnSelfCast = "SetCVar('autoSelfCast',1)";
    }
}