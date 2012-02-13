﻿using System;
using LearnLanguages.Business;
using System.ComponentModel.Composition;
using LearnLanguages.Common.Interfaces;

namespace LearnLanguages.Study
{
  /// <summary>
  /// MeaningStudier has to know how much of the meaning we understand.  It should have already heard how much
  /// the MultiLineTextsStudier said the user understood of the MLT.  If not, it will have to assess this itself,
  /// but we will assume it heard it for now.  Using this % understood (% known), it will choose a phrase
  /// aggregate size, first publishing a thinking event.  Once the aggregate size is chosen, it will parse
  /// the MLT into aggregate phrases of this size and analyze how much of these phrases it thinks the user knows.
  /// Then based on this, it will choose which phrase to present to the user.  For now, this will be in the form of 
  /// a Q & A.  Once chosen, it will publish an acting event and then publish an offer.  At this point, the 
  /// MeaningStudier is done, though it may listen for feedback with that offer. (future, it can start anticipating 
  /// feedbacks and make ready its next offer).
  /// </summary>
  [Export(typeof(Interfaces.IMeaningStudier<MultiLineTextList>))]
  [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
  public class DefaultMultiLineTextsMeaningStudier : MultiLineTextsMeaningStudierBase
  {
    protected override void StudyImpl()
    {
      
    }
  }
}
