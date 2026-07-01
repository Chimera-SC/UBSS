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
using UBSS.Core;
using UBSS.PacketProcessing;
using UBSS.GameFiles;
using UBSS.Helpers;
using Ionic.Zlib;

namespace UBSS.Logic
{
    class ClientHome : Base
    {
        private long m_vId;
        private int m_vRemainingShieldTime;
        private byte[] m_vSerializedVillage;

        public ClientHome() : base(0)
        {
        }

        public ClientHome(long id)
            : base(0)
        {
            m_vId = id;
        }

        public override byte[] Encode()
        {
            List<Byte> data = new List<Byte>();

            data.Add(0);//2017189);  // Shop Timestamp
            data.Add(100); // box cost (gold)
            data.Add(10); // box cost (gems)
            data.Add(80); // box cost (gems)
            data.Add(10); // box cost (gems)
            data.Add(20); // Coin Boost cost
            data.Add(50); // Coin Boost %
            data.Add(50); // Coin Doubler cost
            data.Add(0);//1000); // Coin Doubled
            data.Add(7 * 24); // Coin Boost Hours
            data.Add(0);//self.brawlersTrophies); // Minimum Brawler Trophies For Season Reset
            data.Add(50); // Brawler Trophy Loss Percentage in Season Reset
            data.Add(0);//9999); // Coin Limit Remaining

            data.Add(0);
            data.Add(0);
            data.Add(0);
            //self.writeArrayVInt([1, 2, 5, 10, 20, 60]); // Duplicated Brawler Chips
            //self.writeArrayVInt([3, 10, 20, 60, 200, 500]); // Brawler Chip Cost
            //self.writeArrayVInt([0, 30, 80, 170, 0, 0]); // Boxes With Guaranteed Brawlers Cost
            // Events array starts

            // Brawlers required for events starts

            data.Add(0);//self.player.eventCount); // 
            /*requiredBrawlers = [0, 3, 5, 7
            for event in range(self.player.eventCount):
                data.Add(event + 1); // event index
                data.Add(requiredBrawlers[event]); // Brawlers needed for that*/

            // Brawlers required for events ends

            // disponible events starts
            //eventData = db.loadEvents(1)["info"]["events"]
            data.Add(0);//len(eventData)); // disponibles event slot
            /*index = 0
            for event in eventData:
                events = eventData [event]
                data.Add(index + 1); // slot index
                data.Add(index + 1); // slot number
                data.Add(events["TimeStamp"] - int (time())); // comming soon timer
                data.Add(events["TimeStamp"] - int (time())); // Time Left
                data.Add(events["Tokens"]); // coins to claim
                data.Add(8); // bonuska coins
                data.Add(999); // coins to win
                data.Add(False); // double coins
                data.Add(index == 3); // double exp
                self.writeScID(15, events["ID"]); // map
                data.Add(0); //  coins already collected
                data.Add(2); //  coins collected statut
                self.writeString("Server by PrimoDEVHacc"); // text for event (TID); please keep it for credits
                data.Add(False)
                index += 1*/

            // disponible events ends

            // comming soon events starts
            //eventData = db.loadEvents(2)["info"]["events"]
            data.Add(0);//len(eventData)); // disponibles event slot
            /*index = 0
            for event in eventData:
                events = eventData [event]
                data.Add(index + 1); // slot index
                data.Add(index + 1); // slot number
                data.Add(events["TimeStamp"] - int (time())); // comming soon timer
                data.Add(events["TimeStamp"] - int (time())); // Time Left
                data.Add(events["Tokens"]); // coins to claim
                data.Add(8); // bonuska coins
                data.Add(999); // coins to win
                data.Add(False); // double coins
                data.Add(index == 3); // double exp
                self.writeScID(15, events["ID"]); // map
                data.Add(0); //  coins already collected
                data.Add(2); //  coins collected statut
                self.writeString("Server by PrimoDEVHacc"); // text for event (TID); please keep it for credits
                data.Add(False)
                index += 1*/
            // comming soon event ends

            // Events array ends

            data.Add(0);//self.maximumUpgradeLevel); // upgrades Array
            /*for x in range(self.maximumUpgradeLevel); :
                data.Add(x + 1); // price*/

            data.Add(0);//2017189) // Timestamp
            data.Add(10); // Create new band timer

            data.Add(0);//self.player.trophies)  // Trophies
            data.Add(0);//self.player.highest_trophies)  // Highest Trophies
            data.Add(0);
            data.Add(0);//self.player.player_experience)  // Experience

            data.Add(28);
            data.Add(0);
            //self.writeScID(28, self.player.profile_icon)  // Player Icon

            data.Add(7); // Played Game Modes Count
            /*for x in range(7): 
                data.Add(x) // Played Game Mode*/
            //non_zero_skins = []
            /*for brawler in self.player.unlocked_brawlers.values():
                if brawler["selectedSkin"] != 0:
                    non_zero_skins.append(brawler["selectedSkin"])*/
            data.Add(0);//len(non_zero_skins))
            /*for skin in non_zero_skins:
                self.writeDataReference(29, skin)

            non_zero_skins = []
            for brawler in self.player.unlocked_brawlers.values():
                for skin in brawler["Skins"]:
                    if skin != 0:
                        non_zero_skins.append(skin)*/
            data.Add(0);//len(non_zero_skins))
            /*for skin in non_zero_skins:
                self.writeDataReference(29, skin)*/

            data.Add(0);//False) // is time required to create new Band
            data.Add(0); // unknown bruh
            data.Add(0);// self.player.coins_reward) // coins got
            data.Add(0);//False)
            data.Add(0);//self.player.control_mode) // Control Mode [0 - Tap to move, 1 - Joystick move, 2 - Double Joysticks (prototype)]
            data.Add(0);//self.player.has_battle_hints) // is battle hints enabled
            data.Add(0);//self.player.coinsdoubler) // coins doubler coins remaining (0 = not activated)
            /*if self.player.coinsbooster - int(datetime.timestamp(datetime.now())) > 0:
                data.Add(self.player.coinsbooster - int(datetime.timestamp(datetime.now()))) // coin boost secs remaining (0 = not activated)
            else:*/
                data.Add(0);
                /*self.player.coinsbooster = int(datetime.timestamp(datetime.now()))
                db.replaceValue("coinsbooster", self.player.coinsbooster)*/
            data.Add(0);//self.settings["nextSeasonEndTimestamp"] - int(time()))
            data.Add(0);// False) // unknown

            data.Add(0);
            data.Add(1);
            //self.writeLogicLong(0, 1
            data.Add(0);
            data.Add(1);
            //self.writeLogicLong(0, 1)
            data.Add(0);
            data.Add(1);
            //self.writeLogicLong(0, 1)
            data.Add(0);
            data.Add(1);
            //self.writeLogicLong(0, 1)
            data.Add(0);
            data.Add(1);
            //self.writeDataReference(0, 1) // shop dataref (the id is 2)
            data.Add(0);
            data.Add(1);//True)
            data.Add(1);//True)

            data.AddInt64(m_vId);
            /*seenNotifs = []
            for key in self.player.homeNotifications:
                notif = self.player.homeNotifications[key]
                if not notif["seen"]:
                    seenNotifs.append(key)*/
            data.Add(0);//len(seenNotifs))
            /*for key in seenNotifs:
                notif = self.player.homeNotifications[key]
                self.writeVInt(notif["ID"]) # notif id 97 (expired coins booster), 98 (gatcha drop or someshit) or 99 (season end)
                if notif["ID"] == 97:
                    self.writeVInt(notif["type"])
                elif notif["ID"] == 98:
                    self.writeVInt(1) # unk
                    self.writeVInt(2) # unk
                elif notif["ID"] == 99:
                    # region 1
                    self.writeVInt(self.player.unlocked_brawlers[str(notif["bestBrawler"])]["Trophies"]) # total best brawler trophies
                    self.writeVInt(notif["bestBrawlerPoints"]) # total season points for the best hero
                    self.writeVInt(notif["bestBrawlerCoins"]) # coins gained for the best hero
                    self.writeVInt(notif["bestBrawlerBonus"]) # bonus coins gained for the best hero
                    # end
                    # region 2
                    self.writeVInt(self.player.trophies) # total trophies
                    self.writeVInt(notif["totalTrophiesPoints"]) # total trophies total points
                    self.writeVInt(notif["totalTrophiesCoins"]) # total trophies gained coins
                    self.writeVInt(notif["totalTrophiesBonus"]) # total trophies gained bonus
                    # end
                    self.writeVInt(0) # idk
                    self.writeVInt(9000) # idk
                    self.writeDataReference(16, notif["bestBrawler"]) # best braler
                    self.writeVInt(9000) # idk
                    self.writeVInt(0) # idk*/

            return data.ToArray();
        }

        public byte[] GetHomeJSON()
        {
            return m_vSerializedVillage;
        }

        public void SetHomeJSON(string json)
        {
            m_vSerializedVillage = ZlibStream.CompressString(json);
        }

        public void SetShieldDurationSeconds(int seconds)
        {
            m_vRemainingShieldTime = seconds;
        }
    }
}
