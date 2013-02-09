using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.Callbacks
{
   public class AllSummonerData : RiotGamesObject
   {
      public AllSummonerData(Callback callback)
      {
         this.callback = callback;
      }

      public AllSummonerData(TypedObject result)
      {
         base.SetFields<AllSummonerData>(this, result);
      }


      public delegate void Callback(AllSummonerData result);
      private Callback callback;
      public override void DoCallback(TypedObject result)
      {
         base.SetFields<AllSummonerData>(this, result);
         callback(this);
      }

      [InternalName("summoner")]
      public Summoner Summoner { get; set; }

      [InternalName("summonerLevel")]
      public SummonerLevel SummonerLevel { get; set; }
   }
}
