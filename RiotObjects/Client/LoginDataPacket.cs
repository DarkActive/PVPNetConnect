using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PVPNetConnect.RiotObjects.Summoner;
using PVPNetConnect.RiotObjects.Statistics;
using PVPNetConnect.RiotObjects.Game;

namespace PVPNetConnect.RiotObjects.Client
{
    /// <summary>
    /// Class that defines the login data packet.
    /// </summary>
    public class LoginDataPacket : RiotGamesObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginDataPacket"/> class.
        /// </summary>
        /// <param name="callback">The callback.</param>
        public LoginDataPacket(Callback callback)
        {
            this.callback = callback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginDataPacket"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public LoginDataPacket(TypedObject result)
        {
            base.SetFields<LoginDataPacket>(this, result);
        }

        /// <summary>
        /// The delegate for the callback method.
        /// </summary>
        /// <param name="result">The result.</param>
        public delegate void Callback(LoginDataPacket result);

        /// <summary>
        /// The callback method.
        /// </summary>
        /// 
        private Callback callback;

        /// <summary>
        /// The DoCallback method.
        /// </summary>
        /// <param name="result">The result.</param>
        /// 
        public override void DoCallback(TypedObject result)
        {
            base.SetFields<LoginDataPacket>(this, result);
            callback(this);
        }

        /// <summary>
        /// PlayerStat summaries
        /// </summary>
        [InternalName("playerStatSummaries")]
        public PlayerStatSummaries PlayerStatSummaries { get; set; } //PlayerStatSummaries

        /// <summary>
        /// Minutes until shut down.
        /// </summary>
        [InternalName("minutesUntilShutdown")]
        public int MinutesUntilShutdown { get; set; }

        /// <summary>
        /// Minor
        /// </summary>
        [InternalName("minor")]
        public bool Minor { get; set; }

        /// <summary>
        /// Max practice game size.
        /// </summary>
        [InternalName("maxPracticeGameSize")]
        public int MaxPracticeGameSize { get; set; }

        /// <summary>
        /// Summoner Catalog.
        /// </summary>
        [InternalName("summonerCatalog")]
        public SummonerCatalog SummonerCatalog { get; set; }//SummonerCatalog

        /// <summary>
        /// IP Balance
        /// </summary>
        [InternalName("ipBalance")]
        public double IPBalance { get; set; }

        /// <summary>
        /// Reconnection info to reconnect to game.
        /// </summary>
        [InternalName("reconnectInfo")]
        public object ReconnectInfo { get; set; }

        /// <summary>
        /// Languages
        /// </summary>
        [InternalName("languages")]
        public ArrayCollection Languages;

        /// <summary>
        /// Allsummonerdata class with most summoner dta
        /// </summary>
        [InternalName("allSummonerData")]
        public AllSummonerData AllSummonerData { get; set; } //AllSummonerData

        /// <summary>
        /// Custom minutes left today for credit.
        /// </summary>
        [InternalName("customMinutesLeftToday")]
        public int CustomMinutesLeftToday { get; set; }

        /// <summary>
        /// CoOp minutes left today for credit.
        /// </summary>
        [InternalName("coOpVsAiMunitesLeftToday")]
        public int CoOpVsAiMunitesLeftToday { get; set; }

        /// <summary>
        /// Binge Data
        /// </summary>
        [InternalName("bingeData")]
        public object BingeData { get; set; }

        /// <summary>
        /// In Ghost game? (boolean)
        /// </summary>
        [InternalName("inGhostGame")]
        public bool InGhostGame { get; set; }

        /// <summary>
        /// Leaver penalty level.
        /// </summary>
        [InternalName("leaverPenaltyLevel")]
        public int LeaverPenaltyLevel { get; set; }

        /// <summary>
        /// Binge Prevention System Enabled For Client
        /// </summary>
        [InternalName("bingePreventionSystemEnabledForClient")]
        public bool BingePreventionSystemEnabledForClient { get; set; }

        /// <summary>
        /// Pending Badges
        /// </summary>
        [InternalName("pendingBadges")]
        public int PendingBadges { get; set; }

        /// <summary>
        /// Broadcast notification
        /// </summary>
        [InternalName("broadcastNotification")]
        public object BroadcastNotification { get; set; } //BroadcastNotification

        /// <summary>
        /// Minutes until midnight
        /// </summary>
        [InternalName("minutesUntilMidnight")]
        public int MinutesUntilMidnight { get; set; }

        /// <summary>
        /// Minutes until first win of day available.
        /// </summary>
        [InternalName("minutesUntilFirstWinOfDay")]
        public double MinutesUntilFirstWinOfDay { get; set; }

        /// <summary>
        /// Minutes until CoOpvsAi resets.
        /// </summary>
        [InternalName("coOpVsAiMsecsUntilReset")]
        public double CoOpVsAiMsecsUntilReset { get; set; }

        /// <summary>
        /// Client system states.
        /// </summary>
        [InternalName("clientSystemStates")]
        public object ClientSystemStates { get; set; } //ClientStstemStatesNotification

        /// <summary>
        /// Binge Minutes remaining.
        /// </summary>
        [InternalName("bingeMinutesRemaining")]
        public double BingeMinutesRemaining { get; set; }

        /// <summary>
        /// Pending kudos DTO.
        /// </summary>
        [InternalName("pendingKudosDTO")]
        public object PendingKudosDTO { get; set; } //PendingKudosDTO

        /// <summary>
        /// Leaver buster penalty time.
        /// </summary>
        [InternalName("leaverBusterPenaltyTime")]
        public int LeaverBusterPenaltyTime { get; set; }

        /// <summary>
        /// Platform ID
        /// </summary>
        [InternalName("platformId")]
        public string PlatformId { get; set; }

        /// <summary>
        /// Is MatchMaking Enabled? (boolean)
        /// </summary>
        [InternalName("matchMakingEnabled")]
        public bool MatchMakingEnabled { get; set; }

        /// <summary>
        /// Minutes until shut down enabled.
        /// </summary>
        [InternalName("minutesUntilShutdownEnabled")]
        public bool MinutesUntilShutdownEnabled { get; set; }

        /// <summary>
        /// RP balance.
        /// </summary>
        [InternalName("rpBalance")]
        public double RpBalance { get; set; }

        /// <summary>
        /// Game type configs
        /// </summary>
        [InternalName("gameTypeConfigs")]
        public List<GameTypeConfig> GameTypeConfigs { get; set; } //GameTypeConfigDTO

        /// <summary>
        /// binge Is Player In Binge Prevention Window
        /// </summary>
        [InternalName("bingeIsPlayerInBingePreventionWindow")]
        public bool BingeIsPlayerInBingePreventionWindow { get; set; }

        /// <summary>
        /// Minor shutdown enforced
        /// </summary>
        [InternalName("minorShutdownEnforced")]
        public bool MinorShutdownEnforced { get; set; }

        /// <summary>
        /// Competitive region
        /// </summary>
        [InternalName("competitiveRegion")]
        public string CompetitiveRegion { get; set; }

        /// <summary>
        /// Custom mseconds until reset
        /// </summary>
        [InternalName("customMsecsUntilReset")]
        public double CustomMsecsUntilReset { get; set; }

    }
}
