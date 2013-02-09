using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PVPNetConnect.Callbacks
{
   public class Summoner : RiotGamesObject
   {
      public Summoner(Callback callback)
      {
         this.callback = callback;
      }

      public Summoner(TypedObject result)
      {
         base.SetFields<Summoner>(this, result);
      }


      public delegate void Callback(Summoner result);
      private Callback callback;
      public override void DoCallback(TypedObject result)
      {
         base.SetFields<Summoner>(this, result);
         callback(this);
      }

      [InternalName("seasonTwoTier")]
      public string SeasonTwoTier { get; set; }

      [InternalName("internalName")]
      public string InternalName { get; set; }

      [InternalName("acctid")]
      public int Acctid { get; set; }

      [InternalName("helpFlag")]
      public bool HelpFlag { get; set; }

      [InternalName("sumId")]
      public int SumId { get; set; }

      [InternalName("profileIconId")]
      public int ProfileIconId { get; set; }

      [InternalName("displayEloQuestionaire")]
      public bool DisplayEloQuestionaire { get; set; }

      [InternalName("lastGameDate")]
      public DateTime LastGameDate { get; set; }

      [InternalName("advancedTutorialFlag")]
      public bool AdvancedTutorialFlag { get; set; }

      [InternalName("revisionDate")]
      public DateTime RevisionDate { get; set; }

      [InternalName("revisionId")]
      public int RevisionId { get; set; }

      [InternalName("seasonOneTier")]
      public string SeasonOneTier { get; set; }

      [InternalName("name")]
      public string Name { get; set; }

      [InternalName("nameChangeFlag")]
      public bool NameChangeFlag { get; set; }

      [InternalName("tutorialFlag")]
      public bool TutorialFlag { get; set; }

      [InternalName("socialNetworkUserIds")]
      public object SocialNetworkUserIds { get; set; }

   }
}
