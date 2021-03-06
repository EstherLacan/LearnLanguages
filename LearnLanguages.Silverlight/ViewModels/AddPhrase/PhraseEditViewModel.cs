﻿using System;
using System.ComponentModel.Composition;
using LearnLanguages.Business;
using LearnLanguages.DataAccess;

namespace LearnLanguages.Silverlight.ViewModels
{
  [Export(typeof(PhraseEditViewModel))]
  [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
  public class PhraseEditViewModel : ViewModelBase<PhraseEdit, PhraseDto>
  {
    public PhraseEditViewModel()
    {
      Languages = Services.Container.GetExportedValue<LanguageSelectorViewModel>();
      Languages.SelectedItemChanged += HandleLanguageChanged;
    }

    void HandleLanguageChanged(object sender, EventArgs e)
    {
      Model.Language = ((LanguageEditViewModel)sender).Model;
    }

    private LanguageSelectorViewModel _Languages;
    public LanguageSelectorViewModel Languages
    {
      get { return _Languages; }
      set
      {
        if (value != _Languages)
        {
          _Languages = value;
          NotifyOfPropertyChange(() => Languages);
        }
      }
    }

    public string LabelPhraseText { get { return ViewViewModelResources.LabelAddPhraseViewPhraseText; } }
    public string LabelLanguageText { get { return ViewViewModelResources.LabelAddPhraseViewLanguageText; } }
  }
}
