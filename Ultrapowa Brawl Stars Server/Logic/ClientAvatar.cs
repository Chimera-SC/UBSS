using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UCS.Core;
using UCS.PacketProcessing;
using UCS.GameFiles;
using UCS.Helpers;

namespace UCS.Logic
{
    class ClientAvatar : Avatar
    {

        /*void updateLeague()
        {
            var table = ObjectManager.DataTables.GetTable(12);
            var i = 0;
            bool found = false;
            while (!found)
            {
                var league = (LeagueData)table.GetItemAt(i);
                if (m_vScore <= league.BucketPlacementRangeHigh[league.BucketPlacementRangeHigh.Count - 1] &&
                    m_vScore >= league.BucketPlacementRangeLow[0])
                {
                    found = true;
                    SetLeagueId(i);
                }
                i++;
            }
        }*/

        private long m_vId;
        private long m_vCurrentHomeId;
        private long m_vAllianceId;
        private int m_vAvatarLevel;
        private string m_vAvatarName;
        private int m_vExperience;
        private int m_vCurrentGems;
        private int m_vScore;
        private byte m_vIsAvatarNameSet;
        private int m_vLeagueId;
        private string m_vRegion;

        public ClientAvatar() : base()
        {
            this.Achievements = new List<DataSlot>();
            this.AllianceUnits = new List<DataSlot>();
            this.NpcStars = new List<DataSlot>();
            this.NpcLootedGold = new List<DataSlot>();
            this.NpcLootedElixir = new List<DataSlot>();
            m_vLeagueId = 9;
        }

        public ClientAvatar(long id) : this()
        {
            this.LastUpdate = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            this.Login = id.ToString() + ((int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds).ToString();
            this.m_vId = id;
            this.m_vCurrentHomeId = id;
            m_vIsAvatarNameSet = 0x00;
            m_vAvatarLevel = 9;
            this.m_vAllianceId = 0;
            m_vExperience = 115;
            this.EndShieldTime = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds + Convert.ToInt32(ConfigurationManager.AppSettings["startingShieldTime"]));
            m_vCurrentGems = Convert.ToInt32(ConfigurationManager.AppSettings["startingGems"]);
            m_vScore = Convert.ToInt32(ConfigurationManager.AppSettings["startingTrophies"]);
            this.TutorialStepsCount = 0x0A;
            m_vAvatarName = "NoNameYet";
            SetResourceCount(ObjectManager.DataTables.GetResourceByName("Gold"), Convert.ToInt32(ConfigurationManager.AppSettings["startingGold"]));
            SetResourceCount(ObjectManager.DataTables.GetResourceByName("Elixir"), Convert.ToInt32(ConfigurationManager.AppSettings["startingElixir"]));
            SetResourceCount(ObjectManager.DataTables.GetResourceByName("DarkElixir"), Convert.ToInt32(ConfigurationManager.AppSettings["startingDarkElixir"]));
            SetResourceCount(ObjectManager.DataTables.GetResourceByName("Diamonds"), Convert.ToInt32(ConfigurationManager.AppSettings["startingGems"]));
        }

        public byte[] Encode()
        {
            List<Byte> data = new List<Byte>();

            data.AddInt64(m_vId);
            data.AddInt64(m_vCurrentHomeId);
            data.AddInt64(m_vCurrentHomeId);

            data.AddString("Ultrapowa");// self.player.name)
            data.Add(1);//self.player.name != "Brawler"); // nameSet
            data.AddInt32(1); // Player region ?

            // motorised arrays stars 
            data.Add(0);//5); // Commodity Array
            /*cards = { }
                for key, brawler in self.player.unlocked_brawlers.items():
                for card, amount in brawler["Cards"].items():
                    cards[card] = amount*/

            data.Add(0);//len(cards); + len(ressources_ids)); // cards and ressources array
                /*for key, amount in cards.items():

                    self.writeScId(23, int(key))
    
                    data.Add(amount); // upgrades count*/

            // ressources
                    /*for res in range(len(ressources_ids)):
                self.writeScID(5, ressources_ids[res]); // resource 
                data.Add(ressources[res]); // count*/

            // cards and ressources Array End

            data.Add(0);//len(self.player.unlocked_brawlers));  // brawlers count
                /*for key, brawler_id in self.player.unlocked_brawlers.items():

                    self.writeDataReference(16, int(key))
    
                    data.Add(brawler_id["Trophies"])*/
    
            // Brawlers Trophies for Rank array
            data.Add(0);//len(self.player.unlocked_brawlers));  // brawlers count
            /*for key, brawler_id in self.player.unlocked_brawlers.items():
                self.writeDataReference(16, int(key))
                data.Add(brawler_id["HighestTrophies"])*/

            data.Add(0); // highest ressources array (smart supercell)
                         // brawler seen state array
            data.Add(0);// len(self.player.unlocked_brawlers));  // brawlers count
                /*for key, brawler_id in self.player.unlocked_brawlers.items():
                    self.writeDataReference(16, int(key))
                    data.Add(2)*/

            data.Add(0); //self.player.gems); // gems
            data.Add(13); // free gems (?); 

            data.Add(0); // experience level (unused)
            data.Add(0); // cumulative purchased gems
            data.Add(0); // battles couns
            data.Add(0); // win count
            data.Add(0); // lose count
            data.Add(0); // win/loose streak (in v1 yeah)
            data.Add(0); // npc win count
            data.Add(0); // npc lose count

            data.Add(2);//self.player.tutorialState); // tutorial state

            return data.ToArray();
        }

