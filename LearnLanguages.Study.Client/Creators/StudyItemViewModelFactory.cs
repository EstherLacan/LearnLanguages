﻿using System;
using System.Linq;

using LearnLanguages.Study.Interfaces;
using LearnLanguages.Business;
using LearnLanguages.Common.Delegates;
using LearnLanguages.Common;

namespace LearnLanguages.Study
{
  /// <summary>
  /// The purpose of this class is to produce viewmodels given some input.
  /// </summary>
  public class StudyItemViewModelFactory
  {
    #region Singleton Pattern Members
    private static volatile StudyItemViewModelFactory _Ton;
    private static object _Lock = new object();
    public static StudyItemViewModelFactory Ton
    {
      get
      {
        if (_Ton == null)
        {
          lock (_Lock)
          {
            if (_Ton == null)
              _Ton = new StudyItemViewModelFactory();
          }
        }

        return _Ton;
      }
    }
    #endregion

    /// <summary>
    /// Creates a study item using the phrase and nativeLanguageText.  If the phrase.Language
    /// is the same as the native language, then this will be a study item related to studying
    /// the meaning of the phrase, all in the native language.  If these two languages are 
    /// different, then it will procure a translation study item.
    /// This prepares the view model so that all that is necessary for the caller is to call
    /// viewmodel.Show(...);
    /// </summary>
    public void Procure(PhraseEdit phrase, 
                        string nativeLanguageText, 
                        AsyncCallback<IStudyItemViewModelBase> callback)
    {
      //FOR NOW, THIS WILL JUST CREATE ONE TYPE OF STUDY ITEM VIEW MODEL: STUDY QUESTION ANSWER VIEWMODEL.
      //IT WILL SHOW THE QUESTION AND HIDE THE ANSWER FOR VARIOUS AMOUNTS OF TIME, DEPENDING ON QUESTION
      //LENGTH, AND OTHER FACTORS.
      //IN THE FUTURE, THIS WILL BE EXTENSIBILITY POINT WHERE WE CAN SELECT DIFFERENT VARIETIES OF Q AND A 
      //TRANSLATION VIEW MODELS BASED ON HOW SUCCESSFUL THEY ARE.  WE CAN ALSO HAVE DIFFERENT CONFIGURATIONS
      //OF VARIETIES BE SELECTED HERE.

      ViewModels.StudyTimedQuestionAnswerViewModel viewModel = new ViewModels.StudyTimedQuestionAnswerViewModel();

      var languageText = phrase.Language.Text;

      bool phraseIsInForeignLanguage = (languageText != nativeLanguageText);
      if (phraseIsInForeignLanguage)
      {
        //DO A TRANSLATION Q & A
        //WE NEED TO FIND A TRANSLATION FOR THIS FOREIGN LANGUAGE PHRASE.
        PhraseEdit translatedPhrase = null;

        //FIRST LOOK IN DB.  IF NOT, THEN AUTO TRANSLATE.
        var searchCriteria = new Business.Criteria.TranslationSearchCriteria(phrase, nativeLanguageText);
        TranslationSearchRetriever.CreateNew(searchCriteria, (s, r) =>
          {
            if (r.Error != null)
              throw r.Error;

            var retriever = r.Object;
            var foundTranslation = retriever.Translation;
            if (foundTranslation != null)
            {
              //FOUND TRANSLATION IN DATABASE.  USE THIS AS THE SECOND PHRASE.
              translatedPhrase = (from p in foundTranslation.Phrases
                                  where p.Language.Text == nativeLanguageText
                                  select p).First();
            }
            else
            {
              #region NO TRANSLATION FOUND, WILL NEED TO AUTO TRANSLATE

              AutoTranslate(phrase, nativeLanguageText, (s2, r2) =>
                {

                });

              #endregion
            }

          });



        //RIGHT NOW, WE HAVE A MANUAL AND A TIMED QA.
        var timedQA = new ViewModels.StudyTimedQuestionAnswerViewModel();
        var manualQA = new ViewModels.StudyManualQuestionAnswerViewModel();
        var qaViewModel = RandomPicker.Ton.PickOne<IStudyItemViewModelBase>(timedQA, manualQA);

        if (qaViewModel is ViewModels.StudyTimedQuestionAnswerViewModel)
          ((ViewModels.StudyTimedQuestionAnswerViewModel)qaViewModel).Initialize(phrase, phrase);
        else
          ((ViewModels.StudyManualQuestionAnswerViewModel)qaViewModel).Initialize(phrase, phrase);

        StudyItemViewModelArgs returnArgs = new StudyItemViewModelArgs(qaViewModel);

        //INITIATE THE CALLBACK TO LET IT KNOW WE HAVE OUR VIEWMODEL!  WHEW THAT'S A LOT OF ASYNC.
        //callback(this, new ResultArgs<StudyItemViewModelArgs>(returnArgs));
      }
      else
      {
        //DO A NATIVE LANGUAGE Q & A
      }
    }

