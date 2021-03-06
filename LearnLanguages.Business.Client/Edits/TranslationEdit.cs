﻿using System;
using Csla;
using System.ComponentModel;
using Csla.Serialization;
using Csla.DataPortalClient;
using LearnLanguages.Common.Interfaces;
#if !SILVERLIGHT
using LearnLanguages.DataAccess.Exceptions;
#endif
using LearnLanguages.DataAccess;
using LearnLanguages.Business.Security;
using System.Collections.Generic;


namespace LearnLanguages.Business
{
  [Serializable]
  public class TranslationEdit : LearnLanguages.Business.Bases.BusinessBase<TranslationEdit, TranslationDto>, IHaveId
  {
    #region Ctors and Init
    public TranslationEdit()
    {
      Phrases = PhraseList.NewPhraseList();
    }
    #endregion
    
    #region Factory Methods
    #region Wpf Factory Methods
#if !SILVERLIGHT

    /// <summary>
    /// Creates a new TranslationEdit sync
    /// </summary>
    public static TranslationEdit NewTranslationEdit()
    {
      return DataPortal.Create<TranslationEdit>();
    }
    /// <summary>
    /// Creates a new TranslationEdit sync with given id.
    /// </summary>
    public static TranslationEdit NewTranslationEdit(Guid id)
    {
      return DataPortal.Create<TranslationEdit>(id);
    }
    /// <summary>
    /// Fetches a TranslationEdit sync with given id
    /// </summary>
    public static TranslationEdit GetTranslationEdit(Guid id)
    {
      return DataPortal.Fetch<TranslationEdit>(id);
    }

#endif
    #endregion

    #region Silverlight Factory Methods
#if SILVERLIGHT

    public static void NewTranslationEdit(EventHandler<DataPortalResult<TranslationEdit>> callback)
    {
      DataPortal.BeginCreate<TranslationEdit>(callback);
    }

    ///// <summary>
    ///// This happens DataPortal.ProxyModes.LocalOnly
    ///// </summary>
    //public static void NewTranslationEdit(Guid id, EventHandler<DataPortalResult<TranslationEdit>> callback)
    //{
    //  DataPortal.BeginCreate<TranslationEdit>(id, callback, DataPortal.ProxyModes.LocalOnly);
    //  //DataPortal.BeginCreate<TranslationEdit>(id, callback);
    //}

    public static void GetTranslationEdit(Guid id, EventHandler<DataPortalResult<TranslationEdit>> callback)
    {
      DataPortal.BeginFetch<TranslationEdit>(id, callback);
    }

#endif
    #endregion
    #endregion

    #region Business Properties & Methods

    //PHRASES
    //#region public ICollection<Guid> PhraseIds
    //public static readonly PropertyInfo<ICollection<Guid>> PhraseIdsProperty =
    //  RegisterProperty<ICollection<Guid>>(c => c.PhraseIds);
    //public ICollection<Guid> PhraseIds
    //{
    //  get { return GetProperty(PhraseIdsProperty); }
    //  set { SetProperty(PhraseIdsProperty, value); }
    //}
    //#endregion
    #region public PhraseList Phrases (child)
    public static readonly PropertyInfo<PhraseList> PhrasesProperty = 
      RegisterProperty<PhraseList>(c => c.Phrases, RelationshipTypes.Child);
    public PhraseList Phrases
    {
      get { return GetProperty(PhrasesProperty); }
      set { LoadProperty(PhrasesProperty, value); }
    }
    #endregion
        
    //USER
    #region public Guid UserId
    public static readonly PropertyInfo<Guid> UserIdProperty = RegisterProperty<Guid>(c => c.UserId);
    public Guid UserId
    {
      get { return GetProperty(UserIdProperty); }
      set { SetProperty(UserIdProperty, value); }
    }
    #endregion
    #region public string Username
    public static readonly PropertyInfo<string> UsernameProperty = RegisterProperty<string>(c => c.Username);
    public string Username
    {
      get { return GetProperty(UsernameProperty); }
      set { SetProperty(UsernameProperty, value); }
    }
    #endregion
    #region public CustomIdentity User
    public static readonly PropertyInfo<CustomIdentity> UserProperty =
      RegisterProperty<CustomIdentity>(c => c.User, RelationshipTypes.Child);
    public CustomIdentity User
    {
      get { return GetProperty(UserProperty); }
      private set { LoadProperty(UserProperty, value); }
    }
    #endregion