        public long GetAllianceId()
        {
            return m_vAllianceId;
        }

        public AllianceMemberEntry GetAllianceMemberEntry()
        {
            var alliance = ObjectManager.GetAlliance(m_vAllianceId);
            if (alliance != null)
                return alliance.GetAllianceMember(m_vId);
            return null;
        }

        public int GetAllianceRole()
        {
            var ame = GetAllianceMemberEntry();
            if (ame != null)
                return ame.GetRole();
            return -1;
        }

        public int GetAvatarLevel()
        {
            return m_vAvatarLevel;
        }

        public string GetAvatarName()
        {
            return m_vAvatarName;
        }

        public long GetCurrentHomeId()
        {
            return m_vCurrentHomeId;
        }

        public int GetDiamonds()
        {
            return m_vCurrentGems;
        }

        public long GetId()
        {
            return m_vId;
        }

        public int GetLeagueId()
        {
            return m_vLeagueId;
        }

        public int GetUserRegion()
        {
            return m_vLeagueId;
        }

        public int GetSecondsFromLastUpdate()
        {
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds - this.LastUpdate;
        }

        public int GetScore()
        {
            return m_vScore;
        }

        public bool HasEnoughDiamonds(int diamondCount)
        {
            return (m_vCurrentGems >= diamondCount);
        }

        public bool HasEnoughResources(ResourceData rd, int buildCost)
        {
            return GetResourceCount(rd) >= buildCost;
        }

