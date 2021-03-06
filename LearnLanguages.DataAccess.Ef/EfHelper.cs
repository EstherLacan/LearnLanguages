﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using LearnLanguages.Business.Security;

namespace LearnLanguages.DataAccess.Ef
{
  public static class EfHelper
  {
    public static RoleData ToData(RoleDto dto)
    {
      return new RoleData()
      {
        Id = dto.Id,
        Text = dto.Text,
      };
    }
    public static RoleDto ToDto(RoleData data)
    {
      return new RoleDto()
      {
        Id = data.Id,
        Text = data.Text
      };
    }

    //public static UserData ToData(UserDto dto, bool includeForeignEntities = true)
    //{
    //  var retUserData = new UserData()
    //  {
    //    Id = dto.Id,
    //    Username = dto.Username,
    //    Salt = dto.Salt,
    //    SaltedHashedPasswordValue = dto.SaltedHashedPasswordValue
    //  };

    //  if (includeForeignEntities)
    //  {
    //    using (var dalManager = DalFactory.GetDalManager())
    //    {
    //      //USER PHRASES
    //      var phraseDal = dalManager.GetProvider<IPhraseDal>();
    //      foreach (var phraseId in dto.PhraseIds)
    //      {
    //        var result = phraseDal.Fetch(phraseId);
    //        if (!result.IsSuccess || result.IsError)
    //          throw new Exceptions.FetchFailedException(result.Msg);

    //        //var phraseData = ToData(result.Obj);
    //        //retUserData.PhraseDatas.Add(phraseData);
    //        var phraseData = EfHelper.AddToContext(result.Obj, retUserData.
    //      }

    //      //USER ROLES
    //      var customIdentityDal = dalManager.GetProvider<ICustomIdentityDal>();
    //      foreach (var roleId in dto.RoleIds)
    //      {
    //        var result = customIdentityDal.GetRoles(dto.Username);
    //        if (!result.IsSuccess || result.IsError)
    //          throw new Exceptions.FetchFailedException(result.Msg);

    //        var roleDtos = result.Obj;
    //        foreach (var roleDto in roleDtos)
    //        {
    //          RoleData roleData = new RoleData()
    //          {
    //            Id = roleDto.Id,
    //            Text = roleDto.Text
    //          };
    //          retUserData.RoleDatas.Add(roleData);
    //        }
    //      }
    //    }
    //  }

    //  return retUserData;
    //}
    public static UserDto ToDto(UserData data)
    {
      //SCALARS
      var retUserDto = new UserDto()
      {
        Id = data.Id,
        Username = data.Username,
        Salt = data.Salt,
        SaltedHashedPasswordValue = data.SaltedHashedPasswordValue
      };

      //PHRASES
      retUserDto.PhraseIds = new List<Guid>();
      foreach (var phraseData in data.PhraseDatas)
      {
        retUserDto.PhraseIds.Add(phraseData.Id);
      }

      //ROLES
      retUserDto.RoleIds = new List<Guid>();
      foreach (var roleData in data.RoleDatas)
      {
        retUserDto.RoleIds.Add(roleData.Id);
      }

      return retUserDto;
    }

