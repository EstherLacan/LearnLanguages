﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearnLanguages.DataAccess.Mock
{
  public class TranslationDal : TranslationDalBase
  {
    //public Result<TranslationDto> New(object criteria)
    //{
    //  Result<TranslationDto> retResult = Result<TranslationDto>.Undefined(null);
    //  try
    //  {
    //    var dto = new TranslationDto() 
    //    { 
    //      Id = Guid.NewGuid(),
    //      LanguageId = SeedData.Instance.DefaultLanguageId
    //    };
    //    retResult = Result<TranslationDto>.Success(dto);
    //  }
    //  catch (Exception ex)
    //  {
    //    retResult = Result<TranslationDto>.FailureWithInfo(null, ex);
    //  }
    //  return retResult;
    //}
    //public Result<TranslationDto> Fetch(Guid id)
    //{
    //  Result<TranslationDto> retResult = Result<TranslationDto>.Undefined(null);
    //  try
    //  {
    //    var results = from item in SeedData.Instance.Translations
    //                  where item.Id == id
    //                  select item;

    //    if (results.Count() == 1)
    //      retResult = Result<TranslationDto>.Success(results.First());
    //    else
    //    {
    //      if (results.Count() == 0)
    //        retResult = Result<TranslationDto>.FailureWithInfo(null,
    //          new Exceptions.FetchFailedException(DalResources.ErrorMsgIdNotFoundException));
    //      else
    //        retResult = Result<TranslationDto>.FailureWithInfo(null, new Exceptions.FetchFailedException());
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    retResult = Result<TranslationDto>.FailureWithInfo(null, ex);
    //  }
    //  return retResult;
    //}
    //public Result<TranslationDto> Update(TranslationDto dto)
    //{
    //  Result<TranslationDto> retResult = Result<TranslationDto>.Undefined(null);
    //  try
    //  {
    //    var results = from item in SeedData.Instance.Translations
    //                  where item.Id == dto.Id
    //                  select item;

    //    if (results.Count() == 1)
    //    {
    //      var TranslationToUpdate = results.First();
    //      SeedData.Instance.Translations.Remove(TranslationToUpdate);
    //      dto.Id = Guid.NewGuid();
    //      SeedData.Instance.Translations.Add(dto);
    //      retResult = Result<TranslationDto>.Success(dto);
    //    }
    //    else
    //    {
    //      if (results.Count() == 0)
    //        retResult = Result<TranslationDto>.FailureWithInfo(null,
    //          new Exceptions.UpdateFailedException(DalResources.ErrorMsgIdNotFoundException));
    //      else
    //        retResult = Result<TranslationDto>.FailureWithInfo(null, new Exceptions.FetchFailedException());
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    retResult = Result<TranslationDto>.FailureWithInfo(null, ex);
    //  }
    //  return retResult;
    //}
    //public Result<TranslationDto> Insert(TranslationDto dto)
    //{
    //  Result<TranslationDto> retResult = Result<TranslationDto>.Undefined(null);
    //  try
    //  {
    //    var results = from item in SeedData.Instance.Translations
    //                  where item.Id == dto.Id
    //                  select item;

    //    if (results.Count() == 0)
    //    {
    //      dto.Id = Guid.NewGuid();
    //      //MIMIC LANGUAGEID REQUIRED CONSTRAINT IN DB
    //      if (dto.LanguageId == Guid.Empty || !SeedData.Instance.ContainsLanguageId(dto.LanguageId))
    //      {
    //        //I'VE RESTRUCTURED HOW TO DO EXCEPTIONHANDLING, SO THIS IS NOT QUITE HOW IT SHOULD BE DONE.
    //        //THIS SHOULD BE AN INSERTIMPL METHOD, AND IT SHOULD THROW ITS OWN EXCEPTION THAT IS WRAPPED IN THE 
    //        //TranslationDALBASE CLASS IN AN INSERTFAILEDEXCEPTION.
    //        throw new Exceptions.InsertFailedException(string.Format(DalResources.ErrorMsgIdNotFoundException, dto.LanguageId));
    //      }
    //      SeedData.Instance.Translations.Add(dto);
    //      retResult = Result<TranslationDto>.Success(dto);
    //    }
    //    else
    //    {
    //      if (results.Count() == 1) //ID ALREADY EXISTS
    //        retResult = Result<TranslationDto>.FailureWithInfo(dto,
    //          new Exceptions.UpdateFailedException(DalResources.ErrorMsgIdNotFoundException));
    //      else                      //MULTIPLE IDS ALREADY EXIST??
    //        retResult = Result<TranslationDto>.FailureWithInfo(null, new Exceptions.FetchFailedException());
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    retResult = Result<TranslationDto>.FailureWithInfo(null, ex);
    //  }
    //  return retResult;
    //}
    //public Result<TranslationDto> Delete(Guid id)
    //{
    //  Result<TranslationDto> retResult = Result<TranslationDto>.Undefined(null);
    //  try
    //  {
    //    var results = from item in SeedData.Instance.Translations
    //                  where item.Id == id
    //                  select item;

    //    if (results.Count() == 1)
    //    {
    //      var TranslationToRemove = results.First();
    //      SeedData.Instance.Translations.Remove(TranslationToRemove);
    //      retResult = Result<TranslationDto>.Success(TranslationToRemove);
    //    }
    //    else
    //    {
    //      if (results.Count() == 0)
    //        retResult = Result<TranslationDto>.FailureWithInfo(null,
    //          new Exceptions.DeleteFailedException(DalResources.ErrorMsgIdNotFoundException));
    //      else
    //        retResult = Result<TranslationDto>.FailureWithInfo(null, new Exceptions.DeleteFailedException());
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    retResult = Result<TranslationDto>.FailureWithInfo(null, ex);
    //  }
    //  return retResult;
    //}
    //public LearnLanguages.Result<ICollection<TranslationDto>> GetAll()
    //{
    //  Result<ICollection<TranslationDto>> retResult = Result<ICollection<TranslationDto>>.Undefined(null);
    //  try
    //  {
    //    var allDtos = new List<TranslationDto>(SeedData.Instance.Translations);
    //    retResult = Result<ICollection<TranslationDto>>.Success(allDtos);
    //  }
    //  catch (Exception ex)
    //  {
    //    retResult = Result<ICollection<TranslationDto>>.FailureWithInfo(null, ex);
    //  }
    //  return retResult;
    //}

    protected override TranslationDto NewImpl(object criteria)
    {
      var dto = new TranslationDto()
      {
        Id = Guid.NewGuid(),
        UserId = SeedData.Instance.GetTestValidUserDto().Id,
        Username = SeedData.Instance.TestValidUsername
      };
      return dto;
    }
    protected override TranslationDto FetchImpl(Guid id)
    {
      var results = from item in SeedData.Instance.Translations
                    where item.Id == id
                    select item;

      if (results.Count() == 1)
        return results.First();
      else
      {
        if (results.Count() == 0)
          throw new Exceptions.IdNotFoundException(id);
        else
          throw new Exceptions.VeryBadException();
      }
    }
    protected override ICollection<TranslationDto> FetchImpl(ICollection<Guid> ids)
    {
      if (ids == null)
        throw new ArgumentNullException("ids");
      else if (ids.Count == 0)
        throw new ArgumentOutOfRangeException("ids", "ids cannot be empty.");

      var retTranslations = new List<TranslationDto>();

      foreach (var id in ids)
      {
        var dto = FetchImpl(id);
        retTranslations.Add(dto);
      }

      return retTranslations;
    }
    protected override TranslationDto UpdateImpl(TranslationDto dto)
    {
      var results = from item in SeedData.Instance.Translations
                    where item.Id == dto.Id
                    select item;

      if (results.Count() == 1)
      {
        CheckValidity(dto);
        CheckReferentialIntegrity(dto);
        var TranslationToUpdate = results.First();
        SeedData.Instance.Translations.Remove(TranslationToUpdate);
        dto.Id = Guid.NewGuid();
        SeedData.Instance.Translations.Add(dto);
        return dto;
      }
      else
      {
        if (results.Count() == 0)
          throw new Exceptions.IdNotFoundException(dto.Id);
        else
          throw new Exceptions.VeryBadException();
      }
    }
    protected override TranslationDto InsertImpl(TranslationDto dto)
    {
      var results = from item in SeedData.Instance.Translations
                    where item.Id == dto.Id
                    select item;

      if (results.Count() == 0)
      {
        CheckValidity(dto);
        dto.Id = Guid.NewGuid();
        SeedData.Instance.Translations.Add(dto);
        return dto;
      }
      else
      {
        if (results.Count() == 1) //ID ALREADY EXISTS
          throw new Exceptions.IdAlreadyExistsException(dto.Id);
        else                      //MULTIPLE IDS ALREADY EXIST??
          throw new Exceptions.VeryBadException();
      }
    }
    protected override TranslationDto DeleteImpl(Guid id)
    {
      var results = from item in SeedData.Instance.Translations
                    where item.Id == id
                    select item;

      if (results.Count() == 1)
      {
        var TranslationToRemove = results.First();
        SeedData.Instance.Translations.Remove(TranslationToRemove);
        return TranslationToRemove;
      }
      else
      {
        if (results.Count() == 0)
          throw new Exceptions.IdNotFoundException(id);
        else
          throw new Exceptions.VeryBadException();
      }
    }
    protected override ICollection<TranslationDto> GetAllImpl()
    {
      var allDtos = new List<TranslationDto>(SeedData.Instance.Translations);
      return allDtos;
    }

    private void CheckValidity(TranslationDto dto)
    {
      //VALIDITY
      if (dto == null)
        throw new ArgumentNullException("dto");
      if (dto.PhraseIds.Count < int.Parse(DalResources.MinPhrasesPerTranslation))
        throw new ArgumentOutOfRangeException("dto.PhraseIds.Count");
    }
    private static void CheckReferentialIntegrity(TranslationDto dto)
    {
      //REFERENTIAL INTEGRITY
      foreach (var id in dto.PhraseIds)
      {
        var count = (from p in SeedData.Instance.Phrases
                     where p.Id == id
                     select p).Count();

        if (count == 1)
          continue;
        else if (count == 0)
          throw new Exceptions.IdNotFoundException(id);
        else
          throw new Exceptions.VeryBadException();
      }
    }
  }
}