        public int RemainingShieldTime
        {
            get
            {
                int rest = this.EndShieldTime - (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                return rest > 0 ? rest : 0;
            }
        }

        public string SaveToJSON()
        {
            JObject jsonData = new JObject();

            jsonData.Add("avatar_id", m_vId);
            jsonData.Add("region", m_vRegion);
            jsonData.Add("current_home_id", m_vCurrentHomeId);
            jsonData.Add("alliance_id", m_vAllianceId);
            jsonData.Add("alliance_castle_level", GetAllianceCastleLevel());
            jsonData.Add("alliance_castle_total_capacity", GetAllianceCastleTotalCapacity());
            jsonData.Add("alliance_castle_used_capacity", GetAllianceCastleUsedCapacity());
            jsonData.Add("townhall_level", GetTownHallLevel());
            jsonData.Add("avatar_name", m_vAvatarName);
            jsonData.Add("avatar_level", m_vAvatarLevel);
            jsonData.Add("experience", m_vExperience);
            jsonData.Add("current_gems", m_vCurrentGems);
            jsonData.Add("score", m_vScore);
            jsonData.Add("is_avatar_name_set", m_vIsAvatarNameSet);

            /*JArray jsonResourceCapsArray = new JArray();
            foreach (var resource in GetResourceCaps())
                jsonResourceCapsArray.Add(resource.Save(new JObject()));
            jsonData.Add("max_resources", jsonResourceCapsArray);*/

            JArray jsonResourcesArray = new JArray();
            foreach (var resource in GetResources())
                jsonResourcesArray.Add(resource.Save(new JObject()));
            jsonData.Add("resources", jsonResourcesArray);

            JArray jsonUnitsArray = new JArray();
            foreach (var unit in GetUnits())
                jsonUnitsArray.Add(unit.Save(new JObject()));
            jsonData.Add("units", jsonUnitsArray);

            JArray jsonSpellsArray = new JArray();
            foreach (var spell in GetSpells())
                jsonSpellsArray.Add(spell.Save(new JObject()));
            jsonData.Add("spells", jsonSpellsArray);

            JArray jsonUnitUpgradeLevelsArray = new JArray();
            foreach (var unitUpgradeLevel in m_vUnitUpgradeLevel)
                jsonUnitUpgradeLevelsArray.Add(unitUpgradeLevel.Save(new JObject()));
            jsonData.Add("unit_upgrade_levels", jsonUnitUpgradeLevelsArray);

            JArray jsonSpellUpgradeLevelsArray = new JArray();
            foreach (var spellUpgradeLevel in m_vSpellUpgradeLevel)
                jsonSpellUpgradeLevelsArray.Add(spellUpgradeLevel.Save(new JObject()));
            jsonData.Add("spell_upgrade_levels", jsonSpellUpgradeLevelsArray);

            JArray jsonHeroUpgradeLevelsArray = new JArray();
            foreach (var heroUpgradeLevel in m_vHeroUpgradeLevel)
                jsonHeroUpgradeLevelsArray.Add(heroUpgradeLevel.Save(new JObject()));
            jsonData.Add("hero_upgrade_levels", jsonHeroUpgradeLevelsArray);

            JArray jsonHeroHealthArray = new JArray();
            foreach (var heroHealth in m_vHeroHealth)
                jsonHeroHealthArray.Add(heroHealth.Save(new JObject()));
            jsonData.Add("hero_health", jsonHeroHealthArray);

            JArray jsonHeroStateArray = new JArray();
            foreach (var heroState in m_vHeroState)
                jsonHeroStateArray.Add(heroState.Save(new JObject()));
            jsonData.Add("hero_state", jsonHeroStateArray);

            JArray jsonAllianceUnitsArray = new JArray();
            foreach (var allianceUnit in AllianceUnits)
                jsonAllianceUnitsArray.Add(allianceUnit.Save(new JObject()));
            jsonData.Add("alliance_units", jsonAllianceUnitsArray);

            jsonData.Add("tutorial_step", TutorialStepsCount);

            /*JArray jsonAchievementsArray = new JArray();
            foreach (var achievement in Achievements)
            {
                JObject jsonObject = new JObject();
                jsonObject.Add("global_id", achievement.Data.GetGlobalID());
                jsonAchievementsArray.Add(jsonObject);
            }     
            jsonData.Add("unlocked_achievements", jsonAchievementsArray);*/

            JArray jsonAchievementsProgressArray = new JArray();
            foreach (var achievement in Achievements)
                jsonAchievementsProgressArray.Add(achievement.Save(new JObject()));
            jsonData.Add("achievements_progress", jsonAchievementsProgressArray);

            JArray jsonNpcStarsArray = new JArray();
            foreach (var npcLevel in NpcStars)
                jsonNpcStarsArray.Add(npcLevel.Save(new JObject()));
            jsonData.Add("npc_stars", jsonNpcStarsArray);

            JArray jsonNpcLootedGoldArray = new JArray();
            foreach (var npcLevel in NpcLootedGold)
                jsonNpcLootedGoldArray.Add(npcLevel.Save(new JObject()));
            jsonData.Add("npc_looted_gold", jsonNpcLootedGoldArray);

            JArray jsonNpcLootedElixirArray = new JArray();
            foreach (var npcLevel in NpcLootedElixir)
                jsonNpcLootedElixirArray.Add(npcLevel.Save(new JObject()));
            jsonData.Add("npc_looted_elixir", jsonNpcLootedElixirArray);

            return JsonConvert.SerializeObject(jsonData);
        }

        public void LoadFromJSON(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);

            m_vId = jsonObject["avatar_id"].ToObject<long>();
            m_vRegion = jsonObject["region"].ToObject<string>();
            m_vCurrentHomeId = jsonObject["current_home_id"].ToObject<long>();
            m_vAllianceId = jsonObject["alliance_id"].ToObject<long>();
            SetAllianceCastleLevel(jsonObject["alliance_castle_level"].ToObject<int>());
            SetAllianceCastleTotalCapacity(jsonObject["alliance_castle_total_capacity"].ToObject<int>());
            SetAllianceCastleUsedCapacity(jsonObject["alliance_castle_used_capacity"].ToObject<int>());
            SetTownHallLevel(jsonObject["townhall_level"].ToObject<int>());
            m_vAvatarName = jsonObject["avatar_name"].ToObject<string>();
            m_vAvatarLevel = jsonObject["avatar_level"].ToObject<int>();
            m_vExperience = jsonObject["experience"].ToObject<int>();
            m_vCurrentGems = jsonObject["current_gems"].ToObject<int>();
            m_vScore = jsonObject["score"].ToObject<int>();
            m_vIsAvatarNameSet = jsonObject["is_avatar_name_set"].ToObject<byte>();

            /*JArray jsonMaxResources = (JArray)jsonObject["max_resources"];
            foreach (JObject resource in jsonMaxResources)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(resource);
                m_vResourceCaps.Add(ds);
            }*/

            JArray jsonResources = (JArray)jsonObject["resources"];
            foreach (JObject resource in jsonResources)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(resource);
                GetResources().Add(ds);
            }