    public static PhraseDto ToDto(PhraseData data)
    {
      return new PhraseDto()
      {
        Id = data.Id,
        Text = data.Text,
        LanguageId = data.LanguageDataId,
        UserId = data.UserDataId,
        Username = data.UserDataReference.Value.Username
      };
    }
    /// <summary>
    /// Adds the phraseDto to the context, loading UserData and LanguageData into the newly
    /// created PhraseData.  Does NOT save changes to the context.
    /// </summary>
    public static PhraseData AddToContext(PhraseDto dto, LearnLanguagesContext context)
    {
      //only creates, does not add to phrasedatas
      //var beforeCount = context.PhraseDatas.Count();
      var newPhraseData = context.PhraseDatas.CreateObject();
      //var afterCount = context.PhraseDatas.Count();

      //assign properties
      newPhraseData.Text = dto.Text;
      newPhraseData.LanguageDataId = dto.LanguageId;
      newPhraseData.UserDataId = dto.UserId;

      context.PhraseDatas.AddObject(newPhraseData);

      return newPhraseData;
    }
    /// <summary>
    /// Adds the TranslationDto to the context, loading UserData and PhraseDatas into the newly
    /// created PhraseData.  Does NOT save changes to the context.
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="learnLanguagesContext"></param>
    /// <returns></returns>
    public static TranslationData AddToContext(TranslationDto dto, LearnLanguagesContext context)
    {
      //CREATE NEW TRANSLATIONDATA
      var newTranslationData = context.TranslationDatas.CreateObject();

      //ASSIGN USER INFO
      newTranslationData.UserDataId = dto.UserId;
      //var userResults = (from user in context.UserDatas
      //                   where user.Id == dto.UserId
      //                   select user);
      //if (userResults.Count() == 1)
      //  newTranslationData.UserData = userResults.First();
      //else if (userResults.Count() == 0)
      //  throw new Exceptions.IdNotFoundException(dto.UserId);
      //else
      //{
      //  var errorMsg = string.Format(DalResources.ErrorMsgVeryBadException,
      //                               DalResources.ErrorMsgVeryBadExceptionDetail_ResultCountNotOneOrZero);
      //  throw new Exceptions.VeryBadException(errorMsg);
      //}
      

      //GET AND ADD PHRASEDATAS TO TRANSLATIONDATA
      if (dto.PhraseIds != null)
      {
        foreach (var id in dto.PhraseIds)
        {
          var results = (from phrase in context.PhraseDatas
                         where phrase.Id == id
                         select phrase);

          if (results.Count() == 1)
          {
            var phraseData = results.First();
            newTranslationData.PhraseDatas.Add(phraseData);
          }
          else if (results.Count() == 0)
            throw new Exceptions.IdNotFoundException(id);
          else
          {
            var errorMsg = string.Format(DalResources.ErrorMsgVeryBadException,
                                         DalResources.ErrorMsgVeryBadExceptionDetail_ResultCountNotOneOrZero);
            throw new Exceptions.VeryBadException(errorMsg);
          }
        }
      }
      
      //ADD TRANSLATIONDATA TO CONTEXT
      context.TranslationDatas.AddObject(newTranslationData);

      return newTranslationData;
    }

    public static LanguageDto ToDto(LanguageData data)
    {
      var dto = new LanguageDto()
      {
        Id = data.Id,
        Text = data.Text,
        UserId = data.UserDataId,
        Username = data.UserData.Username
      };

      return dto;
    }
    public static LanguageData ToData(LanguageDto dto, LearnLanguagesContext context)
    {
      //CREATE DATA OBJECT
      var languageData = context.LanguageDatas.CreateObject();

      //ASSIGN SIMPLE PROPERTIES
      languageData.Id = dto.Id;
      languageData.Text = dto.Text;
      languageData.UserDataId = dto.UserId;

      //POPULATE USERDATA
      var results = (from u in context.UserDatas
                     where u.Id == dto.UserId
                     select u);
      if (results.Count() == 1)
      {
        var userData = results.First();

        //MAKE SURE USERNAMES MATCH
        if (userData.Username != dto.Username)
          throw new ArgumentException("languageDto dto");
        languageData.UserData = results.First();
      }
      else if (results.Count() == 0)
        throw new Exceptions.UsernameAndUserIdDoNotMatchException(dto.Username, dto.UserId);
      else
        throw new Exceptions.VeryBadException(
          string.Format(DalResources.ErrorMsgVeryBadException,
          DalResources.ErrorMsgVeryBadExceptionDetail_ResultCountNotOneOrZero));

      //RETURN
      return languageData;
    }

    public static string GetConnectionString()
    {
      return ConfigurationManager.ConnectionStrings[EfResources.LearnLanguagesConnectionStringKey].ConnectionString;
    }

    public static TranslationDto ToDto(TranslationData fetchedTranslationData)
    {
      var dto = new TranslationDto()
      {
        Id = fetchedTranslationData.Id,
        PhraseIds = (from phrase in fetchedTranslationData.PhraseDatas
                     select phrase.Id).ToList(),
        UserId = fetchedTranslationData.UserDataId,
        Username = fetchedTranslationData.UserData.Username
      };

      return dto;
    }

