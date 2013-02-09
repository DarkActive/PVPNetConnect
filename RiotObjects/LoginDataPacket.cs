using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PVPNetConnect.RiotObjects.Summoner;
using PVPNetConnect.RiotObjects.Statistics;

namespace PVPNetConnect.RiotObjects.Client
{
    public class LoginDataPacket : RiotGamesObject
    {
        #region Constructors and Callbacks

        public LoginDataPacket(Callback callback)
        {
            this.callback = callback;
        }

        public LoginDataPacket(TypedObject result)
        {
            base.SetFields<LoginDataPacket>(this, result);
        }


        public delegate void Callback(LoginDataPacket result);
        private Callback callback;
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<LoginDataPacket>(this, result);
            callback(this);
        }

        #endregion

        #region Member Properties

        [InternalName("playerStatSummaries")]
        public PlayerStatSummaries PlayerStatSummaries { get; set; } //PlayerStatSummaries

        [InternalName("minutesUntilShutdown")]
        public int MinutesUntilShutdown { get; set; }

        [InternalName("minor")]
        public bool Minor { get; set; }

        [InternalName("maxPracticeGameSize")]
        public int MaxPracticeGameSize { get; set; }

        [InternalName("summonerCatalog")]
        public TypedObject SummonerCatalog { get; set; }//SummonerCatalog

        [InternalName("ipBalance")]
        public double IpBalance { get; set; }

        [InternalName("reconnectInfo")]
        public object ReconnectInfo { get; set; }

        [InternalName("languages")]
        public ArrayCollection Languages;

        [InternalName("allSummonerData")]
        public AllSummonerData AllSummonerData { get; set; } //AllSummonerData

        [InternalName("customMinutesLeftToday")]
        public int CustomMinutesLeftToday { get; set; }

        [InternalName("coOpVsAiMunitesLeftToday")]
        public int CoOpVsAiMunitesLeftToday { get; set; }

        [InternalName("bingeData")]
        public object BingeData { get; set; }

        [InternalName("inGhostGame")]
        public bool InGhostGame { get; set; }

        [InternalName("leaverPenaltyLevel")]
        public int LeaverPenaltyLevel { get; set; }

        [InternalName("bingePreventionSystemEnabledForClient")]
        public bool BingePreventionSystemEnabledForClient { get; set; }

        [InternalName("pendingBadges")]
        public int PendingBadges { get; set; }

        [InternalName("broadcastNotification")]
        public object BroadcastNotification { get; set; } //BroadcastNotification

        [InternalName("minutesUntilMidnight")]
        public int MinutesUntilMidnight { get; set; }

        [InternalName("minutesUntilFirstWinOfDay")]
        public double MinutesUntilFirstWinOfDay { get; set; }

        [InternalName("coOpVsAiMsecsUntilReset")]
        public double CoOpVsAiMsecsUntilReset { get; set; }

        [InternalName("clientSystemStates")]
        public object ClientSystemStates { get; set; } //ClientStstemStatesNotification

        [InternalName("bingeMinutesRemaining")]
        public double BingeMinutesRemaining { get; set; }

        [InternalName("pendingKudosDTO")]
        public object PendingKudosDTO { get; set; } //PendingKudosDTO

        [InternalName("leaverBusterPenaltyTime")]
        public int LeaverBusterPenaltyTime { get; set; }

        [InternalName("platformId")]
        public string PlatformId { get; set; }

        [InternalName("matchMakingEnabled")]
        public bool MatchMakingEnabled { get; set; }

        [InternalName("minutesUntilShutdownEnabled")]
        public bool MinutesUntilShutdownEnabled { get; set; }

        [InternalName("rpBalance")]
        public double RpBalance { get; set; }

        [InternalName("gameTypeConfigs")]
        public ArrayCollection GameTypeConfigs { get; set; } //GameTypeConfigDTO

        [InternalName("bingeIsPlayerInBingePreventionWindow")]
        public bool BingeIsPlayerInBingePreventionWindow { get; set; }

        [InternalName("minorShutdownEnforced")]
        public bool MinorShutdownEnforced { get; set; }

        [InternalName("competitiveRegion")]
        public string CompetitiveRegion { get; set; }

        [InternalName("customMsecsUntilReset")]
        public double CustomMsecsUntilReset { get; set; }

        #endregion
    }
}