    //protected void GetTranslation(PhraseEdit phrase, 
    //                              string targetLanguageText, 
    //                              AsyncCallback<GetTranslationArgs> callback)
    //{
    //  #region Check for translation in DB

    //  //TranslationEdit.GetTranslationEdit(new Business.Criteria.PhraseCriteria(phrase), (s, r) =>
    //  //  {
    //  //    if (r.Error != null)
    //  //      callback(this, new ResultArgs<GetTranslationArgs>(r.Error));

    //  //  });

    //  #endregion






    //  #region Initialize

    //  //_ShowingQuestion = true;
    //  PhraseEdit question = null;
    //  PhraseEdit answer = null;
    //  TranslationEdit qaTranslation = null;

    //  #region 1. CHOOSE RANDOM PHRASE
    //  TranslationEdit.
    //  PhraseList.GetAll((s, r) =>
    //  {
    //    if (r.Error != null)
    //    {
    //      //callback(null, null, r.Error);
    //      callback(this, new ResultArgs<GetTranslationArgs>(r.Error));
    //      return;
    //    }
    //    _Phrases = r.Object;

    //    Random random = new Random(DateTime.Now.Millisecond +
    //                               DateTime.Now.Second +
    //                               DateTime.Now.Month +
    //                               (int)(Mouse.Position.X * 1000));

    //    var randomIndex = random.Next(0, _Phrases.Count);
    //    question = _Phrases[randomIndex];

    //    #region 2. GET TRANSLATION FOR THAT PHRASE, IF WE DON'T HAVE ONE THEN CREATE TRANSLATION.

    //    TranslationList.GetAllTranslationsContainingPhraseById(question, (s2, r2) =>
    //    {
    //      if (r2.Error != null)
    //      {
    //        callback(this, new Args.QuestionAnswerArgs(r2.Error));
    //        //callback(null, null, r2.Error);
    //        return;
    //      }
    //      //throw r2.Error;

    //      #region 3a. FOUND EXISTING TRANSLATION.  SET QUESTION AND ANSWER, AND CALL CALLBACK
    //      //IF WE HAVE FOUND ONE OR MORE TRANSLATIONS, THEN PICK ONE OF THEM AT RANDOM
    //      var foundTranslations = r2.Object;
    //      if (foundTranslations.Count > 0)
    //      {
    //        randomIndex = random.Next(0, foundTranslations.Count);
    //        qaTranslation = foundTranslations[randomIndex];
    //        //PICK ONE OF THE TRANSLATION'S OTHER LANGUAGES THAN THE QUESTION
    //        answer = null;
    //        foreach (var phrase in qaTranslation.Phrases)
    //        {
    //          if (phrase.Text != question.Text)
    //          {
    //            answer = phrase;
    //            break;
    //          }
    //        }
    //        if (answer == null)
    //        {
    //          var ex = new Exception("translation located, but all texts are equal");
    //          callback(this, new Args.QuestionAnswerArgs(ex));
    //          //callback(null, null, ex);
    //          return;
    //        }
    //        //WE HAVE BOTH QUESTION AND ANSWER SO INITIATE CALLBACK
    //        callback(this, new Args.QuestionAnswerArgs(question, answer));
    //        //callback(question, answer, null);
    //        return;
    //      }
    //      #endregion

    //      #region 3b. NO EXISTING TRANSLATION, SO AUTO TRANSLATE WITH BING AND CREATE NEW ANSWER PHRASE
    //      else
    //      {
    //        AutoTranslateText(callback, question, answer);
    //      }
    //      #endregion

    //    });

    //    #endregion
    //  });

    //  #endregion

    //  #endregion
    //}

    private void AutoTranslate(PhraseEdit phrase, 
                               string targetLanguageText, 
                               AsyncCallback<PhraseEdit> callback)
    {

    }
  }
}