    /// <summary>
    /// Loads the information from the dto into the data object. Except...
    /// Does NOT load dto.Id.
    /// </summary>
    /// <param name="translationData"></param>
    /// <param name="dto"></param>
    public static void LoadDataFromDto(ref TranslationData translationData, 
                                       TranslationDto dto, 
                                       LearnLanguagesContext context)
    {
      //COPY USER INFO
      translationData.UserDataId = dto.UserId;
      translationData.UserData = EfHelper.GetUserData(dto.UserId, context);

      var currentPhraseIds = (from phrase in translationData.PhraseDatas
                              select phrase.Id);

      //COPY PHRASEID INFO
      //ADD NEW PHRASEDATAS IN THE DTO
      foreach (var id in dto.PhraseIds)
      {
        if (!currentPhraseIds.Contains(id))
        {
          PhraseData phraseData = EfHelper.GetPhraseData(id, context);
          translationData.PhraseDatas.Add(phraseData);
        }
      }

      //REMOVE PHRASEDATAS THAT ARE NOT IN DTO ANYMORE
      foreach (var phraseId in currentPhraseIds)
      {
        if (!dto.PhraseIds.Contains(phraseId))
        {
          var dataToRemove = (from phraseData in translationData.PhraseDatas
                              where phraseData.Id == phraseId
                              select phraseData).First();
          translationData.PhraseDatas.Remove(dataToRemove);
        }
      }
    }

    /// <summary>
    /// Loads the information from the dto into the data object. Except...
    /// Does NOT load dto.Id.
    /// </summary>
    /// <param name="phraseData"></param>
    /// <param name="dto"></param>
    public static void LoadDataFromDto(ref PhraseData phraseData,
                                       PhraseDto dto,
                                       LearnLanguagesContext context)
    {
      //USER INFO
      phraseData.UserDataId = dto.UserId;
      phraseData.UserData = EfHelper.GetUserData(dto.UserId, context);

      //LANGUAGE INFO
      phraseData.LanguageDataId = dto.LanguageId;
      phraseData.LanguageData = EfHelper.GetLanguageData(dto.LanguageId, context);

      //TEXT
      phraseData.Text = dto.Text;

      //TRANSLATIONDATAS
      phraseData.TranslationDatas.Load();
    }

    /// <summary>
    /// Loads the information from the dto into the data object. Except...
    /// Does NOT load dto.Id.
    /// </summary>
    /// <param name="languageData"></param>
    /// <param name="dto"></param>
    public static void LoadDataFromDto(ref LanguageData languageData,
                                       LanguageDto dto,
                                       LearnLanguagesContext context)
    {
      //USER INFO
      languageData.UserDataId = dto.UserId;
      languageData.UserData = EfHelper.GetUserData(dto.UserId, context);

      //MAKE SURE USERDATA USERNAME MATCHES DTO.USERNAME
      if (languageData.UserData.Username != dto.Username)
        throw new Exceptions.UsernameAndUserIdDoNotMatchException(dto.Username, dto.UserId);

      //TEXT
      languageData.Text = dto.Text;
    }


    private static LanguageData GetLanguageData(Guid languageId, LearnLanguagesContext context)
    {
      var currentUserId = ((CustomIdentity)Csla.ApplicationContext.User.Identity).UserId;

      var results = from languageData in context.LanguageDatas
                    where languageData.Id == languageId 
                    select languageData;

      if (results.Count() == 1)
        return results.First();
      else if (results.Count() == 0)
        throw new Exceptions.IdNotFoundException(languageId);
      else
      {
        var errorMsg = string.Format(DalResources.ErrorMsgVeryBadException,
                                     DalResources.ErrorMsgVeryBadExceptionDetail_ResultCountNotOneOrZero);
        throw new Exceptions.VeryBadException(errorMsg);
      }
    }
    private static UserData GetUserData(Guid userId, LearnLanguagesContext context)
    {
      var results = (from user in context.UserDatas
                     where user.Id == userId
                     select user);

      if (results.Count() == 1)
        return results.First();
      else if (results.Count() == 0)
        throw new Exceptions.IdNotFoundException(userId);
      else
      {
        var errorMsg = string.Format(DalResources.ErrorMsgVeryBadException,
                                     DalResources.ErrorMsgVeryBadExceptionDetail_ResultCountNotOneOrZero);
        throw new Exceptions.VeryBadException(errorMsg);
      }
    }
    private static PhraseData GetPhraseData(Guid phraseId, LearnLanguagesContext context)
    {
      var currentUserId = ((CustomIdentity)Csla.ApplicationContext.User.Identity).UserId;

      var results = from phraseData in context.PhraseDatas
                    where phraseData.Id == phraseId &&
                          phraseData.UserDataId == currentUserId
                    select phraseData;

      if (results.Count() == 1)
        return results.First();
      else if (results.Count() == 0)
        throw new Exceptions.IdNotFoundException(phraseId);
      else
      {
        var errorMsg = string.Format(DalResources.ErrorMsgVeryBadException,
                                     DalResources.ErrorMsgVeryBadExceptionDetail_ResultCountNotOneOrZero);
        throw new Exceptions.VeryBadException(errorMsg);
      }
    }
  }
}