    public override void LoadFromDtoBypassPropertyChecksImpl(TranslationDto dto)
    {
      using (BypassPropertyChecks)
      {
        LoadProperty<Guid>(IdProperty, dto.Id);
        if (dto.PhraseIds != null && dto.PhraseIds.Count > 0)
        {
          //PhraseIds = dto.PhraseIds;
          Phrases = DataPortal.FetchChild<PhraseList>(dto.PhraseIds);
        }
        LoadProperty<Guid>(UserIdProperty, dto.UserId);
        LoadProperty<string>(UsernameProperty, dto.Username);
        if (!string.IsNullOrEmpty(dto.Username))
          User = DataPortal.FetchChild<CustomIdentity>(dto.Username);
      }

      BusinessRules.CheckRules();
    }
    public override TranslationDto CreateDto()
    {
      var _phraseIds = new List<Guid>();
      if (Phrases != null)
      {
        foreach (var p in Phrases)
        {
          _phraseIds.Add(p.Id);
        }
      }
      TranslationDto retDto = new TranslationDto(){
                                          Id = this.Id,
                                          //PhraseIds = this.PhraseIds,
                                          PhraseIds = _phraseIds,
                                          UserId = this.UserId,
                                          Username = this.Username
                                        };
      return retDto;
    }

    /// <summary>
    /// Begins to persist object
    /// </summary>
    public override void BeginSave(bool forceUpdate, EventHandler<Csla.Core.SavedEventArgs> handler, object userState)
    {
      base.BeginSave(forceUpdate, handler, userState);
    }

    /// <summary>
    /// Loads the default properties, including generating a new Id, inside of a using (BypassPropertyChecks) block.
    /// </summary>
    private void LoadDefaults()
    {
      using (BypassPropertyChecks)
      {
        Id = Guid.NewGuid();
        Phrases = null;
        UserId = Guid.Empty;
        Username = DalResources.DefaultNewTranslationUsername;
      }
      BusinessRules.CheckRules();
    }

    public int GetEditLevel()
    {
      return EditLevel;
    }

    public void AddPhrase(PhraseEdit phrase)
    {
      //PhraseIds.Add(phrase.Id);
      Phrases.Add(phrase);
      BusinessRules.CheckRules();
    }

    protected override void OnChildChanged(Csla.Core.ChildChangedEventArgs e)
    {
      base.OnChildChanged(e);
      BusinessRules.CheckRules(PhrasesProperty);
    }

    #endregion

    #region Validation Rules

    protected override void AddBusinessRules()
    {
      base.AddBusinessRules();

      // TODO: add validation rules
      BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(IdProperty));
      BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(PhrasesProperty));
      BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(UserIdProperty));
      BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(UsernameProperty));


      //translation must have 2 phraseids to be valid
      BusinessRules.AddRule(new CollectionMinimumCountBusinessRule(PhrasesProperty, 2));
      //BusinessRules.AddRule(new CollectionCountsAreEqualBusinessRule(PhrasesProperty, PhraseIdsProperty));
    }

    #endregion

    #region Authorization Rules

    public static void AddObjectAuthorizationRules()
    {
      // TODO: TranslationEdit Authorization: add object-level authorization rules
      // Csla.Rules.CommonRules.Required
    }

    #endregion

    #region Data Access (This is run on the server, unless run local set)

    #region Wpf DP_XYZ
