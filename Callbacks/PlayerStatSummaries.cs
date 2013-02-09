using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.Callbacks
{
   public class PlayerStatSummaries : RiotGamesObject
   {
      public PlayerStatSummaries(Callback callback)
      {
         this.callback = callback;
      }

      public PlayerStatSummaries(TypedObject result)
      {
         base.SetFields<PlayerStatSummaries>(this, result);
      }


      public delegate void Callback(PlayerStatSummaries result);
      private Callback callback;
      public override void DoCallback(TypedObject result)
      {
         base.SetFields<PlayerStatSummaries>(this, result);
         callback(this);
      }


      //public List<PlayerStatSummary> playerStatSummarySet;
      //May be a real value, using int out of convenience

      [InternalName("userId")]
      public double UserId { get; set; }

   }
}