            JArray jsonUnits = (JArray)jsonObject["units"];
            foreach (JObject unit in jsonUnits)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(unit);
                m_vUnitCount.Add(ds);
            }

            JArray jsonSpells = (JArray)jsonObject["spells"];
            foreach (JObject spell in jsonSpells)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(spell);
                m_vSpellCount.Add(ds);
            }

            JArray jsonUnitLevels = (JArray)jsonObject["unit_upgrade_levels"];
            foreach (JObject unitLevel in jsonUnitLevels)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(unitLevel);
                m_vUnitUpgradeLevel.Add(ds);
            }

            JArray jsonSpellLevels = (JArray)jsonObject["spell_upgrade_levels"];
            foreach (JObject data in jsonSpellLevels)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                m_vSpellUpgradeLevel.Add(ds);
            }

            JArray jsonHeroLevels = (JArray)jsonObject["hero_upgrade_levels"];
            foreach (JObject data in jsonHeroLevels)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                m_vHeroUpgradeLevel.Add(ds);
            }

            JArray jsonHeroHealth = (JArray)jsonObject["hero_health"];
            foreach (JObject data in jsonHeroHealth)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                m_vHeroHealth.Add(ds);
            }

            JArray jsonHeroState = (JArray)jsonObject["hero_state"];
            foreach (JObject data in jsonHeroState)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                m_vHeroState.Add(ds);
            }

            JArray jsonAllianceUnits = (JArray)jsonObject["alliance_units"];
            foreach (JObject data in jsonAllianceUnits)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                AllianceUnits.Add(ds);
            }

            TutorialStepsCount = jsonObject["tutorial_step"].ToObject<uint>();

            /*JArray jsonUnlockedAchievements = (JArray)jsonObject["unlocked_achievements"];
            foreach (JObject data in jsonUnlockedAchievements)
            {
                int globalId = data["global_id"].ToObject<int>();
                Achievements.Add(globalId);
            }*/

            JArray jsonAchievementsProgress = (JArray)jsonObject["achievements_progress"];
            foreach (JObject data in jsonAchievementsProgress)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                Achievements.Add(ds);
            }

            JArray jsonNpcStars = (JArray)jsonObject["npc_stars"];
            foreach (JObject data in jsonNpcStars)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                NpcStars.Add(ds);
            }

            JArray jsonNpcLootedGold = (JArray)jsonObject["npc_looted_gold"];
            foreach (JObject data in jsonNpcLootedGold)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                NpcLootedGold.Add(ds);
            }

            JArray jsonNpcLootedElixir = (JArray)jsonObject["npc_looted_elixir"];
            foreach (JObject data in jsonNpcLootedElixir)
            {
                var ds = new DataSlot(null, 0);
                ds.Load(data);
                NpcLootedElixir.Add(ds);
            }
        }

        public void SetAllianceId(long id)
        {
            m_vAllianceId = id;
        }

        public void SetAllianceRole(int a)
        {
            var ame = GetAllianceMemberEntry();
            if (ame != null)
                ame.SetRole(a);
        }

        public void SetDiamonds(int count)
        {
            m_vCurrentGems = count;
        }

        public void SetLeagueId(int id)
        {
            m_vLeagueId = id;
        }

        public void SetName(string name)
        {
            m_vAvatarName = name;
            m_vIsAvatarNameSet = 0x01;
            this.TutorialStepsCount = 0x0D;
        }

        public void SetScore(int newScore)
        {
            m_vScore = newScore;
            //updateLeague();
        }

        public void SetRegion(string region)
        {
            m_vRegion = region;
        }
        public void SetAvatarLevel(int newlv)
        {
            m_vAvatarLevel = newlv;
        }

        public void UseDiamonds(int diamondCount)
        {
            m_vCurrentGems -= diamondCount;
        }

        public List<DataSlot> NpcStars { get; set; }
        public List<DataSlot> NpcLootedGold { get; set; }
        public List<DataSlot> NpcLootedElixir { get; set; }
        public List<DataSlot> AllianceUnits { get; set; }
        public List<DataSlot> Achievements { get; set; }
        public int LastUpdate { get; set; }
        public String Login { get; set; }
        public uint Region { get; set; }
        public Village Village { get; set; }
        public int EndShieldTime { get; set; }
        public uint TutorialStepsCount { get; set; }
    }
}