#if !SILVERLIGHT

    [Transactional(TransactionalTypes.TransactionScope)]
    protected override void DataPortal_Create()
    {
      using (var dalManager = DalFactory.GetDalManager())
      {
        var TranslationDal = dalManager.GetProvider<ITranslationDal>();
        var result = TranslationDal.New(null);
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new CreateFailedException(result.Msg);
        }
        TranslationDto dto = result.Obj;
        LoadFromDtoBypassPropertyChecks(dto);
        var dummy = this;
      }
    }

    [Transactional(TransactionalTypes.TransactionScope)]
    protected void DataPortal_Fetch(Guid id)
    {
      using (var dalManager = DalFactory.GetDalManager())
      {
        var TranslationDal = dalManager.GetProvider<ITranslationDal>();
        Result<TranslationDto> result = TranslationDal.Fetch(id);
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new FetchFailedException(result.Msg);
        }
        TranslationDto dto = result.Obj;
        LoadFromDtoBypassPropertyChecks(dto);
      }
    }
    [Transactional(TransactionalTypes.TransactionScope)]
    protected override void DataPortal_Insert()
    {
      //Dal is responsible for setting new Id
      //TranslationDto dto = new TranslationDto()
      //{
      //  Id = this.Id,
      //  LanguageId = this.LanguageId,
      //  Text = this.Text
      //};
      using (var dalManager = DalFactory.GetDalManager())
      {
        FieldManager.UpdateChildren(this);
        //DataPortal.UpdateChild(Phrases);

        var TranslationDal = dalManager.GetProvider<ITranslationDal>();
        var dto = CreateDto();
        var result = TranslationDal.Insert(dto);
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new InsertFailedException(result.Msg);
        }
        //SetIdBypassPropertyChecks(result.Obj.Id);
        //Loading the whole Dto now because I think the insert may affect LanguageId and UserId, and the object
        //may need to load new LanguageEdit child, new languageId, etc.
        LoadFromDtoBypassPropertyChecks(result.Obj);
      }
    }
    [Transactional(TransactionalTypes.TransactionScope)]
    protected override void DataPortal_Update()
    {
      using (var dalManager = DalFactory.GetDalManager())
      {
        FieldManager.UpdateChildren(this);

        var TranslationDal = dalManager.GetProvider<ITranslationDal>();
        var dto = CreateDto();
        Result<TranslationDto> result = TranslationDal.Update(dto);
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new UpdateFailedException(result.Msg);
        }
        //SetIdBypassPropertyChecks(result.Obj.Id);
        //Loading the whole Dto now because I think the insert may affect LanguageId and UserId, and the object
        //may need to load new LanguageEdit child, new languageId, etc.
        LoadFromDtoBypassPropertyChecks(result.Obj);
      }
    }
    [Transactional(TransactionalTypes.TransactionScope)]
    protected override void DataPortal_DeleteSelf()
    {
      using (var dalManager = DalFactory.GetDalManager())
      {
        var TranslationDal = dalManager.GetProvider<ITranslationDal>();
        var result = TranslationDal.Delete(ReadProperty<Guid>(IdProperty));
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new DeleteFailedException(result.Msg);
        }
      }
    }
    [Transactional(TransactionalTypes.TransactionScope)]
    protected void DataPortal_Delete(Guid id)
    {
      using (var dalManager = DalFactory.GetDalManager())
      {
        var TranslationDal = dalManager.GetProvider<ITranslationDal>();
        var result = TranslationDal.Delete(id);
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new DeleteFailedException(result.Msg);
        }
      }
    }

#endif
    #endregion //Wpf DP_XYZ
    
    #region Child DP_XYZ
    
#if !SILVERLIGHT

    //[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    //public void Child_Fetch(Guid id)
    //{
    //  using (var dalManager = DalFactory.GetDalManager())
    //  {
    //    var TranslationDal = dalManager.GetProvider<ITranslationDal>();
    //    var result = TranslationDal.Fetch(id);
    //    if (result.IsError)
    //      throw new FetchFailedException(result.Msg);
    //    TranslationDto dto = result.Obj;
    //    LoadFromDtoBypassPropertyChecks(dto);
    //  }
    //}

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public void Child_Fetch(TranslationDto dto)
    {
      LoadFromDtoBypassPropertyChecks(dto);

      //using (var dalManager = DalFactory.GetDalManager())
      //{
      //  var TranslationDal = dalManager.GetProvider<ITranslationDal>();
      //  var result = TranslationDal.Fetch(id);
      //  if (result.IsError)
      //    throw new FetchFailedException(result.Msg);
      //  TranslationDto dto = result.Obj;
      //  LoadFromDtoBypassPropertyChecks(dto);
      //}
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public void Child_Insert()
    {   
      using (var dalManager = DalFactory.GetDalManager())
      {
        FieldManager.UpdateChildren(this);

        var TranslationDal = dalManager.GetProvider<ITranslationDal>();
        using (BypassPropertyChecks)
        {
          var dto = CreateDto();
          var result = TranslationDal.Insert(dto);
          if (!result.IsSuccess)
          {
            Exception error = result.GetExceptionFromInfo();
            if (error != null)
              throw error;
            else
              throw new InsertFailedException(result.Msg);
          }
          LoadFromDtoBypassPropertyChecks(result.Obj);
        }
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public void Child_Update()
    {
      using (var dalManager = DalFactory.GetDalManager())
      {
        FieldManager.UpdateChildren(this);

        var TranslationDal = dalManager.GetProvider<ITranslationDal>();

        var dto = CreateDto();
        var result = TranslationDal.Update(dto);
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new UpdateFailedException(result.Msg);
        }
        LoadFromDtoBypassPropertyChecks(result.Obj);
      }
    }

    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public void Child_DeleteSelf()
    {
      using (var dalManager = DalFactory.GetDalManager())
      {
        var TranslationDal = dalManager.GetProvider<ITranslationDal>();

        var result = TranslationDal.Delete(Id);
        if (!result.IsSuccess)
        {
          Exception error = result.GetExceptionFromInfo();
          if (error != null)
            throw error;
          else
            throw new DeleteFailedException(result.Msg);
        }
      }
    }

#endif

    #endregion

    #endregion

    
  }
}
